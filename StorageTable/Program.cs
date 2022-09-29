using Azure.Identity;
using Azure.Data.Tables;
using Azure;
using System.Collections.Concurrent;

TableClient tableClient = new TableClient(new Uri("https://mnlaristorage.table.core.windows.net"),
                                                "Customers",
                                                new DefaultAzureCredential());
try
{
    // Create the table.
   // await tableClient.CreateIfNotExistsAsync();
    //var t = new CustomerEntity { RowKey = "mnlari@hotmail.com" , PartitionKey="par1" ,PhoneNumber="9971322993"};

    //var r = await tableClient.AddEntityAsync<CustomerEntity>(t);

    Pageable<CustomerEntity> queryResultsFilter = tableClient.Query<CustomerEntity>(filter: $"PhoneNumber eq '9971322993'");
    foreach (var item in queryResultsFilter)
    {

    }

}
catch (Exception e)
{
    Console.WriteLine("Exception: {0}", e.Message);
}

public class CustomerEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    //public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}