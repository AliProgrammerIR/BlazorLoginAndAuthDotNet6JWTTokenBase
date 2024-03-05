using LoginDC6.Server.AppDbContext;
using LoginDC6.Server.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using LoginDC6.Shared.Helpers.DTOs;
using LoginDC6.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace LoginDC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration iconfiguration;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<ApplicationUser> _userManager, 
            SignInManager<ApplicationUser> _signInManager, 
            IConfiguration _iconfiguration, 
            ApplicationDbContext _applicationDbContext,
            ILogger<AccountController> _logger)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            iconfiguration = _iconfiguration;
            applicationDbContext = _applicationDbContext;
            logger = _logger;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfoLogin userInfo)
        {
            UserToken ut = new UserToken();
            //حذف توکن دو مرحله ای در صورتی که کش شده باشد
            await signInManager.ForgetTwoFactorClientAsync();
            try
            {
                var acc6Leve /*دسترسی ورود به سیستم*/ = await applicationDbContext.TblUsersPermissions
                    .Include(m => m.AppUsers)
                    .SingleAsync(n => n.AppUsers.Email == userInfo.Email && n.PageId == 6);

                var result = await signInManager
                    .PasswordSignInAsync(userInfo.Email, userInfo.StrPassword, false, false);
                
                if (result.Succeeded)
                {//Logining ok.
                    ut = BuildToken(userInfo);
                    ut.Responser.ResponsState = ResponserState.Successful;
                    ut.Responser.StrMessage = "ورود با موفقیت انجام شد . . .";
                    return ut;
                }
                else
                {
                    if (result.RequiresTwoFactor)
                    {
                        var user = await userManager.FindByEmailAsync(userInfo.Email);
                        var tokenMain = await userManager.GenerateTwoFactorTokenAsync(user, "Email");//.GetValidTwoFactorProvidersAsync(user);
                        ut.Responser.StrMessage = tokenMain.ToString();
                        ut.Responser.ResponsState = ResponserState.TwoVerification;
                        Console.WriteLine(ut.Responser.StrMessage);
                        return ut;
                    }
                    else
                    {
                        ut.Responser.ResponsState = ResponserState.Fail;
                        ut.Responser.StrMessage = "مشخصات کاربری صحیح نمی باشند . . . .";
                        //return BadRequest("Login Failed");
                        return ut;
                    }
                }
            }
            catch (Exception ex)
            {
                ut.Responser.ResponsState = ResponserState.Fail;
                ut.Responser.StrMessage = "ورود انجام نشد. لطفا مشخصات کاربری یا سطح دسترسی را بررسی فرمائید . . .";
                return ut;
            }
        }

        [HttpPost("LoginTwoStep")]
        public async Task<ActionResult<UserToken>> LoginTwoStep([FromBody] UserInfoLogin userInfo)
        {
            try
            {
                var appUser = await userManager.FindByEmailAsync(userInfo.Email);

                var result = await signInManager.TwoFactorSignInAsync("Email", userInfo.StrCode, false, true);

                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    //return new UserToken() { response = new Response(StatueResponse.Failed, "کد وارد شده معتبر نمی باشد") };
                    UserToken ut = new UserToken();
                    ut.Responser.ResponsState = ResponserState.Fail;
                    ut.Responser.StrMessage = "کد وارد شده صحیح نمی باشند . . . .";
                    return ut;
                }
            }
            catch (Exception ex)
            {
                String Err = ex.Message;
                //Console.WriteLine(Err);
                UserToken ut = new UserToken();
                ut.Responser.ResponsState = ResponserState.Fail;
                ut.Responser.StrMessage = "کد وارد شده صحیح نمی باشند . . . ." + Err;
                return ut;
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                FaxNumber = model.FaxNumber,
                FirstName = model.FirstName
            ,
                HomeAddress = model.HomeAddress,
                WorkAddress = model.WorkAddress,
                IsWoman = model.IsWoman
            ,
                LandLinePhoneNumber = model.LandLinePhoneNumber,
                LastName = model.LastName
            };
            try
            {
                String ErrStr = "";
                var res1 = await userManager.FindByEmailAsync(model.Email);
                var res2 = await applicationDbContext.Users.AnyAsync(n => n.PhoneNumber == model.PhoneNumber);


                if (res1 != null)
                {
                    ErrStr += "آدرس ایمیل وارد شده تکراری می باشد.";
                }

                if (res2 == true)
                {
                    ErrStr += "تلفن همراه وارد شده تکراری می باشد.";
                }

                if (ErrStr != "")
                {
                    UserToken ut = new UserToken();
                    ut.Responser.ResponsState = ResponserState.Fail;
                    ut.Responser.StrMessage = ErrStr;
                    return ut;
                }

                var result = await userManager.CreateAsync(user, model.StrPassword);
                if (result.Succeeded)
                {
                    #region اضافه کردن دسترسی ورود به سیستم
                    var resCreated = await userManager.FindByEmailAsync(model.Email);
                    if (resCreated != null) 
                    {
                        TblUsersPermissions t = new();
                        t.PUserID = resCreated.Id;
                        t.PageId = 6;
                        t.PageNames = null;
                        t.PageCrcPerm = "----";

                        await applicationDbContext.AddAsync(t);
                        await applicationDbContext.SaveChangesAsync();
                    }
                    #endregion
                    UserInfoLogin ln = new UserInfoLogin() { CaptchaString = "" 
                        , Email = model.Email , StrCode = "0", StrPassword = model.StrPassword };

                    return BuildToken(ln);
                }
                else
                {
                    UserToken ut = new UserToken();
                    ut.Responser.ResponsState = ResponserState.Fail;
                    ut.Responser.StrMessage = "خطا در هنگام ایجاد کاربر جدید" + result.Errors;
                    return ut;
                    //return BadRequest("Username Or Password is invalid");
                }
            }
            catch (Exception ex)
            {
                UserToken ut = new UserToken();
                ut.Responser.ResponsState = ResponserState.Fail;
                ut.Responser.StrMessage = "خطا در هنگام ایجاد کاربر جدید";
                ut.Responser.StrErrorMessage = ex.Message;
                return ut;
                //return BadRequest(ex.Message);
            }
        }

        private UserToken BuildToken(UserInfoLogin userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim("MyClaim", "My Claim Value")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfiguration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.Now.AddDays(30);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expireDate,
                signingCredentials: creds
            );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = expireDate,
                Responser = new PubResponser { StrMessage = "ورود موفقیت آمیز", ResponsState = ResponserState.Successful }
            };
        }

        [HttpGet("GetUsersSmallList")]
        public async Task<List<UsersView>> GetUserList()
        {
            var Usrlists = await (from row in userManager.Users
                                  select new UsersView
                                  {
                                      UserID = row.Id,
                                      AccessFailedCount = row.AccessFailedCount,
                                      EmailConfirmed = row.EmailConfirmed,
                                      LockoutEnabled = row.LockoutEnabled,
                                      LockoutEnd = row.LockoutEnd,
                                      PhoneNumber = row.PhoneNumber,
                                      TwoFactorEnabled = row.TwoFactorEnabled,
                                      UserName = row.UserName,
                                      UsrAddress = row.HomeAddress,
                                      FirstName = row.FirstName,
                                      LastName = row.LastName
                                  }).ToListAsync<UsersView>();

            return Usrlists;
        }

        [HttpPost("DeleteUserByItem")]
        public async Task DeleteUser(UsersView usersView)
        {
            var user = await userManager.FindByIdAsync(usersView.UserID);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    // عملیات حذف با موفقیت انجام شد
                }
                else
                {
                    // خطا در حذف کاربر
                }
            }
            else
            {
                // کاربر پیدا نشد
            }
        }

        [HttpPost("EditUserByItem")]
        public async Task EditUserByItem(UsersView usersView)
        {
            var user = await userManager.FindByIdAsync(usersView.UserID);
            if (user != null)
            {
                user.FirstName = usersView.FirstName;
                user.LastName = usersView.LastName;
                user.PhoneNumber = usersView.PhoneNumber;

                var result = await userManager.UpdateAsync(user);
                if (usersView.PasswordUsr != "" && usersView.PasswordUsr != null)
                {
                    var userChangePss = await userManager.FindByEmailAsync(usersView.UserName);
                    if (userChangePss != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(userChangePss);
                        var resultNew = await userManager.ResetPasswordAsync(user, token, usersView.PasswordUsr);
                        if (resultNew.Succeeded)
                        {
                            // عملیات ریست پسورد با موفقیت انجام شد
                        }
                        else
                        {
                            // خطا در ریست پسورد
                        }
                    }
                    else
                    {
                        // کاربر پیدا نشد
                    }
                }
                if (result.Succeeded)
                {
                    // عملیات حذف با موفقیت انجام شد
                }
                else
                {
                    // خطا در حذف کاربر
                }
            }
            else
            {
                // کاربر پیدا نشد
            }
        }

        [HttpPost("LogoutTotal")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                //الزامی - در غیر اینصورت برای کاربراین لاگین دو مرحله ای در صورت کش شدن به مشکل بر می خوریم
                await signInManager.ForgetTwoFactorClientAsync();
                await signInManager.SignOutAsync(); // ساین اوت کردن کاربر
                return Ok(); // برگرداندن پاسخ 200 OK به مشتری
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError); // برگرداندن پاسخ 500 Internal Server Error به مشتری در صورت بروز خطا
            }
        }

        [HttpPost("ChangePassrosdByEmail")]
        public async Task ChangePassrosdByEmail(UserInfoRequestPassword usersMail)
        {
            String StrNewPassword = GetUniqueKey(15);
            var userChangePss = await userManager.FindByEmailAsync(usersMail.EmailAddress);
            if (userChangePss != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(userChangePss);
                var resultNew = await userManager.ResetPasswordAsync(userChangePss, token, StrNewPassword);
                if (resultNew.Succeeded)
                {
                    UDFAuthenticateEmail(usersMail.EmailAddress, StrNewPassword);
                }
            }
        }

        private void UDFAuthenticateEmail(String StrEmailAddress, String StrPassword)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            String Str_PassWord = StrPassword;//GetUniqueKey(15);
            //set the addresses
            mail.From = new MailAddress("test@test.com");
            mail.To.Add(StrEmailAddress);

            //set the content
            mail.Subject = "Password Information";
            mail.Body = "<div style=\"direction: ltr;\"><span style=\"font-size: large;\"><u><em><strong>&nbsp;Your New PassWord:</u> " + Str_PassWord.Trim() + " </strong></em></span></div><div style=\"direction: ltr;\"><a href=\"http://www.pspbime.ir\"><span style=\"font-size: large;\"><u><em><strong>www.pspbime.ir</strong></em></u></span></a></div>";
            mail.IsBodyHtml = true;
            //send the message
            SmtpClient smtp = new SmtpClient("test.com");
            smtp.Port = 25;

            //to authenticate we set the username and password properites on the SmtpClient
            smtp.Credentials = new NetworkCredential("test@test.com", "54sfj&^$%FgsfdgGHF765");
            smtp.Send(mail);

            //Dal.Lsc_MainMasterDataContext db = new Dal.Lsc_MainMasterDataContext();
            //Dal.tblPuser UsrAds = db.tblPusers.Single(n => n.PuserEmailAddress.Trim() == Str_EmailTo.Trim());

            //UsrAds.PUserPwa = UDF_MakePasswordStr(UsrAds.PUserName.Trim(), Str_PassWord.Trim()).Trim();// ایجاد کلمه عبور کد گذاری شده
            //UsrAds.PUserCrcCode = UDF_MakeCrcStr(UsrAds.PUserName.Trim(), Str_PassWord.Trim()).Trim();// ایجاد کد سی آر سی جدید

            //db.SubmitChanges();
        }

        private string GetUniqueKey(int length)
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#^*+";
            const string allChars = uppercaseChars + lowercaseChars + digitChars + specialChars;

            var random = new Random();
            var result = new char[length];

            // ابتدا یک حرف بزرگ، یک حرف کوچک، یک عدد و یک علامت ویژه به طور تصادفی به رشته اضافه می‌کنیم
            result[0] = uppercaseChars[random.Next(uppercaseChars.Length)];
            result[1] = lowercaseChars[random.Next(lowercaseChars.Length)];
            result[2] = digitChars[random.Next(digitChars.Length)];
            result[3] = specialChars[random.Next(specialChars.Length)];

            for (int i = 4; i < length; i++)
            {
                result[i] = allChars[random.Next(allChars.Length)];
            }

            // ترکیب تصادفی کاراکترها
            for (int i = length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return new string(result);
        }
    }
}