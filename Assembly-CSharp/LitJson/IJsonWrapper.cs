using System;
using System.Collections;
using System.Collections.Specialized;

namespace LitJson
{
	// Token: 0x02000230 RID: 560
	public interface IJsonWrapper : IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000FBD RID: 4029
		bool IsArray { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000FBE RID: 4030
		bool IsBoolean { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000FBF RID: 4031
		bool IsDouble { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000FC0 RID: 4032
		bool IsInt { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000FC1 RID: 4033
		bool IsLong { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000FC2 RID: 4034
		bool IsObject { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000FC3 RID: 4035
		bool IsString { get; }

		// Token: 0x06000FC4 RID: 4036
		bool GetBoolean();

		// Token: 0x06000FC5 RID: 4037
		double GetDouble();

		// Token: 0x06000FC6 RID: 4038
		int GetInt();

		// Token: 0x06000FC7 RID: 4039
		JsonType GetJsonType();

		// Token: 0x06000FC8 RID: 4040
		long GetLong();

		// Token: 0x06000FC9 RID: 4041
		string GetString();

		// Token: 0x06000FCA RID: 4042
		void SetBoolean(bool val);

		// Token: 0x06000FCB RID: 4043
		void SetDouble(double val);

		// Token: 0x06000FCC RID: 4044
		void SetInt(int val);

		// Token: 0x06000FCD RID: 4045
		void SetJsonType(JsonType type);

		// Token: 0x06000FCE RID: 4046
		void SetLong(long val);

		// Token: 0x06000FCF RID: 4047
		void SetString(string val);

		// Token: 0x06000FD0 RID: 4048
		string ToJson();

		// Token: 0x06000FD1 RID: 4049
		void ToJson(JsonWriter writer);
	}
}
