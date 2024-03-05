using LoginDC6.Client.Helpers;
using LoginDC6.Shared.Helpers.DTOs;

namespace LoginDC6.Client.Repositories
{
    public interface IAccountRepository
    {
        Task<UserToken> Register(UserInfo userInfo);
        Task<UserToken> Login(UserInfoLogin userInfo);
        Task<UserToken> LoginTwoStep(UserInfoLogin userInfo);
        Task LogoutTotal();
        /// <summary>
        /// دریافت لیست کاربران برای اسافده در بخش سطوح دسترسی
        /// </summary>
        /// <returns></returns>
        Task<List<UsersView>> GetUserList();
        Task DeleteUserByItem(UsersView usersView);
        Task EditUserByItem(UsersView usersView);
        Task ChangePassrosdByEmail(UserInfoRequestPassword usersMail);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpService httpService;
        private readonly string baseUrl = "api/account";

        public AccountRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var response = await httpService.Post<UserInfo, UserToken>($"{baseUrl}/Create", userInfo);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<UserToken> Login(UserInfoLogin userInfo)
        {
            var response = await httpService.Post<UserInfoLogin, UserToken>($"{baseUrl}/Login", userInfo);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<UserToken> LoginTwoStep(UserInfoLogin userInfo)
        {
            var result = await this.httpService.Post<UserInfoLogin, UserToken>($"{baseUrl}/LoginTwoStep", userInfo);
            if (!result.IsSuccess)
            {
                throw new ApplicationException(await result.GetBody());
            }

            return result.Response;
        }

        /// <summary>
        /// دریافت لیست کاربران برای استفاده در بخش سطوح دسترسی
        /// </summary>
        /// <returns></returns>
        public async Task<List<UsersView>> GetUserList()
        {
            var respose = await httpService.Get<List<UsersView>>($"{baseUrl}/GetUsersSmallList");
            return respose.Response;
        }

        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <param name="usersView"></param>
        /// <returns></returns>
        public async Task DeleteUserByItem(UsersView usersView)
        {
            if (usersView.UserName.ToLower().Trim() != "najafzade@gmail.com".ToLower().Trim())
            {
                var response = await httpService.Post<UsersView>($"{baseUrl}/DeleteUserByItem", usersView);
            }
        }

        /// <summary>
        /// ویرایش کاربران
        /// </summary>
        /// <param name="usersView"></param>
        /// <returns></returns>
        public async Task EditUserByItem(UsersView usersView)
        {
            var response = await httpService.Post<UsersView>($"{baseUrl}/EditUserByItem", usersView);
        }

        /// <summary>
        /// درخواست کلمه عبور جدید
        /// </summary>
        /// <param name="usersMail"></param>
        /// <returns></returns>
        public async Task ChangePassrosdByEmail(UserInfoRequestPassword usersMail)
        {
            var response = await httpService.Post<UserInfoRequestPassword>($"{baseUrl}/ChangePassrosdByEmail", usersMail);
        }

        public async Task LogoutTotal()
        {
            try
            {
                var response = await httpService.Post<object>($"{baseUrl}/LogoutTotal", null);
                if (response.IsSuccess)
                {
                    // انجام عملیات مربوط به خروج موفقیت‌آمیز کاربر
                    // مثلاً نمایش پیام خروج موفقیت‌آمیز و ریدایرکت به صفحه‌ای دیگر
                }
                else
                {
                    // انجام عملیات مربوط به خطا در خروج
                    // مثلاً نمایش پیام خطا
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@2" + ex.Message);
                // انجام عملیات مربوط به خطا در پردازش خطا
                // مثلاً نمایش پیام خطا به کاربر
            }
        }
    }
}
