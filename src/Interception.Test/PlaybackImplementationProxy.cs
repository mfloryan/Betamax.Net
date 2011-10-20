using NUnit.Framework;
using mmSquare.Betamax.Interception.Tests.Interfaces;

namespace mmSquare.Betamax.Interception.Tests
{
	[TestFixture]
	public class PlaybackImplementationProxy
	{

		[Test]
		public void CanInstantiateProxyFromInterface()
		{
			var pi = new PlaybackImplementation();
			var impl = pi.CreatePlaybackImplementation<TestInterface>();

			Assert.That(impl, Is.Not.Null);

			impl.AskAndAnswer("Question");
		}

	}
}
