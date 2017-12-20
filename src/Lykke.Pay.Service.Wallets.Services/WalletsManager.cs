using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Repositories;
using Lykke.Pay.Service.Wallets.Core.Services;

namespace Lykke.Pay.Service.Wallets.Services
{
    [UsedImplicitly]
    public class WalletsManager<TWallet> : IWalletsManager<TWallet> 
        where TWallet : IWallet
    {
        private readonly IWalletsRepository _repository;
        private readonly IWalletsCacheService<TWallet> _cache;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly TimeSpan _cacheExpirationPeriod;
        private DateTime _cacheExpirationMoment;

        public WalletsManager(
            IWalletsRepository repository,
            IWalletsCacheService<TWallet> cache,
            IDateTimeProvider dateTimeProvider,
            TimeSpan cacheExpirationPeriod)
        {
            _repository = repository;
            _cache = cache;
            _dateTimeProvider = dateTimeProvider;
            _cacheExpirationPeriod = cacheExpirationPeriod;

            _cacheExpirationMoment = DateTime.MinValue;
        }


        public async Task<IEnumerable<TWallet>> GetLykkeWalletsAsync(IEnumerable<string> walletAddresses)
        {
            // await EnsureCacheIsUpdatedAsync();
            await UpdateCacheAsync();
            return _cache.GetLykkeWallets(walletAddresses);
        }

        public async Task<IEnumerable<TWallet>> GetAllAsync()
        {
            await EnsureCacheIsUpdatedAsync();

            return _cache.GetAll();
        }

        public void InsertNewAddress(TWallet wallet)
        {
            _cache.InsertNewAddress(wallet);
        }

        public async Task UpdateCacheAsync()
        {
            var items = await _repository.GetAllAsync();

            _cache.Update(items.Select(i=>(TWallet)i));

            _cacheExpirationMoment = _dateTimeProvider.UtcNow + _cacheExpirationPeriod;
        }

        private async Task EnsureCacheIsUpdatedAsync()
        {
            if (_cacheExpirationMoment < _dateTimeProvider.UtcNow)
            {
                await UpdateCacheAsync();
            }
        }
    }
}