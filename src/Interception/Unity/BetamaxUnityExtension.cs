using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace mmSquare.Betamax.Unity
{
	public class BetamaxUnityExtension : UnityContainerExtension
	{
		private BetamaxBuildStrategy _strategy;

		public BetamaxUnityExtension()
		{
			System.Console.WriteLine("New BetamaxUnityExtension");
		}

		private string _testMe;

		public string TestMe
		{
			get
			{
				return _testMe;
			}
			set
			{
				System.Console.WriteLine("Setting TestMe to: " + value);
				_testMe = value;
			}
		}

		protected override void Initialize()
		{
			System.Console.WriteLine("BetamaxUnityExtension Init");
			_strategy = new BetamaxBuildStrategy(Context);
			Context.Strategies.Add(_strategy, UnityBuildStage.PostInitialization);
		}
	}
}