namespace Rhymba.Services.Playlist
{
    using Rhymba.Models.Playlist;

    public class PlaylistService : PlaylistServiceWorker
    {
        private Account? Account;

        internal PlaylistService(string rhymbaAccessToken, string rhymbaAccessSecret, string playlistPublicKey, string playlistPrivateKey) : base(rhymbaAccessToken, rhymbaAccessSecret, playlistPublicKey, playlistPrivateKey)
        {

        }

        public Account GetAccount()
        {
            return this.Account ??= new Account(base.rhymbaAccessToken, base.rhymbaAccessSecret, base.playlistPublicKey, base.playlistPrivateKey);
        }

        public async Task<PlaylistResponseBase<CreatePlaylistResponse>?> CreatePlaylist(CreatePlaylistRequest request)
        {
            return await this.CallPlaylistService<CreatePlaylistResponse>(request, "post", "/playlists");
        }

        public async Task<PlaylistResponseBase<int>?> AddAlbumToPlaylist(int playlistId, AddAlbumRequest request)
        {
            return await this.CallPlaylistService<int>(request, "post", $"/playlists/{playlistId}/albums");
        }

        public async Task<PlaylistResponseBase<int>?> AddArtistToPlaylist(int playlistId, AddArtistRequest request)
        {
            return await this.CallPlaylistService<int>(request, "post", $"/playlists/{playlistId}/artists");
        }

        public async Task<PlaylistResponseBase<int>?> AddMediaToPlaylist(int playlistId, AddMediaRequest request)
        {
            return await this.CallPlaylistService<int>(request, "post", $"/playlists/{playlistId}/media");
        }

        public async Task<PlaylistResponseBase<int>?> AddPlaylistToPlaylist(int playlistId, AddPlaylistRequest request)
        {
            return await this.CallPlaylistService<int>(request, "post", $"/playlists/{playlistId}/playlists");
        }

        public async Task<PlaylistResponseBase<PlaylistResponse>?> AddUserToPlaylist(int playlistId, AddUserToPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistResponse>(request, "post", $"/playlists/{playlistId}/users");
        }

        public async Task<PlaylistResponseBase<bool>?> AddImageToPlaylist(int playlistId, AddAlbumRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PlaylistResponseBase<PlaylistResponse>?> GetPlaylist(int playlistId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistResponse>(request, "get", $"/playlists/{playlistId}");
        }

        public async Task<PlaylistResponseBase<PlaylistItem[]>?> GetPlaylistAlbums(int playlistId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistItem[]>(request, "get", $"/playlists/{playlistId}/albums");
        }

        public async Task<PlaylistResponseBase<PlaylistItem[]>?> GetPlaylistArtists(int playlistId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistItem[]>(request, "get", $"/playlists/{playlistId}/artists");
        }

        public async Task<PlaylistResponseBase<PlaylistItem[]>?> GetPlaylistMedia(int playlistId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistItem[]>(request, "get", $"/playlists/{playlistId}/media"); 
        }

        public async Task<PlaylistResponseBase<PlaylistItem[]>?> GetPlaylistPlaylists(int playlistId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistItem[]>(request, "get", $"/playlists/{playlistId}/playlists");
        }

        public async Task<PlaylistResponseBase<PlaylistUser[]>?> GetPlaylistUsers(int playlistId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistUser[]>(request, "get", $"playlists/{playlistId}/users");
        }

        public async Task<PlaylistResponseBase<PlaylistResponse[]>?> GetUserPlaylists(int userId, GetPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistResponse[]>(request, "get", $"/users/{userId}/playlists");
        }

        public async Task<PlaylistResponseBase<PlaylistResponse>?> EditPlaylist(int playlistId, EditPlaylistRequest request)
        {
            return await this.CallPlaylistService<PlaylistResponse>(request, "put", $"/playlists/{playlistId}");
        }

        public async Task<PlaylistResponseBase?> EditPlaylisAlbumPosition(int playlistId, int albumId, EditPlaylistItemOrderRequest request)
        {
            return await this.CallPlaylistService<string>(request, "put", $"/playlists/{playlistId}/albums/{albumId}");
        }

        public async Task<PlaylistResponseBase?> EditPlaylisArtistPosition(int playlistId, int artistId, EditPlaylistItemOrderRequest request)
        {
            return await this.CallPlaylistService<string>(request, "put", $"/playlists/{playlistId}/artists/{artistId}");
        }

        public async Task<PlaylistResponseBase?> EditPlaylistMediaPosition(int playlistId, int mediaId, EditPlaylistItemOrderRequest request)
        {
            return await this.CallPlaylistService<string>(request, "put", $"/playlists/{playlistId}/media/{mediaId}");
        }

        public async Task<PlaylistResponseBase?> EditPlaylisPlaylistPosition(int playlistId, int itemId, EditPlaylistItemOrderRequest request)
        {
            return await this.CallPlaylistService<string>(request, "put", $"/playlists/{playlistId}/playlists/{itemId}");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylist(int playlistId, DeletePlaylistRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylistAlbum(int playlistId, int albumId, DeletePlaylistAlbumRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}/albums/{albumId}");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylistArtist(int playlistId, int artistId, DeletePlaylistAlbumRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}/artists/{artistId}");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylistMedia(int playlistId, int mediaId, DeletePlaylistAlbumRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}/media/{mediaId}");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylistPlaylist(int playlistId, int itemId, DeletePlaylistAlbumRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}/playlists/{itemId}");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylistImage(int playlistId, DeletePlaylistAlbumRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}/deleteimage");
        }

        public async Task<PlaylistResponseBase?> DeletePlaylistUser(int playlistId, int userId, DeletePlaylistUserRequest request)
        {
            return await this.CallPlaylistService<string>(request, "delete", $"/playlists/{playlistId}/users/{userId}");
        }

        public async Task<PlaylistResponseBase?> StartRadioStream(int playlistId, StartRadioRequest request)
        {
            return await this.CallPlaylistService<string>(request, "post", $"/playlists/{playlistId}/start");
        }

        public async Task<PlaylistResponseBase?> StopRadioStream(int playlistId, StopRadioRequest request)
        {
            return await this.CallPlaylistService<string>(request, "post", $"/playlists/{playlistId}/stop");
        }
    }
}
