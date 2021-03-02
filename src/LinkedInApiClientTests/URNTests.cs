using System;
using System.Linq;
using LinkedInApiClient.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedInApiClientTests
{
    [TestClass]
    public class URNTests
    {

        [TestMethod]
        public void URNEncoding_For_RestLi_V2()
        {
            var urn = CommonURN.OrganizationId("37246747");
            var encoded = urn.UrlEncode();

            Assert.AreEqual("urn%3Ali%3Aorganization%3A37246747", encoded);
        }

        [TestMethod]
        public void UrnParsing_StringMustStartWith_urn()
        {
            var urn = LinkedInURN.Parse("urn:li:share:384576");

            Assert.IsTrue(urn.HasValue);

            Assert.AreEqual("li", urn.Namespace);
            Assert.AreEqual("share", urn.EntityType);
            Assert.AreEqual("384576", urn.Id);
        }

        [TestMethod]
        public void UrnParsing_CanExcept_NestedIdSegments()
        {
            var urn = LinkedInURN.Parse("urn:li:like:(urn:li:person:y635rRy2m3,urn:li:activity:6762019589283995648)");

            Assert.IsTrue(urn.HasValue);

            Assert.AreEqual("li", urn.Namespace);
            Assert.AreEqual("like", urn.EntityType);
            Assert.AreEqual("(urn:li:person:y635rRy2m3,urn:li:activity:6762019589283995648)", urn.Id);
        }

        [TestMethod]
        public void UrnParsing_CanExtract_References()
        {
            var urn = LinkedInURN.Parse("urn:li:like:(urn:li:person:y635rRy2m3,urn:li:activity:6762019589283995648)");

            Assert.IsTrue(urn.HasReferences());

            var refs = urn.IdReferences().ToArray();

            Assert.AreEqual(2, refs.Length);
        }
    }
}
