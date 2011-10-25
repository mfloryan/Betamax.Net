using Microsoft.Practices.Unity.Configuration;

namespace mmSquare.Betamax.Unity.Configuration
{
	public class BetamaxConfigurationExtension : SectionExtension
	{
		public override void AddExtensions(SectionExtensionContext context)
		{
			AddElements(context);
		}

		private void AddElements(SectionExtensionContext context)
		{
			context.AddElement<BetamaxElement>("betamax");
		}
	}
}
