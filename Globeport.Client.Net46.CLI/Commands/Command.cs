using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using JsonPrettyPrinterPlus;
using Mono.Options;
using Newtonsoft.Json.Linq;

using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Extensions;
using Globeport.Client.Net46.CLI.Attributes;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Client.Net46.CLI.Commands
{
    abstract class Command
    {
        public virtual string GetName()
        {
            return GetType().Name.ToLower();
        }

        public virtual string GetDescription()
        {
            return GetType().GetAttribute<CommandAttribute>()?.Description;
        }

        public string GetUsage()
        {
            var properties = GetType().GetProperties().Where(i => i.GetAttribute<ArgumentAttribute>() != null);
            var usage = $"{GetName()}";
            foreach (var property in properties)
            {
                var attribute = property.GetAttribute<ArgumentAttribute>();
                var propertyName = property.Name.ToLower();
                var propertyDescription = property.GetAttribute<ArgumentAttribute>()?.Description;
                if (!attribute.IsOptional)
                {
                    usage = $"{usage} <{propertyName}>";
                }
                else
                {
                    usage = $"{usage} [-{propertyName}]";
                }
            }
            return $"{usage}\n";
        }

        public string[] GetParameters()
        {
            var parameters = new List<string>();
            var properties = GetType().GetProperties().Where(i => i.GetAttribute<ArgumentAttribute>() != null);
            ArgumentAttribute lastAttribute = null;
            if (properties.Any())
            {
                var propertyNameLength = properties.Max(i => i.Name.Length)+2;
                foreach (var property in properties)
                {
                    var attribute = property.GetAttribute<ArgumentAttribute>();
                    if (lastAttribute!=null && !lastAttribute.IsOptional && attribute.IsOptional)
                    {
                        parameters.Add("");
                    }
                    var propertyName = property.Name.ToLower();
                    var propertyDescription = property.GetAttribute<ArgumentAttribute>()?.Description;
                    string parameter;
                    if (attribute.IsOptional)
                    {
                        parameter = $"-{propertyName.PadRight(propertyNameLength)}\t{propertyDescription}";
                    }
                    else
                    {
                        parameter = $"{propertyName.PadRight(propertyNameLength)}\t{propertyDescription}";
                    }
                    if (property.PropertyType.IsEnum)
                    {
                        parameter = $"{parameter} ({string.Join("|", Enum.GetNames(property.PropertyType))})";
                    }
                    parameters.Add(parameter);
                    lastAttribute = attribute;
                }
            }
            return parameters.ToArray();
        }

        public bool Process(CommandArguments args)
        {
            try
            {
                var properties = GetType().GetProperties().Where(i => i.GetAttribute<ArgumentAttribute>() != null);

                var attributes = properties.ToDictionary(i => i, i => i.GetAttribute<ArgumentAttribute>());

                var arguments = new Dictionary<PropertyInfo, string>();

                foreach (var property in properties.Where(i=>!i.GetAttribute<ArgumentAttribute>().IsOptional))
                {
                    if (args.CurrentIndex == args.Count - 1) break;

                    args.MoveNext();

                    var value = args.Current;

                    if (value.StartsWith("\"") && (!value.EndsWith("\"") || value.EndsWith("\\\"")))
                    {
                        if (value.EndsWith("\\\"")) value = value.RemoveEnd("\\\"") + "\"";
                        do
                        {
                            args.MoveNext();
                            value = $"{value} {args.Current}";
                        }
                        while (!args.Current.EndsWith("\""));
                    }

                    arguments.Add(property, StripQuotes(value));
                }

                if (args.Count < attributes.Count(i => !i.Value.IsOptional)) return false;

                var optionalArgs = new List<string>();

                while (args.MoveNext())
                {
                    optionalArgs.Add(args.Current);
                }

                var optionSet = new OptionSet();

                foreach (var property in properties)
                {
                    var attribute = attributes[property];

                    if (!attributes[property].IsOptional)
                    {
                        var value = arguments.GetValue(property);
                        SetValue(property, value, attribute.DefaultValue);
                    }
                    else
                    {
                        SetValue(property, null, attribute.DefaultValue);
                        optionSet.Add($"{property.Name.ToLower()}=", attribute.Description, value => SetValue(property, value, attribute.DefaultValue));
                    }
                }

                optionSet.Parse(optionalArgs);

                return true;
            }
            catch
            {
                return false;
            }
        }

        void SetValue(PropertyInfo property, string value, object defaultValue)
        {
            if (property.PropertyType == typeof(string))
            {
                property.SetValue(this, value);
            }
            else if (property.PropertyType == typeof(int))
            {
                property.SetValue(this, value != null ? int.Parse(value) : defaultValue);
            }
            else if (property.PropertyType == typeof(int?))
            {
                property.SetValue(this, value != null ? int.Parse(value) : defaultValue);
            }
            else if (property.PropertyType == typeof(bool))
            {
                property.SetValue(this, value != null ? bool.Parse(value) : defaultValue);
            }
            else if (property.PropertyType == typeof(bool?))
            {
                property.SetValue(this, value != null ? bool.Parse(value) : defaultValue);
            }
            else if (property.PropertyType == typeof(double))
            {
                property.SetValue(this, value != null ? double.Parse(value) : defaultValue);
            }
            else if (property.PropertyType == typeof(double?))
            {
                property.SetValue(this, value != null ? double.Parse(value) : defaultValue);
            }
            else if (property.PropertyType.IsEnum)
            {
                property.SetValue(this, value != null ? Enum.Parse(property.PropertyType, value, true) : defaultValue);
            }
            else if (Nullable.GetUnderlyingType(property.PropertyType)?.IsEnum == true)
            {
                property.SetValue(this, value != null ? Enum.Parse(Nullable.GetUnderlyingType(property.PropertyType), value, true) : defaultValue);
            }
            else
            {
                property.SetValue(this, value.Deserialize(property.PropertyType));
            }
        }
    
        private static string StripQuotes(string value)
        { 
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.Substring(1, value.Length - 2);
            }
            else if (value.StartsWith("\'") && value.EndsWith("\'"))
            {
                value = value.Substring(1, value.Length - 2);
            }

            return value;
        }

        public virtual CommandResult Execute(CommandArguments args)
        {
            if (!Process(args))
            {
                Console.WriteLine($"\nUsage:\n\t{GetUsage()}");
                Console.WriteLine($"\t{string.Join("\n\t", GetParameters())}\n");
                return CommandResult.ExecutionFailed;
            }

            return ExecuteAsync().Result;
        }

        async Task<CommandResult> ExecuteAsync()
        {
            try
            {
                var result = await Execute();

                if (result!=null)
                {
                    if (!(result is string)) result = result.Serialize();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(result.ToString().PrettyPrintJson());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{GetName()} completed successfully");
                }

                return CommandResult.Success;
            }
            catch (Exception e)
            {
                WriteError(e.Message);

                return CommandResult.ExecutionFailed;
            }
            finally
            {
                Console.ResetColor();
            }
        }

        protected void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {error}");
        }

        public static MediaUpload GetImageUpload(string path)
        {
            try
            {
                var id = Api.Client.CryptoClient.GenerateId();
                var files = new List<MediaFileUpload>();
                var image = Image.FromFile(path);
                var aspectRatio = image.Width / image.Height;
                foreach (var size in ImageSizes.All)
                {
                    using (var newImage = new Bitmap(size, size))
                    using (var graphics = Graphics.FromImage(newImage))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.DrawImage(image, new Rectangle(0, 0, size, size));
                        using (var stream = new MemoryStream())
                        {
                            newImage.Save(stream, ImageFormat.Jpeg);
                            var pixels = stream.ToArray();
                            var signature = Api.Client.CryptoClient.Sign(Api.Client.Session.KeyStore.PrivateIdentityKey.Value, pixels);
                            files.Add(new MediaFileUpload(size, pixels, signature));
                        }
                    }
                }
                return new MediaUpload(id, MediaTypes.Image, files) { AspectRatio = aspectRatio };
            }
            catch
            {
                return null;
            }
        }

        protected async Task GetEntityData(Entity entity)
        {
            if (!entity.IsPublic && entity.PacketId != null)
            {
                var packet = await Api.Client.Session.PacketStore.GetPacket(Packet.GetId(entity.PortalId ?? Api.Client.Session.Portals[SystemPortals.Profile], entity.AccountId, entity.PacketId)).ConfigureAwait(false);

                if (packet != null)
                {
                    var packetData = Api.Client.CryptoClient.Decrypt(packet.Data, entity.PacketKey).FromBytes().Deserialize<DataObject>();

                    var data = JToken.FromObject(entity.Data) as JObject;

                    data.Merge(JObject.FromObject(packetData), new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Merge, MergeNullValueHandling = MergeNullValueHandling.Ignore });

                    entity.Data = data.ToObject<DataObject>();

                    var keyIds = entity.Media.Where(i => i.KeyId != null).Select(i => $"{entity.AccountId}.{i.KeyId}").ToList();

                    if (entity.KeyId != null) keyIds.Add($"{entity.AccountId}.{entity.KeyId}");

                    var tasks = keyIds.Select(i => Api.Client.Session.KeyStore.GetKey(Key.GetId(KeyType.SecretKey, i))).ToList();

                    var keys = await Task.WhenAll(tasks).ConfigureAwait(false);

                    foreach (var item in entity.Media.Where(i => i.KeyId != null))
                    {
                        item.Key = keys.FirstOrDefault(i=>i.KeyId == $"{entity.AccountId}.{item.KeyId}")?.Value;
                    }

                    if (entity.KeyId != null)
                    {
                        entity.Key = keys.FirstOrDefault(i => i?.KeyId == $"{entity.AccountId}.{entity.KeyId}")?.Value;
                    }
                }
            }
        }

        protected abstract Task<object> Execute();
    }
}
