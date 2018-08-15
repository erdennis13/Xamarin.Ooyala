using System;
using CoreGraphics;
using UIKit;
using OoyalaSDK.iOS;
using OoyalaCastSDK.iOS;
using Foundation;

namespace Sample.OoyalaSDK.iOS
{
    public class CastMiniControllerView : OOCastMiniControllerView
    {
        private OOCastManager _castManager;
        private NSObject _delegate;

        UIImage _pauseImage, _playImage;
        UIButton _button;

        public CastMiniControllerView(
            CGRect frame,
            OOCastManager castManager,
            NSObject del)
            : base(frame, castManager, del)
        {
            _castManager = castManager;
            _delegate = del;

            var castPlayer = (OOCastPlayer)_castManager.CastPlayer;

            this.Cell = new UITableViewCell(UITableViewCellStyle.Default, "cell");
            this.Cell.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            this.Cell.TextLabel.Text = castPlayer?.CastItemTitle;
            this.Cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            // Accessory is the play/pause button.
            _pauseImage = UIImage.FromBundle("pause.png");
            _playImage = UIImage.FromBundle("play.png");

            _button = new UIButton();
            _button.TouchUpInside += Button_TouchUpInside;

            var playing = (castPlayer?.State == OOOoyalaPlayerState.Playing ||
                           castPlayer?.State == OOOoyalaPlayerState.Loading);
            var buttonImage = (true ? _pauseImage : _playImage);
            _button.SetBackgroundImage(buttonImage, forState: UIControlState.Normal);

            this.Cell.AccessoryView = _button;

            this.Cell.ImageView.Image = UIImage.FromBundle("ooyala_logo.png");

            Cell.SetNeedsLayout();
            AddSubview(this.Cell);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var bounds = Bounds;

            this.Cell.Frame = bounds;

            _button.Frame = new CGRect(
                0,
                0,
                bounds.Height * 2 / 3,
                bounds.Height * 2 / 3
            );
        }

        void Button_TouchUpInside(object sender, EventArgs e)
        {
            var castPlayer = (OOCastPlayer)_castManager.CastPlayer;

            if (castPlayer?.State == OOOoyalaPlayerState.Playing)
                castPlayer?.Pause();
            else
                castPlayer?.Play();
        }

        [Export("updatePlayState:")]
        public void UpdatePlayState(bool isPlaying)
        {
            // change the icon.
            UIImage buttonImage = isPlaying ? _pauseImage : _playImage;
            ((UIButton)this.Cell.AccessoryView).SetBackgroundImage(buttonImage, UIControlState.Normal);
        }

        class ControllerDelegate : OOCastMiniControllerDelegate
        {
            public override void OnDismissMiniController(OOCastMiniControllerProtocol miniControllerView)
            {
                miniControllerView.Dismiss();
            }
        }
    }
}
