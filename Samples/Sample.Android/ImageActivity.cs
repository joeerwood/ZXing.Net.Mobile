﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing.SkiaSharp;

#if NET6_0
namespace Sample.Net6
#else
namespace Sample.Android
#endif
{
    [Activity(Label = "ImageActivity")]
	public class ImageActivity : Activity
	{
		ImageView imageBarcode;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.ImageActivity);

			imageBarcode = FindViewById<ImageView>(Resource.Id.imageBarcode);

			var barcodeWriter = new BarcodeWriter
			{
				Format = ZXing.BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Width = 300,
					Height = 300
				}
			};
			var barcode = barcodeWriter.Write("ZXing.Net.Mobile");

			imageBarcode.SetImageBitmap(barcode);
		}
	}
}
