using System;
using System.ComponentModel;
#if NET6_0
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Tizen;
using Microsoft.Maui.Controls.Platform;
using UI = Microsoft.Maui.Controls.Compatibility;
#else
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using UI = Xamarin.Forms;
#endif
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using ZXing.Net.Mobile.Forms.Tizen;

[assembly: ExportRenderer(typeof(ZXingScannerView), typeof(ZXingScannerViewRenderer))]

namespace ZXing.Net.Mobile.Forms.Tizen
{
	class ZXingScannerViewRenderer : ViewRenderer<ZXingScannerView, ZXing.Mobile.ZXingMediaView>
	{
		protected ZXingScannerView formsView;
		protected ZXingMediaView zxingWindow;

		public static void Init()
		{
			// Keep linker from stripping empty method
			var temp = DateTime.Now;
		}

		protected override void Dispose(bool disposing)
		{
			zxingWindow?.StopScanning();
			base.Dispose(disposing);
		}
		
		protected override void OnElementChanged(ElementChangedEventArgs<ZXingScannerView> e)
		{
			base.OnElementChanged(e);
			formsView = Element;

			if (zxingWindow == null)
			{
				formsView.AutoFocusRequested += (x, y) =>
				{
					if (zxingWindow != null)
					{
						if (x < 0 && y < 0)
							zxingWindow.AutoFocus();
						else
							zxingWindow.AutoFocus(x, y);
					}
				};
				
				zxingWindow = new ZXing.Mobile.ZXingMediaView(UI.Forms.NativeParent);
				zxingWindow.Show();
				base.SetNativeControl(zxingWindow);

				if (formsView.IsScanning)
					zxingWindow.StartScanning(formsView.RaiseScanResult, formsView.Options);
				if (!formsView.IsAnalyzing)
					zxingWindow.PauseAnalysis();
				if (formsView.IsTorchOn)
					zxingWindow.Torch(formsView.IsTorchOn);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			
			if (zxingWindow != null)
			{
				switch (e.PropertyName)
				{
					case nameof(ZXingScannerView.IsTorchOn):
						zxingWindow.Torch(formsView.IsTorchOn);
						break;
					case nameof(ZXingScannerView.IsScanning):
						if (formsView.IsScanning)
							zxingWindow.StartScanning(formsView.RaiseScanResult, formsView.Options);
						else
							zxingWindow.StopScanning();
						break;
					case nameof(ZXingScannerView.IsAnalyzing):
						if (formsView.IsAnalyzing)
							zxingWindow.ResumeAnalysis();
						else
							zxingWindow.PauseAnalysis();
						break;
				}
			}
		}
	}
}
