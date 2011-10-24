using System.Diagnostics;
using Microsoft.Practices.ObjectBuilder2;

namespace mmSquare.Betamax.Unity
{

	public class BetamaxBuildStrategy : BuilderStrategy
	{

		public override void PreBuildUp(IBuilderContext context)
		{
			var key = context.OriginalBuildKey;

			if (!key.Type.IsInterface)
			{
				// We only record for interfaces
				// TODO: Configure explicitly what to record
				return;
			}

			var existing = context.Existing;

			if (existing == null)
			{
				return;
			}

			Debug.WriteLine("Instantiated " + existing.GetType().FullName);
			Debug.WriteLine("ResolvedFrom " + key.Type.FullName);

			var replacement = new RecordingImplementation().CreateRecordingImplementation(context.OriginalBuildKey.Type,context.Existing);

			Debug.WriteLine("ReplacedWith " + replacement.GetType().FullName);

			context.Existing = replacement;
		}
	}
}
