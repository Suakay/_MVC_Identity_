using System.ComponentModel.DataAnnotations;

namespace _MVC_Identity_.Models.ViewModel
{
    public class RegisterVM
    {
       
            [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
            [Display(Name = "Kullanıcı adı")]
            [MinLength(3, ErrorMessage = "Kullanıcı adı 3 karakterden az olamaz")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Kullanıcı şifre Geçilemez")]
            [Display(Name = "Şifre")]

            public string Password { get; set; }

            [Required(ErrorMessage = "E-Mail Boş Geçilemez")]
            [Display(Name = "E-Mail")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Mail formatı doğru kullanılmadı")]
            public string Email { get; set; }
        
    }
}
