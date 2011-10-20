using Castle.DynamicProxy;

namespace mmSquare.Betamax
{
	public class PlaybackImplementation
	{
		public T CreatePlaybackImplementation<T>() where T: class 
		{
			var generator = new ProxyGenerator();
			return generator.CreateInterfaceProxyWithoutTarget<T>(new MethodInterceptor());
		}

		internal class MethodInterceptor : IInterceptor
		{
			private readonly FileTape _tape;

			public MethodInterceptor()
			{
				_tape = new FileTape();
			}

			public void Intercept(IInvocation invocation)
			{
				invocation.ReturnValue = _tape.Load(invocation.Method.ReflectedType, invocation.Method.Name);
			}
		}
	}
}