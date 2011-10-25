using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NUnit.Framework;
using SampleInterface;
using SampleInterface.WcfStyle;

namespace Unity.Integration.Tests.Configuration
{
	[TestFixture]
	public class BetamaxConfigurationExtensionsTests
	{

		[Test]
		public void Should_Only_Record_For_Types_Declared_In_Config_File()
		{
			var container = new UnityContainer().LoadConfiguration("BetamaxWithConfig");

			Assert.That(container.Resolve<SimpleWidgetService>().GetType().FullName, Is.StringEnding("DummyWidgetService"));
			Assert.That(container.Resolve<WidgetService>().GetType().FullName, Is.StringEnding("Proxy"));
		}

		[Test]
		public void Should_Playback_From_Configured_Tape_Location()
		{
			var container = new UnityContainer().LoadConfiguration("PlaybackBetamaxWithTapeLocation");

			var di = new DirectoryInfo(@"C:\temp\SampleInterface.SimpleWidgetService\GetWidgetName");
			di.Create();
			using (var writer = File.CreateText(Path.Combine(di.FullName, "0000-response.xml")))
			{
				writer.WriteLine("<string z:Type=\"System.String\" z:Assembly=\"0\" xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\">Substituted</string>");
				writer.Close();
			}

			var service = container.Resolve<SimpleWidgetService>();

			Assert.That(service.GetWidgetName(), Is.EqualTo("Substituted"));

			Directory.Delete(@"C:\temp\SampleInterface.SimpleWidgetService", true);
		}
	}
}
