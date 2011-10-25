using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace mmSquare.Betamax.Unity
{

	// Here is for some inspiration:
	// Courtesy of this great answer http://stackoverflow.com/questions/1380375/custom-object-factory-extension-for-unity
	// http://www.beefycode.com/post/Decorator-Unity-Container-Extension.aspx
	// http://www.pnpguidance.net/post/GenericDecoratorChainsExampleUnityDependencyInjectionContainer.aspx

	public class Betamax : UnityContainerExtension
	{
		private BuilderStrategy _strategy;

		private readonly List<string> _interestingTypes = new List<string>();

		private BetamaxMode _mode = BetamaxMode.Record;

		public BetamaxMode Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
			}
		}

		protected override void Initialize()
		{
			if (Mode == BetamaxMode.Record)
			{
				_strategy = new BetamaxRecordingBuilderStrategy(_interestingTypes);
				Context.Strategies.Add(_strategy, UnityBuildStage.PostInitialization);
			} 

			if (Mode == BetamaxMode.Playback)
			{
				_strategy = new BetamaxPlaybackBuilderStrategy(_interestingTypes);
				Context.Strategies.Add(_strategy, UnityBuildStage.PreCreation);
			}

		}

		public Betamax AddInterestingType(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
				throw new ArgumentNullException("typeName");

			_interestingTypes.Add(typeName);

			return this;
		}
	}
}