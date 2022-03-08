using System;
using HDWallet.Api.V1.Models;
using HDWallet.Bitcoin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class BitcoinController : Secp256k1Controller<BitcoinWallet>
    {
        public BitcoinController(ILogger<BitcoinController> logger, Settings settings, IServiceProvider prov) : base(logger, settings, prov) {}

        [HttpGet("/Bitcoin/{account}/external/{index}")]
        public ActionResult<WalletDto> GetDeposit(uint account, uint index) => base.AccountDepositWallet(account, index);

        [HttpGet("/Bitcoin/{account}/internal/{index}")]
        public ActionResult<WalletDto> GetChange(uint account, uint index) => base.AccountChangeWallet(account, index);

        [HttpGet("/Bitcoin/Account/external/{index}")]
        public ActionResult<WalletDto> GetAccountDeposit(uint index) => base.DepositWallet(index);

        [HttpGet("/Bitcoin/Account/internal/{index}")]
        public ActionResult<WalletDto> GetAccountChange(uint index) => base.ChangeWallet(index);
    }
}
