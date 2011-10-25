using System.Configuration;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace mmSquare.Betamax.Unity.Configuration
{
	public class InterestingInterfaceElement : DeserializableConfigurationElement
	{
		private const string NamePropertyName = "name";

		[ConfigurationProperty(NamePropertyName, IsRequired = false)]
		public string Name
		{
			get
			{
				return (string)base[NamePropertyName];
			}
			set
			{
				base[NamePropertyName] = value;
			}
		}

		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeString(NamePropertyName, Name);
		}

		internal void ConfigureContainer(BetamaxSettings settings)
		{
			settings.InterestingTypes.Add(Name);
		}

	}
}