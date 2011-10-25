using Microsoft.Practices.Unity;
using NUnit.Framework;
using SampleInterface;
using SampleInterface.WcfStyle;
using SampleInterfaceImplementation;
using mmSquare.Betamax.Unity;

namespace Unity.Integration.Tests
{
	[TestFixture]
	public class UnityTests
	{
		[Test]
		public void Should_Record_All_Types_If_None_Explicitly_Declared()
		{
			var container = new UnityContainer();
			container.AddNewExtension<Betamax>();
			
			container.RegisterType<WidgetService, WcfWidgetService>();
			container.RegisterType<SimpleWidgetService, DummyWidgetService>();

			Assert.That(container.Resolve<SimpleWidgetService>().GetType().FullName, Is.StringEnding("Proxy"));
			Assert.That(container.Resolve<WidgetService>().GetType().FullName, Is.StringEnding("Proxy"));
		}

		[Test]
		public void Should_Record_Only_Explicitly_Declared_Types()
		{
			var container = new UnityContainer();
			container.AddNewExtension<Betamax>();
			container.Configure<Betamax>().AddInterestingType("SampleInterface.SimpleWidgetService");

			container.RegisterType<WidgetService, WcfWidgetService>();
			container.RegisterType<SimpleWidgetService, DummyWidgetService>();

			Assert.That(container.Resolve<SimpleWidgetService>().GetType().FullName, Is.StringEnding("Proxy"));
			Assert.That(container.Resolve<WidgetService>().GetType().FullName, Is.StringEnding("WcfWidgetService"));
		}
	}
}
