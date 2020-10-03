using System;
using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x02000232 RID: 562
	internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0005FC46 File Offset: 0x0005DE46
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0005FC54 File Offset: 0x0005DE54
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0005FC80 File Offset: 0x0005DE80
		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0005FCA0 File Offset: 0x0005DEA0
		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0005FCC0 File Offset: 0x0005DEC0
		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0005FCCF File Offset: 0x0005DECF
		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0005FCDC File Offset: 0x0005DEDC
		public void Reset()
		{
			this.list_enumerator.Reset();
		}

		// Token: 0x04000F53 RID: 3923
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;
	}
}
