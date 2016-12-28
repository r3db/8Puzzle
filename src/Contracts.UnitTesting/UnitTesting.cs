using System;
using NUnit.Framework;

namespace Contracts.UnitTesting
{
    [TestFixture]
    public class UnitTesting
    {
        [Test]
        public void BiggerOrEqualThan1()
        {
            9.BiggerOrEqualThanDebug(0);
            8.BiggerOrEqualThanDebug(0);
            7.BiggerOrEqualThanDebug(0);
            6.BiggerOrEqualThanDebug(0);
            5.BiggerOrEqualThanDebug(0);
            4.BiggerOrEqualThanDebug(0);
            3.BiggerOrEqualThanDebug(0);
            2.BiggerOrEqualThanDebug(0);
            1.BiggerOrEqualThanDebug(0);
            0.BiggerOrEqualThanDebug(0);
        }

        [Test]
        public void BiggerOrEqualThan2()
        {
            Assert.That(() => 9.BiggerOrEqualThanDebug(10), Throws.TypeOf<ContractOutOfRangeException>());
        }

        [Test]
        public void BiggerOrEqualThan3()
        {
            Assert.That(() => (-1).BiggerOrEqualThanDebug(10), Throws.TypeOf<ContractOutOfRangeException>());
        }

        [Test]
        public void LessThan1()
        {
            9.LessThanDebug(10);
            8.LessThanDebug(10);
            7.LessThanDebug(10);
            6.LessThanDebug(10);
            5.LessThanDebug(10);
            4.LessThanDebug(10);
            3.LessThanDebug(10);
            2.LessThanDebug(10);
            1.LessThanDebug(10);
            (-1).LessThanDebug(10);
        }

        [Test]
        public void LessThan2()
        {
            Assert.That(() => 10.LessThanDebug(10), Throws.TypeOf<ContractOutOfRangeException>());
        }

        [Test]
        public void LessThan3()
        {
            Assert.That(() => 11.LessThanDebug(10), Throws.TypeOf<ContractOutOfRangeException>());
        }

        [Test]
        public void LengthEquals1()
        {
            Contracts.LengthEqualsDebug(null, 0);
            "".LengthEqualsDebug(0);
            "a".LengthEqualsDebug(1);
            "aa".LengthEqualsDebug(2);
            "aaa".LengthEqualsDebug(3);
            "aaaa".LengthEqualsDebug(4);
            "aaaaa".LengthEqualsDebug(5);
            "aaaaaa".LengthEqualsDebug(6);
            "aaaaaaa".LengthEqualsDebug(7);
            "aaaaaaaa".LengthEqualsDebug(8);
            "aaaaaaaaa".LengthEqualsDebug(9);
        }

        [Test]
        public void LengthEquals2()
        {
            Assert.That(() => "".LengthEqualsDebug(1), Throws.TypeOf<ContractOutOfRangeException>());
        }

        [Test]
        public void LengthEquals3()
        {
            Assert.That(() => Contracts.LengthEqualsDebug(null, 1), Throws.TypeOf<ContractOutOfRangeException>());
        }

        [Test]
        public void LengthEquals4()
        {
            Assert.That(() => "3".LengthEqualsDebug(2), Throws.TypeOf<ContractOutOfRangeException>());
        }

    }
}
