﻿using Phantasma.Cryptography;
using System.Collections.Generic;
using System.Linq;

namespace Phantasma.Blockchain.Plugins
{
    public class ChainAddressesPlugin : INexusPlugin
    {
        public Nexus Nexus { get; private set; }

        private Dictionary<Address, HashSet<Address>> _transactions = new Dictionary<Address, HashSet<Address>>();

        public ChainAddressesPlugin(Nexus nexus)
        {
            this.Nexus = nexus;
        }

        public void OnNewBlock(Chain chain, Block block)
        {
        }

        public void OnNewChain(Chain chain)
        {
        }

        public void OnNewTransaction(Chain chain, Block block, Transaction transaction)
        {
            foreach (var evt in transaction.Events)
            {
                RegisterTransaction(chain, evt.Address);
            }
        }

        private void RegisterTransaction(Chain chain, Address address)
        {
            HashSet<Address> set;
            if (_transactions.ContainsKey(chain.Address))
            {
                set = _transactions[chain.Address];
            }
            else
            {
                set = new HashSet<Address>();
                _transactions[chain.Address] = set;
            }

            set.Add(address);
        }

        public IEnumerable<Address> GetChainAddresses(Chain chain)
        {
            if (_transactions.ContainsKey(chain.Address))
            {
                return _transactions[chain.Address];
            }
            else
            {
                return Enumerable.Empty<Address>();
            }

        }
    }
}