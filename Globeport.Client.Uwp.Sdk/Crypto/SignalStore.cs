/** 
 * Copyright (C) 2015 smndtrl
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libsignal;
using libsignal.state;
using libsignal.state.impl;
using libsignal.groups.state;

namespace Globeport.Client.Sdk.Crypto
{
    public class SignalStore : SignalProtocolStore
    {
        private readonly InMemoryIdentityKeyStore identityKeyStore;
        private readonly InMemoryPreKeyStore preKeyStore = new InMemoryPreKeyStore();
        private readonly InMemorySignedPreKeyStore signedPreKeyStore = new InMemorySignedPreKeyStore();
        public SignalSessionStore SessionStore { get; } = new SignalSessionStore();
        public SenderKeyStore SenderKeyStore { get; } = new SignalSenderKeyStore();

        public SignalStore(IdentityKeyPair identityKeyPair)
        {
            identityKeyStore = new InMemoryIdentityKeyStore(identityKeyPair, 0);
        }

        public IdentityKeyPair GetIdentityKeyPair()
        {
            return identityKeyStore.GetIdentityKeyPair();
        }


        public uint GetLocalRegistrationId()
        {
            return identityKeyStore.GetLocalRegistrationId();
        }


        public bool SaveIdentity(string name, IdentityKey identityKey)
        {
            identityKeyStore.SaveIdentity(name, identityKey);
            return true;
        }


        public bool IsTrustedIdentity(string name, IdentityKey identityKey)
        {
            return identityKeyStore.IsTrustedIdentity(name, identityKey);
        }


        public PreKeyRecord LoadPreKey(uint preKeyId)
        {
            return preKeyStore.LoadPreKey(preKeyId);
        }


        public void StorePreKey(uint preKeyId, PreKeyRecord record)
        {
            preKeyStore.StorePreKey(preKeyId, record);
        }


        public bool ContainsPreKey(uint preKeyId)
        {
            return preKeyStore.ContainsPreKey(preKeyId);
        }


        public void RemovePreKey(uint preKeyId)
        {
            preKeyStore.RemovePreKey(preKeyId);
        }


        public SessionRecord LoadSession(SignalProtocolAddress address)
        {
            return SessionStore.LoadSession(address);
        }


        public List<uint> GetSubDeviceSessions(string name)
        {
            return SessionStore.GetSubDeviceSessions(name);
        }


        public void StoreSession(SignalProtocolAddress address, SessionRecord record)
        {
            SessionStore.StoreSession(address, record);
        }


        public bool ContainsSession(SignalProtocolAddress address)
        {
            return SessionStore.ContainsSession(address);
        }


        public void DeleteSession(SignalProtocolAddress address)
        {
            SessionStore.DeleteSession(address);
        }


        public void DeleteAllSessions(string name)
        {
            SessionStore.DeleteAllSessions(name);
        }


        public SignedPreKeyRecord LoadSignedPreKey(uint signedPreKeyId)
        {
            return signedPreKeyStore.LoadSignedPreKey(signedPreKeyId);
        }


        public List<SignedPreKeyRecord> LoadSignedPreKeys()
        {
            return signedPreKeyStore.LoadSignedPreKeys();
        }


        public void StoreSignedPreKey(uint signedPreKeyId, SignedPreKeyRecord record)
        {
            signedPreKeyStore.StoreSignedPreKey(signedPreKeyId, record);
        }


        public bool ContainsSignedPreKey(uint signedPreKeyId)
        {
            return signedPreKeyStore.ContainsSignedPreKey(signedPreKeyId);
        }


        public void RemoveSignedPreKey(uint signedPreKeyId)
        {
            signedPreKeyStore.RemoveSignedPreKey(signedPreKeyId);
        }
    }
}

