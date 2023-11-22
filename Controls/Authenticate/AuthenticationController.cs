using System.Text;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace IDSP_Boiler_Plate;

public class AuthenticationController : IController<string, HttpContext>
{   
    public string Process(HttpContext Input)
    {   
        var headers = Input.Request.Headers;
        StringValues principalName;
        StringValues principalClaims;
        headers.TryGetValue("X-MS-CLIENT-PRINCIPAL-ID", out principalName);
        headers.TryGetValue("X-MS-CLIENT-PRINCIPAL", out principalClaims);
        byte[] data = Convert.FromBase64String(principalClaims.ToString());
        string decodedString = System.Text.Encoding.UTF8.GetString(data);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Hello " + principalName.ToString());
        stringBuilder.AppendLine("Your OAuth Claims Tokens: ");
        stringBuilder.AppendLine(decodedString);
        return  stringBuilder.ToString();
    }
}
