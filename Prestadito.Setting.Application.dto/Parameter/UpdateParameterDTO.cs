namespace Prestadito.Setting.Application.Dto.Parameter
{
    public class UpdateParameterDTO
    {
        public string Id { get; set; }
        public string StrCode { get; set; }
        public string StrName { get; set; }
        public string StrValue { get; set; }
        public string StrType { get; set; }
        public string StrDescription { get; set; }
        public string StrUpdateUser { get; set; }
        public DateTime dteUpdatedAt { get; set; }
    }
}
