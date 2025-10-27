namespace myRideApp.Extensions.Builder;

public class TokenForwardingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpClientFactory _httpClientFactory;

    public TokenForwardingMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
    {
        _next = next;
        _httpClientFactory = httpClientFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(token))
        {
            var client = _httpClientFactory.CreateClient("ForwardingClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            context.Items["ForwardingClient"] = client;
        }

        await _next(context);
    }
}

public static class HttpAppBuilderExtensions
{
    public static IApplicationBuilder UseHttpToken(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<TokenForwardingMiddleware>();
    }
}
