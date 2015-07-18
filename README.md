# FormsMapCenter
Renderers to get center position of a map.

### Usage

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