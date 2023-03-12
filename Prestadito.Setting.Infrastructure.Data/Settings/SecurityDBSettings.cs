using Prestadito.Setting.Infrastructure.Data.Interface;

namespace Prestadito.Setting.Infrastructure.Data.Settings
{
    public class SecurityDBSettings: ISecurityDBSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
    }
}
