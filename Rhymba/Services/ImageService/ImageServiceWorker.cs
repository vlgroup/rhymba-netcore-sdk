namespace Rhymba.Services.Images
{
    using Rhymba.Models.Images;
    using Rhymba.Services.Common;
    using System.Web;

    public abstract class ImageServiceWorker : ServiceWorkerBase
    {
        protected ImageServiceWorker() : base("https://ib3-lb.mcnemanager.com")
        {

        }

        protected string? BuildImageUrl(string imageId, RhymbaImageOptions? imageOptions, string imageType)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                return null;
            }

            var url = $"{this.rhymbaUrl}/";

            if (imageOptions != null)
            {
                if (imageOptions.scaling != null)
                {
                    url += $"scl/{imageOptions.scaling}/";
                }

                if (imageOptions.cropping != null)
                {
                    url += $"crp/{imageOptions.cropping}/";
                }

                if (imageOptions.width != null && imageOptions.height != null)
                {
                    url += $"sz/{imageOptions.width}/{imageOptions.height}/";
                }

                if (imageOptions.reflection != null)
                {
                    url += $"rfl/{imageOptions.reflection.Percent}/{imageOptions.reflection.ClipPercent}/{imageOptions.reflection.Alpha}/{imageOptions.reflection.Angle}/{imageOptions.reflection.Gap}/";
                }
            }

            url += $"{imageId}.{imageType}";

            if (imageOptions != null && !string.IsNullOrWhiteSpace(imageOptions.dflt))
            {
                url += $"?dflt={imageOptions.dflt}";
            }

            return url;
        }
    }
}
