using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Valve.VR;

// Token: 0x020001F8 RID: 504
public static class SteamVR_Utils
{
	// Token: 0x06000E0F RID: 3599 RVA: 0x00059698 File Offset: 0x00057898
	public static Quaternion Slerp(Quaternion A, Quaternion B, float t)
	{
		float num = Mathf.Clamp(A.x * B.x + A.y * B.y + A.z * B.z + A.w * B.w, -1f, 1f);
		if (num < 0f)
		{
			B = new Quaternion(-B.x, -B.y, -B.z, -B.w);
			num = -num;
		}
		float num4;
		float num5;
		if (1f - num > 0.0001f)
		{
			float num2 = Mathf.Acos(num);
			float num3 = Mathf.Sin(num2);
			num4 = Mathf.Sin((1f - t) * num2) / num3;
			num5 = Mathf.Sin(t * num2) / num3;
		}
		else
		{
			num4 = 1f - t;
			num5 = t;
		}
		return new Quaternion(num4 * A.x + num5 * B.x, num4 * A.y + num5 * B.y, num4 * A.z + num5 * B.z, num4 * A.w + num5 * B.w);
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x000597AA File Offset: 0x000579AA
	public static Vector3 Lerp(Vector3 A, Vector3 B, float t)
	{
		return new Vector3(SteamVR_Utils.Lerp(A.x, B.x, t), SteamVR_Utils.Lerp(A.y, B.y, t), SteamVR_Utils.Lerp(A.z, B.z, t));
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x000597E7 File Offset: 0x000579E7
	public static float Lerp(float A, float B, float t)
	{
		return A + (B - A) * t;
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x000597E7 File Offset: 0x000579E7
	public static double Lerp(double A, double B, double t)
	{
		return A + (B - A) * t;
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x000597F0 File Offset: 0x000579F0
	public static float InverseLerp(Vector3 A, Vector3 B, Vector3 result)
	{
		return Vector3.Dot(result - A, B - A);
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00059805 File Offset: 0x00057A05
	public static float InverseLerp(float A, float B, float result)
	{
		return (result - A) / (B - A);
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00059805 File Offset: 0x00057A05
	public static double InverseLerp(double A, double B, double result)
	{
		return (result - A) / (B - A);
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0005980E File Offset: 0x00057A0E
	public static float Saturate(float A)
	{
		if (A < 0f)
		{
			return 0f;
		}
		if (A <= 1f)
		{
			return A;
		}
		return 1f;
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0005982D File Offset: 0x00057A2D
	public static Vector2 Saturate(Vector2 A)
	{
		return new Vector2(SteamVR_Utils.Saturate(A.x), SteamVR_Utils.Saturate(A.y));
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x0005984A File Offset: 0x00057A4A
	public static float Abs(float A)
	{
		if (A >= 0f)
		{
			return A;
		}
		return -A;
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x00059858 File Offset: 0x00057A58
	public static Vector2 Abs(Vector2 A)
	{
		return new Vector2(SteamVR_Utils.Abs(A.x), SteamVR_Utils.Abs(A.y));
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x00059875 File Offset: 0x00057A75
	private static float _copysign(float sizeval, float signval)
	{
		if (Mathf.Sign(signval) != 1f)
		{
			return -Mathf.Abs(sizeval);
		}
		return Mathf.Abs(sizeval);
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x00059894 File Offset: 0x00057A94
	public static Quaternion GetRotation(this Matrix4x4 matrix)
	{
		Quaternion quaternion = default(Quaternion);
		quaternion.w = Mathf.Sqrt(Mathf.Max(0f, 1f + matrix.m00 + matrix.m11 + matrix.m22)) / 2f;
		quaternion.x = Mathf.Sqrt(Mathf.Max(0f, 1f + matrix.m00 - matrix.m11 - matrix.m22)) / 2f;
		quaternion.y = Mathf.Sqrt(Mathf.Max(0f, 1f - matrix.m00 + matrix.m11 - matrix.m22)) / 2f;
		quaternion.z = Mathf.Sqrt(Mathf.Max(0f, 1f - matrix.m00 - matrix.m11 + matrix.m22)) / 2f;
		quaternion.x = SteamVR_Utils._copysign(quaternion.x, matrix.m21 - matrix.m12);
		quaternion.y = SteamVR_Utils._copysign(quaternion.y, matrix.m02 - matrix.m20);
		quaternion.z = SteamVR_Utils._copysign(quaternion.z, matrix.m10 - matrix.m01);
		return quaternion;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x000599E0 File Offset: 0x00057BE0
	public static Vector3 GetPosition(this Matrix4x4 matrix)
	{
		float m = matrix.m03;
		float m2 = matrix.m13;
		float m3 = matrix.m23;
		return new Vector3(m, m2, m3);
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x00059A08 File Offset: 0x00057C08
	public static Vector3 GetScale(this Matrix4x4 m)
	{
		float x = Mathf.Sqrt(m.m00 * m.m00 + m.m01 * m.m01 + m.m02 * m.m02);
		float y = Mathf.Sqrt(m.m10 * m.m10 + m.m11 * m.m11 + m.m12 * m.m12);
		float z = Mathf.Sqrt(m.m20 * m.m20 + m.m21 * m.m21 + m.m22 * m.m22);
		return new Vector3(x, y, z);
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00059AA8 File Offset: 0x00057CA8
	public static object CallSystemFn(SteamVR_Utils.SystemFn fn, params object[] args)
	{
		bool flag = !SteamVR.active && !SteamVR.usingNativeSupport;
		if (flag)
		{
			EVRInitError evrinitError = EVRInitError.None;
			OpenVR.Init(ref evrinitError, EVRApplicationType.VRApplication_Utility);
		}
		CVRSystem system = OpenVR.System;
		object result = (system != null) ? fn(system, args) : null;
		if (flag)
		{
			OpenVR.Shutdown();
		}
		return result;
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00059AF4 File Offset: 0x00057CF4
	public static void TakeStereoScreenshot(uint screenshotHandle, GameObject target, int cellSize, float ipd, ref string previewFilename, ref string VRFilename)
	{
		Texture2D texture2D = new Texture2D(4096, 4096, TextureFormat.ARGB32, false);
		Stopwatch stopwatch = new Stopwatch();
		Camera camera = null;
		stopwatch.Start();
		Camera camera2 = target.GetComponent<Camera>();
		if (camera2 == null)
		{
			if (camera == null)
			{
				camera = new GameObject().AddComponent<Camera>();
			}
			camera2 = camera;
		}
		Texture2D texture2D2 = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
		RenderTexture renderTexture = new RenderTexture(2048, 2048, 24);
		RenderTexture targetTexture = camera2.targetTexture;
		bool orthographic = camera2.orthographic;
		float fieldOfView = camera2.fieldOfView;
		float aspect = camera2.aspect;
		StereoTargetEyeMask stereoTargetEye = camera2.stereoTargetEye;
		camera2.stereoTargetEye = StereoTargetEyeMask.None;
		camera2.fieldOfView = 60f;
		camera2.orthographic = false;
		camera2.targetTexture = renderTexture;
		camera2.aspect = 1f;
		camera2.Render();
		RenderTexture.active = renderTexture;
		texture2D2.ReadPixels(new Rect(0f, 0f, (float)renderTexture.width, (float)renderTexture.height), 0, 0);
		RenderTexture.active = null;
		camera2.targetTexture = null;
		Object.DestroyImmediate(renderTexture);
		SteamVR_SphericalProjection steamVR_SphericalProjection = camera2.gameObject.AddComponent<SteamVR_SphericalProjection>();
		Vector3 localPosition = target.transform.localPosition;
		Quaternion localRotation = target.transform.localRotation;
		Vector3 position = target.transform.position;
		Quaternion lhs = Quaternion.Euler(0f, target.transform.rotation.eulerAngles.y, 0f);
		Transform transform = camera2.transform;
		int num = 1024 / cellSize;
		float num2 = 90f / (float)num;
		float num3 = num2 / 2f;
		RenderTexture renderTexture2 = new RenderTexture(cellSize, cellSize, 24);
		renderTexture2.wrapMode = TextureWrapMode.Clamp;
		renderTexture2.antiAliasing = 8;
		camera2.fieldOfView = num2;
		camera2.orthographic = false;
		camera2.targetTexture = renderTexture2;
		camera2.aspect = aspect;
		camera2.stereoTargetEye = StereoTargetEyeMask.None;
		for (int i = 0; i < num; i++)
		{
			float num4 = 90f - (float)i * num2 - num3;
			int num5 = 4096 / renderTexture2.width;
			float num6 = 360f / (float)num5;
			float num7 = num6 / 2f;
			int num8 = i * 1024 / num;
			for (int j = 0; j < 2; j++)
			{
				if (j == 1)
				{
					num4 = -num4;
					num8 = 2048 - num8 - cellSize;
				}
				for (int k = 0; k < num5; k++)
				{
					float num9 = -180f + (float)k * num6 + num7;
					int destX = k * 4096 / num5;
					int num10 = 0;
					float num11 = -ipd / 2f * Mathf.Cos(num4 * 0.0174532924f);
					for (int l = 0; l < 2; l++)
					{
						if (l == 1)
						{
							num10 = 2048;
							num11 = -num11;
						}
						Vector3 b = lhs * Quaternion.Euler(0f, num9, 0f) * new Vector3(num11, 0f, 0f);
						transform.position = position + b;
						Quaternion quaternion = Quaternion.Euler(num4, num9, 0f);
						transform.rotation = lhs * quaternion;
						Vector3 vector = quaternion * Vector3.forward;
						float num12 = num9 - num6 / 2f;
						float num13 = num12 + num6;
						float num14 = num4 + num2 / 2f;
						float num15 = num14 - num2;
						float y = (num12 + num13) / 2f;
						float x = (Mathf.Abs(num14) < Mathf.Abs(num15)) ? num14 : num15;
						Vector3 vector2 = Quaternion.Euler(x, num12, 0f) * Vector3.forward;
						Vector3 vector3 = Quaternion.Euler(x, num13, 0f) * Vector3.forward;
						Vector3 vector4 = Quaternion.Euler(num14, y, 0f) * Vector3.forward;
						Vector3 vector5 = Quaternion.Euler(num15, y, 0f) * Vector3.forward;
						Vector3 vector6 = vector2 / Vector3.Dot(vector2, vector);
						Vector3 a = vector3 / Vector3.Dot(vector3, vector);
						Vector3 vector7 = vector4 / Vector3.Dot(vector4, vector);
						Vector3 a2 = vector5 / Vector3.Dot(vector5, vector);
						Vector3 a3 = a - vector6;
						Vector3 a4 = a2 - vector7;
						float magnitude = a3.magnitude;
						float magnitude2 = a4.magnitude;
						float num16 = 1f / magnitude;
						float num17 = 1f / magnitude2;
						Vector3 uAxis = a3 * num16;
						Vector3 vAxis = a4 * num17;
						steamVR_SphericalProjection.Set(vector, num12, num13, num14, num15, uAxis, vector6, num16, vAxis, vector7, num17);
						camera2.aspect = magnitude / magnitude2;
						camera2.Render();
						RenderTexture.active = renderTexture2;
						texture2D.ReadPixels(new Rect(0f, 0f, (float)renderTexture2.width, (float)renderTexture2.height), destX, num8 + num10);
						RenderTexture.active = null;
					}
					float flProgress = ((float)i * ((float)num5 * 2f) + (float)k + (float)(j * num5)) / ((float)num * ((float)num5 * 2f));
					OpenVR.Screenshots.UpdateScreenshotProgress(screenshotHandle, flProgress);
				}
			}
		}
		OpenVR.Screenshots.UpdateScreenshotProgress(screenshotHandle, 1f);
		previewFilename += ".png";
		VRFilename += ".png";
		texture2D2.Apply();
		File.WriteAllBytes(previewFilename, texture2D2.EncodeToPNG());
		texture2D.Apply();
		File.WriteAllBytes(VRFilename, texture2D.EncodeToPNG());
		if (camera2 != camera)
		{
			camera2.targetTexture = targetTexture;
			camera2.orthographic = orthographic;
			camera2.fieldOfView = fieldOfView;
			camera2.aspect = aspect;
			camera2.stereoTargetEye = stereoTargetEye;
			target.transform.localPosition = localPosition;
			target.transform.localRotation = localRotation;
		}
		else
		{
			camera.targetTexture = null;
		}
		Object.DestroyImmediate(renderTexture2);
		Object.DestroyImmediate(steamVR_SphericalProjection);
		stopwatch.Stop();
		Debug.Log(string.Format("Screenshot took {0} seconds.", stopwatch.Elapsed));
		if (camera != null)
		{
			Object.DestroyImmediate(camera.gameObject);
		}
		Object.DestroyImmediate(texture2D2);
		Object.DestroyImmediate(texture2D);
	}

	// Token: 0x02000582 RID: 1410
	[Serializable]
	public struct RigidTransform
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000C4BBB File Offset: 0x000C2DBB
		public static SteamVR_Utils.RigidTransform identity
		{
			get
			{
				return new SteamVR_Utils.RigidTransform(Vector3.zero, Quaternion.identity);
			}
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000C4BCC File Offset: 0x000C2DCC
		public static SteamVR_Utils.RigidTransform FromLocal(Transform t)
		{
			return new SteamVR_Utils.RigidTransform(t.localPosition, t.localRotation);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000C4BDF File Offset: 0x000C2DDF
		public RigidTransform(Vector3 pos, Quaternion rot)
		{
			this.pos = pos;
			this.rot = rot;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000C4BEF File Offset: 0x000C2DEF
		public RigidTransform(Transform t)
		{
			this.pos = t.position;
			this.rot = t.rotation;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000C4C0C File Offset: 0x000C2E0C
		public RigidTransform(Transform from, Transform to)
		{
			Quaternion quaternion = Quaternion.Inverse(from.rotation);
			this.rot = quaternion * to.rotation;
			this.pos = quaternion * (to.position - from.position);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000C4C54 File Offset: 0x000C2E54
		public RigidTransform(HmdMatrix34_t pose)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			identity[0, 0] = pose.m0;
			identity[0, 1] = pose.m1;
			identity[0, 2] = -pose.m2;
			identity[0, 3] = pose.m3;
			identity[1, 0] = pose.m4;
			identity[1, 1] = pose.m5;
			identity[1, 2] = -pose.m6;
			identity[1, 3] = pose.m7;
			identity[2, 0] = -pose.m8;
			identity[2, 1] = -pose.m9;
			identity[2, 2] = pose.m10;
			identity[2, 3] = -pose.m11;
			this.pos = identity.GetPosition();
			this.rot = identity.GetRotation();
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000C4D38 File Offset: 0x000C2F38
		public RigidTransform(HmdMatrix44_t pose)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			identity[0, 0] = pose.m0;
			identity[0, 1] = pose.m1;
			identity[0, 2] = -pose.m2;
			identity[0, 3] = pose.m3;
			identity[1, 0] = pose.m4;
			identity[1, 1] = pose.m5;
			identity[1, 2] = -pose.m6;
			identity[1, 3] = pose.m7;
			identity[2, 0] = -pose.m8;
			identity[2, 1] = -pose.m9;
			identity[2, 2] = pose.m10;
			identity[2, 3] = -pose.m11;
			identity[3, 0] = pose.m12;
			identity[3, 1] = pose.m13;
			identity[3, 2] = -pose.m14;
			identity[3, 3] = pose.m15;
			this.pos = identity.GetPosition();
			this.rot = identity.GetRotation();
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000C4E5C File Offset: 0x000C305C
		public HmdMatrix44_t ToHmdMatrix44()
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(this.pos, this.rot, Vector3.one);
			return new HmdMatrix44_t
			{
				m0 = matrix4x[0, 0],
				m1 = matrix4x[0, 1],
				m2 = -matrix4x[0, 2],
				m3 = matrix4x[0, 3],
				m4 = matrix4x[1, 0],
				m5 = matrix4x[1, 1],
				m6 = -matrix4x[1, 2],
				m7 = matrix4x[1, 3],
				m8 = -matrix4x[2, 0],
				m9 = -matrix4x[2, 1],
				m10 = matrix4x[2, 2],
				m11 = -matrix4x[2, 3],
				m12 = matrix4x[3, 0],
				m13 = matrix4x[3, 1],
				m14 = -matrix4x[3, 2],
				m15 = matrix4x[3, 3]
			};
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000C4F90 File Offset: 0x000C3190
		public HmdMatrix34_t ToHmdMatrix34()
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(this.pos, this.rot, Vector3.one);
			return new HmdMatrix34_t
			{
				m0 = matrix4x[0, 0],
				m1 = matrix4x[0, 1],
				m2 = -matrix4x[0, 2],
				m3 = matrix4x[0, 3],
				m4 = matrix4x[1, 0],
				m5 = matrix4x[1, 1],
				m6 = -matrix4x[1, 2],
				m7 = matrix4x[1, 3],
				m8 = -matrix4x[2, 0],
				m9 = -matrix4x[2, 1],
				m10 = matrix4x[2, 2],
				m11 = -matrix4x[2, 3]
			};
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000C5084 File Offset: 0x000C3284
		public override bool Equals(object o)
		{
			if (o is SteamVR_Utils.RigidTransform)
			{
				SteamVR_Utils.RigidTransform rigidTransform = (SteamVR_Utils.RigidTransform)o;
				return this.pos == rigidTransform.pos && this.rot == rigidTransform.rot;
			}
			return false;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000C50C8 File Offset: 0x000C32C8
		public override int GetHashCode()
		{
			return this.pos.GetHashCode() ^ this.rot.GetHashCode();
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000C50ED File Offset: 0x000C32ED
		public static bool operator ==(SteamVR_Utils.RigidTransform a, SteamVR_Utils.RigidTransform b)
		{
			return a.pos == b.pos && a.rot == b.rot;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000C5115 File Offset: 0x000C3315
		public static bool operator !=(SteamVR_Utils.RigidTransform a, SteamVR_Utils.RigidTransform b)
		{
			return a.pos != b.pos || a.rot != b.rot;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000C5140 File Offset: 0x000C3340
		public static SteamVR_Utils.RigidTransform operator *(SteamVR_Utils.RigidTransform a, SteamVR_Utils.RigidTransform b)
		{
			return new SteamVR_Utils.RigidTransform
			{
				rot = a.rot * b.rot,
				pos = a.pos + a.rot * b.pos
			};
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000C5191 File Offset: 0x000C3391
		public void Inverse()
		{
			this.rot = Quaternion.Inverse(this.rot);
			this.pos = -(this.rot * this.pos);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000C51C0 File Offset: 0x000C33C0
		public SteamVR_Utils.RigidTransform GetInverse()
		{
			SteamVR_Utils.RigidTransform result = new SteamVR_Utils.RigidTransform(this.pos, this.rot);
			result.Inverse();
			return result;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000C51E8 File Offset: 0x000C33E8
		public void Multiply(SteamVR_Utils.RigidTransform a, SteamVR_Utils.RigidTransform b)
		{
			this.rot = a.rot * b.rot;
			this.pos = a.pos + a.rot * b.pos;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000C5223 File Offset: 0x000C3423
		public Vector3 InverseTransformPoint(Vector3 point)
		{
			return Quaternion.Inverse(this.rot) * (point - this.pos);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000C5241 File Offset: 0x000C3441
		public Vector3 TransformPoint(Vector3 point)
		{
			return this.pos + this.rot * point;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000C525A File Offset: 0x000C345A
		public static Vector3 operator *(SteamVR_Utils.RigidTransform t, Vector3 v)
		{
			return t.TransformPoint(v);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000C5264 File Offset: 0x000C3464
		public static SteamVR_Utils.RigidTransform Interpolate(SteamVR_Utils.RigidTransform a, SteamVR_Utils.RigidTransform b, float t)
		{
			return new SteamVR_Utils.RigidTransform(Vector3.Lerp(a.pos, b.pos, t), Quaternion.Slerp(a.rot, b.rot, t));
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000C528F File Offset: 0x000C348F
		public void Interpolate(SteamVR_Utils.RigidTransform to, float t)
		{
			this.pos = SteamVR_Utils.Lerp(this.pos, to.pos, t);
			this.rot = SteamVR_Utils.Slerp(this.rot, to.rot, t);
		}

		// Token: 0x04002635 RID: 9781
		public Vector3 pos;

		// Token: 0x04002636 RID: 9782
		public Quaternion rot;
	}

	// Token: 0x02000583 RID: 1411
	// (Invoke) Token: 0x060028D5 RID: 10453
	public delegate object SystemFn(CVRSystem system, params object[] args);
}
