using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace mmSquare.Betamax
{
	public class FileTape : Tape, TapeObserver
	{
		private string _tapeFolder;

		private readonly XmlObjectSerializer _serializer;

		public FileTape() : this(TapeRootPath)
		{

		}

		public FileTape(string tapeFolder)
		{
			if (string.IsNullOrEmpty(tapeFolder))
				throw new ArgumentNullException("tapeFolder");

			_tapeFolder = tapeFolder;

			_serializer = new NetDataContractSerializer();	
		}

		private const string TapeRootPath = @"RecordedCalls";
		private const string ResponseFileTypeName = @"response";
		private const string RequestFileTypeName = @"request";

		public void RecordResponse(object returnValue, Type recordedType, string methodName)
		{
			RecordResponse(returnValue, recordedType, methodName, GetToken());
		}

		public void RecordResponse(object returnValue, Type recordedType, string methodName, TapeToken token)
		{
			RecordObject(returnValue, recordedType, methodName, ResponseFileTypeName, token.Value);
		}

		public void RecordRequest(object arguments, Type recordedType, string methodName)
		{
			RecordRequest(arguments, recordedType, methodName, GetToken());
		}

		public void RecordRequest(object arguments, Type recordedType, string methodName, TapeToken token)
		{
			RecordObject(arguments, recordedType, methodName, RequestFileTypeName, token.Value);
		}

		private void RecordObject(object objectToRecord, Type reflectedType, string methodName, string fileTypeName, string token)
		{
			var path = GetPath(reflectedType, methodName, true);
			SerialiseObject(path, objectToRecord, String.Format("{0}-{1}.xml", token, fileTypeName));
		}

		private void SerialiseObject(string path, object @object, string fileName)
		{
			string tapeLocation = Path.Combine(path, fileName);

			Debug.WriteLine("Writing to tape at: " + tapeLocation);

			using (Stream stream = new FileStream(tapeLocation, FileMode.Create, FileAccess.Write))
			{
				_serializer.WriteObject(stream, @object);
				stream.Close();
			}
		}

		private object DeserialiseObject(Stream openRead)
		{
			using (openRead)
			{
				return _serializer.ReadObject(openRead);
			}
		}

		private string GetPath(Type reflectedType, string methodName, bool forceCrate)
		{
			var directory = new DirectoryInfo(Path.Combine(_tapeFolder, Path.Combine(reflectedType.ToString(), methodName)));

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

		public TapeToken GetToken()
		{
			var stamp = (long)(DateTime.UtcNow - new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
			return new TapeToken()
	       	{
	       		Value = String.Format("{0:x16}", stamp)
	       	};

		}

		public void NotifyEject(string newTapeLocation)
		{
			_tapeFolder = newTapeLocation;
		}
	}
}