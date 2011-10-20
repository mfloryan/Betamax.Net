using System;

namespace mmSquare.Betamax.Utilities
{
	public static class PackValue
	{
		//private static string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public static string Pack(long value)
		{
			return Convert.ToBase64String(BitConverter.GetBytes(value));
		}
	}
}
