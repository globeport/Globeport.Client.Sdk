using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Client.Sdk.Crypto
{
    public class SessionStore
    {
        public HashSet<string> DeletedSessions { get; private set; }
        public Dictionary<string, SignalSession> UpdatedSessions { get; private set; }
        public Dictionary<string, SignalSession> StoredSessions { get; } = new Dictionary<string, SignalSession>();

        public SessionStore()
        {
            Reset();
        }

        public async Task<SignalSession> GetSession(string contactId, long deviceId)
        {
            var id = SignalSession.GetId(contactId, deviceId);
            SignalSession session = null;
            if (!DeletedSessions.Contains(id))
            {
                session = UpdatedSessions.GetValue(id);
                if (session == null)
                {
                    session = await GetStoredSession(id).ConfigureAwait(false);
                }
            }
            if (session == null)
            {
                session = new SignalSession(contactId, deviceId);
            }
            UpdatedSessions[id] = session;
            return session;
        }

        protected virtual Task<SignalSession> GetStoredSession(string id)
        {
            return Task.FromResult(StoredSessions.GetValue(id));
        }

        public void Update(string contactId, long deviceId, byte[] data)
        {
            var id = SignalSession.GetId(contactId, deviceId);
            DeletedSessions.Remove(id);
            var session = UpdatedSessions.GetValue(id);
            session.Data = data;
        }

        public void Delete(string contactId, long deviceId)
        {
            var id = SignalSession.GetId(contactId, deviceId);
            UpdatedSessions.Remove(id);
            DeletedSessions.Add(id);
        }

        public void Reset()
        {
            UpdatedSessions = new Dictionary<string, SignalSession>();
            DeletedSessions = new HashSet<string>();
        }
    }
}
