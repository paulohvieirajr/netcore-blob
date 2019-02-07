using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Blob examples!");

            // BlobService.LoadToAzureFiles();
            BlobService.LoadToAzureBlobs();

            Console.ReadKey();
        }
    }
}
