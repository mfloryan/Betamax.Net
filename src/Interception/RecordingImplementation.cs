using Castle.DynamicProxy;

namespace mmSquare.Betamax
{
	public class RecordingImplementation
	{
		public TInterface CreateRecordingImplementation<TInterface, TImpl>(TImpl target) where TImpl: TInterface 
		{
			var generator = new ProxyGenerator();
			return (TInterface) generator.CreateInterfaceProxyWithTarget(typeof (TInterface), target, new MethodInterceptor(new FileTape()));
		}

		public class MethodInterceptor : IInterceptor
		{
			private readonly Tape _tape;

			public MethodInterceptor(Tape tape)
			{
				_tape = tape;
			}


			public void Intercept(IInvocation invocation)
			{
				if (invocation.Arguments != null && invocation.Arguments.Length > 0)
				{
					_tape.RecordRequest(invocation.Arguments, invocation.Method.ReflectedType, invocation.Method.Name);
				}
				invocation.Proceed();
				if (invocation.ReturnValue != null)
				{
					_tape.RecordResponse(invocation.ReturnValue, invocation.Method.ReflectedType, invocation.Method.Name);
				}
			}
		}
	}
}
