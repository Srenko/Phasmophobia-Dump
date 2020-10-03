using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class Serialisation
{
	// Token: 0x06000143 RID: 323 RVA: 0x00009C3C File Offset: 0x00007E3C
	public static byte[] SerialiseStruct<T>(T structToSerialise)
	{
		MemoryStream memoryStream = new MemoryStream();
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
		xmlSerializer.Serialize(xmlTextWriter, structToSerialise);
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
		if (Serialisation.isVerbose)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Serialising ",
				typeof(T),
				" to a byte array,  ",
				memoryStream.ToArray().Length,
				" bytes"
			}));
		}
		return memoryStream.ToArray();
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00009CD4 File Offset: 0x00007ED4
	public static string SerialiseStructToString<T>(T structToSerialise)
	{
		return new UTF8Encoding().GetString(Serialisation.SerialiseStruct<T>(structToSerialise));
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00009CE8 File Offset: 0x00007EE8
	public static T DeserialiseStruct<T>(byte[] xmlString)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		MemoryStream stream = new MemoryStream(xmlString);
		if (Serialisation.isVerbose)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Deserialising object of type ",
				typeof(T),
				". Given array is ",
				xmlString.Length,
				" bytes long."
			}));
		}
		T result;
		try
		{
			result = (T)((object)xmlSerializer.Deserialize(stream));
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			result = default(T);
		}
		return result;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00009D88 File Offset: 0x00007F88
	public static T DeserialiseStruct<T>(string xmlString)
	{
		if (Serialisation.isVerbose)
		{
			Debug.Log("Deserialising using the string method, now passing control to the byte converter.");
		}
		return Serialisation.DeserialiseStruct<T>(new UTF8Encoding().GetBytes(xmlString));
	}

	// Token: 0x04000191 RID: 401
	public static bool isVerbose;
}
