using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

public class AzureBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobService(string connectionString)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream);
        return blobClient.Uri.ToString();
    }
}

