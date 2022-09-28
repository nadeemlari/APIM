// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Azure.Storage.Blobs;

try
{
	Console.WriteLine("Reading Blob with manged identity");

	var stgConnection = "https://c8ydata.blob.core.windows.net/manual-processed-container";
	var blobContainerClient = new BlobContainerClient(new Uri(stgConnection), new DefaultAzureCredential());
	
	await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
	{
		Console.WriteLine("\t" + blobItem.Name);
		//var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
		//var response = await blobClient.DownloadAsync();
		
	}

	Console.WriteLine("Reading Blob with manged identity");

}
catch (Exception ex )
{

	Console.WriteLine(ex.Message);
}