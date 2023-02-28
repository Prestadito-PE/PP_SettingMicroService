namespace Prestadito.Setting.Infrastructure.Data.Interface
{
    public interface ISecurityDBSettings
    {
        string ConnectionURI { get; set; }
        string DatabaseName { get; set; }
    }
}
