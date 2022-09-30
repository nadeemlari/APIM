using Azure;
using Azure.Identity;
using Azure.Data.Tables;
using StorageTable;

var tableClient = new TableClient(new Uri("https://mnlaristrorage.table.core.windows.net"),
    "Customers", new DefaultAzureCredential());
const string partitionKey = "APIM";
try
{
    // Create the table.
    // await tableClient.CreateIfNotExistsAsync();
    // for (var i = 1; i < 100; i++)
    // {
    //     var entity = new CustomerEntity
    //     {
    //         RowKey = $"customerId{i}",
    //         PartitionKey = partitionKey,
    //         Email = $"mnlari{i}@hotmail.com",
    //         Tenant = "cloud",
    //         BaseUrl = $"https://cloud.iot.solenis.com",
    //         ApimId = $"apimId{i}"
    //     };
    //     await tableClient.AddEntityAsync(entity);
    // }


    // var queryResultsFilter =
    //     tableClient.Query<CustomerEntity>(filter: $"PhoneNumber eq '9971322993'");
    // foreach (var item in queryResultsFilter)
    // {
    // }
    var x = 0;
    while (x< 100)
    {
        x++;
        var y = tableClient.Query<CustomerEntity>(x => x.PartitionKey == partitionKey && x.RowKey == "customerId1", 1).FirstOrDefault();
        Console.WriteLine($"{x}");
    }
}
catch (Exception e)
{
    Console.WriteLine("Exception: {0}", e.Message);
}