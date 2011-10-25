using System;
using System.Linq;
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

		public TInterface Start<TInterface, TImpl>(TImpl target) where TImpl: TInterface
		{
			return (TInterface) Start(typeof (TInterface), target);
		}

		public object Start(Type Interface, object implementation)
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
				RecordArguments(invocation);
				invocation.Proceed();
				RecordReturnValue(invocation);
			}

			private void RecordReturnValue(IInvocation invocation)
			{
				if (invocation.ReturnValue != null && invocation.ReturnValue.GetType().IsSerializable)
				{
					_tape.RecordResponse(invocation.ReturnValue, invocation.Method.ReflectedType, invocation.Method.Name);
				}
			}

			private void RecordArguments(IInvocation invocation)
			{
				if (invocation.Arguments != null && invocation.Arguments.Length > 0)
				{
					if (!invocation.Arguments.All(a => a.GetType().IsSerializable))
					{
						_tape.RecordRequest(invocation.Arguments, invocation.Method.ReflectedType, invocation.Method.Name);
					}
				}
			}
		}
	}
}