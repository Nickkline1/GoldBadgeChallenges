using Cafe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CafeUnitTest
{
    [TestClass]
    public class CafeRepoTest //same pattern for all projects
    {
        [TestMethod]
        public void AddMenuItem_ShouldReturnTrue()
        {
            MenuItem item = new MenuItem();
            MenuRepo menu = new MenuRepo();
            bool addResult = menu.AddMenuItem(item);
            Assert.IsTrue(addResult);
        }
        [TestMethod]
        public void GetMenu_ShouldReturnCorrectItem()
        {
            MenuItem item = new MenuItem();
            MenuRepo menu = new MenuRepo();
            menu.AddMenuItem(item);
            List<MenuItem> menuItems = menu.GetMenu();
            bool menuHasItems = menuItems.Contains(item);
            Assert.IsTrue(menuHasItems);

        }
        [TestMethod]
        public void GetByMealNumber_ShouldReturnCorrectMenutItem()
        {
            MenuItem item = new MenuItem();
            MenuRepo menu = new MenuRepo();


        }

    }
}
