using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeverBadWeather.Infrastructure.WeatherForecastService.Model
{
	public class MetaForecast
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("geometry")]
		public PointGeometry Geometry { get; set; }
	
		[JsonProperty("properties")]
		public Forecast Properties { get; set; }
	}

	public class Forecast
	{
		[JsonProperty("meta")]
		public InlineModel Meta { get; set; }

		[JsonProperty("timeseries")]
		public List<ForecastTimeStep> Timeseries { get; set; }
	}

	public class PointGeometry
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("coordinates")]
		public List<float> Coordinates { get; set; }
	}

	public class InlineModel
	{
		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }

		[JsonProperty("units")]
		public ForecastUnits Units { get; set; }
	}

	public class ForecastTimeStep
	{
		[JsonProperty("time")]
		public string Time { get; set; }

		[JsonProperty("data")]
		public InlineModel_0 Data { get; set; }
	}

	public class ForecastUnits
	{
		[JsonProperty("air_pressure_at_sea_level")]
		public string AirPressureAtSeaLevel { get; set; }

		[JsonProperty("air_temperature")]
		public string AirTemperature { get; set; }

		[JsonProperty("cloud_area_fraction")]
		public string CloudAreaFraction { get; set; }

		[JsonProperty("precipitation_amount")]
		public string PrecipitationAmount { get; set; }

		[JsonProperty("relative_humidity")]
		public string RelativeHumidity { get; set; }

		[JsonProperty("wind_from_direction")]
		public string WindFromDirection { get; set; }

		[JsonProperty("wind_speed")]
		public string WindSpeed { get; set; }
	}

	public class InlineModel_0
	{
		[JsonProperty("instant")]
		public InlineModel_1 Instant { get; set; }

		[JsonProperty("next_1_hours")]
		public InlineModel_2 Next_1_Hours { get; set; }

		[JsonProperty("next_12_hours")]
		public InlineModel_3 Next_12_Hours { get; set; }

		[JsonProperty("next_6_hours")]
		public InlineModel_4 Next_6_Hours { get; set; }
	}

	public class InlineModel_1
	{
		[JsonProperty("details")]
		public ForecastTimeInstant Details { get; set; }
	}

	public class InlineModel_2
	{
		[JsonProperty("summary")]
		public ForecastSummary Summary { get; set; }

		[JsonProperty("details")]
		public ForecastTimePeriod Details { get; set; }
	}

	public class InlineModel_3
	{
		[JsonProperty("summary")]
		public ForecastSummary Summary { get; set; }

		[JsonProperty("details")]
		public ForecastTimePeriod Details { get; set; }
	}

	public class InlineModel_4
	{
		[JsonProperty("summary")]
		public ForecastSummary Summary { get; set; }

		[JsonProperty("details")]
		public ForecastTimePeriod Details { get; set; }
	}

	public class ForecastTimeInstant
	{
		[JsonProperty("air_pressure_at_sea_level")]
		public float AirPressureAtSeaLevel { get; set; }

		[JsonProperty("air_temperature")]
		public float AirTemperature { get; set; }

		[JsonProperty("cloud_area_fraction")]
		public float CloudAreaFraction { get; set; }

		[JsonProperty("relative_humidity")]
		public float RelativeHumidity { get; set; }

		[JsonProperty("wind_from_direction")]
		public float WindFromDirection { get; set; }

		[JsonProperty("wind_speed")]
		public float WindSpeed { get; set; }
	}

	public class ForecastSummary
	{
		[JsonProperty("symbol_code")]
		public string SymbolCode { get; set; }
	}

	public class ForecastTimePeriod
	{
		[JsonProperty("precipitation_amount")]
		public float PrecipitationAmount { get; set; }
	}
}