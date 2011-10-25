namespace mmSquare.Betamax
{
	public interface TapeObserver
	{
		void NotifyEject(string newTapeLocation);
	}
}
