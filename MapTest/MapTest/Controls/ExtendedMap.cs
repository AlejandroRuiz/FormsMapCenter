using System;
using Xamarin.Forms.Maps;

namespace MapTest.Controls
{
	public class ExtendedMap:Map
	{
		public ExtendedMap ()
		{
			
		}

		internal Func<Position> NativeGetMapCenterLocation{ get; set; }

		public Position GetMapCenterLocation()
		{
			if (NativeGetMapCenterLocation != null) {
				return NativeGetMapCenterLocation ();
			} else {
				return new Position(0,0);
			}
		}
	}
}

