using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace mmSquare.Betamax.Unity
{

	// Courtesy of this great answer http://stackoverflow.com/questions/1380375/custom-object-factory-extension-for-unity

	public class BetamaxBuildStrategy : BuilderStrategy
	{

		public override void PostBuildUp(IBuilderContext context)
		{
			// System.Console.WriteLine("PostBuildUp!");

			if (context.Existing != null)
			{
				System.Console.WriteLine("Instantiated " + context.Existing.GetType().FullName);
				System.Console.WriteLine("ResolvedFrom " + context.OriginalBuildKey.Type.FullName);

				var resplacement = new RecordingImplementation().CreateRecordingImplementation(context.OriginalBuildKey.Type,
				                                                                               context.Existing);

				System.Console.WriteLine("ReplacedWith " +  resplacement.GetType().FullName);

				context.Existing = resplacement;

			}
		}
	}
}
