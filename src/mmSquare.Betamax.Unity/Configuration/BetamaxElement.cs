using System.Configuration;
using System.Xml;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace mmSquare.Betamax.Unity.Configuration
{
	public class BetamaxElement : ContainerConfiguringElement
	{
		private const string InterestingInterfacesPropertyName = "interestingInterfaces";

		protected override void ConfigureContainer(IUnityContainer container)
		{
			InterestingInterfaces.ForEach(interesting => interesting.ConfigureContainer(container));
		}

		private static readonly UnknownElementHandlerMap<BetamaxElement> unknownElementHandlerMap =
			new UnknownElementHandlerMap<BetamaxElement>
                {
                    {"interestingInterface", (be, xr) => be.ReadUnwrappedElement(xr, be.InterestingInterfaces)},
                };


		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return unknownElementHandlerMap.ProcessElement(this, elementName, reader) ||
				base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		[ConfigurationProperty(InterestingInterfacesPropertyName)]
		public InterestingInterfaceElementCollection InterestingInterfaces
		{
			get
			{
				return (InterestingInterfaceElementCollection)base[InterestingInterfacesPropertyName];
			}
		}
		
	}
}