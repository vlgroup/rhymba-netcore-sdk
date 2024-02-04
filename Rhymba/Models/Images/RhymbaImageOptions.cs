namespace Rhymba.Models.Images
{
    public class RhymbaImageOptions
    {
        public int? width { get; set; }
        public int? height { get; set; }
        public ScalingType? scaling { get; set; }
        public CroppingType? cropping { get; set; }
        public Reflection? reflection { get; set; }
        public string? dflt { get; set; }
    }
}
