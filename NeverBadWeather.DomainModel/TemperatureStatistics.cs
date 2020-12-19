using System;
using NeverBadWeather.DomainModel.Exception;

namespace NeverBadWeather.DomainModel
{
	public class TemperatureStatistics
	{
		private float _min;
		private float _max;

		public bool Raining { get; private set; }

        public float Min
        {
            get
            {
                if (_hasNoInput) throw new CannotGiveMinOrMaxWithNoNumbersException();
                return _min;
            }
        }

        public float Max
        {
            get
            {
                if (_hasNoInput) throw new CannotGiveMinOrMaxWithNoNumbersException();
                return _max;
            }
        }

        private bool _hasNoInput = true;

        public void AddTemperature(float temperature)
        {
            if (_hasNoInput)
            {
                _max = _min = temperature;
                _hasNoInput = false;
                return;
            }
            _max = Math.Max(_max, temperature);
            _min = Math.Min(_min, temperature);
        }

        public void IsItRaining(float rain)
        {
	        Raining = rain > 0.2;
        }
    }
}
