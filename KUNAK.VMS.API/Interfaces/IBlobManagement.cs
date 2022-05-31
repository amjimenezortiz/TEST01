using Azure.Storage.Blobs.Models;

namespace KUNAK.VMS.API.Interfaces
{
    public interface IBlobManagement
    {
        Task CreateContainer(string RUC);
        Task DeleteContainer(string name);
        Task<BlobDownloadInfo> DownloadBlob(string containerName, string name);
        Task UploadBlob(string containerName, IFormFile file);
        Task DeleteBlob(string containerName, string name);
        string GetLink(string containerName, string name);
        Task ChangeAccessPolicyAsync(string name);
        bool GetContainer(string name);
        //Task EditBlob(string containerName, string oldName, string newName);
    }
}
