# FormsMapCenter
Renderers to get center position of a map.

### Usage

Simple Usage:
```cs
var Map = new ExtendedMap();
var CenterPos = Map.GetMapCenterLocation ();
MainPage.DisplayAlert ("Map",$"Center Position\nLat:{CenterPos.Latitude}\nLgn:{CenterPos.Longitude}","Ok");
```

ExtendedMap Code:
```cs
internal Func<Position> NativeGetMapCenterLocation{ get; set; }
public Position GetMapCenterLocation()
{
    if (NativeGetMapCenterLocation != null) {
    	return NativeGetMapCenterLocation ();
    } else {
		return new Position(0,0);
	}
}
```

iOS Renderer Code:
```cs
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
```

Android Renderer Code:
```cs
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
```

iOS Screenshot

![ScreenShot](http://1.bp.blogspot.com/-_-noJ3-HKn8/Vam_wdkYFpI/AAAAAAAAP_o/TM0IEaHwgpk/s1600/iOS%2BSimulator%2BScreen%2BShot%2BJul%2B17%252C%2B2015%252C%2B9.50.18%2BPM.png)

Android Screenshot

![ScreenShot](http://4.bp.blogspot.com/-LF0-cWoxCHI/Vam_wb-Q_9I/AAAAAAAAP_k/tCi3s5GNL0c/s1600/Screenshot_2015-07-17-21-50-20.png)