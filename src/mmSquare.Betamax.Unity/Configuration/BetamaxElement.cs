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
		private const string ModePropertyName = "mode";

		[ConfigurationProperty(ModePropertyName, IsRequired = false)]
		public string Mode
		{
			get
			{
				return (string)base[ModePropertyName];
			}
			set
			{
				base[ModePropertyName] = value;
			}
		}

		private const string TapesPropertyName = "tapes";

		[ConfigurationProperty(TapesPropertyName, IsRequired = false)]
		public string Tapes
		{
			get
			{
				return (string)base[TapesPropertyName];
			}
			set
			{
				base[TapesPropertyName] = value;
			}
		}
		
		private const string InterestingInterfacesPropertyName = "interestingInterfaces";

		protected override void ConfigureContainer(IUnityContainer container)
		{
			BetamaxSettings settings = null;
			
			var recorder = container.Configure<BetamaxRecorder>();
			if (recorder != null)
			{
				settings = recorder.Settings;
			} else
			{
				var player = container.Configure<BetamaxPlayer>();
				if (player != null)
				{
					settings = player.Settings;
				}
			}

			if (settings == null)
				return;
			
			if (!string.IsNullOrEmpty(Tapes))
			{
				settings.TapesLocation = Tapes;
			}

			InterestingInterfaces.ForEach(interesting => interesting.ConfigureContainer(settings));
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