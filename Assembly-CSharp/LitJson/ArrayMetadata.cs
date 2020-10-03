using System;

namespace LitJson
{
	// Token: 0x02000235 RID: 565
	internal struct ArrayMetadata
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0005FD68 File Offset: 0x0005DF68
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x0005FD89 File Offset: 0x0005DF89
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0005FD92 File Offset: 0x0005DF92
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x0005FD9A File Offset: 0x0005DF9A
		public bool IsArray
		{
			get
			{
				return this.is_array;
			}
			set
			{
				this.is_array = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0005FDA3 File Offset: 0x0005DFA3
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x0005FDAB File Offset: 0x0005DFAB
		public bool IsList
		{
			get
			{
				return this.is_list;
			}
			set
			{
				this.is_list = value;
			}
		}

		// Token: 0x04000F57 RID: 3927
		private Type element_type;

		// Token: 0x04000F58 RID: 3928
		private bool is_array;

		// Token: 0x04000F59 RID: 3929
		private bool is_list;
	}
}
