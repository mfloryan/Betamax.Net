using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace mmSquare.Betamax.Unity
{
	public class RecordingCallHandler : ICallHandler
	{
		private readonly FileTape _tape;

		public RecordingCallHandler()
		{
			_tape = new FileTape();
		}

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			if (input.Arguments.Count > 0)
			{
				var arguments = new Argument[input.Arguments.Count];

				for (int i = 0; i < input.Arguments.Count; i++)
				{
					arguments[i] = new Argument
					               	{
					              		Name = input.Arguments.ParameterName(i),
					              		Value = input.Arguments[i]
					              	};
				}

				_tape.RecordRequest(arguments, input.MethodBase.ReflectedType, input.MethodBase.Name);
			}

			Console.WriteLine("> Intercepting " + input.MethodBase.Name);
			Console.WriteLine("> Intercepting " + input.MethodBase.ReflectedType);

			IMethodReturn methodReturn = getNext()(input, getNext);

			Console.WriteLine("> Intercepted return value: " + methodReturn.ReturnValue.GetType().Name);

			if (methodReturn.ReturnValue != null)
			{
				_tape.RecordResponse(methodReturn.ReturnValue, input.MethodBase.ReflectedType, input.MethodBase.Name);
			}

			return methodReturn;
		}

		public int Order
		{
			get
			{
				return 10;
			}
			set
			{
				var _order = value;
			}
		}

		[Serializable]
		struct Argument
		{
			public string Name;
			public object Value;
		}

	}
}