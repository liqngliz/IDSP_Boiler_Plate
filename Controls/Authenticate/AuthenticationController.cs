using Newtonsoft.Json.Linq;

namespace IDSP_Boiler_Plate;

public class AuthenticationController : IController<JObject, JObject>
{   
    public JObject Process(JObject Input)
    {
        return Input;
    }
}
