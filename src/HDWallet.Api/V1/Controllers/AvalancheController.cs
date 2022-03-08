using System;
using HDWallet.Api.V1.Models;
using HDWallet.Avalanche;
using HDWallet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class AvalancheController : Secp256k1Controller<AvalancheWallet>
    {
        public AvalancheController(ILogger<AvalancheController> logger, Settings settings, IServiceProvider prov) : base(logger, settings, prov) {}

        [HttpGet("/Avalanche/{account}/external/{index}")]
        public ActionResult<WalletDto> GetDeposit(uint account, uint index) => base.AccountDepositWallet(account, index);

        [HttpGet("/Avalanche/{account}/internal/{index}")]
        public ActionResult<WalletDto> GetChange(uint account, uint index) => base.AccountChangeWallet(account, index);

        [HttpGet("/Avalanche/Account/external/{index}")]
        public ActionResult<WalletDto> GetAccountDeposit(uint index) => base.DepositWallet(index);

        [HttpGet("/Avalanche/Account/internal/{index}")]
        public ActionResult<WalletDto> GetAccountChange(uint index) => base.ChangeWallet(index);
    }
}
