using System;

namespace mmSquare.Betamax
{
	public interface Tape
	{
		void RecordResponse(object returnValue, Type recordedType, string methodName);
		void RecordResponse(object returnValue, Type recordedType, string methodName, TapeToken token);

		void RecordRequest(object arguments, Type recordedType, string methodName);
		void RecordRequest(object arguments, Type recordedType, string methodName, TapeToken token);

		object Playback(Type recordedType, string methodName);

		TapeToken GetToken();
	}
}