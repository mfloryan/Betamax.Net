namespace mmSquare.Betamax.Interception.Tests.Interfaces
{
	public class TestImplementation : TestInterface
	{
		public void Hello()
		{
			
		}

		public string HelloString()
		{
			return "HelloString";
		}

		public void AskMe(string question)
		{
			
		}

		public string AskAndAnswer(string question)
		{
			return question;
		}

		public int AskMeFewQuestions(string q1, string q2, string q3)
		{
			return q1.Length + q2.Length + q3.Length;
		}
	}
}
