/** 
 * Copyright (C) 2015 smndtrl, langboost
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using libsignal;
using libsignal.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
