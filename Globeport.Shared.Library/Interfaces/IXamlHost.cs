using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Xaml;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IXamlHost
    {
        [JsInterop]
        int Id { get; }
        [JsInterop]
        string ParentId { get; set; }
        [JsInterop]
        bool DesignModeEnabled { get; }
        [JsInterop]
        bool IsInteractive { get; }
        [JsInterop]
        Account Account { get; }
        [JsInterop]
        Avatar Avatar { get; }
        [JsInterop]
        string CountryCode { get; }
        [JsInterop]
        string Culture { get; }
        [JsInterop]
        Entity Entity { get; }
        [JsInterop]
        List<Reference> Dependencies { get; }
        [JsInterop]
        Model Model { get; }
        [JsInterop]
        string Platform { get; }
        [JsInterop]
        double Width { get; set; }
        [JsInterop]
        double Height { get; set; }
        [JsInterop]
        DataObject Data { get; }
        [JsInterop]
        Metadata Metadata { get; }
        [JsInterop]
        string Mode { get; }
        bool IsActivated { get; set; }
        List<IXamlHost> HostCollection { get; }
        Dictionary<long, DependencyObject> Elements { get; }
        void LoadElement(DependencyObject element);
        void UnloadElement(DependencyObject element);
        object RunScript(string script);
        object CallFunction(string function, object[] args);
        void Launch(string uri);
        void ChooseUser(string callback);
        void ChooseEntity(string callback);
        void GetUser(string id, string callback);
        void ShowError(string error);
        void ShowMessage(string type, string message, string buttons, string callback);
        void OpenImageMenu(double aspectRatio, string defaultImage, string callback);
        void ViewImage(string id, string label);
        Task Validate();
        string AddMediaUpload(string type, string id, byte[] data);
        Task<byte[]> GetMedia(string type, string id, byte[] key);
        void AddTask();
        void RemoveTask();
    }
}
