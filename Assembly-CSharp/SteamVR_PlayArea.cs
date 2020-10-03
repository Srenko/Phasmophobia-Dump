using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;

// Token: 0x020001EE RID: 494
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SteamVR_PlayArea : MonoBehaviour
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x00056FC0 File Offset: 0x000551C0
	public static bool GetBounds(SteamVR_PlayArea.Size size, ref HmdQuad_t pRect)
	{
		if (size == SteamVR_PlayArea.Size.Calibrated)
		{
			bool flag = !SteamVR.active && !SteamVR.usingNativeSupport;
			if (flag)
			{
				EVRInitError evrinitError = EVRInitError.None;
				OpenVR.Init(ref evrinitError, EVRApplicationType.VRApplication_Utility);
			}
			CVRChaperone chaperone = OpenVR.Chaperone;
			bool flag2 = chaperone != null && chaperone.GetPlayAreaRect(ref pRect);
			if (!flag2)
			{
				Debug.LogWarning("Failed to get Calibrated Play Area bounds!  Make sure you have tracking first, and that your space is calibrated.");
			}
			if (flag)
			{
				OpenVR.Shutdown();
			}
			return flag2;
		}
		try
		{
			string[] array = size.ToString().Substring(1).Split(new char[]
			{
				'x'
			}, 2);
			float num = float.Parse(array[0]) / 200f;
			float num2 = float.Parse(array[1]) / 200f;
			pRect.vCorners0.v0 = num;
			pRect.vCorners0.v1 = 0f;
			pRect.vCorners0.v2 = -num2;
			pRect.vCorners1.v0 = -num;
			pRect.vCorners1.v1 = 0f;
			pRect.vCorners1.v2 = -num2;
			pRect.vCorners2.v0 = -num;
			pRect.vCorners2.v1 = 0f;
			pRect.vCorners2.v2 = num2;
			pRect.vCorners3.v0 = num;
			pRect.vCorners3.v1 = 0f;
			pRect.vCorners3.v2 = num2;
			return true;
		}
		catch
		{
		}
		return false;
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00057124 File Offset: 0x00055324
	public void BuildMesh()
	{
		HmdQuad_t hmdQuad_t = default(HmdQuad_t);
		if (!SteamVR_PlayArea.GetBounds(this.size, ref hmdQuad_t))
		{
			return;
		}
		HmdVector3_t[] array = new HmdVector3_t[]
		{
			hmdQuad_t.vCorners0,
			hmdQuad_t.vCorners1,
			hmdQuad_t.vCorners2,
			hmdQuad_t.vCorners3
		};
		this.vertices = new Vector3[array.Length * 2];
		for (int i = 0; i < array.Length; i++)
		{
			HmdVector3_t hmdVector3_t = array[i];
			this.vertices[i] = new Vector3(hmdVector3_t.v0, 0.01f, hmdVector3_t.v2);
		}
		if (this.borderThickness == 0f)
		{
			base.GetComponent<MeshFilter>().mesh = null;
			return;
		}
		for (int j = 0; j < array.Length; j++)
		{
			int num = (j + 1) % array.Length;
			int num2 = (j + array.Length - 1) % array.Length;
			Vector3 normalized = (this.vertices[num] - this.vertices[j]).normalized;
			Vector3 normalized2 = (this.vertices[num2] - this.vertices[j]).normalized;
			Vector3 vector = this.vertices[j];
			vector += Vector3.Cross(normalized, Vector3.up) * this.borderThickness;
			vector += Vector3.Cross(normalized2, Vector3.down) * this.borderThickness;
			this.vertices[array.Length + j] = vector;
		}
		int[] triangles = new int[]
		{
			0,
			4,
			1,
			1,
			4,
			5,
			1,
			5,
			2,
			2,
			5,
			6,
			2,
			6,
			3,
			3,
			6,
			7,
			3,
			7,
			0,
			0,
			7,
			4
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		Color[] colors = new Color[]
		{
			this.color,
			this.color,
			this.color,
			this.color,
			new Color(this.color.r, this.color.g, this.color.b, 0f),
			new Color(this.color.r, this.color.g, this.color.b, 0f),
			new Color(this.color.r, this.color.g, this.color.b, 0f),
			new Color(this.color.r, this.color.g, this.color.b, 0f)
		};
		Mesh mesh = new Mesh();
		base.GetComponent<MeshFilter>().mesh = mesh;
		mesh.vertices = this.vertices;
		mesh.uv = uv;
		mesh.colors = colors;
		mesh.triangles = triangles;
		MeshRenderer component = base.GetComponent<MeshRenderer>();
		component.material = new Material(Shader.Find("Sprites/Default"));
		component.reflectionProbeUsage = ReflectionProbeUsage.Off;
		component.shadowCastingMode = ShadowCastingMode.Off;
		component.receiveShadows = false;
		component.lightProbeUsage = LightProbeUsage.Off;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00057513 File Offset: 0x00055713
	private void OnDrawGizmos()
	{
		if (!this.drawWireframeWhenSelectedOnly)
		{
			this.DrawWireframe();
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00057523 File Offset: 0x00055723
	private void OnDrawGizmosSelected()
	{
		if (this.drawWireframeWhenSelectedOnly)
		{
			this.DrawWireframe();
		}
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00057534 File Offset: 0x00055734
	public void DrawWireframe()
	{
		if (this.vertices == null || this.vertices.Length == 0)
		{
			return;
		}
		Vector3 b = base.transform.TransformVector(Vector3.up * this.wireframeHeight);
		for (int i = 0; i < 4; i++)
		{
			int num = (i + 1) % 4;
			Vector3 vector = base.transform.TransformPoint(this.vertices[i]);
			Vector3 vector2 = vector + b;
			Vector3 vector3 = base.transform.TransformPoint(this.vertices[num]);
			Vector3 to = vector3 + b;
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector, vector3);
			Gizmos.DrawLine(vector2, to);
		}
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x000575D9 File Offset: 0x000557D9
	public void OnEnable()
	{
		if (Application.isPlaying)
		{
			base.GetComponent<MeshRenderer>().enabled = this.drawInGame;
			base.enabled = false;
			if (this.drawInGame && this.size == SteamVR_PlayArea.Size.Calibrated)
			{
				base.StartCoroutine(this.UpdateBounds());
			}
		}
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00057617 File Offset: 0x00055817
	private IEnumerator UpdateBounds()
	{
		base.GetComponent<MeshFilter>().mesh = null;
		CVRChaperone chaperone = OpenVR.Chaperone;
		if (chaperone == null)
		{
			yield break;
		}
		while (chaperone.GetCalibrationState() != ChaperoneCalibrationState.OK)
		{
			yield return null;
		}
		this.BuildMesh();
		yield break;
	}

	// Token: 0x04000E3B RID: 3643
	public float borderThickness = 0.15f;

	// Token: 0x04000E3C RID: 3644
	public float wireframeHeight = 2f;

	// Token: 0x04000E3D RID: 3645
	public bool drawWireframeWhenSelectedOnly;

	// Token: 0x04000E3E RID: 3646
	public bool drawInGame = true;

	// Token: 0x04000E3F RID: 3647
	public SteamVR_PlayArea.Size size;

	// Token: 0x04000E40 RID: 3648
	public Color color = Color.cyan;

	// Token: 0x04000E41 RID: 3649
	[HideInInspector]
	public Vector3[] vertices;

	// Token: 0x02000577 RID: 1399
	public enum Size
	{
		// Token: 0x040025F8 RID: 9720
		Calibrated,
		// Token: 0x040025F9 RID: 9721
		_400x300,
		// Token: 0x040025FA RID: 9722
		_300x225,
		// Token: 0x040025FB RID: 9723
		_200x150
	}
}
