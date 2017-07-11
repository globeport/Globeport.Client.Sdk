using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Client.Sdk.Crypto
{
    public interface ISignalCipher : IDisposable
    {
        Task ProcessMessage(SignalMessage message);
        Task<SignalMessage> GenerateSenderKeyMessage(string groupId, string contentType, byte[] content);
        Task<SignalMessage> GenerateSenderKeyDistributionMessage(string portalId, string contactId, long deviceId);
    }
}
