using System;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetAccountInformationSpecs
{
    [TestFixture]
    public class when_getting_account_information_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAccountInformation(null);
        }
    }

    [TestFixture]
    public class when_getting_account_information_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAccountInformation("");
        }
    }


    [TestFixture]
    public class when_getting_account_information
    {
        private GetAccountInformation getAccountInformation;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            getAccountInformation = new GetAccountInformation("http://storageurl");
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(getAccountInformation.CreateUri().ToString(), Is.EqualTo("http://storageurl/"));
        }

        [Test]
        public void should_have_a_http_head_method()
        {
            getAccountInformation.Apply(_mockrequest.Object);
            _mockrequest.VerifySet(x => x.Method = "HEAD");
           
        }

        
    }
}