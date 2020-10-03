using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001E8 RID: 488
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SteamVR_Frustum : MonoBehaviour
{
	// Token: 0x06000D89 RID: 3465 RVA: 0x00055034 File Offset: 0x00053234
	public void UpdateModel()
	{
		this.fovLeft = Mathf.Clamp(this.fovLeft, 1f, 89f);
		this.fovRight = Mathf.Clamp(this.fovRight, 1f, 89f);
		this.fovTop = Mathf.Clamp(this.fovTop, 1f, 89f);
		this.fovBottom = Mathf.Clamp(this.fovBottom, 1f, 89f);
		this.farZ = Mathf.Max(this.farZ, this.nearZ + 0.01f);
		this.nearZ = Mathf.Clamp(this.nearZ, 0.01f, this.farZ - 0.01f);
		float num = Mathf.Sin(-this.fovLeft * 0.0174532924f);
		float num2 = Mathf.Sin(this.fovRight * 0.0174532924f);
		float num3 = Mathf.Sin(this.fovTop * 0.0174532924f);
		float num4 = Mathf.Sin(-this.fovBottom * 0.0174532924f);
		float num5 = Mathf.Cos(-this.fovLeft * 0.0174532924f);
		float num6 = Mathf.Cos(this.fovRight * 0.0174532924f);
		float num7 = Mathf.Cos(this.fovTop * 0.0174532924f);
		float num8 = Mathf.Cos(-this.fovBottom * 0.0174532924f);
		Vector3[] array = new Vector3[]
		{
			new Vector3(num * this.nearZ / num5, num3 * this.nearZ / num7, this.nearZ),
			new Vector3(num2 * this.nearZ / num6, num3 * this.nearZ / num7, this.nearZ),
			new Vector3(num2 * this.nearZ / num6, num4 * this.nearZ / num8, this.nearZ),
			new Vector3(num * this.nearZ / num5, num4 * this.nearZ / num8, this.nearZ),
			new Vector3(num * this.farZ / num5, num3 * this.farZ / num7, this.farZ),
			new Vector3(num2 * this.farZ / num6, num3 * this.farZ / num7, this.farZ),
			new Vector3(num2 * this.farZ / num6, num4 * this.farZ / num8, this.farZ),
			new Vector3(num * this.farZ / num5, num4 * this.farZ / num8, this.farZ)
		};
		int[] array2 = new int[]
		{
			0,
			4,
			7,
			0,
			7,
			3,
			0,
			7,
			4,
			0,
			3,
			7,
			1,
			5,
			6,
			1,
			6,
			2,
			1,
			6,
			5,
			1,
			2,
			6,
			0,
			4,
			5,
			0,
			5,
			1,
			0,
			5,
			4,
			0,
			1,
			5,
			2,
			3,
			7,
			2,
			7,
			6,
			2,
			7,
			3,
			2,
			6,
			7
		};
		int num9 = 0;
		Vector3[] array3 = new Vector3[array2.Length];
		Vector3[] array4 = new Vector3[array2.Length];
		for (int i = 0; i < array2.Length / 3; i++)
		{
			Vector3 vector = array[array2[i * 3]];
			Vector3 vector2 = array[array2[i * 3 + 1]];
			Vector3 vector3 = array[array2[i * 3 + 2]];
			Vector3 normalized = Vector3.Cross(vector2 - vector, vector3 - vector).normalized;
			array4[i * 3] = normalized;
			array4[i * 3 + 1] = normalized;
			array4[i * 3 + 2] = normalized;
			array3[i * 3] = vector;
			array3[i * 3 + 1] = vector2;
			array3[i * 3 + 2] = vector3;
			array2[i * 3] = num9++;
			array2[i * 3 + 1] = num9++;
			array2[i * 3 + 2] = num9++;
		}
		Mesh mesh = new Mesh();
		mesh.vertices = array3;
		mesh.normals = array4;
		mesh.triangles = array2;
		base.GetComponent<MeshFilter>().mesh = mesh;
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00055420 File Offset: 0x00053620
	private void OnDeviceConnected(int i, bool connected)
	{
		if (i != (int)this.index)
		{
			return;
		}
		base.GetComponent<MeshFilter>().mesh = null;
		if (connected)
		{
			CVRSystem system = OpenVR.System;
			if (system != null && system.GetTrackedDeviceClass((uint)i) == ETrackedDeviceClass.TrackingReference)
			{
				ETrackedPropertyError etrackedPropertyError = ETrackedPropertyError.TrackedProp_Success;
				float floatTrackedDeviceProperty = system.GetFloatTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_FieldOfViewLeftDegrees_Float, ref etrackedPropertyError);
				if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
				{
					this.fovLeft = floatTrackedDeviceProperty;
				}
				floatTrackedDeviceProperty = system.GetFloatTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_FieldOfViewRightDegrees_Float, ref etrackedPropertyError);
				if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
				{
					this.fovRight = floatTrackedDeviceProperty;
				}
				floatTrackedDeviceProperty = system.GetFloatTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_FieldOfViewTopDegrees_Float, ref etrackedPropertyError);
				if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
				{
					this.fovTop = floatTrackedDeviceProperty;
				}
				floatTrackedDeviceProperty = system.GetFloatTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_FieldOfViewBottomDegrees_Float, ref etrackedPropertyError);
				if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
				{
					this.fovBottom = floatTrackedDeviceProperty;
				}
				floatTrackedDeviceProperty = system.GetFloatTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_TrackingRangeMinimumMeters_Float, ref etrackedPropertyError);
				if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
				{
					this.nearZ = floatTrackedDeviceProperty;
				}
				floatTrackedDeviceProperty = system.GetFloatTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_TrackingRangeMaximumMeters_Float, ref etrackedPropertyError);
				if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
				{
					this.farZ = floatTrackedDeviceProperty;
				}
				this.UpdateModel();
			}
		}
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00055500 File Offset: 0x00053700
	private void OnEnable()
	{
		base.GetComponent<MeshFilter>().mesh = null;
		SteamVR_Events.DeviceConnected.Listen(new UnityAction<int, bool>(this.OnDeviceConnected));
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00055524 File Offset: 0x00053724
	private void OnDisable()
	{
		SteamVR_Events.DeviceConnected.Remove(new UnityAction<int, bool>(this.OnDeviceConnected));
		base.GetComponent<MeshFilter>().mesh = null;
	}

	// Token: 0x04000DEA RID: 3562
	public SteamVR_TrackedObject.EIndex index;

	// Token: 0x04000DEB RID: 3563
	public float fovLeft = 45f;

	// Token: 0x04000DEC RID: 3564
	public float fovRight = 45f;

	// Token: 0x04000DED RID: 3565
	public float fovTop = 45f;

	// Token: 0x04000DEE RID: 3566
	public float fovBottom = 45f;

	// Token: 0x04000DEF RID: 3567
	public float nearZ = 0.5f;

	// Token: 0x04000DF0 RID: 3568
	public float farZ = 2.5f;
}
