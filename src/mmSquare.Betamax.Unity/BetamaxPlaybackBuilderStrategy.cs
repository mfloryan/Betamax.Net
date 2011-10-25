using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace mmSquare.Betamax.Unity
{
	public class BetamaxPlaybackBuilderStrategy : BuilderStrategy
	{
		private readonly IEnumerable<string> _interestingTypes;

		private readonly Player _player;

		public BetamaxPlaybackBuilderStrategy(BetamaxSettings settings)
		{
			_interestingTypes = settings.InterestingTypes;
			var tape = new FileTape();
			settings.RegisterObserver(tape);
			_player = new Player(tape);
		}

		public override void PreBuildUp(IBuilderContext context)
		{

			var key = context.OriginalBuildKey;

			if (!key.Type.IsInterface)
			{
				base.PreBuildUp(context);
				return;
			}

			if (
				_interestingTypes != null &&
				_interestingTypes.Count() > 0 &&
				!_interestingTypes.Contains(key.Type.FullName))
			{
				base.PreBuildUp(context);
				return;
			}

			var replacement = _player.Play(key.Type);
			context.Existing = replacement;
			context.BuildComplete = true;
		}
	}
}
