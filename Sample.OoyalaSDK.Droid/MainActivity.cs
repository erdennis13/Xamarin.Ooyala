using Android.App;
using Android.Widget;
using Android.OS;
using Com.Ooyala.Android;
using Com.Ooyala.Android.UI;
using Java.Lang;
using Java.Util;
using Com.Ooyala.Cast;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Gms.Cast.Framework;
using Android.Gms.Cast.Framework.Media.Widget;

namespace Sample.OoyalaSDK.Droid
{
    [Activity(Label = "Sample.OoyalaSDK.Droid", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/Theme.CastVideosTheme")]
    public class MainActivity : AppCompatActivity
    {
        List<ChromecastPlayerSelectionOption> _videoList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //CastManager.Initialize(this.ApplicationContext, NameSpace);
            //_castManager = CastManager.GetCastManager();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.start_view);
            var miniControllerFragment = (MiniControllerFragment)SupportFragmentManager.FindFragmentById(Resource.Id.cast_mini_controller);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //SetContentView(Resource.Layout.Main);
            _videoList = GetVideoList();

            var listview = FindViewById<ListView>(Resource.Id.listView);

            var adapter = new ArrayAdapter<string>(context: this, textViewResourceId: Resource.Layout.list_activity_list_item);
            foreach (var video in _videoList)
            {
                adapter.Add(video.Title);
            }
            listview.Adapter = adapter;
            listview.ItemClick += Listview_ItemClick; ;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var b = base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Menu.browse, menu);
            CastButtonFactory.SetUpMediaRouteButton(ApplicationContext, menu, Resource.Id.media_route_menu_item);
            return b;
        }

        void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = _videoList[e.Position];
            var intent = new Intent(this, item.Activity);
            var preferences = GetSharedPreferences("lastChosenParams", FileCreationMode.Private);
            preferences.Edit()
                       .PutString("embedCode", item.EmbedCode)
                       .PutString("pcode", item.Pcode)
                       .PutString("domain", item.Domain)
                       .Apply();

            StartActivity(intent);
        }

        private List<ChromecastPlayerSelectionOption> GetVideoList()
        {
            return new List<ChromecastPlayerSelectionOption>
            {
                new ChromecastPlayerSelectionOption("HLS Asset", "Y1ZHB1ZDqfhCPjYYRbCEOz0GR8IsVRm1", "c0cTkxOqALQviQIGAHWY5hP0q9gU", "http://www.ooyala.com", typeof(ChromecastPlayerActivity)),
                new ChromecastPlayerSelectionOption("MP4 Video", "h4aHB1ZDqV7hbmLEv4xSOx3FdUUuephx", "c0cTkxOqALQviQIGAHWY5hP0q9gU", "http://www.ooyala.com", typeof(ChromecastPlayerActivity)),
                new ChromecastPlayerSelectionOption("VOD CC", "92cWp0ZDpDm4Q8rzHfVK6q9m6OtFP-ww", "c0cTkxOqALQviQIGAHWY5hP0q9gU", "http://www.ooyala.com",  typeof(ChromecastPlayerActivity)),
                new ChromecastPlayerSelectionOption("Encrypted HLS Asset", "ZtZmtmbjpLGohvF5zBLvDyWexJ70KsL-", "c0cTkxOqALQviQIGAHWY5hP0q9gU", "http://www.ooyala.com",  typeof(ChromecastPlayerActivity)),
                // Will play Playready Smooth on Chromecast, Clear HLS on device
                new ChromecastPlayerSelectionOption("Playready Smooth, Clear HLS Backup", "pkMm1rdTqIAxx9DQ4-8Hyp9P_AHRe4pt", "FoeG863GnBL4IhhlFC1Q2jqbkH9m", "http://www.ooyala.com",  typeof(ChromecastPlayerActivity))
            };
        }
    }
}

