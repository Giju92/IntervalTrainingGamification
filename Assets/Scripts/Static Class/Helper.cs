using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class Helper 
{
	public const int MAX_ACCURACY = 8;
	public const int MAX_ACCURACY_RUN = 8;
	public const int MIN_ACCURACY = 0;
	public const int ANIMAL_COUNT = 20;
	public const int FRUIT_COUNT = 12;
	public static int MAX_DISTANCE = 1;
	public static int DURATION_TIME = 5;
	public static int DURATION_TIME_WALKING = 0;
	public static int MAX_ATTEMPT = 5;

	public static string Serialize<T>(this T toSerialize)
	{
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringWriter writer = new StringWriter ();
		xml.Serialize (writer, toSerialize);
		return writer.ToString ();
	}

	public static T Deserialize<T>(this string toDeserialize)
	{
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringReader reader = new StringReader (toDeserialize);
		return (T)xml.Deserialize(reader);
	}

}

