using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000397 RID: 919
	public class CVRCompositor
	{
		// Token: 0x06001F52 RID: 8018 RVA: 0x0009D536 File Offset: 0x0009B736
		internal CVRCompositor(IntPtr pInterface)
		{
			this.FnTable = (IVRCompositor)Marshal.PtrToStructure(pInterface, typeof(IVRCompositor));
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0009D559 File Offset: 0x0009B759
		public void SetTrackingSpace(ETrackingUniverseOrigin eOrigin)
		{
			this.FnTable.SetTrackingSpace(eOrigin);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0009D56C File Offset: 0x0009B76C
		public ETrackingUniverseOrigin GetTrackingSpace()
		{
			return this.FnTable.GetTrackingSpace();
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0009D57E File Offset: 0x0009B77E
		public EVRCompositorError WaitGetPoses(TrackedDevicePose_t[] pRenderPoseArray, TrackedDevicePose_t[] pGamePoseArray)
		{
			return this.FnTable.WaitGetPoses(pRenderPoseArray, (uint)pRenderPoseArray.Length, pGamePoseArray, (uint)pGamePoseArray.Length);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0009D598 File Offset: 0x0009B798
		public EVRCompositorError GetLastPoses(TrackedDevicePose_t[] pRenderPoseArray, TrackedDevicePose_t[] pGamePoseArray)
		{
			return this.FnTable.GetLastPoses(pRenderPoseArray, (uint)pRenderPoseArray.Length, pGamePoseArray, (uint)pGamePoseArray.Length);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0009D5B2 File Offset: 0x0009B7B2
		public EVRCompositorError GetLastPoseForTrackedDeviceIndex(uint unDeviceIndex, ref TrackedDevicePose_t pOutputPose, ref TrackedDevicePose_t pOutputGamePose)
		{
			return this.FnTable.GetLastPoseForTrackedDeviceIndex(unDeviceIndex, ref pOutputPose, ref pOutputGamePose);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0009D5C7 File Offset: 0x0009B7C7
		public EVRCompositorError Submit(EVREye eEye, ref Texture_t pTexture, ref VRTextureBounds_t pBounds, EVRSubmitFlags nSubmitFlags)
		{
			return this.FnTable.Submit(eEye, ref pTexture, ref pBounds, nSubmitFlags);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0009D5DE File Offset: 0x0009B7DE
		public void ClearLastSubmittedFrame()
		{
			this.FnTable.ClearLastSubmittedFrame();
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0009D5F0 File Offset: 0x0009B7F0
		public void PostPresentHandoff()
		{
			this.FnTable.PostPresentHandoff();
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0009D602 File Offset: 0x0009B802
		public bool GetFrameTiming(ref Compositor_FrameTiming pTiming, uint unFramesAgo)
		{
			return this.FnTable.GetFrameTiming(ref pTiming, unFramesAgo);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0009D616 File Offset: 0x0009B816
		public uint GetFrameTimings(ref Compositor_FrameTiming pTiming, uint nFrames)
		{
			return this.FnTable.GetFrameTimings(ref pTiming, nFrames);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0009D62A File Offset: 0x0009B82A
		public float GetFrameTimeRemaining()
		{
			return this.FnTable.GetFrameTimeRemaining();
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0009D63C File Offset: 0x0009B83C
		public void GetCumulativeStats(ref Compositor_CumulativeStats pStats, uint nStatsSizeInBytes)
		{
			this.FnTable.GetCumulativeStats(ref pStats, nStatsSizeInBytes);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0009D650 File Offset: 0x0009B850
		public void FadeToColor(float fSeconds, float fRed, float fGreen, float fBlue, float fAlpha, bool bBackground)
		{
			this.FnTable.FadeToColor(fSeconds, fRed, fGreen, fBlue, fAlpha, bBackground);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0009D66B File Offset: 0x0009B86B
		public HmdColor_t GetCurrentFadeColor(bool bBackground)
		{
			return this.FnTable.GetCurrentFadeColor(bBackground);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0009D67E File Offset: 0x0009B87E
		public void FadeGrid(float fSeconds, bool bFadeIn)
		{
			this.FnTable.FadeGrid(fSeconds, bFadeIn);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0009D692 File Offset: 0x0009B892
		public float GetCurrentGridAlpha()
		{
			return this.FnTable.GetCurrentGridAlpha();
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0009D6A4 File Offset: 0x0009B8A4
		public EVRCompositorError SetSkyboxOverride(Texture_t[] pTextures)
		{
			return this.FnTable.SetSkyboxOverride(pTextures, (uint)pTextures.Length);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0009D6BA File Offset: 0x0009B8BA
		public void ClearSkyboxOverride()
		{
			this.FnTable.ClearSkyboxOverride();
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0009D6CC File Offset: 0x0009B8CC
		public void CompositorBringToFront()
		{
			this.FnTable.CompositorBringToFront();
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0009D6DE File Offset: 0x0009B8DE
		public void CompositorGoToBack()
		{
			this.FnTable.CompositorGoToBack();
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0009D6F0 File Offset: 0x0009B8F0
		public void CompositorQuit()
		{
			this.FnTable.CompositorQuit();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0009D702 File Offset: 0x0009B902
		public bool IsFullscreen()
		{
			return this.FnTable.IsFullscreen();
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0009D714 File Offset: 0x0009B914
		public uint GetCurrentSceneFocusProcess()
		{
			return this.FnTable.GetCurrentSceneFocusProcess();
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0009D726 File Offset: 0x0009B926
		public uint GetLastFrameRenderer()
		{
			return this.FnTable.GetLastFrameRenderer();
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0009D738 File Offset: 0x0009B938
		public bool CanRenderScene()
		{
			return this.FnTable.CanRenderScene();
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0009D74A File Offset: 0x0009B94A
		public void ShowMirrorWindow()
		{
			this.FnTable.ShowMirrorWindow();
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0009D75C File Offset: 0x0009B95C
		public void HideMirrorWindow()
		{
			this.FnTable.HideMirrorWindow();
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0009D76E File Offset: 0x0009B96E
		public bool IsMirrorWindowVisible()
		{
			return this.FnTable.IsMirrorWindowVisible();
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0009D780 File Offset: 0x0009B980
		public void CompositorDumpImages()
		{
			this.FnTable.CompositorDumpImages();
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0009D792 File Offset: 0x0009B992
		public bool ShouldAppRenderWithLowResources()
		{
			return this.FnTable.ShouldAppRenderWithLowResources();
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0009D7A4 File Offset: 0x0009B9A4
		public void ForceInterleavedReprojectionOn(bool bOverride)
		{
			this.FnTable.ForceInterleavedReprojectionOn(bOverride);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0009D7B7 File Offset: 0x0009B9B7
		public void ForceReconnectProcess()
		{
			this.FnTable.ForceReconnectProcess();
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0009D7C9 File Offset: 0x0009B9C9
		public void SuspendRendering(bool bSuspend)
		{
			this.FnTable.SuspendRendering(bSuspend);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0009D7DC File Offset: 0x0009B9DC
		public EVRCompositorError GetMirrorTextureD3D11(EVREye eEye, IntPtr pD3D11DeviceOrResource, ref IntPtr ppD3D11ShaderResourceView)
		{
			return this.FnTable.GetMirrorTextureD3D11(eEye, pD3D11DeviceOrResource, ref ppD3D11ShaderResourceView);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0009D7F1 File Offset: 0x0009B9F1
		public void ReleaseMirrorTextureD3D11(IntPtr pD3D11ShaderResourceView)
		{
			this.FnTable.ReleaseMirrorTextureD3D11(pD3D11ShaderResourceView);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0009D804 File Offset: 0x0009BA04
		public EVRCompositorError GetMirrorTextureGL(EVREye eEye, ref uint pglTextureId, IntPtr pglSharedTextureHandle)
		{
			pglTextureId = 0U;
			return this.FnTable.GetMirrorTextureGL(eEye, ref pglTextureId, pglSharedTextureHandle);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0009D81C File Offset: 0x0009BA1C
		public bool ReleaseSharedGLTexture(uint glTextureId, IntPtr glSharedTextureHandle)
		{
			return this.FnTable.ReleaseSharedGLTexture(glTextureId, glSharedTextureHandle);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0009D830 File Offset: 0x0009BA30
		public void LockGLSharedTextureForAccess(IntPtr glSharedTextureHandle)
		{
			this.FnTable.LockGLSharedTextureForAccess(glSharedTextureHandle);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0009D843 File Offset: 0x0009BA43
		public void UnlockGLSharedTextureForAccess(IntPtr glSharedTextureHandle)
		{
			this.FnTable.UnlockGLSharedTextureForAccess(glSharedTextureHandle);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0009D856 File Offset: 0x0009BA56
		public uint GetVulkanInstanceExtensionsRequired(StringBuilder pchValue, uint unBufferSize)
		{
			return this.FnTable.GetVulkanInstanceExtensionsRequired(pchValue, unBufferSize);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0009D86A File Offset: 0x0009BA6A
		public uint GetVulkanDeviceExtensionsRequired(IntPtr pPhysicalDevice, StringBuilder pchValue, uint unBufferSize)
		{
			return this.FnTable.GetVulkanDeviceExtensionsRequired(pPhysicalDevice, pchValue, unBufferSize);
		}

		// Token: 0x04001930 RID: 6448
		private IVRCompositor FnTable;
	}
}
