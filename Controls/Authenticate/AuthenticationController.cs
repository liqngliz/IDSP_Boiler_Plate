using Newtonsoft.Json.Linq;

namespace IDSP_Boiler_Plate;

public class AuthenticationController : IController<string, HttpContext>
{   
    public string Process(HttpContext Input)
    {   
        var headers = Input.Request.Headers;

        return JsonConvert.SerializeObject(headers);
    }
}
