
using System.Collections.Generic;
using NUnit.Framework;

namespace CustomCollections.UnitTesting
{
    [TestFixture]
    public class UnitTesting
    {
        [Test]
        public void CountTest()
        {
            var pq = new PriorityQueue<int>();

            Assert.AreEqual(0, pq.Count);
            Assert.AreEqual(true, pq.IsEmpty);

            pq.Enqueue(0, 0);
            pq.Enqueue(0, 0);
            pq.Enqueue(1, 1);
            pq.Enqueue(1, 1);
            pq.Enqueue(2, 2);
            pq.Enqueue(2, 2);

            Assert.AreEqual(6, pq.Count);
            Assert.AreEqual(false, pq.IsEmpty);

            Assert.AreEqual(0, pq.Dequeue());
            Assert.AreEqual(0, pq.Dequeue());

            Assert.AreEqual(4, pq.Count);
            Assert.AreEqual(false, pq.IsEmpty);

            Assert.AreEqual(1, pq.Dequeue());
            Assert.AreEqual(1, pq.Dequeue());
            Assert.AreEqual(2, pq.Dequeue());
            Assert.AreEqual(2, pq.Dequeue());

            Assert.AreEqual(0, pq.Count);
            Assert.AreEqual(true, pq.IsEmpty);
        }

        [Test]
        public void AddRemoveTest()
        {
            var pq = new PriorityQueue<int>();

            pq.Enqueue(0, 12);
            pq.Enqueue(0, 11);
            pq.Enqueue(2, 100);
            pq.Enqueue(1, 122);
            pq.Enqueue(4, 112);
            pq.Enqueue(2, 132);
            pq.Enqueue(9, 12);

            Assert.AreEqual(11, pq.Dequeue());
            Assert.AreEqual(12, pq.Dequeue());
            Assert.AreEqual(122, pq.Dequeue());
            Assert.AreEqual(132, pq.Peek());
            Assert.AreEqual(132, pq.Dequeue());
            Assert.AreEqual(100, pq.Dequeue());
            Assert.AreEqual(112, pq.Dequeue());
            Assert.AreEqual(12, pq.Peek());
            Assert.AreEqual(12, pq.Peek());
            Assert.AreEqual(12, pq.Peek());
            Assert.AreEqual(12, pq.Peek());
            Assert.AreEqual(12, pq.Peek());
            Assert.AreEqual(12, pq.Dequeue());
        }
    }
}
