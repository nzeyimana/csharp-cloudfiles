///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Generic;
using System.Text;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.exceptions;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles.domain.request
{
    /// <summary>
    /// GetContainerInformation
    /// </summary>
    public class GetContainerInformation : IAddToWebRequest
    {
        private readonly string _storageUrl;
        private readonly string _containerName;

        /// <summary>
        /// GetContainerInformation constructor
        /// </summary>
        /// <param name="storageUrl">the customer unique url to interact with cloudfiles</param>
        /// <param name="containerName">the name of the container where the storage item is located</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the reference parameters are null</exception>
        /// <exception cref="ContainerNameException">Thrown when the container name is invalid</exception>
        public GetContainerInformation(string storageUrl, string containerName)
        {

            if (string.IsNullOrEmpty(storageUrl)
                || string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException();

            if (!ContainerNameValidator.Validate(containerName)) throw new ContainerNameException();

            _storageUrl = storageUrl;
            _containerName = containerName;


        }

        public Uri CreateUri()
        {
            return new Uri(_storageUrl + "/" + _containerName.Encode());
        }

        public void Apply(ICloudFilesRequest request)
        {
            request.Method = "HEAD";
        }
    }

    public class GetContainerInformationSerialized : IAddToWebRequest
    {
        private readonly string _storageUrl;
        private readonly string _containerName;
        private readonly Format _format;
        private readonly StringBuilder _stringBuilder;

        /// <summary>
        /// GetContainerInformationSerialized constructor
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null</exception>
        public GetContainerInformationSerialized(string storageUrl, string containerName, Format format)
        {
            if (string.IsNullOrEmpty(storageUrl)
                || string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException();

            if (!ContainerNameValidator.Validate(containerName)) throw new ContainerNameException();
            _storageUrl = storageUrl;
            _containerName = containerName;
            _format = format;

            _stringBuilder = new StringBuilder();

        }

        /// <summary>
        /// GetContainerInformationSerialized constructor
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null</exception>
        public GetContainerInformationSerialized(string storageUrl, string containerName, Format format, Dictionary<GetItemListParameters, string> requestParameters)
        {
            if (string.IsNullOrEmpty(storageUrl) || string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException();
            }

            if (!ContainerNameValidator.Validate(containerName))
            {
                throw new ContainerNameException();
            }
            _storageUrl = storageUrl;
            _containerName = containerName;
            _format = format;

            _stringBuilder = new StringBuilder();

            if (requestParameters == null || requestParameters.Count <= 0)
            {
                return;
            }

            foreach (GetItemListParameters param in requestParameters.Keys)
            {
                var paramName = param.ToString().ToLower();
                if (param == GetItemListParameters.Limit)
                    int.Parse(requestParameters[param]);

                _stringBuilder.Append("&" + paramName + "=" + requestParameters[param].Encode());
            }
        }

        public Uri CreateUri()
        {
            return new Uri(_storageUrl + "/" + _containerName.Encode() + "?format=" + EnumHelper.GetDescription(_format) + _stringBuilder);
        }

        public void Apply(ICloudFilesRequest request)
        {
            request.Method = "GET";
        }
    }
}