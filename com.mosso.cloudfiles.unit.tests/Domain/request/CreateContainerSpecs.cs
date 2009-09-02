using System;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using SpecMaker.Core;
using SpecMaker.Core.Matchers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.CreateContainerSpecs
{
    public class CreateContainerSpecs: BaseSpec
    {
        public void when_creating_a_container_and_storae_url_is_null()
        {
            should("throw ArgumentNullException",()=> new CreateContainer(null, "containername"), typeof(ArgumentNullException));
        }
        public void when_creating_a_container_and_storage_url_is_emptry_string()
        {
            should("throw ArugmentNullException ",()=> new CreateContainer("", "containername"), typeof(ArgumentNullException));
        }
        public void when_creating_a_container_and_container_name_is_null()
        {
            should("throw ArgumentNullException", ()=> new CreateContainer("http://storageuri", null), typeof(ArgumentNullException) );
        }
        public void  when_creating_a_container_and_container_name_is_empty_string()
        {
            should("throw ArgumentNullException", 
                ()=> new CreateContainer("http://storageuri", ""), typeof(ArgumentNullException) );
        }
        public void when_creating_a_container()
        {
            var createContainer = new CreateContainer("http://storageurl", "containername");   
            var mock = new Mock<ICloudFilesRequest>();
            createContainer.Apply(mock.Object);
           
            
            should("append container name to storage url",()=>createContainer.CreateUri().ToString().Is("http://storageurl/containername"));
            should("use PUT method", () => 
                mock.VerifySet(x => x.Method = "PUT")
                );
        }
    }

}