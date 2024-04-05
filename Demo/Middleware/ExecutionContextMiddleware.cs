using Demo.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Demo.Api.Middleware
{
    /// <summary>
    /// Manage the setting of the execution context.
    /// </summary>
    internal class ExecutionContextMiddleware
    {
        private readonly RequestDelegate _next;

        private const string UserIdHeader = "X-User-Id";
        private const string CorrelationIdHeader = "X-Correlation-Id";
        private const string RequestDateHeader = "Date";

        public ExecutionContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        internal class HttpExecutionContext : IExecutionContext
        {

            private readonly HttpRequest _httpRequest;

            public HttpExecutionContext(IHttpContextAccessor httpContextAccessor)
            {
                _httpRequest = httpContextAccessor.HttpContext!.Request;
                ReferenceDateTime = DefineReferenceDate(_httpRequest.Headers);
                UserID = DefineUserId(_httpRequest.Headers);
                CorrelationId = DefineCorrelationId(_httpRequest.Headers);
            }

            public DateTimeOffset ReferenceDateTime { get; }

            public Guid CorrelationId { get; }

            public Guid? UserID { get; }

            private DateTimeOffset DefineReferenceDate(IHeaderDictionary headers)
            {
                return DateTimeOffset.TryParse(headers[RequestDateHeader], out DateTimeOffset extractedDate) ? extractedDate : DateTimeOffset.Now;
            }

            private Guid? DefineUserId(IHeaderDictionary headers)
            {
                return Guid.TryParse(headers[UserIdHeader], out Guid userId) ? userId : null;
            }

            private Guid DefineCorrelationId(IHeaderDictionary headers)
            {
                return Guid.TryParse(headers[CorrelationIdHeader], out Guid userId) ? userId : Guid.NewGuid();
            }
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext, IExecutionContext appContext)
        {
            await _next(httpContext);
        }
    }
}
