using System.ServiceModel;

namespace SampleInterface.WcfStyle
{
	[ServiceContract]
	public interface WidgetService
	{
		[OperationContract]
		WidgetNameResponse GetWidgetName();

		[OperationContract]
		WidgetNameForResponse GetWidgetNameFor(WidgetNameForRequest request);
	}
}
