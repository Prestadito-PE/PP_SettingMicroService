using Microsoft.AspNetCore.Builder;

namespace Prestadito.Setting.Application.Manager.Endpoints
{
    public static class EndPoint
    {
        readonly static string basePath = "/api";
        public static WebApplication UseSettingEndpoint(this WebApplication app)
        {
            app.UseHealthEndpoints();
            app.UseParameterEndpoint(basePath);
            app.UseEndpointInterservices(basePath);
            return app;
        }
    }
}
