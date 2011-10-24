using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NUnit.Framework;
using SampleInterface;
using SampleInterface.WcfStyle;

namespace Unity.Integration.Tests
{
	[TestFixture]
	public class UnityInterceptionTests
	{

		[Test]
		public void ShouldInterceptSimpleServiceCalls()
		{
			string rootPathToExpectedResults = Path.Combine("RecordedCalls", "SampleInterface.SimpleWidgetService");

			var container = new UnityContainer().LoadConfiguration("InterceptionContainer");

			var widgetService = container.Resolve<SimpleWidgetService>();
			Assert.That(widgetService.GetWidgetName(), Is.EqualTo("Dummy Widget"));
			Assert.That(widgetService.GetWidgetNameFor("version 1"), Is.EqualTo("Dummy Widget for version 1"));

			Assert.That(Directory.Exists(Path.Combine(rootPathToExpectedResults, "GetWidgetName")));
			Assert.That(Directory.Exists(Path.Combine(rootPathToExpectedResults, "GetWidgetNameFor")));

			Directory.Delete("RecordedCalls", true);
		}

		[Test]
		public void ShouldInterceptServiceCalls()
		{
			string rootPathToExpectedResults = Path.Combine("RecordedCalls", "SampleInterface.WcfStyle.WidgetService");

			var container = new UnityContainer().LoadConfiguration("InterceptionContainer");

			var widgetService = container.Resolve<WidgetService>();
			Assert.That(widgetService.GetWidgetName().WidgetName, Is.EqualTo("Wcf Widget"));
			var request = new WidgetNameForRequest
			{
				VersionNumber = "version 1"
			};
			Assert.That(widgetService.GetWidgetNameFor(request).WidgetName, Is.EqualTo("Wcf Widget versioned version 1"));

			Assert.That(Directory.Exists(Path.Combine(rootPathToExpectedResults, "GetWidgetName")));
			Assert.That(Directory.Exists(Path.Combine(rootPathToExpectedResults, "GetWidgetNameFor")));

			Directory.Delete("RecordedCalls", true);
		}

	}
}
