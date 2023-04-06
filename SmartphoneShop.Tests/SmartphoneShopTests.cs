using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private Shop shop;
        private Smartphone smartphone;
        private string modelName = "IPhone13 Pro Max";
        private int maximumCharge = 100;
        public int capacity = 1;

        [SetUp]
        public void SetUp()
        {
            smartphone = new Smartphone(modelName, maximumCharge);
            shop = new Shop(capacity);
        }

        [TearDown]
        public void TearDown()
        {
            shop = null;
        }

        [Test]
        public void Test_ShopConstructorShouldWorkCorrectly()
        {
            Assert.AreEqual(capacity, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void Test_ShopConstructorShouldThrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() => shop = new Shop(-5));
        }

        [Test]
        public void Test_AddingSmartphonetoShopShouldWorkCorrectly()
        {
            shop.Add(smartphone);

            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void Test_DublicateSmartphoneShoultNotBeAllowed()
        {
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone));
        }

        [Test]
        public void Test_CullShopCapacityShouldThrow()
        {
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.Add(new Smartphone("IPhone 12", 100)));
        }

        [Test]
        public void Test_RemoveSmartphoneFromShopShouldWorkCorrectly()
        {
            shop.Add(smartphone);
            shop.Remove(smartphone.ModelName);

            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void Test_RemoveShouldThrowIfSmartphoneDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() => shop.Remove(smartphone.ModelName));
        }

        [Test]
        public void Test_TestSmartphoneThatDoesNotExistShoulThrow()
        {
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone(smartphone.ModelName, 10));
        }

        [Test]
        public void Test_TestSmartphoneThatBatteryIsNotEnough()
        {
            shop.Add(new Smartphone("IPhone 5", 10));
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("IPhone 5", 50));
        }

        [Test]
        public void Test_TestSmartphoneShouldWorkCorrectly()
        {
            shop.Add(smartphone);
            shop.TestPhone(smartphone.ModelName, 50);

            Assert.AreEqual(50, smartphone.CurrentBateryCharge);
        }
        [Test]
        public void Test_ChargePhoneShouldThrowIfSmartphoneDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone(smartphone.ModelName));
        }

        [Test]
        public void Test_ChargePhoneShouldWorkCorrectly()
        {
            Smartphone chargedPhone = new Smartphone("Nokia 3310", 100);
            shop.Add(chargedPhone);
            shop.TestPhone(chargedPhone.ModelName, 50);
            shop.ChargePhone(chargedPhone.ModelName);

            Assert.AreEqual(100, chargedPhone.CurrentBateryCharge);
        }
    }
}