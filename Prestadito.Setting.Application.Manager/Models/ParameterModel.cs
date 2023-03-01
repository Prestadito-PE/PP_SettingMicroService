namespace Prestadito.Setting.Application.Manager.Models
{
    public class ParameterModel
    {
        public string? Id { get; set; }
        public string StrCode { get; set; }
        public string? StrParentCode { get; set; }
        public string StrName { get; set; }
        public string StrValue { get; set; }
        public string? StrType { get; set; }
        public string StrDescription { get; set; }
        public bool BlnActive { get; set; }
        public string StrCreateUser { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public string? StrUpdateUSer { get; set; }
        public DateTime? DteUpdatedAt { get; set; }
    }
}
