///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.domain.request;

namespace com.mosso.cloudfiles
{
    /// <summary>
    /// The interface dictating the required methods for all implementing classes
    /// </summary>
    public abstract class AbstractConnection
    {
        public abstract AccountInformation GetAccountInformation();
        public abstract string GetAccountInformationJson();
        public abstract XmlDocument GetAccountInformationXml();

        public abstract void CreateContainer(string containerName);

        public abstract void DeleteContainer(string continerName);

        public abstract List<string> GetContainers();

        public abstract List<string> GetContainerItemList(string containerName);
        public abstract List<string> GetContainerItemList(string containerName, Dictionary<GetItemListParameters, string> parameters);

        public abstract Container GetContainerInformation(string containerName);
        public abstract string GetContainerInformationJson(string containerName);
        public abstract XmlDocument GetContainerInformationXml(string containerName);


        public abstract void PutStorageItemAsync(string containerName, Stream storageStream, string remoteStorageItemName);
        public abstract void PutStorageItemAsync(string containerName, string localStorageItemName);

        public abstract void GetStorageItemAsync(string containerName, string storageItemName, string localItemName);

        public abstract void PutStorageItem(string containerName, string localFilePath, Dictionary<string, string> metadata);
        public abstract void PutStorageItem(string containerName, string localFilePath);
        public abstract void PutStorageItem(string containerName, Stream storageStream, string remoteStorageItemName);
        public abstract void PutStorageItem(string containerName, Stream storageStream, string remoteStorageItemName, Dictionary<string, string> metadata);

        public abstract void DeleteStorageItem(string containerName, string storageItemname);
        public abstract StorageItem GetStorageItem(string containerName, string storageItemName);
        public abstract void GetStorageItem(string containerName, string storageItemName, string localFileName);
        public abstract StorageItem GetStorageItem(string containerName, string storageItemName, Dictionary<RequestHeaderFields, string> requestHeaderFields);
        public abstract void GetStorageItem(string containerName, string storageItemName, string localFileName, Dictionary<RequestHeaderFields, string> requestHeaderFields);
        public abstract StorageItemInformation GetStorageItemInformation(string containerName, string storageItemName);
        public abstract void SetStorageItemMetaInformation(string containerName, string storageItemName, Dictionary<string, string> metadata);

        public abstract  List<string> GetPublicContainers();
        public abstract Uri MarkContainerAsPublic(string containerName);
        public abstract  Uri MarkContainerAsPublic(string containerName, int timeToLiveInSeconds);
        public abstract void MarkContainerAsPrivate(string containerName);
        public abstract void SetTTLOnPublicContainer(string containerName, int timeToLiveInSeconds);
        public abstract Container GetPublicContainerInformation(string containerName);

        public abstract void MakePath(string containerName, string path);

        public abstract IAccount Account { get; }

        /// <summary>
        /// The storage url used to interact with cloud files
        /// </summary>
        public string StorageUrl { get; protected set; }
        /// <summary>
        /// the public cdn url for the authenticated user
        /// </summary>
        protected string CdnManagementUrl { get; set; }

        /// <summary>
        /// the session based token used to ensure the user was authenticated
        /// </summary>
        public string AuthToken { get; protected set; }

        /// <summary>
        /// The user credentials used to authenticate against cloud files
        /// </summary>
        protected UserCredentials UserCredentials { get; set; }

        /// <summary>
        /// Adds logging for access to your public containers.
        /// </summary>
        /// <example>
        /// UserCredentials userCredentials = new UserCredentials("username", "api key");
        /// IConnection connection = new Connection(userCredentials);
        /// connection.SetLoggingOnPublicContainer("container name")
        /// </example>
        /// <param name="publiccontainer">must be an already existig public container</param>
        public abstract void SetLoggingOnPublicContainer(string publiccontainer, bool loggingenabled);

        }
    
        
}