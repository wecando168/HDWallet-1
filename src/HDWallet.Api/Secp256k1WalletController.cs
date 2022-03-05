using System;
using HDWallet.Core;
using HDWallet.Secp256k1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api
{
    public class Secp256k1WalletController<TWallet> : ControllerBase where TWallet: Wallet, new()
    {
        private readonly ILogger<Secp256k1WalletController<TWallet>> _logger;
        private readonly IAccount<TWallet> _accountHDWallet;

        public Secp256k1WalletController(
            ILogger<Secp256k1WalletController<TWallet>> logger,
            IServiceProvider prov)
        {
            _logger = logger;
            _accountHDWallet = prov.GetService<IAccount<TWallet>>();
        }

        protected ActionResult<string> DepositWallet(uint index)
        {
            if(_accountHDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");
            }

            var wallet = _accountHDWallet.GetExternalWallet(index);
            return wallet.Address;
        }

        protected ActionResult<string> ChangeWallet(uint index)
        {
            if(_accountHDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");
            }

            var wallet = _accountHDWallet.GetInternalWallet(index);
            return wallet.Address;
        }
    }
}