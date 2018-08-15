using System;
using Android.App;
namespace Sample.OoyalaSDK.Droid
{
    public class ChromecastPlayerSelectionOption
    {
        public string Title { get; private set; }
        public string EmbedCode { get; private set; }
        public string Pcode { get; private set; }
        public string Domain { get; private set; }
        public Type Activity { get; private set; }

        public ChromecastPlayerSelectionOption(String title, String embedCode, String pcode, String domain, Type activity)
        {
            this.Title = title;
            this.EmbedCode = embedCode;
            this.Pcode = pcode;
            this.Domain = domain;
            this.Activity = activity;
        }
    }
}
