using UIKit;
using OoyalaSDK.tvOS;
//using OoyalaSkinSDK.tvOS;
using System.Threading.Tasks;

namespace Sample.OoyalaSDK.tvOS
{
    public class TvViewController : OOOoyalaTVPlayerViewController
    {
        public override void LoadView()
        {
            base.LoadView();

            ProgressTintColor = UIColor.Red;

            OODebugMode.SetDebugMode(DebugMode.LogAndAbort);

            Player = new OOOoyalaPlayer(
                pcode: "c0cTkxOqALQviQIGAHWY5hP0q9gU",
                domain: new OOPlayerDomain("http://www.ooyala.com"));

            Player.SetEmbedCode("Y1ZHB1ZDqfhCPjYYRbCEOz0GR8IsVRm1");
            Player.Play();
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            await Task.Delay(5000);
            System.Diagnostics.Debug.WriteLine("delayed");
        }
    }
}