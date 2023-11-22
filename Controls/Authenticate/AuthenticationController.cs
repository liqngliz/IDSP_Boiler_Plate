using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace IDSP_Boiler_Plate;

public class AuthenticationController : IController<Task<string>, HttpContext>
{   
    public async Task<string> Process(HttpContext Input)
    {   
        var headers = Input.Request.Headers;
        StringValues principalName;
        StringValues principalClaims;
        StringValues idToken;
        StringValues accessToken;
        StringValues expiresOn;
        headers.TryGetValue("X-MS-CLIENT-PRINCIPAL-NAME", out principalName);
        headers.TryGetValue("X-MS-CLIENT-PRINCIPAL", out principalClaims);
        headers.TryGetValue("X-MS-TOKEN-AAD-ID-TOKEN", out idToken);
        headers.TryGetValue("X-MS-TOKEN-AAD-ACCESS-TOKEN", out accessToken);
        headers.TryGetValue("X-MS-TOKEN-AAD-EXPIRES-ON", out expiresOn);
        byte[] data = Convert.FromBase64String(principalClaims.ToString());
        string decodedString = System.Text.Encoding.UTF8.GetString(data);

        //get info from BE app
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
        var result = await client.GetAsync("https://authbe20.azurewebsites.net");
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Hello " + principalName.ToString());
        stringBuilder.AppendLine("Your OAuth Claims Tokens: ");
        stringBuilder.AppendLine(decodedString);
        stringBuilder.AppendLine("Id Token: " + idToken.ToString());
        stringBuilder.AppendLine("Access Token: " + accessToken.ToString());
        stringBuilder.AppendLine("Expires: " + expiresOn.ToString());
        stringBuilder.AppendLine("request to app 2: " + result.StatusCode.ToString());
        stringBuilder.AppendLine("response from app 2: " + result.Content.ReadAsStringAsync().Result);

        client.Dispose();
        return  stringBuilder.ToString();
    }
}
