///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Specialized;
using System.Web;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.exceptions;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles.domain.request
{
    /// <summary>
    /// DeleteStorageItem
    /// </summary>
    public class DeleteStorageItem : IAddToWebRequest
    {
        private readonly string _storageUrl;
        
        private readonly string _containerName;
        private readonly string _storageItemName;

        /// <summary>
        /// DeleteStorageItem constructor
        /// </summary>
        /// <param name="storageUrl">the customer unique url to interact with cloudfiles</param>
        /// <param name="containerName">the name of the container where the storage item is located</param>
        /// <param name="storageItemName">the name of the storage item to add meta information too</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the reference parameters are null</exception>
        /// <exception cref="ContainerNameException">Thrown when the container name is invalid</exception>
        /// <exception cref="StorageItemNameException">Thrown when the object name is invalid</exception>
        public DeleteStorageItem(string storageUrl,  string containerName, string storageItemName)
        {
            if (string.IsNullOrEmpty(_storageUrl)
            
            || string.IsNullOrEmpty(_containerName)
            || string.IsNullOrEmpty(_storageItemName))
                throw new ArgumentNullException();

            if (!ContainerNameValidator.Validate(_containerName)) throw new ContainerNameException();
            if (!ObjectNameValidator.Validate(_storageItemName)) throw new StorageItemNameException();

            _storageUrl = storageUrl;
         
            _containerName = containerName;
            _storageItemName = storageItemName;
        }

        public Uri CreateUri()
        {
            return new Uri(_storageUrl + "/" + _containerName.Encode() + "/" + _storageItemName.StripSlashPrefix().Encode());
        }

        public void Apply(ICloudFilesRequest request)
        {
            request.Method = "DELETE";

        }
    }
}