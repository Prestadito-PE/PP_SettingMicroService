using Microsoft.AspNetCore.Builder;

namespace Prestadito.Setting.Application.Manager.Endpoints
{
    public static class EndPoint
    {
        readonly static string basePath = "/api";
        public static WebApplication UseSettingEndpoint(this WebApplication app, string cors)
        {
            app.UseHealthEndpoints();
            app.UseParameterEndpoint(cors, basePath);
            return app;
        }
    }
}
