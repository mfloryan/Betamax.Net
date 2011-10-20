using System;

namespace mmSquare.Betamax
{
	public interface Tape
	{
		void RecordResponse(object returnValue, Type reflectedType, string methodName);
		void RecordRequest(object arguments, Type reflectedType, string methodName);
	}
}