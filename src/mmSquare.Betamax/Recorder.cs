using System;
using Castle.DynamicProxy;

namespace mmSquare.Betamax
{
	public class Recorder
	{
		private readonly ProxyGenerator _generator;

		private readonly Tape _tape;

		public Recorder() : this(new FileTape())
		{
		}

		public Recorder(Tape tape)
		{
			_generator = new ProxyGenerator();
			_tape = tape;
		}

		public TInterface Record<TInterface, TImpl>(TImpl target) where TImpl: TInterface
		{
			return (TInterface) Record(typeof (TInterface), target);
		}

		public object Record(Type Interface, object implementation)
		{
			return _generator.CreateInterfaceProxyWithTarget(Interface, implementation, new MethodInterceptor(_tape));
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
				using (var token = _tape.GetToken())
				{
					RecordArguments(invocation, token);
					invocation.Proceed();
					RecordReturnValue(invocation, token);
				}
			}

			private void RecordReturnValue(IInvocation invocation, TapeToken token)
			{
				if (invocation.ReturnValue != null)
				{
					_tape.RecordResponse(invocation.ReturnValue, invocation.Method.ReflectedType, invocation.Method.Name, token);
				}
			}

			private void RecordArguments(IInvocation invocation, TapeToken token)
			{
				if (invocation.Arguments != null && invocation.Arguments.Length > 0)
				{
					_tape.RecordRequest(invocation.Arguments, invocation.Method.ReflectedType, invocation.Method.Name, token);
				}
			}
		}
	}
}