using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000385 RID: 901
	public struct IVRTrackedCamera
	{
		// Token: 0x0400183F RID: 6207
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetCameraErrorNameFromEnum GetCameraErrorNameFromEnum;

		// Token: 0x04001840 RID: 6208
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._HasCamera HasCamera;

		// Token: 0x04001841 RID: 6209
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetCameraFrameSize GetCameraFrameSize;

		// Token: 0x04001842 RID: 6210
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetCameraIntrinsics GetCameraIntrinsics;

		// Token: 0x04001843 RID: 6211
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetCameraProjection GetCameraProjection;

		// Token: 0x04001844 RID: 6212
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._AcquireVideoStreamingService AcquireVideoStreamingService;

		// Token: 0x04001845 RID: 6213
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._ReleaseVideoStreamingService ReleaseVideoStreamingService;

		// Token: 0x04001846 RID: 6214
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetVideoStreamFrameBuffer GetVideoStreamFrameBuffer;

		// Token: 0x04001847 RID: 6215
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetVideoStreamTextureSize GetVideoStreamTextureSize;

		// Token: 0x04001848 RID: 6216
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetVideoStreamTextureD3D11 GetVideoStreamTextureD3D11;

		// Token: 0x04001849 RID: 6217
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._GetVideoStreamTextureGL GetVideoStreamTextureGL;

		// Token: 0x0400184A RID: 6218
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRTrackedCamera._ReleaseVideoStreamTextureGL ReleaseVideoStreamTextureGL;

		// Token: 0x02000676 RID: 1654
		// (Invoke) Token: 0x06002BDF RID: 11231
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetCameraErrorNameFromEnum(EVRTrackedCameraError eCameraError);

		// Token: 0x02000677 RID: 1655
		// (Invoke) Token: 0x06002BE3 RID: 11235
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _HasCamera(uint nDeviceIndex, ref bool pHasCamera);

		// Token: 0x02000678 RID: 1656
		// (Invoke) Token: 0x06002BE7 RID: 11239
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetCameraFrameSize(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, ref uint pnWidth, ref uint pnHeight, ref uint pnFrameBufferSize);

		// Token: 0x02000679 RID: 1657
		// (Invoke) Token: 0x06002BEB RID: 11243
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetCameraIntrinsics(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, ref HmdVector2_t pFocalLength, ref HmdVector2_t pCenter);

		// Token: 0x0200067A RID: 1658
		// (Invoke) Token: 0x06002BEF RID: 11247
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetCameraProjection(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, float flZNear, float flZFar, ref HmdMatrix44_t pProjection);

		// Token: 0x0200067B RID: 1659
		// (Invoke) Token: 0x06002BF3 RID: 11251
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _AcquireVideoStreamingService(uint nDeviceIndex, ref ulong pHandle);

		// Token: 0x0200067C RID: 1660
		// (Invoke) Token: 0x06002BF7 RID: 11255
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _ReleaseVideoStreamingService(ulong hTrackedCamera);

		// Token: 0x0200067D RID: 1661
		// (Invoke) Token: 0x06002BFB RID: 11259
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetVideoStreamFrameBuffer(ulong hTrackedCamera, EVRTrackedCameraFrameType eFrameType, IntPtr pFrameBuffer, uint nFrameBufferSize, ref CameraVideoStreamFrameHeader_t pFrameHeader, uint nFrameHeaderSize);

		// Token: 0x0200067E RID: 1662
		// (Invoke) Token: 0x06002BFF RID: 11263
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetVideoStreamTextureSize(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, ref VRTextureBounds_t pTextureBounds, ref uint pnWidth, ref uint pnHeight);

		// Token: 0x0200067F RID: 1663
		// (Invoke) Token: 0x06002C03 RID: 11267
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetVideoStreamTextureD3D11(ulong hTrackedCamera, EVRTrackedCameraFrameType eFrameType, IntPtr pD3D11DeviceOrResource, ref IntPtr ppD3D11ShaderResourceView, ref CameraVideoStreamFrameHeader_t pFrameHeader, uint nFrameHeaderSize);

		// Token: 0x02000680 RID: 1664
		// (Invoke) Token: 0x06002C07 RID: 11271
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _GetVideoStreamTextureGL(ulong hTrackedCamera, EVRTrackedCameraFrameType eFrameType, ref uint pglTextureId, ref CameraVideoStreamFrameHeader_t pFrameHeader, uint nFrameHeaderSize);

		// Token: 0x02000681 RID: 1665
		// (Invoke) Token: 0x06002C0B RID: 11275
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRTrackedCameraError _ReleaseVideoStreamTextureGL(ulong hTrackedCamera, uint glTextureId);
	}
}
