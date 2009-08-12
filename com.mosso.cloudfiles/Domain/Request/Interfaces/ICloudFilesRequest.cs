using System;
using System.IO;
using System.Net;
using com.mosso.cloudfiles.domain.response.Interfaces;

namespace com.mosso.cloudfiles.domain.request.Interfaces
{
    public interface ICloudFilesRequest
    {
        ICloudFilesResponse GetResponse();
        Uri RequestUri { get; }
        string Method { get; set; }
        WebHeaderCollection Headers { get; }
       
        long ContentLength { get; set; }
        int RangeTo { get; set; }
        int RangeFrom { get; set; }
        string ContentType { get; set; }
        DateTime IfModifiedSince { get; set; }
        string ETag { get; set; }
        Stream GetRequestStream();
    }
}