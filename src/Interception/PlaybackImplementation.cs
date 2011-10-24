using Castle.DynamicProxy;

namespace mmSquare.Betamax
{
	public class PlaybackImplementation
	{
		public T CreatePlaybackImplementation<T>() where T: class 
		{
			var generator = new ProxyGenerator();
			return generator.CreateInterfaceProxyWithoutTarget<T>(new MethodInterceptor(new FileTape()));
		}

		internal class MethodInterceptor : IInterceptor
		{
			private readonly Tape _tape;

			public MethodInterceptor(Tape tape)
			{
				_tape = tape;
			}

			public void Intercept(IInvocation invocation)
			{
				invocation.ReturnValue = _tape.Playback(invocation.Method.ReflectedType, invocation.Method.Name);
			}
		}
	}
}