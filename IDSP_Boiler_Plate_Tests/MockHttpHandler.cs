
namespace IDSP_Boiler_Plate_Tests;

public class MockHttpHandler : HttpMessageHandler
{
    readonly Queue<HttpResponseMessage> _responseMsg = new Queue<HttpResponseMessage>();

    internal void setResponse (HttpResponseMessage httpResponse) 
    {
        _responseMsg.Enqueue(httpResponse);
    }

    
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responseMsg.Dequeue());
    }

}
