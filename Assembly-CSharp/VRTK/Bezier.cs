using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002B4 RID: 692
	public static class Bezier
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x0007B4D8 File Offset: 0x000796D8
		public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * p0 + 2f * num * t * p1 + t * t * p2;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0007B520 File Offset: 0x00079720
		public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0007B554 File Offset: 0x00079754
		public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0007B5C0 File Offset: 0x000797C0
		public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return 3f * num * num * (p1 - p0) + 6f * num * t * (p2 - p1) + 3f * t * t * (p3 - p2);
		}
	}
}
