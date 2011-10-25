using System.IO;
using NUnit.Framework;
using SampleInterface;
using SampleInterface.WcfStyle;
using SampleInterfaceImplementation;

namespace mmSquare.Betamax.Integration.Tests
{
	[TestFixture]
	public class RecordingImplementationTests
	{

		[Test]
		public void ShouldRecordMethodCallsTransparently()
		{
			var service = new WcfWidgetService();
			var recordingImplementation = new Recorder().Record<WidgetService, WcfWidgetService>(service);
			var request = new WidgetNameForRequest
							{
								VersionNumber = "1"
							};

			var response = recordingImplementation.GetWidgetNameFor(request);

			Assert.That(response.WidgetName, Is.EqualTo("Wcf Widget versioned 1"));

			var dir = new DirectoryInfo(@"RecordedCalls\SampleInterface.WcfStyle.WidgetService\GetWidgetNameFor");
			var requestFiles = dir.GetFiles("*-request.xml");
			var responseFiles = dir.GetFiles("*-response.xml");

			Assert.That(requestFiles.Length, Is.EqualTo(1));
			Assert.That(responseFiles.Length, Is.EqualTo(1));

			//Directory.Delete("RecordedCalls", true);
		}
	}
}
