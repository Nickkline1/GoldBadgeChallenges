using Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ClaimsTests
{
    [TestClass]
    public class ClaimsTest
    {
        [TestMethod]
        public void AddClaims_GetCorrectBool()
        {
            //ARRANGE
            Claim claim = new Claim();
            Claim_Repo repo = new Claim_Repo();

            //ACT
            bool addClaim = repo.AddClaim(claim);

            //ASSERT
            Assert.IsTrue(addClaim);
        }
        [TestMethod]
        public void GetClaims_ShouldReturnCorrect()
        {
            //ARRANGE
            Claim claim = new Claim();
            Claim_Repo repo = new Claim_Repo();
            repo.AddClaim(claim);

            //ACT
            Queue<Claim> claims = repo.GetAllClaims();
            bool hasClaim = claims.Contains(claim);

            //ASSERT
            Assert.IsTrue(hasClaim);
        }
        [TestMethod]
        public void PeekClaim_ShouldReturnCorrect()
        {
            //ARRANGE
            Claim claim = new Claim();
            Claim_Repo repo = new Claim_Repo();
            repo.AddClaim(claim);

            //ACT
            Claim nextClaim = repo.PeekClaim();
            //ASSERT
            Assert.AreEqual(nextClaim, claim);

        }
        [TestMethod]
        public void DequeueClaim_ShouldReturnTrue()
        {
            //ARRANGE
            Claim claim = new Claim();
            Claim_Repo repo = new Claim_Repo();
            repo.AddClaim(claim);

            //ACT
            bool dequeuedClaim = repo.DequeueClaim();

            //ASSERT
            Assert.IsTrue(dequeuedClaim);
        }
    }
}
