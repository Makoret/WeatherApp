using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System;

namespace WeatherApp {

	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void Close(object sender, RoutedEventArgs e) {
			this.Close();
		}

		private void NewCity(object sender, RoutedEventArgs e) {
			tb_CityName.Text = "";
		}

		private void Tb_CityName_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key.ToString() == "Return") Search(tb_CityName.Text);
		}

		private void Search(string CityName) {
			HttpClient Client = new HttpClient();
			string APIKEY = "1c6339177a2a4ae691910926221710";
			var Url = "http://api.weatherapi.com/v1/current.json?key=" + APIKEY + "&q=" + CityName;

			HttpResponseMessage response = Client.GetAsync(Url).Result;
			if (response.IsSuccessStatusCode) {
				string JsonData = response.Content.ReadAsStringAsync().Result;
				var temperatures = Temperatures.FromJson(JsonData);
				tb_TempC.Text = temperatures.Current.TempC.ToString() + "°";
				tb_Condition.Text = temperatures.Current.Condition.Text.ToString();
				//MessageBox.Show(temperatures.Location.Localtime);
				//MessageBox.Show(temperatures.Location.Localtime.Remove(0,11));
				//MessageBox.Show(temperatures.Location.Localtime.Remove(2,11));
				//MessageBox.Show(temperatures.Location.Localtime.Remove(2,0));
				//if (temperatures.Location.Localtime.Remove(2, 11).Remove() var LocalTime 
				tb_LocalTime.Text = temperatures.Location.Localtime.Remove(0, 11);
			}
		}

		private void Tb_CityName_LostFocus(object sender, RoutedEventArgs e) {
			tb_CityName.Select(0, 0);
		}

		public partial class Temperatures {
			[JsonProperty("location")]
			public Location Location { get; set; }

			[JsonProperty("current")]
			public Current Current { get; set; }
		}

		public partial class Current {
			[JsonProperty("last_updated_epoch")]
			public long LastUpdatedEpoch { get; set; }

			[JsonProperty("last_updated")]
			public string LastUpdated { get; set; }

			[JsonProperty("temp_c")]
			public double TempC { get; set; }

			[JsonProperty("temp_f")]
			public double TempF { get; set; }

			[JsonProperty("is_day")]
			public long IsDay { get; set; }

			[JsonProperty("condition")]
			public Condition Condition { get; set; }

			[JsonProperty("wind_mph")]
			public double WindMph { get; set; }

			[JsonProperty("wind_kph")]
			public double WindKph { get; set; }

			[JsonProperty("wind_degree")]
			public long WindDegree { get; set; }

			[JsonProperty("wind_dir")]
			public string WindDir { get; set; }

			[JsonProperty("pressure_mb")]
			public long PressureMb { get; set; }

			[JsonProperty("pressure_in")]
			public double PressureIn { get; set; }

			[JsonProperty("precip_mm")]
			public long PrecipMm { get; set; }

			[JsonProperty("precip_in")]
			public long PrecipIn { get; set; }

			[JsonProperty("humidity")]
			public long Humidity { get; set; }

			[JsonProperty("cloud")]
			public long Cloud { get; set; }

			[JsonProperty("feelslike_c")]
			public double FeelslikeC { get; set; }

			[JsonProperty("feelslike_f")]
			public double FeelslikeF { get; set; }

			[JsonProperty("vis_km")]
			public long VisKm { get; set; }

			[JsonProperty("vis_miles")]
			public long VisMiles { get; set; }

			[JsonProperty("uv")]
			public long Uv { get; set; }

			[JsonProperty("gust_mph")]
			public double GustMph { get; set; }

			[JsonProperty("gust_kph")]
			public double GustKph { get; set; }
		}

		public partial class Condition {
			[JsonProperty("text")]
			public string Text { get; set; }

			[JsonProperty("icon")]
			public string Icon { get; set; }

			[JsonProperty("code")]
			public long Code { get; set; }
		}

		public partial class Location {
			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("region")]
			public string Region { get; set; }

			[JsonProperty("country")]
			public string Country { get; set; }

			[JsonProperty("lat")]
			public double Lat { get; set; }

			[JsonProperty("lon")]
			public double Lon { get; set; }

			[JsonProperty("tz_id")]
			public string TzId { get; set; }

			[JsonProperty("localtime_epoch")]
			public long LocaltimeEpoch { get; set; }

			[JsonProperty("localtime")]
			public string Localtime { get; set; }
		}

		public partial class Temperatures {
			public static Temperatures FromJson(string json) => JsonConvert.DeserializeObject<Temperatures>(json, Converter.Settings);
		}

		internal static class Converter {
			public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
			};
		}
		
		private void Btn_Move_MouseDown(object sender, MouseButtonEventArgs e) {
			DragMove();
		}
	}
}
