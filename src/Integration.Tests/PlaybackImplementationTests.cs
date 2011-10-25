using System.IO;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using SampleInterface.WcfStyle;
using mmSquare.Betamax;

namespace Integration.Tests
{
	
	[TestFixture]
	public class PlaybackImplementationTests
	{

		class ProxyFactory
		{
			public WidgetService CreateWidgetServiceInstance()
			{
				return new Player().Play<WidgetService>();
			}
		}

		[Test]
		public void ShouldReadDataFromRecordedInfo()
		{
			Setup();

			var container = new UnityContainer();
			container.RegisterType<WidgetService>(new InjectionFactory(c => new ProxyFactory().CreateWidgetServiceInstance()));

			var service = container.Resolve<WidgetService>();

			Assert.That(service.GetWidgetName().WidgetName, Is.EqualTo("Wcf Widget"));

			TearDown();
		}

		private void TearDown()
		{
			Directory.Delete("RecordedCalls", true);
		}

		private void Setup()
		{
			const string tapeLocation = @"RecordedCalls\SampleInterface.WcfStyle.WidgetService\GetWidgetName";
			Directory.CreateDirectory(tapeLocation);
			var writer = File.CreateText(Path.Combine(tapeLocation, "0000000d39ac355b-response.xml"));
			using (writer)
			{
				writer.WriteLine(
					"<WidgetNameResponse z:Id=\"1\" z:Type=\"SampleInterface.WcfStyle.WidgetNameResponse\" z:Assembly=\"SampleInterface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\" xmlns=\"http://schemas.datacontract.org/2004/07/SampleInterface.WcfStyle\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><WidgetName z:Id=\"2\">Wcf Widget</WidgetName></WidgetNameResponse>");
				writer.Close();
			}
		}
	}
}