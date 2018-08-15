using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Cast.Framework;
using Android.Gms.Cast.Framework.Media;
using Android.Runtime;

//[assembly: MetaData("com.google.android.gms.cast.framework.OPTIONS_PROVIDER_CLASS_NAME", Value = ".CastOptionsProvider")]

namespace Sample.OoyalaSDK.Droid
{
    [Register("Sample/OoyalaSDK/Droid/CastOptionsProvider")]
    public class CastOptionsProvider : Java.Lang.Object, IOptionsProvider
    {
        private const string AppId = "4172C76F";

        public IList<SessionProvider> GetAdditionalSessionProviders(Context appContext)
        {
            return null;
        }

        public CastOptions GetCastOptions(Context appContext)
        {
            var name = Java.Lang.Class.FromType(typeof(ChromecastPlayerActivity)).Name;

            var notificationOptions = new NotificationOptions
                .Builder()
                .SetTargetActivityClassName(Java.Lang.Class.FromType(typeof(ChromecastPlayerActivity)).Name)
                .SetPlayDrawableResId(Resource.Drawable.ic_media_play_light)
                .SetPauseDrawableResId(Resource.Drawable.ic_media_pause_light)
                .Build();

            var mediaOptions = new CastMediaOptions
                .Builder()
                .SetNotificationOptions(notificationOptions)
                .SetExpandedControllerActivityClassName(Java.Lang.Class.FromType(typeof(ChromecastPlayerActivity)).Name)
                .Build();

            return new CastOptions
                .Builder()
                .SetReceiverApplicationId(AppId)
                .Build();
        }
    }
}