using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using libsignal;
using libsignal.state;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Client.Sdk.Crypto
{
    public class SignalSessionStore : libsignal.state.SessionStore
    {
        public event EventHandler<SignalProtocolAddress> SessionStored;

        public IDictionary<string, SessionRecord> SessionRecords { get; set; } = new Dictionary<string, SessionRecord>();

        public SignalSessionStore() { }

        public SessionRecord LoadSession(SignalProtocolAddress remoteAddress)
        {
            return SessionRecords.GetValue(remoteAddress.ToString()) ?? new SessionRecord();
        }


        public void StoreSession(SignalProtocolAddress address, SessionRecord record)
        {
            var id = address.ToString();
            SessionRecords[id] = record;
            OnSessionStored(address);
        }


        public bool ContainsSession(SignalProtocolAddress address)
        {
            return SessionRecords.ContainsKey(address.ToString());
        }

        protected virtual void OnSessionStored(SignalProtocolAddress address)
        {
            SessionStored?.Invoke(this, address);
        }

        public List<uint> GetSubDeviceSessions(string id)
        {
            throw new NotImplementedException();
        }


        public void DeleteSession(SignalProtocolAddress address)
        {
            throw new NotImplementedException();
        }


        public void DeleteAllSessions(string id)
        {
            throw new NotImplementedException();
        }
    }
}
