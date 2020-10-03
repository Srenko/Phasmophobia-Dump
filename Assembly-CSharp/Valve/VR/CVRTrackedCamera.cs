using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000393 RID: 915
	public class CVRTrackedCamera
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x0009CE07 File Offset: 0x0009B007
		internal CVRTrackedCamera(IntPtr pInterface)
		{
			this.FnTable = (IVRTrackedCamera)Marshal.PtrToStructure(pInterface, typeof(IVRTrackedCamera));
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0009CE2A File Offset: 0x0009B02A
		public string GetCameraErrorNameFromEnum(EVRTrackedCameraError eCameraError)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetCameraErrorNameFromEnum(eCameraError));
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0009CE42 File Offset: 0x0009B042
		public EVRTrackedCameraError HasCamera(uint nDeviceIndex, ref bool pHasCamera)
		{
			pHasCamera = false;
			return this.FnTable.HasCamera(nDeviceIndex, ref pHasCamera);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0009CE59 File Offset: 0x0009B059
		public EVRTrackedCameraError GetCameraFrameSize(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, ref uint pnWidth, ref uint pnHeight, ref uint pnFrameBufferSize)
		{
			pnWidth = 0U;
			pnHeight = 0U;
			pnFrameBufferSize = 0U;
			return this.FnTable.GetCameraFrameSize(nDeviceIndex, eFrameType, ref pnWidth, ref pnHeight, ref pnFrameBufferSize);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0009CE7D File Offset: 0x0009B07D
		public EVRTrackedCameraError GetCameraIntrinsics(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, ref HmdVector2_t pFocalLength, ref HmdVector2_t pCenter)
		{
			return this.FnTable.GetCameraIntrinsics(nDeviceIndex, eFrameType, ref pFocalLength, ref pCenter);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0009CE94 File Offset: 0x0009B094
		public EVRTrackedCameraError GetCameraProjection(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, float flZNear, float flZFar, ref HmdMatrix44_t pProjection)
		{
			return this.FnTable.GetCameraProjection(nDeviceIndex, eFrameType, flZNear, flZFar, ref pProjection);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0009CEAD File Offset: 0x0009B0AD
		public EVRTrackedCameraError AcquireVideoStreamingService(uint nDeviceIndex, ref ulong pHandle)
		{
			pHandle = 0UL;
			return this.FnTable.AcquireVideoStreamingService(nDeviceIndex, ref pHandle);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0009CEC5 File Offset: 0x0009B0C5
		public EVRTrackedCameraError ReleaseVideoStreamingService(ulong hTrackedCamera)
		{
			return this.FnTable.ReleaseVideoStreamingService(hTrackedCamera);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0009CED8 File Offset: 0x0009B0D8
		public EVRTrackedCameraError GetVideoStreamFrameBuffer(ulong hTrackedCamera, EVRTrackedCameraFrameType eFrameType, IntPtr pFrameBuffer, uint nFrameBufferSize, ref CameraVideoStreamFrameHeader_t pFrameHeader, uint nFrameHeaderSize)
		{
			return this.FnTable.GetVideoStreamFrameBuffer(hTrackedCamera, eFrameType, pFrameBuffer, nFrameBufferSize, ref pFrameHeader, nFrameHeaderSize);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0009CEF3 File Offset: 0x0009B0F3
		public EVRTrackedCameraError GetVideoStreamTextureSize(uint nDeviceIndex, EVRTrackedCameraFrameType eFrameType, ref VRTextureBounds_t pTextureBounds, ref uint pnWidth, ref uint pnHeight)
		{
			pnWidth = 0U;
			pnHeight = 0U;
			return this.FnTable.GetVideoStreamTextureSize(nDeviceIndex, eFrameType, ref pTextureBounds, ref pnWidth, ref pnHeight);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0009CF14 File Offset: 0x0009B114
		public EVRTrackedCameraError GetVideoStreamTextureD3D11(ulong hTrackedCamera, EVRTrackedCameraFrameType eFrameType, IntPtr pD3D11DeviceOrResource, ref IntPtr ppD3D11ShaderResourceView, ref CameraVideoStreamFrameHeader_t pFrameHeader, uint nFrameHeaderSize)
		{
			return this.FnTable.GetVideoStreamTextureD3D11(hTrackedCamera, eFrameType, pD3D11DeviceOrResource, ref ppD3D11ShaderResourceView, ref pFrameHeader, nFrameHeaderSize);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0009CF2F File Offset: 0x0009B12F
		public EVRTrackedCameraError GetVideoStreamTextureGL(ulong hTrackedCamera, EVRTrackedCameraFrameType eFrameType, ref uint pglTextureId, ref CameraVideoStreamFrameHeader_t pFrameHeader, uint nFrameHeaderSize)
		{
			pglTextureId = 0U;
			return this.FnTable.GetVideoStreamTextureGL(hTrackedCamera, eFrameType, ref pglTextureId, ref pFrameHeader, nFrameHeaderSize);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0009CF4B File Offset: 0x0009B14B
		public EVRTrackedCameraError ReleaseVideoStreamTextureGL(ulong hTrackedCamera, uint glTextureId)
		{
			return this.FnTable.ReleaseVideoStreamTextureGL(hTrackedCamera, glTextureId);
		}

		// Token: 0x0400192C RID: 6444
		private IVRTrackedCamera FnTable;
	}
}
