using System;
using Castle.DynamicProxy;

namespace mmSquare.Betamax
{
	public class RecordingImplementation
	{
		private readonly ProxyGenerator _generator;

		public RecordingImplementation()
		{
			_generator = new ProxyGenerator();
		}

		public TInterface CreateRecordingImplementation<TInterface, TImpl>(TImpl target) where TImpl: TInterface
		{
			return (TInterface) CreateRecordingImplementation(typeof (TInterface), target);
		}

		public object CreateRecordingImplementation(Type Interface, object implementation)
		{
			return _generator.CreateInterfaceProxyWithTarget(Interface, implementation, new MethodInterceptor(new FileTape()));
		} 

		private class MethodInterceptor : IInterceptor
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
