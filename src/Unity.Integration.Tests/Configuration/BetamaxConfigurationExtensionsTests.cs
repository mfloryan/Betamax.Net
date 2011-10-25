using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NUnit.Framework;
using SampleInterface;
using SampleInterface.WcfStyle;
using mmSquare.Betamax.Unity;

namespace Unity.Integration.Tests.Configuration
{
	[TestFixture]
	public class BetamaxConfigurationExtensionsTests
	{

		[Test]
		public void Should_Only_Record_For_Types_Declared_In_Config_File()
		{
			var container = new UnityContainer().LoadConfiguration("BetamaxWithConfig");
			Assert.That(container.Configure<Betamax>().Mode, Is.EqualTo(BetamaxMode.Record));

			Assert.That(container.Resolve<SimpleWidgetService>().GetType().FullName, Is.StringEnding("DummyWidgetService"));
			Assert.That(container.Resolve<WidgetService>().GetType().FullName, Is.StringEnding("Proxy"));
		}

		[Test]
		public void Should_Resolve_Implementing_Type_With_Playback_Extension()
		{
			var container = new UnityContainer().LoadConfiguration("PlaybackBetamaxWithConfig");

			Assert.That(container.Configure<Betamax>().Mode, Is.EqualTo(BetamaxMode.Playback));
		}
	}
}
