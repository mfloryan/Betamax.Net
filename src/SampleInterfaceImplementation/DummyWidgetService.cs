namespace SampleInterfaceImplementation
{
	public class DummyWidgetService : SampleInterface.SimpleWidgetService
	{
		public string GetWidgetName()
		{
			return "Dummy Widget";
		}

		public string GetWidgetNameFor(string version)
		{
			return string.Format("Dummy Widget for {0}", version);
		}
	}
}
