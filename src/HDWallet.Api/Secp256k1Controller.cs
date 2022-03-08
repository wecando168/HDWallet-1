using System;
using HDWallet.Api.V1.Models;
using HDWallet.Core;
using HDWallet.Secp256k1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api
{
    public class Secp256k1Controller<TWallet> : ControllerBase where TWallet: Wallet, new()
    {
        private readonly ILogger<Secp256k1Controller<TWallet>> _logger;
        private readonly IHDWallet<TWallet> _hDWallet;
        private readonly IAccount<TWallet> _accountHDWallet;
        private readonly Settings _settings;

        public Secp256k1Controller(ILogger<Secp256k1Controller<TWallet>> logger, Settings settings, IServiceProvider prov)
        {
            _logger = logger;
            _hDWallet = prov.GetService<IHDWallet<TWallet>>();
            _accountHDWallet = prov.GetService<IAccount<TWallet>>();
            _settings = settings;
        }

        protected ActionResult<WalletDto> DepositWallet(uint index)
        {
            if(_accountHDWallet == null) return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");
            var wallet = _accountHDWallet.GetExternalWallet(index);
            var response =  new WalletDto(){
                Address = wallet.Address
            };

            if(_settings.ExportPrivateKey) 
            {
                response.PrivateKey = wallet.PrivateKey.ToHex();
            }

            return response;
        }

        protected ActionResult<WalletDto> ChangeWallet(uint index)
        {
            if(_accountHDWallet == null) return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");

            var wallet = _accountHDWallet.GetInternalWallet(index);
            var response =  new WalletDto(){
                Address = wallet.Address
            };

            if(_settings.ExportPrivateKey) 
            {
                response.PrivateKey = wallet.PrivateKey.ToHex();
            }

            return response;
        }

        protected ActionResult<WalletDto> AccountDepositWallet(uint accountNumber, uint index)
        {
            if(_hDWallet == null) return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            var wallet =  _hDWallet.GetAccount(accountNumber).GetExternalWallet(index);

            var response =  new WalletDto(){
                Address = wallet.Address
            };

            if(_settings.ExportPrivateKey) 
            {
                response.PrivateKey = wallet.PrivateKey.ToHex();
            }

            return response;
        }
        
        protected ActionResult<WalletDto> AccountChangeWallet(uint accountNumber, uint index)
        {
            if(_hDWallet == null) return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            var wallet = _hDWallet.GetAccount(accountNumber).GetInternalWallet(index);

            var response =  new WalletDto(){
                Address = wallet.Address
            };

            if(_settings.ExportPrivateKey) 
            {
                response.PrivateKey = wallet.PrivateKey.ToHex();
            }

            return response;
        }
    }
}