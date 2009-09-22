using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using SpecMaker.Core.Matchers;
using SpecMaker.Core;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.DeleteStorageItemSpecs
{
    public class DeleteStorageItemSpecs: BaseSpec
    {
        public void when_deleting_a_storage_item_and_storage_url_is_null()
        {
            should("throw ArgumentNullException",()=>new DeleteStorageItem(null, "containername", "storageitemname"), typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_storage_url_is_emptry_string()
        {
            
              should("throw ArgumentNullException",()=>new DeleteStorageItem("", "containername", "storageitemname"), typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_storage_item_name_is_null()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageItem("http://storageurl", "containername", null),typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_storage_item_name_is_emptry_string()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageItem("http://storageurl", "containername", ""),typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_container_name_is_null()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageItem("http://storageurl", null, "storageitemname"),typeof(ArgumentNullException));
        }

        public void when_deleting_a_storage_item_and_container_name_is_emptry_string()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageItem("http://storageurl", "", "storageitemname"),typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item()
        {
            var deleteStorageItem = new DeleteStorageItem("http://storageurl", "containername", "storageitemname");
            var _mockrequest = new Mock<ICloudFilesRequest>();
             _mockrequest.SetupGet(x => x.Headers).Returns(new WebHeaderCollection());
            deleteStorageItem.Apply(_mockrequest.Object);
            
            should("start with storageurl, have container name next, and then end with the item being deleted",
                ()=>deleteStorageItem.CreateUri().Is("http://storageurl/containername/storageitemname"));
            should("use HTTP DELETE method",()=> _mockrequest.VerifySet(x => x.Method = "DELETE"));
        }
  }     
    
}