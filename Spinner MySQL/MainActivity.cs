using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Spinner_MySQL.mCode.mMySQL;


namespace Spinner_MySQL
{
    [Activity(Label = "Spinner_MySQL", MainLauncher = true, Icon = "@drawable/icon" )]
    public class MainActivity : Activity
    {
        private Spinner sp;
        private String urlAddress = "http://10.0.2.2/android/spacecraft_select.php";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            sp = FindViewById<Spinner>(Resource.Id.sp);
            Button downloadBtn = FindViewById<Button>(Resource.Id.downloadBtn);

            downloadBtn.Click += downloadBtn_Click;

        }

        void downloadBtn_Click(object sender, EventArgs e)
        {
            new Downloader(this, urlAddress, sp).Execute();
        }
    }
}

