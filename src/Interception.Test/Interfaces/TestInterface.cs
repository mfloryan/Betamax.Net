namespace mmSquare.Betamax.Interception.Tests.Interfaces
{
	public interface TestInterface
	{
		void Hello();

		string HelloString();

		void AskMe(string question);

		string AskAndAnswer(string question);

		int AskMeFewQuestions(string q1, string q2, string q3);
	}
}
