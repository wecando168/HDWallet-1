using System;
using HDWallet.Api.V1.Models;
using HDWallet.Core;
using HDWallet.FileCoin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class FileCoinController : Secp256k1Controller<FileCoinWallet>
    {
        public FileCoinController(ILogger<FileCoinController> logger, Settings settings, IServiceProvider prov) : base(logger, settings, prov) {}

        [HttpGet("/FileCoin/{account}/external/{index}")]
        public ActionResult<WalletDto> GetDeposit(uint account, uint index) => base.AccountDepositWallet(account, index);

        [HttpGet("/FileCoin/{account}/internal/{index}")]
        public ActionResult<WalletDto> GetChange(uint account, uint index) => base.AccountChangeWallet(account, index);

        [HttpGet("/FileCoin/Account/external/{index}")]
        public ActionResult<WalletDto> GetAccountDeposit(uint index) => base.DepositWallet(index);

        [HttpGet("/FileCoin/Account/internal/{index}")]
        public ActionResult<WalletDto> GetAccountChange(uint index) => base.ChangeWallet(index);
    }
}
