using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using ConsoleApp;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class BizDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraisReturnsCorrectNumberOfInsertedRows()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddMultipleSamurais");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinessDataLogic(context);
                var nameList = new string[] { "Penelope", "Fred", "bart" };
                var result = bizlogic.AddMultipleSamurais(nameList);
                Assert.AreEqual(nameList.Count(), result);
            }
        }
        [TestMethod]
        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("InsertNewSamurai");

            using (var context = new SamuraiContext(builder.Options))
            {
                var bizLogic = new BusinessDataLogic(context);
                bizLogic.InsertNewSamurai(new Samurai());
            };
            using (var context2 = new SamuraiContext(builder.Options))
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }

        }
    }
}
