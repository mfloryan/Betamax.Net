using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NUnit.Framework;
using SampleInterface.WcfStyle;

namespace Unity.Integration.Tests
{
	[TestFixture]
	public class ContainerTests
	{
		[Test]
		public void ShouldInterceptResolutionAndDecorateWithRecordingProxy()
		{
			var container = new UnityContainer().LoadConfiguration("ContainerWithBetamax");
			var service = container.Resolve<WidgetService>();
			Assert.That(service.GetType().FullName, Is.EqualTo("Castle.Proxies.WidgetServiceProxy"));
		}

		[Test]
		public void ShouldResolveImplementingTypeWithouthExtension()
		{
			var container = new UnityContainer().LoadConfiguration("ContainerNoExtension");
			var service = container.Resolve<WidgetService>();
			Assert.That(service.GetType().FullName, Is.EqualTo("SampleInterfaceImplementation.WcfWidgetService"));
		}

	}
}
