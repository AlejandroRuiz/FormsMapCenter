using System;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MapTest;
using MapTest.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace MapTest.iOS.Renderers
{
	public class ExtendedMapRenderer:MapRenderer
	{
		public ExtendedMapRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);
			if (Element == null || Control == null)
				return;

			if (e.OldElement != null) {
				(e.OldElement as ExtendedMap).NativeGetMapCenterLocation = null;
			}

			if (e.NewElement != null) {
				(e.NewElement as ExtendedMap).NativeGetMapCenterLocation = new Func<Position> (GetMapCenterLocation);
			}
		}

		internal Position GetMapCenterLocation()
		{
			var centerPosition = (Control as MapKit.MKMapView).CenterCoordinate;
			return new Position (centerPosition.Latitude, centerPosition.Longitude);
		}
	}
}

