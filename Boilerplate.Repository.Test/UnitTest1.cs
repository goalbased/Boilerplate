using System;
using System.Collections.Generic;
using System.Linq;
using Bolierplate.Repository;
using Bolierplate.Repository.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boilerplate.Repository.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int firstNumber = 0;
            int secondNumber = 0;
            int expected = 0;
            int actual;

            actual = firstNumber + secondNumber;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Add()
        {
            var expected = "Michael Test";

            var db = new BolierplateDbContext();
            var test = db.Tests.Add(new Bolierplate.Repository.Tables.Test
            {
                Name = expected
            });
            db.SaveChanges();

            var actual = db.Tests.FirstOrDefault(x => x.Id == test.Id)?.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Relation()
        {
            var name = "Michael Test";
            var expected = "relation 1";

            var db = new BolierplateDbContext();
            var test = db.Tests.Add(new Bolierplate.Repository.Tables.Test
            {
                Name = name
            });

            db.TestsDetails.Add(new TestsDetail
            {
                Name = expected,
                TestsId = test.Id
            });

            db.SaveChanges();

            var actual = db.Tests
                .FirstOrDefault(x => x.Id == test.Id)?
                .TestsDetails.FirstOrDefault()?
                .Name;

            Assert.AreEqual(expected, actual);
        }
    }
}
