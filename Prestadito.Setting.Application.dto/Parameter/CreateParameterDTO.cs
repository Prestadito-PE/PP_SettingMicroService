namespace Prestadito.Setting.Application.Dto.Parameter
{
    public class CreateParameterDTO
    {
        public string StrCode { get; set; }
        public string StrParentCode { get; set; }
        public string StrName { get; set; }
        public string StrValue { get; set; }
        public string StrType { get; set; }
        public string StrDescription { get; set; }
        public string strCreateUser { get; set; }
        public bool blnActive { get; set; }
    }
}
