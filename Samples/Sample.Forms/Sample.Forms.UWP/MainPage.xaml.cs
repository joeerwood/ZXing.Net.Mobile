﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Sample.Forms.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			LoadApplication(new Sample.Forms.App());

			ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer.Init();
			ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingBarcodeImageViewRenderer.Init();

		}
	}
}
