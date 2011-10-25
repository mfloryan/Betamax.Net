using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace mmSquare.Betamax.Unity
{

	// Here is for some inspiration:
	// Courtesy of this great answer http://stackoverflow.com/questions/1380375/custom-object-factory-extension-for-unity
	// http://www.beefycode.com/post/Decorator-Unity-Container-Extension.aspx
	// http://www.pnpguidance.net/post/GenericDecoratorChainsExampleUnityDependencyInjectionContainer.aspx

	public class BetamaxRecorder : UnityContainerExtension
	{
		private BetamaxRecordingBuilderStrategy _strategy;

		private readonly BetamaxSettings _settings = new BetamaxSettings();

		protected override void Initialize()
		{
			_strategy = new BetamaxRecordingBuilderStrategy(_settings);
			Context.Strategies.Add(_strategy, UnityBuildStage.PostInitialization);
		}

		public BetamaxSettings Settings
		{
			get
			{
				return _settings;
			}
		}
		
	}
}
