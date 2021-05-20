using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Badges;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BadgesTests
{
    [TestClass]
    public class Badge_RepoTests
    {
        [TestMethod]
        public void AddToRepo_ShouldBeCorrect()
        {
            //ARRANGE
            Badge badge = new Badge(01);
            Badge_Repo repo = new Badge_Repo();

            //ACT
            bool addResult = repo.AddBadge(badge);

            //ASSERT
            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GetBadge_ShouldBeCorrect()
        {
            //ARRANGE
            Badge badge = new Badge(001);
            Badge_Repo repo = new Badge_Repo();
            repo.AddBadge(badge);

            //ACT
            Dictionary<int, List<string>> badges = repo.GetAllBadges();
            bool hasBadges = badges.ContainsKey(badge.BadgeID);

            //ASSERT
            Assert.IsTrue(hasBadges);

        }

        [TestMethod]
        public void GetBadgeByID_ShouldBeCorrect()
        {

            //ARRANGE
            Badge badge = new Badge(001, new List<string> { "A1", "A2", });
            Badge_Repo repo = new Badge_Repo();
            repo.AddBadge(badge);
            int badgeID = 001;


            //ACT
            Badge searchResult = repo.GetABadgeByID(badgeID);

            //ASSERT
            Assert.AreEqual(searchResult.BadgeID, badgeID);
        }

        [TestMethod]
        public void UpdateExistingBadge_ShouldBeTrue()
        {
            //ARRANGE
            Badge oldBadge = new Badge(001, new List<string> { "A1", "A2" });
            Badge newBadge = new Badge(001, new List<string> { "A2", "A3", "B5" });
            Badge_Repo repo = new Badge_Repo();
            repo.AddBadge(oldBadge);

            //ACT
            bool updateResult = repo.UpdateExistingBadge(oldBadge.BadgeID, newBadge);

            //ASSERT
            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void DeleteBadge_ShouldReturnTrue()
        {
            //ARRANGE
            Badge badge = new Badge(001, new List<string> { "A1", "A2", });
            Badge_Repo repo = new Badge_Repo();
            repo.AddBadge(badge);
            int badgeID = 001;

            //ACT
            Badge oldBadge = repo.GetABadgeByID(badgeID);
            bool removeResult = repo.DeleteBadge(oldBadge);

            //ASSERT
            Assert.IsTrue(removeResult);
        }
    }
}
