///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Generic;
using System.Net;
using com.mosso.cloudfiles.domain.response.Interfaces;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles.domain
{
    public class StorageItemInformation
    {
        private readonly string objectName;
        private readonly DateTime lastModified;
        private readonly long sizeInBytes;
        private readonly WebHeaderCollection headers;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="objectName">Storage Item Name</param>
        /// <param name="response">CloudFiles response</param>
        public StorageItemInformation(string objectName, ICloudFilesResponse response)
        {
            this.headers = response.Headers;
            this.objectName = objectName;
            this.lastModified = response.LastModified;
            this.sizeInBytes = response.ContentLength;
        }

        /// <summary>
        /// The name in the container
        /// </summary>
        public string Name
        {
            get { return objectName; }
        }

        /// <summary>
        /// entity tag used to determine if any content changed in transfer - http://en.wikipedia.org/wiki/HTTP_ETag
        /// </summary>
        public string ETag
        {
            get { return headers[Constants.ETAG]; }
        }

        /// <summary>
        /// http content type of the storage item
        /// </summary>
        public string ContentType
        {
            get { return headers[Constants.CONTENT_TYPE_HEADER]; }
        }

        /// <summary>
        /// http content length of the storage item
        /// </summary>
        public string ContentLength
        {
            get { return headers[Constants.CONTENT_LENGTH_HEADER]; }
        }

        /// <summary>
        /// Size in bytes
        /// </summary>
        public long SizeInBytes
        {
            get { return sizeInBytes; }
        }

        /// <summary>
        /// Last modified
        /// </summary>
        public DateTime LastModified
        {
            get { return lastModified; }
        }

        /// <summary>
        /// dictionary of meta tags assigned to this storage item
        /// </summary>
        public Dictionary<string, string> Metadata
        {
            get
            {
                Dictionary<string, string> tags = new Dictionary<string, string>();
                foreach (string s in headers.Keys)
                {
                    if (s.IndexOf(Constants.META_DATA_HEADER) != -1)
                    {
                        int metaKeyStart = s.LastIndexOf("-");
                        tags.Add(s.Substring(metaKeyStart + 1), headers[s]);
                    }
                }
                return tags;
            }
        }
    }
}