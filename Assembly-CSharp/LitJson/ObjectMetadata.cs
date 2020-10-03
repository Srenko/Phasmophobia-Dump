using System;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x02000236 RID: 566
	internal struct ObjectMetadata
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0005FDB4 File Offset: 0x0005DFB4
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x0005FDD5 File Offset: 0x0005DFD5
		public Type ElementType
		{
			get
			{
				if (this.element_type == null)
				{
					return typeof(JsonData);
				}
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0005FDDE File Offset: 0x0005DFDE
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x0005FDE6 File Offset: 0x0005DFE6
		public bool IsDictionary
		{
			get
			{
				return this.is_dictionary;
			}
			set
			{
				this.is_dictionary = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0005FDEF File Offset: 0x0005DFEF
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x0005FDF7 File Offset: 0x0005DFF7
		public IDictionary<string, PropertyMetadata> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x04000F5A RID: 3930
		private Type element_type;

		// Token: 0x04000F5B RID: 3931
		private bool is_dictionary;

		// Token: 0x04000F5C RID: 3932
		private IDictionary<string, PropertyMetadata> properties;
	}
}
