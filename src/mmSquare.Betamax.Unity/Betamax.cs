using System;
using System.Collections.Generic;
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
		private BetamaxBuildStrategy _strategy;

		private readonly List<string> _interestingTypes = new List<string>();

		protected override void Initialize()
		{
			_strategy = new BetamaxBuildStrategy(_interestingTypes);
			Context.Strategies.Add(_strategy, UnityBuildStage.PostInitialization);
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