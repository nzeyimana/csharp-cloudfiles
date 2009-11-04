using System;
using System.IO;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.unit.tests.CustomMatchers;
using Moq;
using SpecMaker.Core;
using SpecMaker.Core.Matchers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request
{
    public class PutStorageDirectorySpecs:BaseSpec
    {
        public void when_adding_storage_object()
        {


            var createContainer = new PutStorageDirectory("http://storageurl", "containername", "objname");
            var mock = new Mock<ICloudFilesRequest>();
            createContainer.Apply(mock.Object);


            should("append container and object name to storage url",
                   () => createContainer.CreateUri().ToString().Is("http://storageurl/containername/objname"));
            should("use PUT method", () =>
                                     mock.VerifySet(x => x.Method = "PUT")
                );
            should("have content type of application/directory", () =>
                   mock.VerifySet(x => x.ContentType = "application/directory")
                );
            should("set content with basic empty object", () =>
                  mock.Verify(x => x.SetContent(It.IsAny<MemoryStream>(), It.IsAny<Connection.ProgressCallback>()))
                );
        }

        public void when_creating_uri_and_storage_item_has_forward_slashes_at_the_beginning()
        {

            var item = new PutStorageDirectory("http://storeme", "itemcont", "/dir1/dir2");
            Uri url = item.CreateUri();
            should("remove all forward slashes", () => url.EndsWith("dir1/dir2"));
        }
    }
}