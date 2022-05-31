using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KUNAK.VMS.API.Interfaces;

namespace KUNAK.VMS.API.Methods
{
    public class BlobManagement : IBlobManagement
    {
        private readonly BlobServiceClient _client;
        public BlobManagement(BlobServiceClient client)
        {
            _client = client;
        }

        public async Task CreateContainer(string name)
        {
            await _client.CreateBlobContainerAsync(name);

        }
        public async Task ChangeAccessPolicyAsync(string name)
        {
            BlobContainerClient container = _client.GetBlobContainerClient(name);
            await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
        }
        public async Task DeleteContainer(string name)
        {
            await _client.DeleteBlobContainerAsync(name);
        }
        public async Task<BlobDownloadInfo> DownloadBlob(string containerName, string name)
        {
            BlobContainerClient container = _client.GetBlobContainerClient(containerName);
            await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            BlobClient blob = container.GetBlobClient(name);
            return await blob.DownloadAsync();
        }
        public async Task UploadBlob(string containerName, IFormFile file)
        {
            BlobContainerClient container = _client.GetBlobContainerClient(containerName);
            await container.DeleteBlobIfExistsAsync(file.FileName);
            await container.UploadBlobAsync(file.FileName, file.OpenReadStream());
        }
        public async Task DeleteBlob(string containerName, string name)
        {
            BlobContainerClient container = _client.GetBlobContainerClient(containerName);
            BlobClient blob = container.GetBlobClient(name);
            await blob.DeleteAsync();

        }
        public string GetLink(string containerName, string name)
        {
            BlobContainerClient container = _client.GetBlobContainerClient(containerName);
            BlobClient blob = container.GetBlobClient(name);
            return blob.Exists() ? blob.Uri.ToString() : "";
        }

        public bool GetContainer(string name)
        {
            BlobContainerClient container = _client.GetBlobContainerClient(name);
            return container.Exists() ? true : false;
        }
        //public async Task EditBlob(string containerName, string oldName,string newName)
        //{

        //    //BlobContainerClient container = _client.GetBlobContainerClient(containerName);
        //    //CloudBlob blobCopy = container.(newName);

        //    //if (!await blobCopy.ExistsAsync())
        //    //{
        //    //    CloudBlob blob = container.GetBlobReference(oldName);

        //    //    if (await blob.ExistsAsync())
        //    //    {
        //    //        await blobCopy.StartCopyAsync(blob);
        //    //        await blob.DeleteIfExistsAsync();
        //    //    }
        //    //}
        //    //BlobClient blob = container.GetBlobClient(name);
        //    //await blob.OpenReadAsync();
        //}
    }
}
