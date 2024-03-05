namespace LoginDC6.Client.Auth
{
    public interface ILoginService
    {
        Task Login(string token);
        Task LogOut();
    }
}
