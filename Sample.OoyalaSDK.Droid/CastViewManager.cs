using System;
using Android.App;
using Com.Ooyala.Cast;
using Android.Views;
using Android.Widget;
using Android.Content;
using Com.Ooyala.Android;

namespace Sample.OoyalaSDK.Droid
{
    public class CastViewManager
    {
        private View _castView;
        private TextView _stateTextView;

        public CastViewManager(Activity activity, CastManager manager)
        {
            _castView = activity.LayoutInflater.Inflate(Resource.Layout.cast_video_view, null);
            manager.CastView = _castView;
            _stateTextView = _castView.FindViewById<TextView>(Resource.Id.castStateTextView);
        }

        public void ConfigureCastView(string title, string description, string imageUrl)
        {
            var castBackgroundImage = _castView.FindViewById(Resource.Id.castBackgroundImage);

            // Update the ImageView on a separate thread
            //new Thread(new UpdateImageViewRunnable(castBackgroundImage, imageUrl)).start();

            TextView videoTitle = _castView.FindViewById<TextView>(Resource.Id.videoTitle);
            videoTitle.Text = title;

            TextView videoDescription = _castView.FindViewById<TextView>(Resource.Id.videoDescription);
            videoDescription.Text = description;
        }

        public void UpdateCastState(Context c, OoyalaPlayer.State state)
        {
            var castDeviceName = CastManager.GetCastManager().DeviceName;
            if (state == OoyalaPlayer.State.Loading)
            {
                _stateTextView.Text = c.GetString(Resource.String.loading);
            }
            else if (state == OoyalaPlayer.State.Playing || state == OoyalaPlayer.State.Paused)
            {
                var statusString = $"{c.GetString(Resource.String.castingTo)} {castDeviceName}";
                _stateTextView.Text = statusString;
            }
            else
            {
                _stateTextView.Text = "";
            }
        }
    }
}
