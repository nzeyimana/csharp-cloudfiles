using System;
using System.Collections.Generic;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.unit.tests.CustomMatchers;
using Moq;
using SpecMaker.Core;


namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetContainerItemListSpecs
{
    public class GentContainerItemListSpec : BaseSpec
    {
        public void when_getting_a_list_of_items_in_a_container_and_storage_url_is_null()
        {
            should("throw ArgumentNullException", () => new GetContainerItemList(null, "containername"), typeof(ArgumentNullException));
        }
        public void when_getting_a_list_of_items_in_a_container_and_storage_url_is_empty_string()
        {
            should("throw ArgumentNullException", () => new GetContainerItemList(null, "containername"), typeof(ArgumentNullException));
        }
        public void when_getting_a_list_of_items_in_a_container_and_container_name_is_null()
        {
            should("throw ArgumentNullException", () => new GetContainerItemList("http://storageurl", null), typeof(ArgumentNullException));
        }
        public void when_getting_a_list_of_items_in_a_container_and_container_name_is_empty_string()
        {
            should("throw ArgumentNullException", () => new GetContainerItemList("http://storageurl", ""), typeof(ArgumentNullException));
        }
        public void when_getting_a_list_of_items_in_a_container_with_query_parameters()
        {
            var getContainerItemList = new GetContainerItemList("http://storageurl", "containername");
            var uri = getContainerItemList.CreateUri();
            var _mockrequest = new Mock<ICloudFilesRequest>();
            getContainerItemList.Apply(_mockrequest.Object);
            should("url should have storage url at beginning ", () => uri.StartsWith("http://storageurl"));
            should("url should have container name at the end ", () => uri.EndsWith("containername"));
            should("use HTTP GET method", () => _mockrequest.VerifySet(x => x.Method = "GET"));
        }
        public void when_getting_a_list_of_items_in_a_container_with_limit_query_parameter()
        {
            var parameters = new Dictionary<GetItemListParameters, string> { { GetItemListParameters.Limit, "2" } };
            Uri uri;
            Mock<ICloudFilesRequest> _mockrequest = GetMockrequest(parameters, out uri);

            should("url should have storage url at beginning ", () => uri.StartsWith("http://storageurl"));
            should("url should have container name followed by query string and limit at the end ",
                () => uri.EndsWith("containername?limit=2"));
            should("use HTTP GET method", () => _mockrequest.VerifySet(x => x.Method = "GET"));
        }
        public void when_getting_a_list_of_items_in_a_container_with_marker_query_parameter()
        {
            var parameters = new Dictionary<GetItemListParameters, string> { { GetItemListParameters.Marker, "abc" } };
            Uri uri;
            Mock<ICloudFilesRequest> _mockrequest = GetMockrequest(parameters, out uri);

            should("have url with storage url at beginning ", () => uri.StartsWith("http://storageurl"));
            should("have url with container name followed by query string with marker at the end ",
                () => uri.EndsWith("containername?marker=abc"));
            should("use HTTP GET method", () => _mockrequest.VerifySet(x => x.Method = "GET"));
        }
        public void when_getting_a_list_of_items_in_a_container_with_prefix_query_parameter()
        {
              var parameters = new Dictionary<GetItemListParameters, string> { { GetItemListParameters.Prefix, "a" } };
              Uri uri;
              Mock<ICloudFilesRequest> _mockrequest = GetMockrequest(parameters, out uri);

              should("have url with storage url at beginning ", () => uri.StartsWith("http://storageurl"));
              should("have url with container name followed by query string with prefix at the end ",
                  () => uri.EndsWith("containername?prefix=a"));
            should("use HTTP GET method", () => _mockrequest.VerifySet(x => x.Method = "GET"));
        }
        public void when_getting_a_list_of_items_in_a_container_with_path_query_parameter()
        {
            var parameters = new Dictionary<GetItemListParameters, string> { { GetItemListParameters.Path, "dir1/subdir2/" } };
      
            Uri uri;
            Mock<ICloudFilesRequest> _mockrequest = GetMockrequest(parameters, out uri);

            should("have url with storage url at beginning ", () => uri.StartsWith("http://storageurl"));
            should("have url with container name followed by query string with path at the end ",
                () => uri.EndsWith("containername?path=dir1/subdir2/"));
            should("use HTTP GET method", () => _mockrequest.VerifySet(x => x.Method = "GET"));
        }
        public void when_getting_a_list_of_items_in_a_container_with_more_than_one_query_parameter()
        {
            var parameters = new Dictionary<GetItemListParameters, string>
                                 {
                                     { GetItemListParameters.Limit, "2" },
                                     { GetItemListParameters.Marker, "abc" },
                                     { GetItemListParameters.Prefix, "a" },
                                     { GetItemListParameters.Path, "dir1/subdir2/" }
                                 };
            Uri uri;
            Mock<ICloudFilesRequest> _mockrequest = GetMockrequest(parameters, out uri);

            should("have url with storage url at beginning ", () => uri.StartsWith("http://storageurl"));
            should("have url with container name followed by query strings ",
                () => uri.EndsWith("containername?limit=2&marker=abc&prefix=a&path=dir1/subdir2/"));
            should("use HTTP GET method", () => _mockrequest.VerifySet(x => x.Method = "GET"));
            
        }
        #region privates
        private Mock<ICloudFilesRequest> GetMockrequest(Dictionary<GetItemListParameters, string> parameters, out Uri uri)
        {
            var getContainerItemList = new GetContainerItemList("http://storageurl", "containername", parameters);
            var _mockrequest = new Mock<ICloudFilesRequest>();
            getContainerItemList.Apply(_mockrequest.Object);
            uri = getContainerItemList.CreateUri();
            return _mockrequest;
        }
        #endregion
    }

    
}