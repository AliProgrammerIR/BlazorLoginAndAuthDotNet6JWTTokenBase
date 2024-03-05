using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LoginDC6.Shared.Helpers.DTOs
{
    public class UserInfo
    {
        [Display(Name = "آدرس ایمیل (نام کابری محسوب می شود)"), Required(ErrorMessage = "ورود آدرس ایمیل الزامی می باشد . . ."), EmailAddress(ErrorMessage = "قالب آدرس ایمیل به درستی وارد نشده است")]
        public string Email { get; set; }
        [Display(Name = "تکرار آدرس ایمیل"), Required(ErrorMessage = "ورود تکرار آدرس ایمیل الزامی می باشد . . ."), EmailAddress(ErrorMessage = "قالب تکرار آدرس ایمیل به درستی وارد نشده است"), Compare("Email", ErrorMessage = "آدرس ایمیل و تکرار آدرس ایمیل با هم تفاوت دارند.")]
        public string ConfirmEmail { get; set; }
        [Display(Name = "کلمه عبور"), Required(ErrorMessage = "ورود کلمه عبور الزامی می باشد . . ."), MinLength(6, ErrorMessage = "طول کلمه عبور حداقل 6 کاراکتر می باشد.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,}$", ErrorMessage = "کلمه عبور حداقل یک حرف بزرگ [A-Z] و حداقل یک حرف کوچک و حداقل یک رقم 0-9 حداقل یک علامت خاص مانند @ و یا # و یا * دارد و کمترین طول رمز عبور ۶ کاراکتر است.")]
        public string StrPassword { get; set; }
        [Display(Name = "تائید کلمه عبور"), Compare("StrPassword", ErrorMessage = "کلمه عبور و تکرار کلمه عبور باید با هم مشابه باشند."), Required(ErrorMessage = "ورود تکرار کلمه عبور الزامی می باشد.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "کد تائیدیه")]
        public string StrCode { get; set; }
        [Display(Name = "شماره تلفن همراه"), Required(ErrorMessage = "ورود شماره تلفن همراه الزامی می باشد . . .")]
        public string PhoneNumber { get; set; }
        [Display(Name = "شماره تلفن ثابت"), MaxLength(3900, ErrorMessage = "حداکثر طول تلفن ها 3900 کاراکتر می باشد.")]
        public string LandLinePhoneNumber { get; set; }
        [Display(Name = "شماره فکس"), MaxLength(3900, ErrorMessage = "حداکثر طول شماره فکس ها 3900 کاراکتر می باشد.")]
        public string FaxNumber { get; set; }
        public Boolean CanRememberMe { get; set; }
        [Display(Name = "جنسیت"), Required]
        public Boolean IsWoman { get; set; }
        [Display(Name = "نام"), Required(ErrorMessage = "ورود نام الزامی می باشد."), MinLength(2, ErrorMessage = "حداقل کاراکترهای مجاز 2 می باشد"), MaxLength(150, ErrorMessage = "حداکثر طول نام 150 حرف می باشد")]
        public String FirstName { get; set; }
        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "ورود نام خانوادگی الزامی می باشد."), MinLength(2, ErrorMessage = "حداقل کاراکترهای مجاز 2 می باشد"), MaxLength(150, ErrorMessage = "حداکثر طول نام خانوادگی 150 حرف می باشد")]
        public String LastName { get; set; }
        [Display(Name = "آدرس منزل"), MaxLength(3900, ErrorMessage = "حداکثر طول آدرس 3900 کاراکتر می باشد.")]
        public String HomeAddress { get; set; }
        [Display(Name = "آدرس محل کار"), MaxLength(3900, ErrorMessage = "حداکثر طول آدرس محل کار 3900 کاراکتر می باشد.")]
        public String WorkAddress { get; set; }
        [Display(Name = "کپچا"), Required(ErrorMessage = "ورود کد امنیتی کپچا الزامی می باشد"), NotMapped]
        public String CaptchaString { get; set; }
    }

    public class UserInfoLogin
    {
        [Display(Name = "آدرس ایمیل (نام کابری محسوب می شود)"), Required(ErrorMessage = "ورود آدرس ایمیل الزامی می باشد . . ."), EmailAddress(ErrorMessage = "قالب آدرس ایمیل به درستی وارد نشده است")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور"), Required(ErrorMessage = "ورود کلمه عبور الزامی می باشد . . ."), MinLength(6, ErrorMessage = "طول کلمه عبور حداقل 6 کاراکتر می باشد.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,}$", ErrorMessage = "کلمه عبور حداقل یک حرف بزرگ [A-Z] و حداقل یک حرف کوچک و حداقل یک رقم 0-9 حداقل یک علامت خاص مانند @ و یا # و یا * دارد و کمترین طول رمز عبور ۶ کاراکتر است.")]
        public string StrPassword { get; set; }
        [Display(Name = "کد تائیدیه")]
        public string StrCode { get; set; }
        [Display(Name = "کپچا"), Required(ErrorMessage = "ورود کد امنیتی کپچا الزامی می باشد"), NotMapped]
        public String CaptchaString { get; set; }
    }

    /// <summary>
    /// از این کلاس برای ارسال درخواست کلمه عبور جدید به ایمیل استفاده می شود
    /// </summary>
    public class UserInfoRequestPassword
    {
        [Display(Name = "آدرس ایمیل (نام کابری محسوب می شود)"), Required(ErrorMessage = "ورود آدرس ایمیل الزامی می باشد . . ."), EmailAddress(ErrorMessage = "قالب آدرس ایمیل به درستی وارد نشده است")]
        public string EmailAddress { get; set; }
        [Display(Name = "کپچا"), Required(ErrorMessage = "ورود کد امنیتی کپچا الزامی می باشد"), NotMapped]
        public String CaptchaString { get; set; }
    }

    /// <summary>
    /// ویو برای نمایش یوزر ها
    /// </summary>
    public class UsersView
    {
        public String UserID { get; set; }
        public String UserName { get; set; }
        public Boolean EmailConfirmed { get; set; }
        public String PhoneNumber { get; set; }
        public Boolean TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public Boolean LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public String UsrAddress { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,}$", ErrorMessage = "کلمه عبور حداقل یک حرف بزرگ [A-Z] و حداقل یک حرف کوچک و حداقل یک رقم 0-9 حداقل یک علامت خاص مانند @ و یا # و یا * دارد و کمترین طول رمز عبور ۶ کاراکتر است.")]
        public String PasswordUsr { get; set; }
    }

    /// <summary>
    /// کلاس برای ایجاد عکس کپچا
    /// </summary>
    public class CaptchaPic
    {
        /// <summary>
        /// عکس مربوط به کپچا
        /// </summary>
        public byte[] RandomImageByte { get; set; }
        /// <summary>
        /// متن کپچا
        /// </summary>
        public string RandomImageText { get; set; }
    }

    /// <summary>
    /// کلاس برای ثبت نام کاربر جدید
    /// </summary>
    public class UserInfoRegister
    {
        [Display(Name = "Email Address (considered as username)"), Required(ErrorMessage = "Entering email address is required..."), EmailAddress(ErrorMessage = "Email address format is not entered correctly.")]
        public string Email { get; set; }

        [Display(Name = "Confirm Email Address"), Required(ErrorMessage = "Entering email address confirmation is required..."), EmailAddress(ErrorMessage = "Email address confirmation format is not entered correctly."), Compare("Email", ErrorMessage = "Email and email confirmation do not match.")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Entering password is required..."), MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,}$", ErrorMessage = "Password must contain at least one uppercase letter [A-Z], one lowercase letter, one digit 0-9, and one special character such as @, #, or *. The minimum password length is 6 characters.")]
        public string StrPassword { get; set; }

        [Display(Name = "Confirm Password"), Compare("StrPassword", ErrorMessage = "Password and password confirmation must match."), Required(ErrorMessage = "Entering password confirmation is required.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mobile Phone Number"), Required(ErrorMessage = "Entering mobile phone number is required...")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender"), Required]
        public Boolean IsWoman { get; set; }

        [Display(Name = "First Name"), Required(ErrorMessage = "Entering first name is required."), MinLength(2, ErrorMessage = "Minimum allowed characters for the first name is 2."), MaxLength(150, ErrorMessage = "Maximum length for the first name is 150 characters.")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name"), Required(ErrorMessage = "Entering last name is required."), MinLength(2, ErrorMessage = "Minimum allowed characters for the last name is 2."), MaxLength(150, ErrorMessage = "Maximum length for the last name is 150 characters.")]
        public String LastName { get; set; }

        [Display(Name = "Captcha"), Required(ErrorMessage = "Entering Captcha security code is required"), NotMapped]
        public String? CaptchaString { get; set; }
    }
}