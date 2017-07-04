using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Services;
using Lykke.Pay.Service.Wallets.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lykke.Pay.Service.Wallets.Tests
{
    [TestClass]
    public class DictionaryManagerTests
    {
        [UsedImplicitly]
        public class TestItem : IWallet
        {
            public string WalletAddress { get; set; }
        }

        private readonly TimeSpan _cacheExpirationPeriod = TimeSpan.FromMinutes(1);

        private IWalletsManager<TestItem> _manager;
        private Mock<IWalletsRepository<TestItem>> _repositoryMock;
        private Mock<IWalletsCacheService<TestItem>> _cacheServiceMock;
        private Mock<IDateTimeProvider> _dateTimeProviderMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _repositoryMock = new Mock<IWalletsRepository<TestItem>>();
            _cacheServiceMock = new Mock<IWalletsCacheService<TestItem>>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();

            _manager = new WalletsManager<TestItem>(_repositoryMock.Object, _cacheServiceMock.Object, _dateTimeProviderMock.Object, _cacheExpirationPeriod);
        }

        #region Getting item

      
       

        #endregion


        #region Getting all items

        [TestMethod]
        public async Task Getting_all_items_returns_empty_enumerable_if_no_items()
        {
            // Arrange
            _cacheServiceMock
                .Setup(s => s.GetAll())
                .Returns(() => new TestItem[0]);

            // Act
            var items = await _manager.GetAllAsync();

            // Assert
            Assert.IsNotNull(items);
            Assert.IsFalse(items.Any());
        }

        [TestMethod]
        public async Task Getting_all_items_returns_items()
        {
            // Arrange
            _cacheServiceMock
                .Setup(s => s.GetAll())
                .Returns(() => new[]
                {
                    new TestItem { WalletAddress = "Test1" },
                    new TestItem { WalletAddress = "Test2" },
                    new TestItem { WalletAddress = "Test3" }
                });

            // Act
            var pairs = (await _manager.GetAllAsync()).ToArray();

            // Assert
            Assert.AreEqual(3, pairs.Length);
            Assert.AreEqual("Test1", pairs[0].WalletAddress);
            Assert.AreEqual("Test2", pairs[1].WalletAddress);
            Assert.AreEqual("Test3", pairs[2].WalletAddress);
        }

        #endregion


        #region Cache updating

        [TestMethod]
        public async Task Getting_item_first_time_updates_cache()
        {
            // Arrange
            _dateTimeProviderMock.SetupGet(p => p.UtcNow).Returns(() => new DateTime(2017, 06, 23, 21, 00, 00));
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(() => new[]
                {
                    new TestItem {WalletAddress = "Test1"},
                });

            // Act
            await _manager.GetLykkeWalletsAsync(new[] { "Test1" });

            // Asert
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            // ReSharper disable once PossibleMultipleEnumeration
            _cacheServiceMock.Verify(s => s.Update(It.Is<IEnumerable<TestItem>>(p => p.Count() == 1 && p.Count(a => a.WalletAddress == "Test1") == 1)), Times.Once);
        }

        [TestMethod]
        public async Task Getting_all_items_first_time_updates_cache()
        {
            // Arrange
            _dateTimeProviderMock.SetupGet(p => p.UtcNow).Returns(() => new DateTime(2017, 06, 23, 21, 00, 00));
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(() => new[]
                {
                    new TestItem {WalletAddress = "Test1"}
                });
            _cacheServiceMock.Setup(s => s.GetAll()).Returns(() => new TestItem[0]);

            // Act
            await _manager.GetAllAsync();

            // Asert
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            // ReSharper disable once PossibleMultipleEnumeration
            _cacheServiceMock.Verify(s => s.Update(It.Is<IEnumerable<TestItem>>(p => p.Count() == 1 && p.Count(a => a.WalletAddress == "Test1") == 1)), Times.Once);
        }

        [TestMethod]
        public async Task Getting_item_once_after_cache_expiration_updates_cache()
        {
            // Arrange
            var initialNow = new DateTime(2017, 06, 23, 21, 00, 00);

            _dateTimeProviderMock.SetupGet(p => p.UtcNow).Returns(() => initialNow);
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(() => new []
                {
                    new TestItem {WalletAddress = "Test1"},
                });

            await _manager.GetLykkeWalletsAsync(new []{"Test1"});

            _dateTimeProviderMock.SetupGet(p => p.UtcNow).Returns(() => initialNow.Add(_cacheExpirationPeriod).AddTicks(1));

            _repositoryMock.ResetCalls();
            _cacheServiceMock.ResetCalls();

            // Act
            await _manager.GetLykkeWalletsAsync(new[] { "Test1" });

            // Asert
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            // ReSharper disable once PossibleMultipleEnumeration
            _cacheServiceMock.Verify(s => s.Update(It.Is<IEnumerable<TestItem>>(p => p.Count() == 1 && p.Count(a => a.WalletAddress == "Test1") == 1)), Times.Once);
        }

        [TestMethod]
        public async Task Getting_all_items_once_after_cache_expiration_updates_cache()
        {
            // Arrange
            var initialNow = new DateTime(2017, 06, 23, 21, 00, 00);

            _dateTimeProviderMock.SetupGet(p => p.UtcNow).Returns(() => initialNow);
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(() => new []
                {
                    new TestItem {WalletAddress = "Test1"},
                });
            _cacheServiceMock.Setup(s => s.GetAll()).Returns(() => new TestItem[0]);

            await _manager.GetLykkeWalletsAsync(new [] {"Test1"});

            _dateTimeProviderMock.SetupGet(p => p.UtcNow).Returns(() => initialNow.Add(_cacheExpirationPeriod).AddTicks(1));

            _repositoryMock.ResetCalls();
            _cacheServiceMock.ResetCalls();

            // Act
            await _manager.GetAllAsync();

            // Asert
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            // ReSharper disable once PossibleMultipleEnumeration
            _cacheServiceMock.Verify(s => s.Update(It.Is<IEnumerable<TestItem>>(p => p.Count() == 1 && p.Count(a => a.WalletAddress == "Test1") == 1)), Times.Once);
        }

        #endregion
    }
}