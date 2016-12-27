using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeSolver2VS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver2VS.Tests
{
    [TestClass()]
    public class PointTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            var p1 = new Point(1, 1, null);
            var p2 = new Point(1, 1, null);
            var p3 = new Point(1, 1, p2);
            var p4 = new Point(2, 1, null);
            Assert.AreEqual(p1, p2);
            Assert.AreEqual(p2, p3);
            Assert.AreNotEqual(p3, p4);
        }

        [TestMethod()]
        public void HashTest()
        {
            var p1 = new Point(1, 1, null);
            var p2 = new Point(1, 1, null);
            var p3 = new Point(1, 1, p2);
            var p4 = new Point(2, 1, null);
            var dic = new Dictionary<Point, Point>();
            dic.Add(p1, p1);
            Assert.IsTrue(dic.ContainsKey(p2));
            Assert.IsFalse(dic.ContainsKey(p4));         

        }
    }
}