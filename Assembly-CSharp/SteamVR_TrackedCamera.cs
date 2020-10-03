using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR;

// Token: 0x020001F5 RID: 501
public class SteamVR_TrackedCamera
{
	// Token: 0x06000E01 RID: 3585 RVA: 0x0005947C File Offset: 0x0005767C
	public static SteamVR_TrackedCamera.VideoStreamTexture Distorted(int deviceIndex = 0)
	{
		if (SteamVR_TrackedCamera.distorted == null)
		{
			SteamVR_TrackedCamera.distorted = new SteamVR_TrackedCamera.VideoStreamTexture[16];
		}
		if (SteamVR_TrackedCamera.distorted[deviceIndex] == null)
		{
			SteamVR_TrackedCamera.distorted[deviceIndex] = new SteamVR_TrackedCamera.VideoStreamTexture((uint)deviceIndex, false);
		}
		return SteamVR_TrackedCamera.distorted[deviceIndex];
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x000594AF File Offset: 0x000576AF
	public static SteamVR_TrackedCamera.VideoStreamTexture Undistorted(int deviceIndex = 0)
	{
		if (SteamVR_TrackedCamera.undistorted == null)
		{
			SteamVR_TrackedCamera.undistorted = new SteamVR_TrackedCamera.VideoStreamTexture[16];
		}
		if (SteamVR_TrackedCamera.undistorted[deviceIndex] == null)
		{
			SteamVR_TrackedCamera.undistorted[deviceIndex] = new SteamVR_TrackedCamera.VideoStreamTexture((uint)deviceIndex, true);
		}
		return SteamVR_TrackedCamera.undistorted[deviceIndex];
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x000594E2 File Offset: 0x000576E2
	public static SteamVR_TrackedCamera.VideoStreamTexture Source(bool undistorted, int deviceIndex = 0)
	{
		if (!undistorted)
		{
			return SteamVR_TrackedCamera.Distorted(deviceIndex);
		}
		return SteamVR_TrackedCamera.Undistorted(deviceIndex);
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x000594F4 File Offset: 0x000576F4
	private static SteamVR_TrackedCamera.VideoStream Stream(uint deviceIndex)
	{
		if (SteamVR_TrackedCamera.videostreams == null)
		{
			SteamVR_TrackedCamera.videostreams = new SteamVR_TrackedCamera.VideoStream[16];
		}
		if (SteamVR_TrackedCamera.videostreams[(int)deviceIndex] == null)
		{
			SteamVR_TrackedCamera.videostreams[(int)deviceIndex] = new SteamVR_TrackedCamera.VideoStream(deviceIndex);
		}
		return SteamVR_TrackedCamera.videostreams[(int)deviceIndex];
	}

	// Token: 0x04000E73 RID: 3699
	private static SteamVR_TrackedCamera.VideoStreamTexture[] distorted;

	// Token: 0x04000E74 RID: 3700
	private static SteamVR_TrackedCamera.VideoStreamTexture[] undistorted;

	// Token: 0x04000E75 RID: 3701
	private static SteamVR_TrackedCamera.VideoStream[] videostreams;

	// Token: 0x0200057F RID: 1407
	public class VideoStreamTexture
	{
		// Token: 0x060028A5 RID: 10405 RVA: 0x000C4774 File Offset: 0x000C2974
		public VideoStreamTexture(uint deviceIndex, bool undistorted)
		{
			this.undistorted = undistorted;
			this.videostream = SteamVR_TrackedCamera.Stream(deviceIndex);
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x000C4796 File Offset: 0x000C2996
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x000C479E File Offset: 0x000C299E
		public bool undistorted { get; private set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000C47A7 File Offset: 0x000C29A7
		public uint deviceIndex
		{
			get
			{
				return this.videostream.deviceIndex;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x000C47B4 File Offset: 0x000C29B4
		public bool hasCamera
		{
			get
			{
				return this.videostream.hasCamera;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000C47C1 File Offset: 0x000C29C1
		public bool hasTracking
		{
			get
			{
				this.Update();
				return this.header.standingTrackedDevicePose.bPoseIsValid;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000C47D9 File Offset: 0x000C29D9
		public uint frameId
		{
			get
			{
				this.Update();
				return this.header.nFrameSequence;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000C47EC File Offset: 0x000C29EC
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000C47F4 File Offset: 0x000C29F4
		public VRTextureBounds_t frameBounds { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000C47FD File Offset: 0x000C29FD
		public EVRTrackedCameraFrameType frameType
		{
			get
			{
				if (!this.undistorted)
				{
					return EVRTrackedCameraFrameType.Distorted;
				}
				return EVRTrackedCameraFrameType.Undistorted;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000C480A File Offset: 0x000C2A0A
		public Texture2D texture
		{
			get
			{
				this.Update();
				return this._texture;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000C4818 File Offset: 0x000C2A18
		public SteamVR_Utils.RigidTransform transform
		{
			get
			{
				this.Update();
				return new SteamVR_Utils.RigidTransform(this.header.standingTrackedDevicePose.mDeviceToAbsoluteTracking);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000C4838 File Offset: 0x000C2A38
		public Vector3 velocity
		{
			get
			{
				this.Update();
				TrackedDevicePose_t standingTrackedDevicePose = this.header.standingTrackedDevicePose;
				return new Vector3(standingTrackedDevicePose.vVelocity.v0, standingTrackedDevicePose.vVelocity.v1, -standingTrackedDevicePose.vVelocity.v2);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000C4880 File Offset: 0x000C2A80
		public Vector3 angularVelocity
		{
			get
			{
				this.Update();
				TrackedDevicePose_t standingTrackedDevicePose = this.header.standingTrackedDevicePose;
				return new Vector3(-standingTrackedDevicePose.vAngularVelocity.v0, -standingTrackedDevicePose.vAngularVelocity.v1, standingTrackedDevicePose.vAngularVelocity.v2);
			}
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000C48C7 File Offset: 0x000C2AC7
		public TrackedDevicePose_t GetPose()
		{
			this.Update();
			return this.header.standingTrackedDevicePose;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000C48DA File Offset: 0x000C2ADA
		public ulong Acquire()
		{
			return this.videostream.Acquire();
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000C48E7 File Offset: 0x000C2AE7
		public ulong Release()
		{
			ulong result = this.videostream.Release();
			if (this.videostream.handle == 0UL)
			{
				Object.Destroy(this._texture);
				this._texture = null;
			}
			return result;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000C4914 File Offset: 0x000C2B14
		private void Update()
		{
			if (Time.frameCount == this.prevFrameCount)
			{
				return;
			}
			this.prevFrameCount = Time.frameCount;
			if (this.videostream.handle == 0UL)
			{
				return;
			}
			SteamVR instance = SteamVR.instance;
			if (instance == null)
			{
				return;
			}
			CVRTrackedCamera trackedCamera = OpenVR.TrackedCamera;
			if (trackedCamera == null)
			{
				return;
			}
			IntPtr nativeTex = IntPtr.Zero;
			Texture2D texture2D = (this._texture != null) ? this._texture : new Texture2D(2, 2);
			uint nFrameHeaderSize = (uint)Marshal.SizeOf(this.header.GetType());
			if (instance.textureType == ETextureType.OpenGL)
			{
				if (this.glTextureId != 0U)
				{
					trackedCamera.ReleaseVideoStreamTextureGL(this.videostream.handle, this.glTextureId);
				}
				if (trackedCamera.GetVideoStreamTextureGL(this.videostream.handle, this.frameType, ref this.glTextureId, ref this.header, nFrameHeaderSize) != EVRTrackedCameraError.None)
				{
					return;
				}
				nativeTex = (IntPtr)((long)((ulong)this.glTextureId));
			}
			else if (instance.textureType == ETextureType.DirectX && trackedCamera.GetVideoStreamTextureD3D11(this.videostream.handle, this.frameType, texture2D.GetNativeTexturePtr(), ref nativeTex, ref this.header, nFrameHeaderSize) != EVRTrackedCameraError.None)
			{
				return;
			}
			if (this._texture == null)
			{
				this._texture = Texture2D.CreateExternalTexture((int)this.header.nWidth, (int)this.header.nHeight, TextureFormat.RGBA32, false, false, nativeTex);
				uint num = 0U;
				uint num2 = 0U;
				VRTextureBounds_t vrtextureBounds_t = default(VRTextureBounds_t);
				if (trackedCamera.GetVideoStreamTextureSize(this.deviceIndex, this.frameType, ref vrtextureBounds_t, ref num, ref num2) == EVRTrackedCameraError.None)
				{
					vrtextureBounds_t.vMin = 1f - vrtextureBounds_t.vMin;
					vrtextureBounds_t.vMax = 1f - vrtextureBounds_t.vMax;
					this.frameBounds = vrtextureBounds_t;
					return;
				}
			}
			else
			{
				this._texture.UpdateExternalTexture(nativeTex);
			}
		}

		// Token: 0x0400261A RID: 9754
		private Texture2D _texture;

		// Token: 0x0400261B RID: 9755
		private int prevFrameCount = -1;

		// Token: 0x0400261C RID: 9756
		private uint glTextureId;

		// Token: 0x0400261D RID: 9757
		private SteamVR_TrackedCamera.VideoStream videostream;

		// Token: 0x0400261E RID: 9758
		private CameraVideoStreamFrameHeader_t header;
	}

	// Token: 0x02000580 RID: 1408
	private class VideoStream
	{
		// Token: 0x060028B7 RID: 10423 RVA: 0x000C4AC4 File Offset: 0x000C2CC4
		public VideoStream(uint deviceIndex)
		{
			this.deviceIndex = deviceIndex;
			CVRTrackedCamera trackedCamera = OpenVR.TrackedCamera;
			if (trackedCamera != null)
			{
				trackedCamera.HasCamera(deviceIndex, ref this._hasCamera);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000C4AF5 File Offset: 0x000C2CF5
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x000C4AFD File Offset: 0x000C2CFD
		public uint deviceIndex { get; private set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x000C4B06 File Offset: 0x000C2D06
		public ulong handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x000C4B0E File Offset: 0x000C2D0E
		public bool hasCamera
		{
			get
			{
				return this._hasCamera;
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000C4B18 File Offset: 0x000C2D18
		public ulong Acquire()
		{
			if (this._handle == 0UL && this.hasCamera)
			{
				CVRTrackedCamera trackedCamera = OpenVR.TrackedCamera;
				if (trackedCamera != null)
				{
					trackedCamera.AcquireVideoStreamingService(this.deviceIndex, ref this._handle);
				}
			}
			ulong result = this.refCount + 1UL;
			this.refCount = result;
			return result;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000C4B64 File Offset: 0x000C2D64
		public ulong Release()
		{
			if (this.refCount > 0UL)
			{
				ulong num = this.refCount - 1UL;
				this.refCount = num;
				if (num == 0UL && this._handle != 0UL)
				{
					CVRTrackedCamera trackedCamera = OpenVR.TrackedCamera;
					if (trackedCamera != null)
					{
						trackedCamera.ReleaseVideoStreamingService(this._handle);
					}
					this._handle = 0UL;
				}
			}
			return this.refCount;
		}

		// Token: 0x04002620 RID: 9760
		private ulong _handle;

		// Token: 0x04002621 RID: 9761
		private bool _hasCamera;

		// Token: 0x04002622 RID: 9762
		private ulong refCount;
	}
}
