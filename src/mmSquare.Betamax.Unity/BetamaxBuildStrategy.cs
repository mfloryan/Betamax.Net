using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace mmSquare.Betamax.Unity
{
	public class BetamaxBuildStrategy : BuilderStrategy
	{
		private readonly IEnumerable<string> _interestingTypes;

		private Recorder _recorder;

		public BetamaxBuildStrategy(IEnumerable<string> interestingTypes)
		{
			_interestingTypes = interestingTypes;
			_recorder = new Recorder();
		}

		public BetamaxBuildStrategy(): this(new List<string>())
		{
		}

		public override void PreBuildUp(IBuilderContext context)
		{
			var key = context.OriginalBuildKey;

			if (!key.Type.IsInterface)
			{
				// We only record for interfaces
				return;
			}

			var existing = context.Existing;

			if (existing == null)
			{
				return;
			}

			if (_interestingTypes != null && _interestingTypes.Count() > 0)
			{
				if (!_interestingTypes.Contains(key.Type.FullName))
				{
					return;
				}
			}

			Debug.WriteLine("Instantiated " + existing.GetType().FullName);
			Debug.WriteLine("ResolvedFrom " + key.Type.FullName);

			var replacement = _recorder.Start(context.OriginalBuildKey.Type,context.Existing);

			Debug.WriteLine("ReplacedWith " + replacement.GetType().FullName);

			context.Existing = replacement;
		}
	}
}