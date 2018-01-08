using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Services;
using Lykke.Pay.Service.Wallets.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lykke.Pay.Service.Wallets.Controllers
{
    /// <summary>
    /// Controller for asset pairs
    /// </summary>
    [Route("api/[controller]")]
    public class WalletsController : Controller
    {
        private readonly IWalletsManager<IWallet> _manager;

        public WalletsController(IWalletsManager<IWallet> manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Forcibly updates asset pairs cache
        /// </summary>
        /// <returns></returns>
        [HttpPost("updateCache")]
        [SwaggerOperation("UpdateWalletsCache")]
        public async Task UpdateCache()
        {
            await _manager.UpdateCacheAsync();
        }

        /// <summary>
        /// Returns wallet list which are in lykke system
        /// </summary>
        /// <param name="walletAddresses"></param>
        [HttpPost]
        [SwaggerOperation("GetLykkeWallets")]
        [ProducesResponseType(typeof(WalletResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLykkeWallets([FromBody]List<string> walletAddresses)
        {
            var lykkeWallets = await _manager.GetLykkeWalletsAsync(walletAddresses);

            
            return Ok(WalletResponseModel.Create(lykkeWallets));
        }


        /// <summary>
        /// Returns information about valid or not address of a wallet
        /// </summary>
        /// <param name="walletAddress"></param>
        [HttpGet("{walletAddress}")]
        [SwaggerOperation("ValideteWallets")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public bool ValideteWallets(string walletAddress)
        {
            return _manager.ValideteWallets(walletAddress);
        }


    }
}