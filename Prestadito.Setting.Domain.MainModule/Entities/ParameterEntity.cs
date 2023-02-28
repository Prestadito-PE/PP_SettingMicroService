using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Prestadito.Setting.Domain.MainModule.Core;

namespace Prestadito.Setting.Domain.MainModule.Entities
{
    public class ParameterEntity: BaseAuditEntity
    {
        [BsonElement("strCode")]
        public string StrCode { get; set; }
        [BsonElement("strParentCode")]
        public string? StrParentCode { get; set; }
        [BsonElement("strName")]
        public string StrName { get; set; }
        [BsonElement("strValue")]
        public string StrValue { get; set; }
        [BsonElement("strType")]
        public string? StrType { get; set; }
        [BsonElement("strDescription")]
        public string StrDescription { get; set; }
    }
}
