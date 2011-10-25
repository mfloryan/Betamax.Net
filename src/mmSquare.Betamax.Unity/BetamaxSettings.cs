using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace mmSquare.Betamax.Unity
{
	public class BetamaxSettings : TapeObservable
	{
		private readonly List<string> _interestingTypes = new List<string>();

		public IList<string> InterestingTypes
		{
			get
			{
				return _interestingTypes;
			}
		}

		private string _tapesLocation;

		public string TapesLocation
		{
			get
			{
				return _tapesLocation;
			}
			set
			{
				_tapesLocation = value;
				NotifyObservers();
			}
		}

		readonly IList<TapeObserver> _observers = new List<TapeObserver>();

		public void RegisterObserver(TapeObserver observer)
		{
			_observers.Add(observer);
		}

		public void NotifyObservers()
		{
			_observers.ForEach(o => o.NotifyEject(TapesLocation));
		}
	}
}
