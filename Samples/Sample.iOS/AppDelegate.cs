using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

#if NET6_0
namespace Sample.Net6
#else
namespace Sample.iOS
#endif
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navController;
		HomeViewController homeViewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			//Xamarin.Calabash.Start();

			// create a new window instance based on the screen size
			window = new UIWindow(UIScreen.MainScreen.Bounds);

			homeViewController = new HomeViewController();

			// If you have defined a view, add it here:
			navController = new UINavigationController(homeViewController);

			window.RootViewController = navController;
			//window.AddSubview(navController.View);

			// make the window visible
			window.MakeKeyAndVisible();

			return true;
		}

		[Export("UITestBackdoorScan:")] // notice the colon at the end of the method name
		public NSString UITestBackdoorScan(NSString value)
		{
			homeViewController.UITestBackdoorScan(value.ToString());

			return new NSString();
		}
	}
}

