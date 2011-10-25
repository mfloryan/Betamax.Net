namespace mmSquare.Betamax.Unity
{
	interface TapeObservable
	{
		void RegisterObserver(TapeObserver observer);
		void NotifyObservers();
	}
}
