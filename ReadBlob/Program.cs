// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

try
{
	Console.WriteLine("Reading Blob with manged identity");

	string stgConnection = "https://c8ydata.blob.core.windows.net/manual-processed-container";
	BlobContainerClient blobContainerClient = new BlobContainerClient(new Uri(stgConnection), new DefaultAzureCredential());
	//var x = blobContainerClient.GetBlobsAsync();
	var c = new AzureCliCredential();

    await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
	{
		Console.WriteLine("\t" + blobItem.Name);
	}

	Console.WriteLine("Reading Blob with manged identity");

}
catch (Exception ex )
{

	Console.WriteLine(ex.Message);
}