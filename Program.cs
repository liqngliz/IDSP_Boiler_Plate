using IDSP_Boiler_Plate;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var auth = new AuthenticationController();
app.MapGet("/", (HttpContext context) => auth.Process(context));


app.Run();
