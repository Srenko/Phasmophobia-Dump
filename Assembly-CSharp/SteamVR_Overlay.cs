using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR;

// Token: 0x020001ED RID: 493
public class SteamVR_Overlay : MonoBehaviour
{
	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000569D6 File Offset: 0x00054BD6
	// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000569DD File Offset: 0x00054BDD
	public static SteamVR_Overlay instance { get; private set; }

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000569E5 File Offset: 0x00054BE5
	public static string key
	{
		get
		{
			return "unity:" + Application.companyName + "." + Application.productName;
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00056A00 File Offset: 0x00054C00
	private void OnEnable()
	{
		CVROverlay overlay = OpenVR.Overlay;
		if (overlay != null)
		{
			EVROverlayError evroverlayError = overlay.CreateOverlay(SteamVR_Overlay.key, base.gameObject.name, ref this.handle);
			if (evroverlayError != EVROverlayError.None)
			{
				Debug.Log(overlay.GetOverlayErrorNameFromEnum(evroverlayError));
				base.enabled = false;
				return;
			}
		}
		SteamVR_Overlay.instance = this;
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00056A50 File Offset: 0x00054C50
	private void OnDisable()
	{
		if (this.handle != 0UL)
		{
			CVROverlay overlay = OpenVR.Overlay;
			if (overlay != null)
			{
				overlay.DestroyOverlay(this.handle);
			}
			this.handle = 0UL;
		}
		SteamVR_Overlay.instance = null;
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x00056A8C File Offset: 0x00054C8C
	public void UpdateOverlay()
	{
		CVROverlay overlay = OpenVR.Overlay;
		if (overlay == null)
		{
			return;
		}
		if (this.texture != null)
		{
			EVROverlayError evroverlayError = overlay.ShowOverlay(this.handle);
			if ((evroverlayError == EVROverlayError.InvalidHandle || evroverlayError == EVROverlayError.UnknownOverlay) && overlay.FindOverlay(SteamVR_Overlay.key, ref this.handle) != EVROverlayError.None)
			{
				return;
			}
			Texture_t texture_t = default(Texture_t);
			texture_t.handle = this.texture.GetNativeTexturePtr();
			texture_t.eType = SteamVR.instance.textureType;
			texture_t.eColorSpace = EColorSpace.Auto;
			overlay.SetOverlayTexture(this.handle, ref texture_t);
			overlay.SetOverlayAlpha(this.handle, this.alpha);
			overlay.SetOverlayWidthInMeters(this.handle, this.scale);
			overlay.SetOverlayAutoCurveDistanceRangeInMeters(this.handle, this.curvedRange.x, this.curvedRange.y);
			VRTextureBounds_t vrtextureBounds_t = default(VRTextureBounds_t);
			vrtextureBounds_t.uMin = (0f + this.uvOffset.x) * this.uvOffset.z;
			vrtextureBounds_t.vMin = (1f + this.uvOffset.y) * this.uvOffset.w;
			vrtextureBounds_t.uMax = (1f + this.uvOffset.x) * this.uvOffset.z;
			vrtextureBounds_t.vMax = (0f + this.uvOffset.y) * this.uvOffset.w;
			overlay.SetOverlayTextureBounds(this.handle, ref vrtextureBounds_t);
			HmdVector2_t hmdVector2_t = default(HmdVector2_t);
			hmdVector2_t.v0 = this.mouseScale.x;
			hmdVector2_t.v1 = this.mouseScale.y;
			overlay.SetOverlayMouseScale(this.handle, ref hmdVector2_t);
			SteamVR_Camera steamVR_Camera = SteamVR_Render.Top();
			if (steamVR_Camera != null && steamVR_Camera.origin != null)
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(steamVR_Camera.origin, base.transform);
				rigidTransform.pos.x = rigidTransform.pos.x / steamVR_Camera.origin.localScale.x;
				rigidTransform.pos.y = rigidTransform.pos.y / steamVR_Camera.origin.localScale.y;
				rigidTransform.pos.z = rigidTransform.pos.z / steamVR_Camera.origin.localScale.z;
				rigidTransform.pos.z = rigidTransform.pos.z + this.distance;
				HmdMatrix34_t hmdMatrix34_t = rigidTransform.ToHmdMatrix34();
				overlay.SetOverlayTransformAbsolute(this.handle, SteamVR_Render.instance.trackingSpace, ref hmdMatrix34_t);
			}
			overlay.SetOverlayInputMethod(this.handle, this.inputMethod);
			if (this.curved || this.antialias)
			{
				this.highquality = true;
			}
			if (this.highquality)
			{
				overlay.SetHighQualityOverlay(this.handle);
				overlay.SetOverlayFlag(this.handle, VROverlayFlags.Curved, this.curved);
				overlay.SetOverlayFlag(this.handle, VROverlayFlags.RGSS4X, this.antialias);
				return;
			}
			if (overlay.GetHighQualityOverlay() == this.handle)
			{
				overlay.SetHighQualityOverlay(0UL);
				return;
			}
		}
		else
		{
			overlay.HideOverlay(this.handle);
		}
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00056DB0 File Offset: 0x00054FB0
	public bool PollNextEvent(ref VREvent_t pEvent)
	{
		CVROverlay overlay = OpenVR.Overlay;
		if (overlay == null)
		{
			return false;
		}
		uint uncbVREvent = (uint)Marshal.SizeOf(typeof(VREvent_t));
		return overlay.PollNextOverlayEvent(this.handle, ref pEvent, uncbVREvent);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00056DE8 File Offset: 0x00054FE8
	public bool ComputeIntersection(Vector3 source, Vector3 direction, ref SteamVR_Overlay.IntersectionResults results)
	{
		CVROverlay overlay = OpenVR.Overlay;
		if (overlay == null)
		{
			return false;
		}
		VROverlayIntersectionParams_t vroverlayIntersectionParams_t = default(VROverlayIntersectionParams_t);
		vroverlayIntersectionParams_t.eOrigin = SteamVR_Render.instance.trackingSpace;
		vroverlayIntersectionParams_t.vSource.v0 = source.x;
		vroverlayIntersectionParams_t.vSource.v1 = source.y;
		vroverlayIntersectionParams_t.vSource.v2 = -source.z;
		vroverlayIntersectionParams_t.vDirection.v0 = direction.x;
		vroverlayIntersectionParams_t.vDirection.v1 = direction.y;
		vroverlayIntersectionParams_t.vDirection.v2 = -direction.z;
		VROverlayIntersectionResults_t vroverlayIntersectionResults_t = default(VROverlayIntersectionResults_t);
		if (!overlay.ComputeOverlayIntersection(this.handle, ref vroverlayIntersectionParams_t, ref vroverlayIntersectionResults_t))
		{
			return false;
		}
		results.point = new Vector3(vroverlayIntersectionResults_t.vPoint.v0, vroverlayIntersectionResults_t.vPoint.v1, -vroverlayIntersectionResults_t.vPoint.v2);
		results.normal = new Vector3(vroverlayIntersectionResults_t.vNormal.v0, vroverlayIntersectionResults_t.vNormal.v1, -vroverlayIntersectionResults_t.vNormal.v2);
		results.UVs = new Vector2(vroverlayIntersectionResults_t.vUVs.v0, vroverlayIntersectionResults_t.vUVs.v1);
		results.distance = vroverlayIntersectionResults_t.fDistance;
		return true;
	}

	// Token: 0x04000E2E RID: 3630
	public Texture texture;

	// Token: 0x04000E2F RID: 3631
	public bool curved = true;

	// Token: 0x04000E30 RID: 3632
	public bool antialias = true;

	// Token: 0x04000E31 RID: 3633
	public bool highquality = true;

	// Token: 0x04000E32 RID: 3634
	[Tooltip("Size of overlay view.")]
	public float scale = 3f;

	// Token: 0x04000E33 RID: 3635
	[Tooltip("Distance from surface.")]
	public float distance = 1.25f;

	// Token: 0x04000E34 RID: 3636
	[Tooltip("Opacity")]
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x04000E35 RID: 3637
	public Vector4 uvOffset = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x04000E36 RID: 3638
	public Vector2 mouseScale = new Vector2(1f, 1f);

	// Token: 0x04000E37 RID: 3639
	public Vector2 curvedRange = new Vector2(1f, 2f);

	// Token: 0x04000E38 RID: 3640
	public VROverlayInputMethod inputMethod;

	// Token: 0x04000E3A RID: 3642
	private ulong handle;

	// Token: 0x02000576 RID: 1398
	public struct IntersectionResults
	{
		// Token: 0x040025F3 RID: 9715
		public Vector3 point;

		// Token: 0x040025F4 RID: 9716
		public Vector3 normal;

		// Token: 0x040025F5 RID: 9717
		public Vector2 UVs;

		// Token: 0x040025F6 RID: 9718
		public float distance;
	}
}
