using System.Collections.Generic;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
#endif

namespace ZXing.Mobile
{
	public static class VisualTreeHelperExt
	{
		// The method traverses the visual tree lazily, layer by layer
		// and returns the objects of the desired type
		public static T GetFirstChildOfType<T>(this DependencyObject start) where T : class
		{
			var queue = new Queue<DependencyObject>();
			queue.Enqueue(start);

			while (queue.Count > 0)
			{
				var item = queue.Dequeue();

				var realItem = item as T;
				if (realItem != null)
				{
					return realItem;
				}

				var count = VisualTreeHelper.GetChildrenCount(item);
				for (var i = 0; i < count; i++)
				{
					queue.Enqueue(VisualTreeHelper.GetChild(item, i));
				}
			}
			return null;
		}
	}
}
