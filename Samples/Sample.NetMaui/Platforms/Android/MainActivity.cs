using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
#if !NET6_0
using Xamarin.Essentials
#endif

#if NET6_0
namespace Sample.NetMaui
#else
namespace Sample.Forms.Droid
#endif
{
	[Activity(Label = "ZXing Forms", Icon = "@mipmap/launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
#if NET6_0
    public class MainActivity : MauiAppCompatActivity
#else
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
#endif
    {
        App formsApp;

#if !NET6_0
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

			formsApp = new App();
			LoadApplication(new App());
    }
#endif
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		[Java.Interop.Export("UITestBackdoorScan")]
		public Java.Lang.String UITestBackdoorScan(string param)
		{
			formsApp.UITestBackdoorScan(param);
			return new Java.Lang.String();
		}
	}
}