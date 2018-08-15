using System;
using UIKit;
using CoreGraphics;
using OoyalaSDK.iOS;
using OoyalaCastSDK.iOS;
using Foundation;
using System.Collections.Generic;

namespace Sample.OoyalaSDK.iOS
{
    public class PlayerViewController : UIViewController, IOOCastMiniControllerDelegate, IOOCastManagerDelegate
    {
        private const string _embedCode = "Y1ZHB1ZDqfhCPjYYRbCEOz0GR8IsVRm1";

        UIButton _startButton;

        OOOoyalaPlayerViewController _castVC;
        OOCastMiniControllerView _castController;
        OOCastManager castManager;

        public override void LoadView()
        {
            base.LoadView();

            View.BackgroundColor = UIColor.White;

            _startButton = new UIButton(UIButtonType.System);
            _startButton.SetTitle("Click me", UIControlState.Normal);
            View.AddSubview(_startButton);

            castManager = OOCastManager.CastManagerWithAppID("4172C76F", "urn:x-cast:ooyala");
            castManager.WeakDelegate = this;

            var castPlayer = new OOOoyalaPlayer(
                pcode: "c0cTkxOqALQviQIGAHWY5hP0q9gU",
                domain: new OOPlayerDomain("http://www.ooyala.com"));
            castPlayer.InitCastManager(castManager);

            var rightButton = new UIBarButtonItem(castManager.CastButton);
            NavigationItem.RightBarButtonItem = rightButton;

            _castVC = new OOOoyalaPlayerViewController(player: castPlayer);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _startButton.TouchUpInside += _startButton_TouchUpInside;

            if (castManager.IsMiniControllerInteractionAvailable)
            {
                DisplayMiniController();
            }
        }

        void DisplayMiniController()
        {
            if (_castController == null)
            {
                var tap = new UITapGestureRecognizer(() =>
                {
                    ShowViewController(_castVC, this);
                    NavigationController.SetToolbarHidden(true, true);
                })
                { NumberOfTapsRequired = 1 };

                NavigationController.Toolbar.AddGestureRecognizer(tap);

                _castController = new OOCastMiniControllerView(
                    NavigationController.NavigationBar.Frame,
                    castManager,
                    this
                );

                _castController.Cell.BackgroundColor = UIColor.Clear;

                var player = (OOCastPlayer)castManager.CastPlayer;
                player.RegisterMiniController(_castController);
                _castController.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

                var miniController = new UIBarButtonItem(_castController);

                var negativeSeparator = new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace);
                negativeSeparator.Width = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad ? -20 : -16;

                var toolbarItems = new List<UIBarButtonItem>();
                toolbarItems.Add(negativeSeparator);
                toolbarItems.Add(miniController);

                ToolbarItems = toolbarItems.ToArray();
            }

            NavigationController.SetToolbarHidden(false, true);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            _startButton.TouchUpInside -= _startButton_TouchUpInside;
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            var bounds = View.Bounds;

            _startButton.Frame = new CGRect(
                20,
                bounds.GetMidY() - 20,
                bounds.Width - 40,
                40
            );
        }

        void _startButton_TouchUpInside(object sender, System.EventArgs e)
        {
            ShowViewController(_castVC, this);
            _castVC.NavigationItem.RightBarButtonItem = this.NavigationItem.RightBarButtonItem;
            _castVC.Player.SetEmbedCode(_embedCode);
            _castVC.Player.Play();
        }

        public UIViewController CurrentTopUIViewController
        {
            get
            {
                var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (vc.PresentedViewController != null)
                    vc = vc.PresentedViewController;
                return vc;
            }
        }
        public void OnDismissMiniController(OOCastMiniControllerProtocol miniControllerView)
        {
            NavigationController.SetToolbarHidden(true, true);
            miniControllerView.Dismiss();
            castManager.DisconnectFromOoyalaPlayer();
        }
    }
}