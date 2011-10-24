using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NUnit.Framework;
using SampleInterface.WcfStyle;

namespace Unity.Integration.Tests
{
	[TestFixture]
	public class UnityTests
	{
		[Test]
		public void ShouldInterceptResolutionAndDecorateWithRecordingThingy()
		{
			var container = new UnityContainer().LoadConfiguration("Container1");
			System.Console.WriteLine("Before resolution");
			var service = container.Resolve<WidgetService>();
			System.Console.WriteLine("After resolution");
			service.GetWidgetName();

			var service2 = container.Resolve<WidgetService>();
			service2.GetWidgetNameFor(new WidgetNameForRequest()
			                          	{
			                          		VersionNumber = "2"
			                          	});
		}
	}
}
