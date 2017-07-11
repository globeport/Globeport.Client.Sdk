using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.Chakra.Hosting;
using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Chakra
{
    public class ChakraHost : IScriptHost
    {
        public event EventHandler<CallbackEventArgs> CallbackExecuted;
        static JavaScriptRuntime runtime;
        static readonly object runtimeLock = new object();
        JavaScriptContext context;
        JavaScriptValue global, parse, stringify;
        Queue<JavaScriptValue> taskQueue = new Queue<JavaScriptValue>();
        JavaScriptNativeFunction CallbackDelegate;
        static string GlobalScript { get; set; }

        static ChakraHost()
        {
            runtime = JavaScriptRuntime.Create(JavaScriptRuntimeAttributes.None, JavaScriptRuntimeVersion.VersionEdge);

            JavaScriptContext.Current = JavaScriptContext.Invalid;
        }

        public static void SetGlobalScript(string globalScript)
        {
            GlobalScript = globalScript;
        }

        public ChakraHost(bool runGlobalScript = false)
        {
            lock (runtimeLock)
            {
                CallbackDelegate = Callback;

                context = runtime.CreateContext();
                context.AddRef();

                using (new JavaScriptContext.Scope(context))
                {
                    global = JavaScriptValue.GlobalObject;
                    DefineCallback(global, "Callback", CallbackDelegate);
                    parse = JavaScriptContext.RunScript("JSON.parse");
                    stringify = JavaScriptContext.RunScript("JSON.stringify");
                    if (runGlobalScript && !GlobalScript.IsNullOrEmpty())
                    {
                        JavaScriptContext.RunScript(GlobalScript);
                    }

                    // ES6 Promise callback
                    JavaScriptPromiseContinuationCallback promiseContinuationCallback = delegate (JavaScriptValue task, IntPtr callbackState)
                    {
                        taskQueue.Enqueue(task);
                    };

                    Native.ThrowIfError(Native.JsSetPromiseContinuationCallback(promiseContinuationCallback, IntPtr.Zero));
                }
            }
        }

        void DefineCallback(JavaScriptValue hostObject, string callbackName, JavaScriptNativeFunction callbackDelegate)
        {
            var propertyId = JavaScriptPropertyId.FromString(callbackName);

            var function = JavaScriptValue.CreateFunction(callbackDelegate);

            hostObject.SetProperty(propertyId, function, true);
        }

        private JavaScriptValue Callback(JavaScriptValue callee, bool isConstructCall, JavaScriptValue[] arguments, ushort argumentCount, IntPtr callbackData)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var args = new CallbackEventArgs(arguments.Skip(1).Select(i => ConvertReturnValue(i)).ToArray());
                    CallbackExecuted?.Invoke(this, args);
                    return ConvertValue(args.Result);
                }
            }
        }

        public object RunScript(string script)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var result = JavaScriptContext.RunScript(script);

                    // Execute promise tasks stored in taskQueue 
                    while (taskQueue.Count != 0)
                    {
                        var task = taskQueue.Dequeue();

                        var promiseResult = task.CallFunction(global);
                    }

                    return ConvertReturnValue(result);
                }
            }
        }

        public byte[] SerializeScript(string script)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var buffer = new byte[1048576];
                    var result = JavaScriptContext.SerializeScript(script, buffer);
                    Array.Resize(ref buffer, (int)result);
                    return buffer;
                }
            }
        }

        public object CallFunction(string name, params object[] parameters)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var obj = global;

                    var function = GetProperty(name, ref obj);

                    var javascriptParameters = new List<JavaScriptValue>();

                    try
                    {
                        javascriptParameters.Add(obj);

                        foreach (var parameter in parameters)
                        {
                            var parameterType = parameter?.GetType().Name ?? "Null";
                            var val = JavaScriptValue.Invalid;

                            switch (parameterType)
                            {
                                case "Int32":
                                    val = JavaScriptValue.FromInt32((int)parameter);
                                    break;
                                case "Double":
                                    val = JavaScriptValue.FromDouble((double)parameter);
                                    break;
                                case "Boolean":
                                    val = JavaScriptValue.FromBoolean((bool)parameter);
                                    break;
                                case "String":
                                    val = JavaScriptValue.FromString((string)parameter);
                                    break;
                                case "Null":
                                    val = JavaScriptValue.Null;
                                    break;
                                case "JavaScriptValue":
                                    val = (JavaScriptValue)parameter;
                                    break;
                                case "JavaScriptParameter":
                                    val = JavaScriptContext.RunScript(((JavaScriptParameter)parameter).Expression);
                                    break;
                                default:
                                    val = ConvertValue(parameter);
                                    break;
                            }

                            if (val.IsValid)
                            {
                                val.AddRef();
                                javascriptParameters.Add(val);
                            }
                        }

                        var value = function.CallFunction(javascriptParameters.ToArray());

                        return ConvertReturnValue(value);
                    }
                    finally
                    {
                        foreach (var parameter in javascriptParameters) parameter.Release();
                    }
                }
            }
        }

        public object GetProperty(string name)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var obj = global;

                    var value = GetProperty(name, ref obj);

                    return ConvertReturnValue(value);
                }
            }
        }

        JavaScriptValue GetProperty(string name, ref JavaScriptValue obj)
        {
            obj = global;

            var index = name.LastIndexOf('.');

            if (index > 0)
            {
                var path = name.Substring(0, index);

                obj = JavaScriptContext.RunScript(path);

                name = name.Substring(index + 1);
            }

            var propertyId = JavaScriptPropertyId.FromString(name);

            var property = obj.GetProperty(propertyId);

            return property;
        }

        public void SetProperty(string name, object value)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var obj = global;

                    SetProperty(name, value, ref obj);
                }
            }
        }

        void SetProperty(string name, object value, ref JavaScriptValue obj)
        {
            obj = global;

            var index = name.LastIndexOf('.');

            if (index > 0)
            {
                var path = name.Substring(0, index);

                obj = JavaScriptContext.RunScript(path);

                name = name.Substring(index + 1);
            }

            var propertyId = JavaScriptPropertyId.FromString(name);

            obj.SetProperty(propertyId, ConvertValue(value), true);
        }

        public void AddRef(string propertyName)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var global = this.global;

                    var propertyId = JavaScriptPropertyId.FromString(propertyName);

                    var property = global.GetProperty(propertyId);

                    property.AddRef();
                }
            }
        }

        public void Release(string propertyName)
        {
            lock (runtimeLock)
            {
                using (new JavaScriptContext.Scope(context))
                {
                    var global = this.global;

                    var propertyId = JavaScriptPropertyId.FromString(propertyName);

                    var property = global.GetProperty(propertyId);

                    property.Release();
                }
            }
        }

        JavaScriptValue ConvertValue(object obj)
        {
            if (obj == null) return JavaScriptValue.Null;

            if (obj is string)
            {
                return JavaScriptValue.FromString((string)obj);
            }
            else if (obj is bool)
            {
                return JavaScriptValue.FromBoolean((bool)obj);
            }
            else if (obj is double)
            {
                return JavaScriptValue.FromDouble((double)obj);
            }
            else if (obj is int)
            {
                return JavaScriptValue.FromInt32((int)obj);
            }
            else
            {
                return parse.CallFunction(global, JavaScriptValue.FromString(obj.Serialize(JsInterop.JsonSettings)));
            }
        }

        object ConvertReturnValue(JavaScriptValue value)
        {
            switch (value.ValueType)
            {
                case JavaScriptValueType.Boolean:
                    return value.ConvertToBoolean().ToBoolean();
                case JavaScriptValueType.String:
                    return value.ConvertToString().ToString();
                case JavaScriptValueType.Number:
                    return value.ConvertToNumber().ToDouble();
                case JavaScriptValueType.Null:
                case JavaScriptValueType.Undefined:
                    return null;
                default:
                    return stringify.CallFunction(global, value).ConvertToString().ToString();
            }
        }

        bool isDisposed;
        public void Dispose()
        {
            if (isDisposed) return;

            isDisposed = true;

            lock (runtimeLock)
            {
                context.Release();
            }
        }

        ~ChakraHost()
        {
            Dispose();
        }
    }
}