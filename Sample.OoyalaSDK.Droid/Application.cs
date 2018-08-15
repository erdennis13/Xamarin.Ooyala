using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Com.Ooyala.Cast;
namespace Sample.OoyalaSDK.Droid
{
    [Application]
    public class Application : Android.App.Application, Android.App.Application.IActivityLifecycleCallbacks
    {
        CastManager _castManager;
        private const string NameSpace = "urn:x-cast:ooyala";

        public Application(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            RegisterActivityLifecycleCallbacks(this);
            CastManager.Initialize(this, NameSpace);
            _castManager = CastManager.GetCastManager();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}
