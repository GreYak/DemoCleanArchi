using Demo.Api.Middleware;

namespace Demo.Api.Extensions
{
    internal static class MiddlewaresExtension
    {
        public static void UseCustomPipeline(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExecutionContextMiddleware>();
        }
    }
}
