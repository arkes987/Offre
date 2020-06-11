using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Offre.Validation.AuthorizePrefilters;

namespace Offre.Test.Validation
{
    [TestClass]
    public class PrefilterLoginTests
    {
        [TestMethod]
        public void ThrowsArgumentExceptionWhenLoginIsNull()
        {
            string testData = null;

            try
            {
                new PrefilterLogin(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Login cannot be empty."));
            }
        }

        [TestMethod]
        public void ThrowsArgumentExceptionWhenLoginIsWhiteSpace()
        {
            string testData = "";

            try
            {
                new PrefilterLogin(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Login cannot be empty."));
            }
        }

        [TestMethod]
        public void ThrowsArgumentExceptionWhenLoginIsTooShort()
        {
            string testData = new string('a', 7);

            try
            {
                new PrefilterLogin(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Login too short."));
            }
        }
        [TestMethod]
        public void ThrowsArgumentExceptionWhenLoginIsTooLong()
        {
            string testData = new string('a', 129);

            try
            {
                new PrefilterLogin(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Login too long."));
            }
        }

        [TestMethod]
        public void ThrowsArgumentExceptionWhenLoginIsNotEmail()
        {
            const string testData = "as1234QWER";

            try
            {
                new PrefilterLogin(testData).MatchPrefilter();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Equals("Login is not valid email."));
            }
        }

        [TestMethod]
        public void PassesValidationWhenLoginIsValidEmail()
        {
            string[] testData = {"arek1998@op.pl", "arkadiusz@gmail.com", "testoWyMail.test@interia.pl"};

            foreach (var email in testData)
            {
                Assert.IsTrue(new PrefilterLogin(email).MatchPrefilter());
            }
        }
    }
}
