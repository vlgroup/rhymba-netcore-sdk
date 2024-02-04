namespace Rhymba.Services.Playlist
{
    using Rhymba.Models.Playlist;

    public class Account : PlaylistServiceWorker
    {
        internal Account(string accessToken, string accessSecret, string publicKey, string privateKey) : base(accessToken, accessSecret, publicKey, privateKey)
        {

        }

        public async Task<PlaylistResponseBase<int>?> Register(AccountRegisterRequest request)
        {
            return await this.CallPlaylistAccountService<AccountRegisterRequest, int>(request, "post", "/account/register");
        }

        public async Task<PlaylistResponseBase<AccountLoginResponse>?> Login(AccountLoginRequest request)
        {
            return await this.CallPlaylistAccountService<AccountLoginRequest, AccountLoginResponse>(request, "post", "/account/login");
        }

        public async Task<PlaylistResponseBase<string>?> Logout(AccountLogoutRequest request)
        {
            return await this.CallPlaylistAccountService<AccountLogoutRequest, string>(request, "post", "/account/logout");
        }

        public async Task<PlaylistResponseBase<string>?> ChangePassword(AccountChangePasswordRequest request)
        {
            return await this.CallPlaylistAccountService<AccountChangePasswordRequest, string>(request, "post", "/account/changepassword");
        }
    }
}
