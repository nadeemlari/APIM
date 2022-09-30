using Azure;
using Azure.Data.Tables;

namespace StorageTable;

public class CustomerEntity : ITableEntity
{
    public string PartitionKey { get; set; } // tenant
    public string RowKey { get; set; } // customer id
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string Email { get; set; }
    public string ApimId { get; set; }
   public string Tenant { get; set; }
    public string BaseUrl { get; set; }
}