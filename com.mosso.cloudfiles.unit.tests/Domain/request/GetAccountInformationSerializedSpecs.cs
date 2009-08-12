using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetAccountInformationSerializedSpecs
{
    [TestFixture]
    public class when_getting_account_information_in_json_format_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAccountInformationSerialized(null, Format.JSON);
        }
    }

    [TestFixture]
    public class when_getting_account_information_in_json_format_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAccountInformationSerialized("", Format.JSON);
        }
    }


    [TestFixture]
    public class when_getting_account_information_in_json_format
    {
        private GetAccountInformationSerialized getAccountInformationSerialized;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            getAccountInformationSerialized = new GetAccountInformationSerialized("http://storageurl", Format.JSON);
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(getAccountInformationSerialized.CreateUri().ToString(), Is.EqualTo("http://storageurl/?format=json"));
        }

        [Test]
        public void should_have_a_http_get_method()
        {
            getAccountInformationSerialized.Apply(_mockrequest.Object);
            _mockrequest.VerifySet(x => x.Method = "GET");
           
        }

      
    }

    [TestFixture]
    public class when_getting_account_information_in_xml_format_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAccountInformationSerialized(null, Format.XML);
        }
    }

    [TestFixture]
    public class when_getting_account_information_in_xml_format_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAccountInformationSerialized("", Format.XML);
        }
    }


    [TestFixture]
    public class when_getting_account_information_in_xml_format
    {
        private GetAccountInformationSerialized getAccountInformationSerialized;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            getAccountInformationSerialized = new GetAccountInformationSerialized("http://storageurl", Format.XML);
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(getAccountInformationSerialized.CreateUri().ToString(), Is.EqualTo("http://storageurl/?format=xml"));
        }

        [Test]
        public void should_have_a_http_get_method()
        {
            getAccountInformationSerialized.Apply(_mockrequest.Object);
            _mockrequest.VerifySet(x => x.Method = "GET");
        }

        
    }
}