using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace LitJson
{
	// Token: 0x02000231 RID: 561
	public class JsonData : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary, IEquatable<JsonData>
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0005EE46 File Offset: 0x0005D046
		public int Count
		{
			get
			{
				return this.EnsureCollection().Count;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0005EE53 File Offset: 0x0005D053
		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0005EE5E File Offset: 0x0005D05E
		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0005EE69 File Offset: 0x0005D069
		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0005EE74 File Offset: 0x0005D074
		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0005EE7F File Offset: 0x0005D07F
		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0005EE8A File Offset: 0x0005D08A
		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0005EE95 File Offset: 0x0005D095
		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0005EEA0 File Offset: 0x0005D0A0
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().IsSynchronized;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0005EEB5 File Offset: 0x0005D0B5
		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().SyncRoot;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0005EEC2 File Offset: 0x0005D0C2
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().IsFixedSize;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0005EECF File Offset: 0x0005D0CF
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().IsReadOnly;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0005EEDC File Offset: 0x0005D0DC
		ICollection IDictionary.Keys
		{
			get
			{
				this.EnsureDictionary();
				IList<string> list = new List<string>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Key);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0005EF44 File Offset: 0x0005D144
		ICollection IDictionary.Values
		{
			get
			{
				this.EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Value);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0005EFAC File Offset: 0x0005D1AC
		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0005EFB4 File Offset: 0x0005D1B4
		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0005EFBC File Offset: 0x0005D1BC
		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0005EFC4 File Offset: 0x0005D1C4
		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0005EFCC File Offset: 0x0005D1CC
		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0005EFD4 File Offset: 0x0005D1D4
		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0005EFDC File Offset: 0x0005D1DC
		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().IsFixedSize;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0005EFF1 File Offset: 0x0005D1F1
		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().IsReadOnly;
			}
		}

		// Token: 0x17000138 RID: 312
		object IDictionary.this[object key]
		{
			get
			{
				return this.EnsureDictionary()[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData value2 = this.ToJsonData(value);
				this[(string)key] = value2;
			}
		}

		// Token: 0x17000139 RID: 313
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				this.EnsureDictionary();
				return this.object_list[idx].Value;
			}
			set
			{
				this.EnsureDictionary();
				JsonData value2 = this.ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = this.object_list[idx];
				this.inst_object[keyValuePair.Key] = value2;
				KeyValuePair<string, JsonData> value3 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value2);
				this.object_list[idx] = value3;
			}
		}

		// Token: 0x1700013A RID: 314
		object IList.this[int index]
		{
			get
			{
				return this.EnsureList()[index];
			}
			set
			{
				this.EnsureList();
				JsonData value2 = this.ToJsonData(value);
				this[index] = value2;
			}
		}

		// Token: 0x1700013B RID: 315
		public JsonData this[string prop_name]
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object[prop_name];
			}
			set
			{
				this.EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				if (this.inst_object.ContainsKey(prop_name))
				{
					for (int i = 0; i < this.object_list.Count; i++)
					{
						if (this.object_list[i].Key == prop_name)
						{
							this.object_list[i] = keyValuePair;
							break;
						}
					}
				}
				else
				{
					this.object_list.Add(keyValuePair);
				}
				this.inst_object[prop_name] = value;
				this.json = null;
			}
		}

		// Token: 0x1700013C RID: 316
		public JsonData this[int index]
		{
			get
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					return this.inst_array[index];
				}
				return this.object_list[index].Value;
			}
			set
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					this.inst_array[index] = value;
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = this.object_list[index];
					KeyValuePair<string, JsonData> value2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value);
					this.object_list[index] = value2;
					this.inst_object[keyValuePair.Key] = value;
				}
				this.json = null;
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00008842 File Offset: 0x00006A42
		public JsonData()
		{
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0005F24F File Offset: 0x0005D44F
		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005F265 File Offset: 0x0005D465
		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005F27B File Offset: 0x0005D47B
		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0005F291 File Offset: 0x0005D491
		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0005F2A8 File Offset: 0x0005D4A8
		public JsonData(object obj)
		{
			if (obj is bool)
			{
				this.type = JsonType.Boolean;
				this.inst_boolean = (bool)obj;
				return;
			}
			if (obj is double)
			{
				this.type = JsonType.Double;
				this.inst_double = (double)obj;
				return;
			}
			if (obj is int)
			{
				this.type = JsonType.Int;
				this.inst_int = (int)obj;
				return;
			}
			if (obj is long)
			{
				this.type = JsonType.Long;
				this.inst_long = (long)obj;
				return;
			}
			if (obj is string)
			{
				this.type = JsonType.String;
				this.inst_string = (string)obj;
				return;
			}
			throw new ArgumentException("Unable to wrap the given object with JsonData");
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005F351 File Offset: 0x0005D551
		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005F367 File Offset: 0x0005D567
		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005F36F File Offset: 0x0005D56F
		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0005F377 File Offset: 0x0005D577
		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0005F37F File Offset: 0x0005D57F
		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0005F387 File Offset: 0x0005D587
		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0005F38F File Offset: 0x0005D58F
		public static explicit operator bool(JsonData data)
		{
			if (data.type != JsonType.Boolean)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0005F3AB File Offset: 0x0005D5AB
		public static explicit operator double(JsonData data)
		{
			if (data.type != JsonType.Double)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0005F3C7 File Offset: 0x0005D5C7
		public static explicit operator int(JsonData data)
		{
			if (data.type != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0005F3E3 File Offset: 0x0005D5E3
		public static explicit operator long(JsonData data)
		{
			if (data.type != JsonType.Long)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0005F3FF File Offset: 0x0005D5FF
		public static explicit operator string(JsonData data)
		{
			if (data.type != JsonType.String)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0005F41B File Offset: 0x0005D61B
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005F42C File Offset: 0x0005D62C
		void IDictionary.Add(object key, object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, value2);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>((string)key, value2);
			this.object_list.Add(item);
			this.json = null;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0005F46F File Offset: 0x0005D66F
		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0005F48E File Offset: 0x0005D68E
		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0005F49C File Offset: 0x0005D69C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0005F4A4 File Offset: 0x0005D6A4
		void IDictionary.Remove(object key)
		{
			this.EnsureDictionary().Remove(key);
			for (int i = 0; i < this.object_list.Count; i++)
			{
				if (this.object_list[i].Key == (string)key)
				{
					this.object_list.RemoveAt(i);
					break;
				}
			}
			this.json = null;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0005F509 File Offset: 0x0005D709
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005F516 File Offset: 0x0005D716
		bool IJsonWrapper.GetBoolean()
		{
			if (this.type != JsonType.Boolean)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005F532 File Offset: 0x0005D732
		double IJsonWrapper.GetDouble()
		{
			if (this.type != JsonType.Double)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0005F54E File Offset: 0x0005D74E
		int IJsonWrapper.GetInt()
		{
			if (this.type != JsonType.Int)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0005F56A File Offset: 0x0005D76A
		long IJsonWrapper.GetLong()
		{
			if (this.type != JsonType.Long)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005F586 File Offset: 0x0005D786
		string IJsonWrapper.GetString()
		{
			if (this.type != JsonType.String)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005F5A2 File Offset: 0x0005D7A2
		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0005F5B9 File Offset: 0x0005D7B9
		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.json = null;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0005F5E7 File Offset: 0x0005D7E7
		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.json = null;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0005F5FE File Offset: 0x0005D7FE
		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0005F615 File Offset: 0x0005D815
		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0005F61D File Offset: 0x0005D81D
		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0005F626 File Offset: 0x0005D826
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005F62F File Offset: 0x0005D82F
		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005F643 File Offset: 0x0005D843
		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0005F651 File Offset: 0x0005D851
		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0005F65F File Offset: 0x0005D85F
		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0005F675 File Offset: 0x0005D875
		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0005F68A File Offset: 0x0005D88A
		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0005F69F File Offset: 0x0005D89F
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData value2 = this.ToJsonData(value);
			this[text] = value2;
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>(text, value2);
			this.object_list.Insert(idx, item);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0005F6F4 File Offset: 0x0005D8F4
		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list[idx].Key);
			this.object_list.RemoveAt(idx);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005F734 File Offset: 0x0005D934
		private ICollection EnsureCollection()
		{
			if (this.type == JsonType.Array)
			{
				return (ICollection)this.inst_array;
			}
			if (this.type == JsonType.Object)
			{
				return (ICollection)this.inst_object;
			}
			throw new InvalidOperationException("The JsonData instance has to be initialized first");
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0005F76C File Offset: 0x0005D96C
		private IDictionary EnsureDictionary()
		{
			if (this.type == JsonType.Object)
			{
				return (IDictionary)this.inst_object;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a dictionary");
			}
			this.type = JsonType.Object;
			this.inst_object = new Dictionary<string, JsonData>();
			this.object_list = new List<KeyValuePair<string, JsonData>>();
			return (IDictionary)this.inst_object;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0005F7CC File Offset: 0x0005D9CC
		private IList EnsureList()
		{
			if (this.type == JsonType.Array)
			{
				return (IList)this.inst_array;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a list");
			}
			this.type = JsonType.Array;
			this.inst_array = new List<JsonData>();
			return (IList)this.inst_array;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005F81E File Offset: 0x0005DA1E
		private JsonData ToJsonData(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is JsonData)
			{
				return (JsonData)obj;
			}
			return new JsonData(obj);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0005F83C File Offset: 0x0005DA3C
		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			if (obj.IsString)
			{
				writer.Write(obj.GetString());
				return;
			}
			if (obj.IsBoolean)
			{
				writer.Write(obj.GetBoolean());
				return;
			}
			if (obj.IsDouble)
			{
				writer.Write(obj.GetDouble());
				return;
			}
			if (obj.IsInt)
			{
				writer.Write(obj.GetInt());
				return;
			}
			if (obj.IsLong)
			{
				writer.Write(obj.GetLong());
				return;
			}
			if (obj.IsArray)
			{
				writer.WriteArrayStart();
				foreach (object obj2 in obj)
				{
					JsonData.WriteJson((JsonData)obj2, writer);
				}
				writer.WriteArrayEnd();
				return;
			}
			if (obj.IsObject)
			{
				writer.WriteObjectStart();
				foreach (object obj3 in obj)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					writer.WritePropertyName((string)dictionaryEntry.Key);
					JsonData.WriteJson((JsonData)dictionaryEntry.Value, writer);
				}
				writer.WriteObjectEnd();
				return;
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0005F984 File Offset: 0x0005DB84
		public int Add(object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(value2);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0005F9AC File Offset: 0x0005DBAC
		public void Clear()
		{
			if (this.IsObject)
			{
				((IDictionary)this).Clear();
				return;
			}
			if (this.IsArray)
			{
				((IList)this).Clear();
				return;
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0005F9CC File Offset: 0x0005DBCC
		public bool Equals(JsonData x)
		{
			if (x == null)
			{
				return false;
			}
			if (x.type != this.type)
			{
				return false;
			}
			switch (this.type)
			{
			case JsonType.None:
				return true;
			case JsonType.Object:
				return this.inst_object.Equals(x.inst_object);
			case JsonType.Array:
				return this.inst_array.Equals(x.inst_array);
			case JsonType.String:
				return this.inst_string.Equals(x.inst_string);
			case JsonType.Int:
				return this.inst_int.Equals(x.inst_int);
			case JsonType.Long:
				return this.inst_long.Equals(x.inst_long);
			case JsonType.Double:
				return this.inst_double.Equals(x.inst_double);
			case JsonType.Boolean:
				return this.inst_boolean.Equals(x.inst_boolean);
			default:
				return false;
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0005FAA1 File Offset: 0x0005DCA1
		public JsonType GetJsonType()
		{
			return this.type;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0005FAAC File Offset: 0x0005DCAC
		public void SetJsonType(JsonType type)
		{
			if (this.type == type)
			{
				return;
			}
			switch (type)
			{
			case JsonType.Object:
				this.inst_object = new Dictionary<string, JsonData>();
				this.object_list = new List<KeyValuePair<string, JsonData>>();
				break;
			case JsonType.Array:
				this.inst_array = new List<JsonData>();
				break;
			case JsonType.String:
				this.inst_string = null;
				break;
			case JsonType.Int:
				this.inst_int = 0;
				break;
			case JsonType.Long:
				this.inst_long = 0L;
				break;
			case JsonType.Double:
				this.inst_double = 0.0;
				break;
			case JsonType.Boolean:
				this.inst_boolean = false;
				break;
			}
			this.type = type;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0005FB4C File Offset: 0x0005DD4C
		public string ToJson()
		{
			if (this.json != null)
			{
				return this.json;
			}
			StringWriter stringWriter = new StringWriter();
			JsonData.WriteJson(this, new JsonWriter(stringWriter)
			{
				Validate = false
			});
			this.json = stringWriter.ToString();
			return this.json;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0005FB98 File Offset: 0x0005DD98
		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		public override string ToString()
		{
			switch (this.type)
			{
			case JsonType.Object:
				return "JsonData object";
			case JsonType.Array:
				return "JsonData array";
			case JsonType.String:
				return this.inst_string;
			case JsonType.Int:
				return this.inst_int.ToString();
			case JsonType.Long:
				return this.inst_long.ToString();
			case JsonType.Double:
				return this.inst_double.ToString();
			case JsonType.Boolean:
				return this.inst_boolean.ToString();
			default:
				return "Uninitialized JsonData";
			}
		}

		// Token: 0x04000F49 RID: 3913
		private IList<JsonData> inst_array;

		// Token: 0x04000F4A RID: 3914
		private bool inst_boolean;

		// Token: 0x04000F4B RID: 3915
		private double inst_double;

		// Token: 0x04000F4C RID: 3916
		private int inst_int;

		// Token: 0x04000F4D RID: 3917
		private long inst_long;

		// Token: 0x04000F4E RID: 3918
		private IDictionary<string, JsonData> inst_object;

		// Token: 0x04000F4F RID: 3919
		private string inst_string;

		// Token: 0x04000F50 RID: 3920
		private string json;

		// Token: 0x04000F51 RID: 3921
		private JsonType type;

		// Token: 0x04000F52 RID: 3922
		private IList<KeyValuePair<string, JsonData>> object_list;
	}
}
