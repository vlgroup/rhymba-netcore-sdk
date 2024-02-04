namespace Rhymba.Models.Playlist
{
    public class PlaylistResponse
    {
        public List<PlaylistUser>? Users { get; set; }
        public List<PlaylistItem>? Items { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageId { get; set; } = string.Empty;
        public int PlaylistId { get; set; }
        public int SortSeed { get; set; }
        public bool IsRandom { get; set; }
        public bool DMCA { get; private set; }
        public int TotalItems { get; set; }
        public bool Playing { get; set; }
        public PlaylistPlayingItem? currentItem { get; set; }
        public int currentItemTime { get; set; }
    }
}
