using System.ComponentModel.DataAnnotations;

namespace ProgramStudio.DeployCheckToolkits.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}