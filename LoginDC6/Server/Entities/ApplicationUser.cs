using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LoginDC6.Server.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "نام"), Required(ErrorMessage = "ورود نام الزامی می باشد."), MinLength(2, ErrorMessage = "حداقل کاراکترهای مجاز 2 می باشد"), MaxLength(150, ErrorMessage = "حداکثر طول نام 150 حرف می باشد")]
        public String FirstName { get; set; }
        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "ورود نام خانوادگی الزامی می باشد."), MinLength(2, ErrorMessage = "حداقل کاراکترهای مجاز 2 می باشد"), MaxLength(150, ErrorMessage = "حداکثر طول نام خانوادگی 150 حرف می باشد")]
        public String LastName { get; set; }
        [Display(Name = "شماره تلفن ثابت"), MaxLength(3900, ErrorMessage = "حداکثر طول تلفن ها 3900 کاراکتر می باشد.")]
        public string LandLinePhoneNumber { get; set; }
        [Display(Name = "شماره فکس"), MaxLength(3900, ErrorMessage = "حداکثر طول شماره فکس ها 3900 کاراکتر می باشد.")]
        public string FaxNumber { get; set; }
        public Boolean CanRememberMe { get; set; }
        [Display(Name = "جنسیت"), Required]
        public Boolean IsWoman { get; set; }
        [Display(Name = "آدرس منزل"), MaxLength(3900, ErrorMessage = "حداکثر طول آدرس 3900 کاراکتر می باشد.")]
        public String HomeAddress { get; set; }
        [Display(Name = "آدرس محل کار"), MaxLength(3900, ErrorMessage = "حداکثر طول آدرس محل کار 3900 کاراکتر می باشد.")]
        public String WorkAddress { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        [MaxLength(1500)]
        public Byte[]? PictureOfUser { get; set; }
    }
}
