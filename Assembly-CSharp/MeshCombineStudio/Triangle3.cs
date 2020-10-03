using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004B2 RID: 1202
	public struct Triangle3
	{
		// Token: 0x0600257F RID: 9599 RVA: 0x000BB194 File Offset: 0x000B9394
		public void Calc()
		{
			Vector3 vector = this.a;
			Vector3 vector2 = this.b;
			Vector3 vector3 = this.c;
			Vector3 vector4 = this.b - this.a;
			Vector3 vector5 = this.c - this.a;
			Vector3 vector6 = this.c - this.b;
			float magnitude = vector4.magnitude;
			float magnitude2 = vector5.magnitude;
			float magnitude3 = vector6.magnitude;
			if (magnitude2 > magnitude && magnitude2 > magnitude3)
			{
				this.a = vector;
				this.b = vector3;
				this.c = vector2;
			}
			else if (magnitude3 > magnitude)
			{
				this.a = vector3;
				this.b = vector2;
				this.c = vector;
			}
			this.dirAb = this.b - this.a;
			this.dirAc = this.c - this.a;
			this.dirBc = this.c - this.b;
			this.ab = this.dirAb.magnitude;
			this.ac = this.dirAc.magnitude;
			this.bc = this.dirBc.magnitude;
			float num = (this.ab + this.ac + this.bc) * 0.5f;
			this.area = Mathf.Sqrt(num * (num - this.ab) * (num - this.ac) * (num - this.bc));
			this.h = 2f * this.area / this.ab;
			this.ah = Mathf.Sqrt(this.ac * this.ac - this.h * this.h);
			this.hb = this.ab - this.ah;
			this.h1 = this.a + this.dirAb * (1f / this.ab * this.ah);
		}

		// Token: 0x040022E6 RID: 8934
		public Vector3 a;

		// Token: 0x040022E7 RID: 8935
		public Vector3 b;

		// Token: 0x040022E8 RID: 8936
		public Vector3 c;

		// Token: 0x040022E9 RID: 8937
		public Vector3 dirAb;

		// Token: 0x040022EA RID: 8938
		public Vector3 dirAc;

		// Token: 0x040022EB RID: 8939
		public Vector3 dirBc;

		// Token: 0x040022EC RID: 8940
		public Vector3 h1;

		// Token: 0x040022ED RID: 8941
		public float ab;

		// Token: 0x040022EE RID: 8942
		public float ac;

		// Token: 0x040022EF RID: 8943
		public float bc;

		// Token: 0x040022F0 RID: 8944
		public float area;

		// Token: 0x040022F1 RID: 8945
		public float h;

		// Token: 0x040022F2 RID: 8946
		public float ah;

		// Token: 0x040022F3 RID: 8947
		public float hb;
	}
}
