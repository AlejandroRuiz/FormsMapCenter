using System;

using Xamarin.Forms;

namespace MapTest
{
	public class App : Application
	{
		public Button MainButton { get; set; }

		public ExtendedMap Map { get; set; }
		
		public App ()
		{
			MainButton = new Button {
				VerticalOptions = LayoutOptions.End,
				Text = "Click Me"
			};
			MainButton.Clicked += MainButton_Clicked;
			Map = new ExtendedMap {
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					Children = {
						Map,
						MainButton
					}
				}
			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		void MainButton_Clicked (object sender, EventArgs e)
		{
			var CenterPos = Map.GetMapCenterLocation ();

			MainPage.DisplayAlert ("Map",$"Center Position\nLat:{CenterPos.Latitude}\nLgn:{CenterPos.Longitude}","Ok");
			Map.Pins.Add (new Xamarin.Forms.Maps.Pin () {
				Position = CenterPos,
				Address = "Address",
				Label = "Center Position"
			});
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

