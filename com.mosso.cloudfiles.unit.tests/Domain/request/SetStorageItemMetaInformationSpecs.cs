using System;
using System.Collections.Generic;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.SetStorageItemMetaInformationSpecs
{
    [TestFixture]
    public class when_setting_storage_item_information_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetStorageItemMetaInformation(null, "containername", "storageitemname", null);
        }
    }

    [TestFixture]
    public class when_setting_storage_item_information_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetStorageItemMetaInformation("", "containername", "storageitemname", null);
        }
    }

    [TestFixture]
    public class when_setting_storage_item_information_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetStorageItemMetaInformation("http://storageurl", null, "storageitemname", null);
        }
    }

    [TestFixture]
    public class when_setting_storage_item_information_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetStorageItemMetaInformation("http://storageurl", "", "storageitemname", null);
        }
    }

    [TestFixture]
    public class when_setting_storage_item_information_and_storage_item_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetStorageItemMetaInformation("http://storageurl", "containername", null, null);
        }
    }

    [TestFixture]
    public class when_setting_storage_item_information_and_storage_item_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetStorageItemMetaInformation("http://storageurl", "containername", "", null);
        }
    }

    [TestFixture]
    public class when_setting_storage_item_information
    {
        private SetStorageItemMetaInformation setStorageItemInformation;

        [SetUp]
        public void setup()
        {
            var metadata = new Dictionary<string, string>{{"key1", "value1"},{"key2", "value2"}};
            setStorageItemInformation = new SetStorageItemMetaInformation("http://storageurl", "containername", "storageitemname", metadata);
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(setStorageItemInformation.CreateUri().ToString(), Is.EqualTo("http://storageurl/containername/storageitemname"));
        }

        [Test]
        public void should_have_a_http_post_method()
        {
            Asserts.AssertMethod(setStorageItemInformation, "POST");
          
        }

        [Test]
        public void should_have_metadata_in_the_headers()
        {
            var headers = new WebHeaderCollection();
            var mock = new Mock<ICloudFilesRequest>();
            mock.SetupGet(x => x.Headers).Returns(headers);
            setStorageItemInformation.Apply(mock.Object);
          
            Assert.That(headers[utils.Constants.META_DATA_HEADER + "key1"], Is.EqualTo("value1"));
            Assert.That(headers[utils.Constants.META_DATA_HEADER + "key2"], Is.EqualTo("value2"));
        }
    }
}