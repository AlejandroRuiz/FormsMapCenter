using System;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MapTest;
using MapTest.Droid.Renderers;
using MapTest.Controls;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace MapTest.Droid.Renderers
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
			//METHOD 1 FORM CameraPosition
			var centerPosition = (Control as global::Android.Gms.Maps.MapView).Map.CameraPosition.Target;

			//METHOD 2 CALCULATE PROJECTION
			var visibleRegion = (Control as global::Android.Gms.Maps.MapView).Map.Projection.VisibleRegion;
			var x = (Control as global::Android.Gms.Maps.MapView).Map.Projection.ToScreenLocation(
				visibleRegion.FarRight);
			var y = (Control as global::Android.Gms.Maps.MapView).Map.Projection.ToScreenLocation(
				visibleRegion.NearLeft);
			var centerPoint = new Android.Graphics.Point(x.X / 2, y.Y / 2);
			var centerFromPoint = (Control as global::Android.Gms.Maps.MapView).Map.Projection.FromScreenLocation(
				centerPoint);

			//BOTH RETURNS THE SAME RESULT
			
			return new Position (centerPosition.Latitude, centerPosition.Longitude);
		}
	}
}

