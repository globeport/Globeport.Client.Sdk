﻿/** 
 * Copyright (C) 2016 smndtrl, langboost
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

using Google.ProtocolBuffers;
using libsignal.ecc;
using static libsignal.state.StorageProtos;

namespace libsignal
{
    /**
     * Holder for public and private identity key pair.
     *
     * @author
     */
    public class IdentityKeyPair
    {

        private readonly IdentityKey publicKey;
        private readonly ECPrivateKey privateKey;

        public IdentityKeyPair(IdentityKey publicKey, ECPrivateKey privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        public IdentityKeyPair(byte[] serialized)
        {
            try
            {
                IdentityKeyPairStructure structure = IdentityKeyPairStructure.ParseFrom(serialized);
                this.publicKey = new IdentityKey(structure.PublicKey.ToByteArray(), 0);
                this.privateKey = Curve.decodePrivatePoint(structure.PrivateKey.ToByteArray());
            }
            catch (InvalidProtocolBufferException e)
            {
                throw new InvalidKeyException(e);
            }
        }

        public IdentityKey getPublicKey()
        {
            return publicKey;
        }

        public ECPrivateKey getPrivateKey()
        {
            return privateKey;
        }

        public byte[] serialize()
        {
            return IdentityKeyPairStructure.CreateBuilder()
                                           .SetPublicKey(ByteString.CopyFrom(publicKey.serialize()))
                                           .SetPrivateKey(ByteString.CopyFrom(privateKey.serialize()))
                                           .Build().ToByteArray();
        }
    }
}
