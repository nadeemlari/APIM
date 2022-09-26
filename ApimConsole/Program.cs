//using Microsoft.Azure.Management.ApiManagement;

using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

var apiVersion = "2014-02-14-preview";

// service name and base url - https://aka.ms/smapi#BaseURL
var serviceName = "mnl-apim";
var baseUrl = string.Format("https://{0}.management.azure-api.net", serviceName);

var id = "integration";
// key - either the primary or secondary key from that same tab.
var key = "5KUaJXV1Ecf61jrO0fFGqooJ23z0nfWbqRWGc6VGmqvNamRv+IINLhX2dFM6nadlHAoQi9ZR2oaO3FeO+tFDjQ==";
// expiry - the expiration date and time of the generated access token. In this example
//          the expiry is one day from the time the sample is run.
var expiry = DateTime.UtcNow.AddDays(1);

var sharedAccessSignature = CreateSharedAccessToken(id, key, expiry);

var prd = await GetProductsAsync(baseUrl, apiVersion, sharedAccessSignature);

var apis = await GetAPIsAsync(baseUrl, apiVersion, sharedAccessSignature);

static string CreateSharedAccessToken(string id, string key, DateTime expiry)
{
    using var encoder = new HMACSHA512(Encoding.UTF8.GetBytes(key));
    var dataToSign = id + "\n" + expiry.ToString("O", CultureInfo.InvariantCulture);
    var x = string.Format("{0}\n{1}", id, expiry.ToString("O", CultureInfo.InvariantCulture));
    var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
    var signature = Convert.ToBase64String(hash);
    var encodedToken = $"uid={id}&ex={expiry:o}&sn={signature}";
    return encodedToken;
}
static async Task<string> GetProductsAsync(string baseUrl, string apiVersion,string sharedAccessSignature)
{
    // Call the GET /products operation
    // https://msdn.microsoft.com/en-us/library/azure/dn776336.aspx#ListProducts
    string requestUrl = string.Concat(baseUrl, "/products", "?api-version=", apiVersion);

    using var httpClient = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

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

    return responseBody;
}

static async Task<string> GetAPIsAsync(string baseUrl, string apiVersion, string sharedAccessSignature)
{
    // Call the GET /apis operation
    // https://msdn.microsoft.com/en-us/library/azure/dn781423.aspx#ListAPIs
    string requestUrl = string.Concat(baseUrl, "/apis", "?api-version=", apiVersion);

    using (HttpClient httpClient = new HttpClient())
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        // Default media type for this operation is application/json, no need to
        // set the accept header.

        // Set the SharedAccessSignature header
        request.Headers.Authorization =
            new AuthenticationHeaderValue("SharedAccessSignature", sharedAccessSignature);

        // Make the request
        HttpResponseMessage response = await httpClient.SendAsync(request);

        // Throw if there is an error
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;
    }
}

Console.WriteLine("");