///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.response;

namespace com.mosso.cloudfiles.domain
{
    public interface IResponseFactoryWithContentBody
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CloudFilesResponseWithContentBody Create(CloudFilesRequest request);

        GetStorageItemResponse CreateStorageItem(CloudFilesRequest request);
    }

    /// <summary>
    /// ResponseFactoryWithContentBody
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseFactoryWithContentBody : IResponseFactoryWithContentBody
    {
        private HttpWebResponse httpResponse;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CloudFilesResponseWithContentBody Create(CloudFilesRequest request)
        {
            HttpStatusCode statusCode;
            Stream responseStream;
            WebHeaderCollection headerCollection = GetHeaderCollection(request, out statusCode, out responseStream);

            var response = new CloudFilesResponseWithContentBody()
                             {
                                 Headers = headerCollection,
                                 Status = statusCode,
                                 ContentStream = responseStream
                             };

            return response;
        }

        private WebHeaderCollection GetHeaderCollection(CloudFilesRequest request, out HttpStatusCode statusCode, out Stream responseStream)
        {
            var httpWebRequest = request.GetRequest();
//            OutputRequestInformation(httpWebRequest);
            
            httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();

            var headerCollection = httpResponse.Headers;
            statusCode = httpResponse.StatusCode;
            responseStream = httpResponse.GetResponseStream();
            return headerCollection;
        }

//        private void OutputRequestInformation(HttpWebRequest request)
//        {
//            Console.WriteLine(request.Method +" "+ request.RequestUri);
//            foreach(var key in request.Headers.AllKeys)
//            {
//                Console.WriteLine(key + ": " + request.Headers[key]);
//            }
//        }
        public GetStorageItemResponse CreateStorageItem(CloudFilesRequest request)
        {
            HttpStatusCode statusCode;
            Stream responseStream;
            WebHeaderCollection headerCollection = GetHeaderCollection(request, out statusCode, out responseStream);

            var response = new GetStorageItemResponse()
            {
                Headers = headerCollection,
                Status = statusCode,
                ContentStream = responseStream
            };

            return response;
        }
    }
}