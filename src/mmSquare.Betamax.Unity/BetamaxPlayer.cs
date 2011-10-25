using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace mmSquare.Betamax.Unity
{
	public class BetamaxPlayer : UnityContainerExtension
	{
		private BetamaxPlaybackBuilderStrategy _strategy;

		private readonly BetamaxSettings _settings = new BetamaxSettings();

		protected override void Initialize()
		{
			_strategy = new BetamaxPlaybackBuilderStrategy(_settings);
			Context.Strategies.Add(_strategy, UnityBuildStage.PreCreation);
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
