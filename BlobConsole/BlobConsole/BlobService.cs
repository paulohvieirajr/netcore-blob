using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace BlobConsole
{
    public class BlobService
    {
        const string StorageAccountName = "blobteste";
        const string StorageAccountKey = "L4D8xsUzqHk7qd/SQFuXhyWcBOI9O5ZOkUSVyp+XPSuiaP06E43aZfh0wLjOgGKr8XiYyZig==";

        public static void LoadToAzureFiles()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);

            // Create azure file share
            var share = storageAccount.CreateCloudFileClient().GetShareReference("my-files-share");
            share.CreateIfNotExistsAsync();

            //Crate file in root directory
            var rootDir = share.GetRootDirectoryReference();
            rootDir.GetFileReference("imagem.jpg").UploadFromFileAsync(@"c:\temp\imagem.jpg");

            // Crate folders with files
            var folder1 = rootDir.GetDirectoryReference("folder1");
            folder1.CreateIfNotExistsAsync();

            folder1.GetFileReference("file1.txt").UploadTextAsync("File1 context");
            folder1.GetFileReference("file2.txt").UploadTextAsync("File2 context");

            var folder2 = rootDir.GetDirectoryReference("folder2");
            folder2.CreateIfNotExistsAsync();

            folder2.GetFileReference("file1.txt").UploadTextAsync("File1 context");
            folder2.GetFileReference("file2.txt").UploadTextAsync("File2 context");

            rootDir.GetFileReference("rootfile.txt").DownloadToFileAsync(@"c:\temp\imagem.jpg", System.IO.FileMode.Create);
        }

        const string FolderPath = @"C:\temp\blob";

        public static void LoadToAzureBlobs()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("files");
            container.CreateIfNotExistsAsync();
            container.SetPermissionsAsync(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var blob = container.GetBlockBlobReference("imagem.jpg");
            blob.UploadFromFileAsync(@"C:\temp\imagem.jpg");

            // Upload de uma pasta de todos os arquivos
            //foreach (var filePath in Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories))
            //{
            //    var blob = container.GetBlockBlobReference(filePath);
            //    blob.UploadFromFileAsync(filePath);

            //    Console.WriteLine("Uploaded {0}", filePath);

            //}

            Console.WriteLine("Completed");
        }
    }
}
