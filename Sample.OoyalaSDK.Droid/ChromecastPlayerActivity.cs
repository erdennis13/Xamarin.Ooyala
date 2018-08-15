
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Com.Ooyala.Cast;
using Com.Ooyala.Android;
using Com.Ooyala.Android.Configuration;
using Com.Ooyala.Android.UI;
using Java.Util;
using Java.Lang;
using Android.Gms.Cast.Framework;
using Com.Ooyala.Cast.Mediainfo;

namespace Sample.OoyalaSDK.Droid
{
    [Activity(Label = "ChromecastPlayerActivity")]
    public class ChromecastPlayerActivity : AppCompatActivity, IObserver
    {
        CastManager _castManager;
        CastViewManager _castViewManager;

        private string _embedCode;
        private string _pcode;
        private string _domain;

        private OoyalaPlayer _player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            var preferences = GetSharedPreferences("lastChosenParams", FileCreationMode.Private);

            _embedCode = preferences.GetString("embedCode", "");
            _pcode = preferences.GetString("pcode", "");
            _domain = preferences.GetString("domain", "");

            SetContentView(Resource.Layout.player_activity);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _castManager = CastManager.GetCastManager();
            InitOoyala();
            _castViewManager = new CastViewManager(this, _castManager);
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (_castManager != null && _player != null)
                _castManager.RegisterWithOoyalaPlayer(_player);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var b = base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Menu.browse, menu);
            CastButtonFactory.SetUpMediaRouteButton(ApplicationContext, menu, Resource.Id.media_route_menu_item);
            return b;
        }

        private void InitOoyala()
        {
            PlayerDomain playerDomain = new PlayerDomain(_domain);
            var options = new Options.Builder().SetUseExoPlayer(true).Build();
            OoyalaPlayerLayout ooyalaPlayerLayout = FindViewById<OoyalaPlayerLayout>(Resource.Id.ooyalaPlayer);
            _player = new OoyalaPlayer(_pcode, playerDomain, options);
            var controller = new OoyalaPlayerLayoutController(ooyalaPlayerLayout, _player);
            _castManager.RegisterWithOoyalaPlayer(_player);
            _player.AddObserver(this);
            Play(_embedCode);
        }

        private void Play(string code)
        {
            _player.SetEmbedCode(code);
            _player.Play();
        }

        public void Update(Observable o, Java.Lang.Object arg)
        {
            if (arg != _player) return;

            OoyalaNotification notification = null;
            if (arg is OoyalaNotification)
                notification = arg as OoyalaNotification;

            var argString = OoyalaNotification.GetNameOrUnknown(arg);
            if (argString == OoyalaPlayer.TimeChangedNotificationName)
                return;

            if (argString == OoyalaPlayer.CurrentItemChangedNotificationName)
            {
                UpdateCustView(notification);
            }
            else if (argString == OoyalaPlayer.ErrorNotificationName)
            {
                var msg = "Error Event Received";
                if (_player != null && _player.Error != null)
                {
                    System.Diagnostics.Debug.WriteLine($"{msg}, {_player.Error}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            }

            if (argString == OoyalaPlayer.StateChangedNotificationName)
            {
                if (_player.IsInCastMode)
                {
                    OoyalaPlayer.State state = _player.GetState();
                    _castViewManager.UpdateCastState(this, state);
                }
            }

            // Automation Hook: to write Notifications to a temporary file on the device/emulator
            var text = "Notification Received: " + argString + " - state: " + _player.GetState();
            System.Diagnostics.Debug.WriteLine(text);
        }

        private void UpdateCustView(OoyalaNotification notification)
        {
            if (notification?.Data is VideoData data)
            {
                _castViewManager.ConfigureCastView(data.Title, data.Description, data.Url);
            }
            //else if (_player.getCurrentItem() != null)
            //{
            //castViewManager.configureCastView(
            //    player.getCurrentItem().getTitle(),
            //    player.getCurrentItem().getDescription(),
            //    player.getCurrentItem().getPromoImageURL(0, 0)
            //);
            //}
        }

        protected override void OnStop()
        {
            base.OnStop();
            _castManager?.DeregisterFromOoyalaPlayer();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _player?.Resume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _player?.Suspend();
        }
    }
}
