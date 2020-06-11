using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Offre.Validation.AuthorizePrefilters;

namespace Offre.Test.Validation
{
    [TestClass]
    public class PrefilterPasswordTests
    {
        [TestMethod]
        public void ThrowsArgumentExceptionWhenPasswordIsNull()
        {
            string testData = null;

            try
            {
                new PrefilterPassword(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Password cannot be empty."));
            }
        }
        [TestMethod]
        public void ThrowsArgumentExceptionWhenPasswordIsWhiteSpace()
        {
            string testData = "";

            try
            {
                new PrefilterPassword(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Password cannot be empty."));
            }
        }
        [TestMethod]
        public void ThrowsArgumentExceptionWhenPasswordIsTooShort()
        {
            string testData = new string('a', 7);

            try
            {
                new PrefilterPassword(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Password too short."));
            }
        }
        [TestMethod]
        public void ThrowsArgumentExceptionWhenLoginIsTooLong()
        {
            string testData = new string('a', 129);

            try
            {
                new PrefilterPassword(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Password too long."));
            }
        }

        [TestMethod]
        public void ThrowsArgumentExceptionWhenPasswordNotMatchRules()
        {
            string[] testData = { "qwertyuiop", "Qwertyuiop", "Qwertyuiop123" };

            foreach (var password in testData)
            {
                try
                {
                    new PrefilterPassword(password).MatchPrefilter();
                }
                catch (ArgumentException ex)
                {
                    Assert.IsTrue(ex.Message.Equals("Password does not match rules."));
                }
                
            }
        }

        [TestMethod]
        public void PassesValidationWhenPasswordMatchAllRules()
        {
            string testData = "Qwertyuiop123!@";

            Assert.IsTrue(new PrefilterPassword(testData).MatchPrefilter());
        }
    }
}
