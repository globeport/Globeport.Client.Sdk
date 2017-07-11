using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libsignal.groups;
using libsignal.groups.state;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Client.Sdk.Crypto
{
    public class SignalSenderKeyStore : SenderKeyStore
    {
        private readonly IDictionary<SenderKeyName, byte[]> store = new Dictionary<SenderKeyName, byte[]>();

        public SenderKeyRecord loadSenderKey(SenderKeyName senderKeyName)
        {
            var senderKey = store.GetValue(senderKeyName);
            if (senderKey!=null) return new SenderKeyRecord(senderKey);
            return new SenderKeyRecord();
        }

        public void storeSenderKey(SenderKeyName senderKeyName, SenderKeyRecord record)
        {
            store[senderKeyName] = record.serialize();
        }
    }
}
