using UIKit;
using OoyalaSDK.tvOS;

namespace Sample.OoyalaSDK.tvOS
{
    public class TvViewController : OOOoyalaTVPlayerViewController
    {
        public override void LoadView()
        {
            base.LoadView();

            Player = new OOOoyalaPlayer(
                pcode: "c0cTkxOqALQviQIGAHWY5hP0q9gU",
                domain: new OOPlayerDomain("http://www.ooyala.com"));

            Player.SetEmbedCode("Y1ZHB1ZDqfhCPjYYRbCEOz0GR8IsVRm1");
            Player.Play();
        }
    }
}