using System.Diagnostics;
using Microsoft.Practices.ObjectBuilder2;

namespace mmSquare.Betamax.Unity
{
	public class BetamaxRecordingBuilderStrategy : BuilderStrategy
	{
		private readonly BetamaxSettings _settings;

		private readonly Recorder _recorder;

		public BetamaxRecordingBuilderStrategy(BetamaxSettings settings)
		{
			_settings = settings;
			var tape = new FileTape();
			settings.RegisterObserver(tape);
			_recorder = new Recorder(tape);
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

			if (_settings.InterestingTypes.Count > 0)
			{
				if (!_settings.InterestingTypes.Contains(key.Type.FullName))
				{
					return;
				}
			}

			Debug.WriteLine("Instantiated " + existing.GetType().FullName);
			Debug.WriteLine("ResolvedFrom " + key.Type.FullName);

			var replacement = _recorder.Record(context.OriginalBuildKey.Type,context.Existing);

			Debug.WriteLine("ReplacedWith " + replacement.GetType().FullName);

			context.Existing = replacement;
		}
	}
}