using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace mmSquare.Betamax
{
	public class FileTape : Tape
	{
		private readonly NetDataContractSerializer _serializer;

		public FileTape()
		{
			_serializer = new NetDataContractSerializer();
		}

		private const string TapeRootPath = @"RecordedCalls";
		private const string ResponseFileTypeName = @"response";
		private const string RequestFileTypeName = @"request";

		public void RecordResponse(object returnValue, Type recordedType, string methodName)
		{
			RecordObject(returnValue , ResponseFileTypeName, recordedType, methodName);
		}

		public void RecordRequest(object arguments, Type recordedType, string methodName)
		{
			RecordObject(arguments, RequestFileTypeName, recordedType, methodName);
		}

		private void RecordObject(object arguments, string fileTypeName, Type reflectedType, string methodName)
		{
			var stamp = (long) (DateTime.UtcNow - new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
			//var nonce = Convert.ToBase64String(BitConverter.GetBytes(stamp));
			var nonce = String.Format("{0:x16}", stamp);
			var path = GetPath(reflectedType, methodName, true);
			SerialiseObject(path, arguments, String.Format("{0}-{1}.xml", nonce, fileTypeName));
		}

		private void SerialiseObject(string path, object @object, string fileName)
		{
			string tapeLocation = Path.Combine(path, fileName);

			Console.WriteLine("Writing to tape at: " + tapeLocation);

			using (Stream stream = new FileStream(tapeLocation, FileMode.Create, FileAccess.Write))
			{
				_serializer.Serialize(stream, @object);
				stream.Close();
			}
		}

		private object DeserialiseObject(Stream openRead)
		{
			using (openRead)
			{
				return _serializer.Deserialize(openRead);
			}
		}

		private string GetPath(Type reflectedType, string methodName, bool forceCrate)
		{
			var directory = new DirectoryInfo(Path.Combine(TapeRootPath, Path.Combine(reflectedType.ToString(), methodName)));

			if (forceCrate && !directory.Exists)
			{
				directory.Create();
			}
			return directory.FullName;
		}

		public object Playback(Type recordedType, string methodName)
		{
			var path = GetPath(recordedType, methodName, false);
			var di = new DirectoryInfo(path);

			if (!di.Exists)
			{
				throw new Exception("Expected tape location does not exist: " + di.FullName);
			}

			var files = di.GetFiles(string.Format("*-{0}.xml",ResponseFileTypeName));
			var file = files.FirstOrDefault();
			if (file != null)
			{
				return DeserialiseObject(file.OpenRead());
			}

			throw new Exception(string.Format("Unable to find a saved response on tape at {0}", di.FullName));
		}
	}
}