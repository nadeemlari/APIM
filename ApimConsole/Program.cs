//References  https://github.com/Azure/api-management-samples/tree/master/restApiDemo/APIMgtRESTAPIDemo -- obsolete now
//https://learn.microsoft.com/en-us/rest/api/apimanagement/

using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

const string apiVersion = "2021-08-01";

// service name and base url - https://aka.ms/smapi#BaseURL
const string serviceName = "mnl-apim";
const string baseUrl = $"https://{serviceName}.management.azure-api.net";

const string id = "integration";
// key - either the primary or secondary key from that same tab.
const string key = "5KUaJXV1Ecf61jrO0fFGqooJ23z0nfWbqRWGc6VGmqvNamRv+IINLhX2dFM6nadlHAoQi9ZR2oaO3FeO+tFDjQ==";
// expiry - the expiration date and time of the generated access token. In this example
//          the expiry is one day from the time the sample is run.
var expiry = DateTime.UtcNow.AddDays(1);

var sharedAccessSignature = CreateSharedAccessToken(id, key, expiry);

await GetProductsAsync(baseUrl, apiVersion, sharedAccessSignature);

await GetApIsAsync(baseUrl, apiVersion, sharedAccessSignature);

static string CreateSharedAccessToken(string id, string key, DateTime expiry)
{
    using var encoder = new HMACSHA512(Encoding.UTF8.GetBytes(key));
    var dataToSign = id + "\n" + expiry.ToString("O", CultureInfo.InvariantCulture);
    var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
    var signature = Convert.ToBase64String(hash);
    var encodedToken = $"uid={id}&ex={expiry:o}&sn={signature}";
    return encodedToken;
}
static async Task GetProductsAsync(string baseUrl, string apiVersion, string sharedAccessSignature)
{
    using var httpClient = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/subscriptions/184534d4-185d-468e-9146-00f3c2e2ba14/resourceGroups/My_RG/providers/Microsoft.ApiManagement/service/mnl-apim/products?api-version={apiVersion}");

    // Default media type for this operation is application/json, no need to
    // set the accept header.
    // Set the SharedAccessSignature header
    request.Headers.Authorization =
        new AuthenticationHeaderValue("SharedAccessSignature", sharedAccessSignature);

    // Make the request
    var response = await httpClient.SendAsync(request);

    // Throw if there is an error
    response.EnsureSuccessStatusCode();

    var responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);

    
}

static async Task GetApIsAsync(string baseUrl, string apiVersion, string sharedAccessSignature)
{
    using var httpClient = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/subscriptions/184534d4-185d-468e-9146-00f3c2e2ba14/resourceGroups/My_RG/providers/Microsoft.ApiManagement/service/mnl-apim/apis?api-version={apiVersion}");

    // Default media type for this operation is application/json, no need to
    // set the accept header.

    // Set the SharedAccessSignature header
    request.Headers.Authorization =
        new AuthenticationHeaderValue("SharedAccessSignature", sharedAccessSignature);

    // Make the request
    var response = await httpClient.SendAsync(request);

    // Throw if there is an error
    response.EnsureSuccessStatusCode();

    var responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
    
}

Console.WriteLine("done!!");