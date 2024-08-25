namespace Forum.API.Infrastructure.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path = @"RequestResponse.txt";
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await Logger(context.Request);

            await _next(context);
        }

        private async Task Logger(HttpRequest request)
        {
            var log = $"\n{{\n Custom Middleware\nIP: {request.HttpContext.Connection.LocalIpAddress}\n" +
                $"Address: {request.Scheme}\n" +
                $"Path: {request.Path}\n" +
                $"Method: {request.Method}\n" +
                $"IsSecured: {request.IsHttps}\n" +
                $"Time: {DateTime.Now}\n}}";

            await File.AppendAllTextAsync(_path, log);
        }
    }
}
