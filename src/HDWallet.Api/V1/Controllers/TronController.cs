using System;
using HDWallet.Api.V1.Models;
using HDWallet.Core;
using HDWallet.Tron;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class TronController : Secp256k1Controller<TronWallet>
    {
        public TronController(ILogger<TronController> logger, Settings settings, IServiceProvider prov) : base(logger, settings, prov) {}

        [HttpGet("/Tron/{account}/external/{index}")]
        public ActionResult<WalletDto> GetDeposit(uint account, uint index) => base.AccountDepositWallet(account, index);

        [HttpGet("/Tron/{account}/internal/{index}")]
        public ActionResult<WalletDto> GetChange(uint account, uint index) => base.AccountChangeWallet(account, index);

        [HttpGet("/Tron/Account/external/{index}")]
        public ActionResult<WalletDto> GetAccountDeposit(uint index) => base.DepositWallet(index);

        [HttpGet("/Tron/Account/internal/{index}")]
        public ActionResult<WalletDto> GetAccountChange(uint index) => base.ChangeWallet(index);
    }
}
