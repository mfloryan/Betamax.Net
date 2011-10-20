using SampleInterface.WcfStyle;

namespace SampleInterfaceImplementation
{
	public class WcfWidgetService : WidgetService
	{
		public WidgetNameResponse GetWidgetName()
		{
			return new WidgetNameResponse
			       	{
                        WidgetName = "Wcf Widget"
			       	};
		}

		public WidgetNameForResponse GetWidgetNameFor(WidgetNameForRequest request)
		{
			return new WidgetNameForResponse
			       	{
                        WidgetName = string.Format("Wcf Widget versioned {0}",request.VersionNumber)
			       	};
		}
	}
}
