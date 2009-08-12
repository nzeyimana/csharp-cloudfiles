///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using com.mosso.cloudfiles.domain.response.Interfaces;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles.domain.response
{
    /// <summary>
    /// Represents the response information from a CloudFiles request
    /// </summary>
    public class CloudFilesResponse : ICloudFilesResponse
    {
        private readonly HttpWebResponse _webResponse;
        private IList<string> _contentbody = new List<string>();
        public CloudFilesResponse(HttpWebResponse webResponse)
        {
            _webResponse = webResponse;
        }

        /// <summary>
        /// A property representing the HTTP Status code returned from cloudfiles
        /// </summary>
        public HttpStatusCode Status { get { return _webResponse.StatusCode; } }

        /// <summary>
        /// A collection of key-value pairs representing the headers returned from the create container request
        /// </summary>
        public WebHeaderCollection Headers { get { return _webResponse.Headers; } }

        public void Close()
        {
            _webResponse.Close();
        }

        /// <summary>
        /// dictionary of meta tags assigned to this storage item
        /// </summary>
        public Dictionary<string, string> Metadata
        {
            get
            {
                var tags = new Dictionary<string, string>();
                foreach (string s in _webResponse.Headers.Keys)
                {
                    if (s.IndexOf(Constants.META_DATA_HEADER) == -1) continue;
                    var metaKeyStart = s.LastIndexOf("-");
                    tags.Add(s.Substring(metaKeyStart + 1), _webResponse.Headers[s]);
                }
                return tags;
            }
        }

        public string Method
        {
            get { return _webResponse.Method; }
        }

        public HttpStatusCode StatusCode
        {
            get { return _webResponse.StatusCode; }
        }

        public string StatusDescription
        {
            get { return _webResponse.StatusDescription; }
        }

        public IList<string> ContentBody
        {
            get
            {
                return _contentbody;
            }
        }

        public string ContentType
        {
            get { return 
                _webResponse.ContentType; }
        }

        public string ETag
        {
            get { return _webResponse.Headers[Constants.ETAG]; }
            set { _webResponse.Headers[Constants.ETAG] = value; }
        }

        public long ContentLength
        {
            get { return _webResponse.ContentLength; }
        }

        public Stream GetResponseStream()
        {
            return  _webResponse.GetResponseStream();
        }

        public void Dispose()
        {
            _webResponse.Close();
        }
    }
}