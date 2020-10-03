using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004B6 RID: 1206
	public static class Mathw
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x000BB3C8 File Offset: 0x000B95C8
		public static Vector3 Clamp(Vector3 v, float min, float max)
		{
			if (v.x < min)
			{
				v.x = min;
			}
			else if (v.x > max)
			{
				v.x = max;
			}
			if (v.y < min)
			{
				v.y = min;
			}
			else if (v.y > max)
			{
				v.y = max;
			}
			if (v.z < min)
			{
				v.z = min;
			}
			else if (v.z > max)
			{
				v.z = max;
			}
			return v;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000BB442 File Offset: 0x000B9642
		public static Vector3 FloatToVector3(float v)
		{
			return new Vector3(v, v, v);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000BB44C File Offset: 0x000B964C
		public static float SinDeg(float angle)
		{
			return Mathf.Sin(angle * 0.0174532924f);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000BB45C File Offset: 0x000B965C
		public static float GetMax(Vector3 v)
		{
			float num = v.x;
			if (v.y > num)
			{
				num = v.y;
			}
			if (v.z > num)
			{
				num = v.z;
			}
			return num;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000BB491 File Offset: 0x000B9691
		public static Vector3 SetMin(Vector3 v, float min)
		{
			if (v.x < min)
			{
				v.x = min;
			}
			if (v.y < min)
			{
				v.y = min;
			}
			if (v.z < min)
			{
				v.z = min;
			}
			return v;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000BB4C8 File Offset: 0x000B96C8
		public static Vector3 Snap(Vector3 v, float snapSize)
		{
			v.x = Mathf.Floor(v.x / snapSize) * snapSize;
			v.y = Mathf.Floor(v.y / snapSize) * snapSize;
			v.z = Mathf.Floor(v.z / snapSize) * snapSize;
			return v;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000BB518 File Offset: 0x000B9718
		public static Vector3 SnapRound(Vector3 v, float snapSize)
		{
			v.x = Mathf.Round(v.x / snapSize) * snapSize;
			v.y = Mathf.Round(v.y / snapSize) * snapSize;
			v.z = Mathf.Round(v.z / snapSize) * snapSize;
			return v;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000BB568 File Offset: 0x000B9768
		public static Vector3 Abs(Vector3 v)
		{
			return new Vector3((v.x < 0f) ? (-v.x) : v.x, (v.y < 0f) ? (-v.y) : v.y, (v.z < 0f) ? (-v.z) : v.z);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000BB5D0 File Offset: 0x000B97D0
		public static bool IntersectAABB3Sphere3(AABB3 box, Sphere3 sphere)
		{
			Vector3 center = sphere.center;
			Vector3 min = box.min;
			Vector3 max = box.max;
			float num = 0f;
			if (center.x < min.x)
			{
				float num2 = center.x - min.x;
				num += num2 * num2;
			}
			else if (center.x > max.x)
			{
				float num2 = center.x - max.x;
				num += num2 * num2;
			}
			if (center.y < min.y)
			{
				float num2 = center.y - min.y;
				num += num2 * num2;
			}
			else if (center.y > max.y)
			{
				float num2 = center.y - max.y;
				num += num2 * num2;
			}
			if (center.z < min.z)
			{
				float num2 = center.z - min.z;
				num += num2 * num2;
			}
			else if (center.z > max.z)
			{
				float num2 = center.z - max.z;
				num += num2 * num2;
			}
			return num <= sphere.radius * sphere.radius;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000BB6F0 File Offset: 0x000B98F0
		public static bool IntersectAABB3Triangle3(Vector3 boxCenter, Vector3 boxHalfSize, Triangle3 triangle)
		{
			Vector3 vector = triangle.a - boxCenter;
			Vector3 vector2 = triangle.b - boxCenter;
			Vector3 vector3 = triangle.c - boxCenter;
			Vector3 lhs = vector2 - vector;
			Vector3 rhs = vector3 - vector2;
			Vector3 vector4 = vector - vector3;
			float fb = Mathw.Abs(lhs[0]);
			float num = Mathw.Abs(lhs[1]);
			float fa = Mathw.Abs(lhs[2]);
			float num2;
			float num3;
			if (!Mathw.AxisTest_X01(vector, vector3, boxHalfSize, lhs[2], lhs[1], fa, num, out num2, out num3))
			{
				return false;
			}
			if (!Mathw.AxisTest_Y02(vector, vector3, boxHalfSize, lhs[2], lhs[0], fa, fb, out num2, out num3))
			{
				return false;
			}
			if (!Mathw.AxisTest_Z12(vector2, vector3, boxHalfSize, lhs[1], lhs[0], num, fb, out num2, out num3))
			{
				return false;
			}
			fb = Mathw.Abs(rhs[0]);
			num = Mathw.Abs(rhs[1]);
			fa = Mathw.Abs(rhs[2]);
			if (!Mathw.AxisTest_X01(vector, vector3, boxHalfSize, rhs[2], rhs[1], fa, num, out num2, out num3))
			{
				return false;
			}
			if (!Mathw.AxisTest_Y02(vector, vector3, boxHalfSize, rhs[2], rhs[0], fa, fb, out num2, out num3))
			{
				return false;
			}
			if (!Mathw.AxisTest_Z0(vector, vector2, boxHalfSize, rhs[1], rhs[0], num, fb, out num2, out num3))
			{
				return false;
			}
			fb = Mathw.Abs(vector4[0]);
			num = Mathw.Abs(vector4[1]);
			fa = Mathw.Abs(vector4[2]);
			if (!Mathw.AxisTest_X2(vector, vector2, boxHalfSize, vector4[2], vector4[1], fa, num, out num2, out num3))
			{
				return false;
			}
			if (!Mathw.AxisTest_Y1(vector, vector2, boxHalfSize, vector4[2], vector4[0], fa, fb, out num2, out num3))
			{
				return false;
			}
			if (!Mathw.AxisTest_Z12(vector2, vector3, boxHalfSize, vector4[1], vector4[0], num, fb, out num2, out num3))
			{
				return false;
			}
			Mathw.GetMinMax(vector[0], vector2[0], vector3[0], out num2, out num3);
			if (num2 > boxHalfSize[0] || num3 < -boxHalfSize[0])
			{
				return false;
			}
			Mathw.GetMinMax(vector[1], vector2[1], vector3[1], out num2, out num3);
			if (num2 > boxHalfSize[1] || num3 < -boxHalfSize[1])
			{
				return false;
			}
			Mathw.GetMinMax(vector[2], vector2[2], vector3[2], out num2, out num3);
			return num2 <= boxHalfSize[2] && num3 >= -boxHalfSize[2] && Mathw.PlaneBoxOverlap(Vector3.Cross(lhs, rhs), vector, boxHalfSize);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000BB9D0 File Offset: 0x000B9BD0
		private static void GetMinMax(float x0, float x1, float x2, out float min, out float max)
		{
			max = x0;
			min = x0;
			if (x1 < min)
			{
				min = x1;
			}
			else if (x1 > max)
			{
				max = x1;
			}
			if (x2 < min)
			{
				min = x2;
				return;
			}
			if (x2 > max)
			{
				max = x2;
			}
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000BBA10 File Offset: 0x000B9C10
		private static bool PlaneBoxOverlap(Vector3 normal, Vector3 vert, Vector3 maxBox)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int i = 0; i <= 2; i++)
			{
				float num = vert[i];
				if (normal[i] > 0f)
				{
					zero[i] = -maxBox[i] - num;
					zero2[i] = maxBox[i] - num;
				}
				else
				{
					zero[i] = maxBox[i] - num;
					zero2[i] = -maxBox[i] - num;
				}
			}
			return Vector3.Dot(normal, zero) <= 0f && Vector3.Dot(normal, zero2) >= 0f;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0005984A File Offset: 0x00057A4A
		private static float Abs(float v)
		{
			if (v >= 0f)
			{
				return v;
			}
			return -v;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000BBABC File Offset: 0x000B9CBC
		private static bool AxisTest_X01(Vector3 v0, Vector3 v2, Vector3 boxHalfSize, float a, float b, float fa, float fb, out float min, out float max)
		{
			float num = a * v0[1] - b * v0[2];
			float num2 = a * v2[1] - b * v2[2];
			if (num < num2)
			{
				min = num;
				max = num2;
			}
			else
			{
				min = num2;
				max = num;
			}
			float num3 = fa * boxHalfSize[1] + fb * boxHalfSize[2];
			return min <= num3 && max >= -num3;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000BBB38 File Offset: 0x000B9D38
		private static bool AxisTest_X2(Vector3 v0, Vector3 v1, Vector3 boxHalfSize, float a, float b, float fa, float fb, out float min, out float max)
		{
			float num = a * v0[1] - b * v0[2];
			float num2 = a * v1[1] - b * v1[2];
			if (num < num2)
			{
				min = num;
				max = num2;
			}
			else
			{
				min = num2;
				max = num;
			}
			float num3 = fa * boxHalfSize[1] + fb * boxHalfSize[2];
			return min <= num3 && max >= -num3;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000BBBB4 File Offset: 0x000B9DB4
		private static bool AxisTest_Y02(Vector3 v0, Vector3 v2, Vector3 boxHalfSize, float a, float b, float fa, float fb, out float min, out float max)
		{
			float num = -a * v0[0] + b * v0[2];
			float num2 = -a * v2[0] + b * v2[2];
			if (num < num2)
			{
				min = num;
				max = num2;
			}
			else
			{
				min = num2;
				max = num;
			}
			float num3 = fa * boxHalfSize[0] + fb * boxHalfSize[2];
			return min <= num3 && max >= -num3;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000BBC30 File Offset: 0x000B9E30
		private static bool AxisTest_Y1(Vector3 v0, Vector3 v1, Vector3 boxHalfSize, float a, float b, float fa, float fb, out float min, out float max)
		{
			float num = -a * v0[0] + b * v0[2];
			float num2 = -a * v1[0] + b * v1[2];
			if (num < num2)
			{
				min = num;
				max = num2;
			}
			else
			{
				min = num2;
				max = num;
			}
			float num3 = fa * boxHalfSize[0] + fb * boxHalfSize[2];
			return min <= num3 && max >= -num3;
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000BBCAC File Offset: 0x000B9EAC
		private static bool AxisTest_Z12(Vector3 v1, Vector3 v2, Vector3 boxHalfSize, float a, float b, float fa, float fb, out float min, out float max)
		{
			float num = a * v1[0] - b * v1[1];
			float num2 = a * v2[0] - b * v2[1];
			if (num2 < num)
			{
				min = num2;
				max = num;
			}
			else
			{
				min = num;
				max = num2;
			}
			float num3 = fa * boxHalfSize[0] + fb * boxHalfSize[1];
			return min <= num3 && max >= -num3;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000BBD28 File Offset: 0x000B9F28
		private static bool AxisTest_Z0(Vector3 v0, Vector3 v1, Vector3 boxHalfSize, float a, float b, float fa, float fb, out float min, out float max)
		{
			float num = a * v0[0] - b * v0[1];
			float num2 = a * v1[0] - b * v1[1];
			if (num < num2)
			{
				min = num;
				max = num2;
			}
			else
			{
				min = num2;
				max = num;
			}
			float num3 = fa * boxHalfSize[0] + fb * boxHalfSize[1];
			return min <= num3 && max >= -num3;
		}

		// Token: 0x040022FB RID: 8955
		public static readonly int[] bits = new int[]
		{
			1,
			2,
			4,
			8,
			16,
			32,
			64,
			128,
			256,
			512,
			1024,
			2048,
			4096,
			8192,
			16384,
			32768,
			65536,
			131072,
			262144,
			524288,
			1048576,
			2097152,
			4194304,
			8388608,
			16777216,
			33554432,
			67108864,
			134217728,
			268435456,
			536870912,
			1073741824,
			int.MinValue
		};
	}
}
