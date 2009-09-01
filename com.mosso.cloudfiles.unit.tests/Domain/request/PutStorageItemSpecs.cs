using System;
using System.IO;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.exceptions;
using com.mosso.cloudfiles.unit.tests.CustomMatchers;
using Moq;
using NUnit.Framework;
using SpecMaker.Core;
using SpecMaker.Core.Matchers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.PutStorageItemSpecs
{
    public class PutStorageItemSpec : BaseSpec
    {
        public void when_creating_uri()
        {
            should("start with storage url passed from constructor", RuleIs.Pending);
            should("have container name in the middle", RuleIs.Pending);
            should("have storage item at end", RuleIs.Pending);
        }
        public void when_creating_uri_and_storage_item_has_forward_slashes_at_the_beginning()
        {
            var memstream = new MemoryStream();
            PutStorageItem item = new PutStorageItem("http://storeme", "itemcont", "/stuffhacks.txt", memstream);
            Uri url = item.CreateUri();
            should("remove all slashes", ()=> url.EndsWith("stuffhacks.txt"));
        }
    }
    [TestFixture]
    public class when_putting_a_storage_item_via_local_file_path_and_the_local_file_does_not_exist
    {
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void should_throw_file_not_found_exception()
        {
            var mock = new Mock<ICloudFilesRequest>();
            new PutStorageItem("a", "a", "a", "a").Apply(mock.Object);
        }

    }

    [TestFixture]
    public class when_putting_a_storage_item_via_local_file_path_and_the_container_name_exceeds_the_maximum_length
    {
        [Test]
        [ExpectedException(typeof(ContainerNameException))]
        public void should_throw_container_name_exception()
        {
            new PutStorageItem("a", new string('a', Constants.MAX_CONTAINER_NAME_LENGTH + 1), "a", "a");
        }

    }

    [TestFixture]
    public class when_putting_a_storage_item_via_stream_and_the_container_name_exceeds_the_maximum_length
    {
        [Test]
        [ExpectedException(typeof(ContainerNameException))]
        public void should_throw_container_name_exception()
        {
            var s = new MemoryStream(new byte[0]);
            new PutStorageItem("a", new string('a', Constants.MAX_CONTAINER_NAME_LENGTH + 1), "a", s);
        }
    }

    [TestFixture]
    public class when_putting_a_storage_item_via_local_file_path_and_the_storage_item_name_exceeds_the_maximum_length
    {
        [Test]
        [ExpectedException(typeof(StorageItemNameException))]
        public void should_throw_container_name_exception()
        {
            new PutStorageItem("a", "a", new string('a', Constants.MAX_OBJECT_NAME_LENGTH + 1), "a");
        }

    }

    [TestFixture]
    public class when_putting_a_storage_item_via_stream_and_the_storage_item_name_exceeds_the_maximum_length
    {
        [Test]
        [ExpectedException(typeof(StorageItemNameException))]
        public void should_throw_container_name_exception()
        {
            var s = new MemoryStream(new byte[0]);
            new PutStorageItem("a", "a", new string('a', Constants.MAX_OBJECT_NAME_LENGTH + 1), s);
        }
    }

    
}