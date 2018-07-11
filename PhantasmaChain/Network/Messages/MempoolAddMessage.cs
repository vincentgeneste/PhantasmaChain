﻿using Phantasma.Core;
using System;
using System.IO;

namespace Phantasma.Network
{
    internal class MempoolAddMessage : Message
    {
        public readonly Transaction transaction;

        public MempoolAddMessage(byte[] pubKey, Transaction tx) : base(Opcode.MEMPOOL_Add, pubKey)
        {
            this.transaction = tx;
        }

        internal static MempoolAddMessage FromReader(byte[] pubKey, BinaryReader reader)
        {
            var tx = Transaction.Unserialize(reader);
            return new MempoolAddMessage(pubKey, tx);
        }
    }
}