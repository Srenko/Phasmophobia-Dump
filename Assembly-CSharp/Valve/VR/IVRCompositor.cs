using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000389 RID: 905
	public struct IVRCompositor
	{
		// Token: 0x04001886 RID: 6278
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._SetTrackingSpace SetTrackingSpace;

		// Token: 0x04001887 RID: 6279
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetTrackingSpace GetTrackingSpace;

		// Token: 0x04001888 RID: 6280
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._WaitGetPoses WaitGetPoses;

		// Token: 0x04001889 RID: 6281
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetLastPoses GetLastPoses;

		// Token: 0x0400188A RID: 6282
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetLastPoseForTrackedDeviceIndex GetLastPoseForTrackedDeviceIndex;

		// Token: 0x0400188B RID: 6283
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._Submit Submit;

		// Token: 0x0400188C RID: 6284
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ClearLastSubmittedFrame ClearLastSubmittedFrame;

		// Token: 0x0400188D RID: 6285
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._PostPresentHandoff PostPresentHandoff;

		// Token: 0x0400188E RID: 6286
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetFrameTiming GetFrameTiming;

		// Token: 0x0400188F RID: 6287
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetFrameTimings GetFrameTimings;

		// Token: 0x04001890 RID: 6288
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetFrameTimeRemaining GetFrameTimeRemaining;

		// Token: 0x04001891 RID: 6289
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetCumulativeStats GetCumulativeStats;

		// Token: 0x04001892 RID: 6290
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._FadeToColor FadeToColor;

		// Token: 0x04001893 RID: 6291
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetCurrentFadeColor GetCurrentFadeColor;

		// Token: 0x04001894 RID: 6292
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._FadeGrid FadeGrid;

		// Token: 0x04001895 RID: 6293
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetCurrentGridAlpha GetCurrentGridAlpha;

		// Token: 0x04001896 RID: 6294
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._SetSkyboxOverride SetSkyboxOverride;

		// Token: 0x04001897 RID: 6295
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ClearSkyboxOverride ClearSkyboxOverride;

		// Token: 0x04001898 RID: 6296
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._CompositorBringToFront CompositorBringToFront;

		// Token: 0x04001899 RID: 6297
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._CompositorGoToBack CompositorGoToBack;

		// Token: 0x0400189A RID: 6298
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._CompositorQuit CompositorQuit;

		// Token: 0x0400189B RID: 6299
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._IsFullscreen IsFullscreen;

		// Token: 0x0400189C RID: 6300
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetCurrentSceneFocusProcess GetCurrentSceneFocusProcess;

		// Token: 0x0400189D RID: 6301
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetLastFrameRenderer GetLastFrameRenderer;

		// Token: 0x0400189E RID: 6302
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._CanRenderScene CanRenderScene;

		// Token: 0x0400189F RID: 6303
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ShowMirrorWindow ShowMirrorWindow;

		// Token: 0x040018A0 RID: 6304
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._HideMirrorWindow HideMirrorWindow;

		// Token: 0x040018A1 RID: 6305
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._IsMirrorWindowVisible IsMirrorWindowVisible;

		// Token: 0x040018A2 RID: 6306
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._CompositorDumpImages CompositorDumpImages;

		// Token: 0x040018A3 RID: 6307
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ShouldAppRenderWithLowResources ShouldAppRenderWithLowResources;

		// Token: 0x040018A4 RID: 6308
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ForceInterleavedReprojectionOn ForceInterleavedReprojectionOn;

		// Token: 0x040018A5 RID: 6309
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ForceReconnectProcess ForceReconnectProcess;

		// Token: 0x040018A6 RID: 6310
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._SuspendRendering SuspendRendering;

		// Token: 0x040018A7 RID: 6311
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetMirrorTextureD3D11 GetMirrorTextureD3D11;

		// Token: 0x040018A8 RID: 6312
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ReleaseMirrorTextureD3D11 ReleaseMirrorTextureD3D11;

		// Token: 0x040018A9 RID: 6313
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetMirrorTextureGL GetMirrorTextureGL;

		// Token: 0x040018AA RID: 6314
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._ReleaseSharedGLTexture ReleaseSharedGLTexture;

		// Token: 0x040018AB RID: 6315
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._LockGLSharedTextureForAccess LockGLSharedTextureForAccess;

		// Token: 0x040018AC RID: 6316
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._UnlockGLSharedTextureForAccess UnlockGLSharedTextureForAccess;

		// Token: 0x040018AD RID: 6317
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetVulkanInstanceExtensionsRequired GetVulkanInstanceExtensionsRequired;

		// Token: 0x040018AE RID: 6318
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRCompositor._GetVulkanDeviceExtensionsRequired GetVulkanDeviceExtensionsRequired;

		// Token: 0x020006BD RID: 1725
		// (Invoke) Token: 0x06002CFB RID: 11515
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetTrackingSpace(ETrackingUniverseOrigin eOrigin);

		// Token: 0x020006BE RID: 1726
		// (Invoke) Token: 0x06002CFF RID: 11519
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ETrackingUniverseOrigin _GetTrackingSpace();

		// Token: 0x020006BF RID: 1727
		// (Invoke) Token: 0x06002D03 RID: 11523
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _WaitGetPoses([In] [Out] TrackedDevicePose_t[] pRenderPoseArray, uint unRenderPoseArrayCount, [In] [Out] TrackedDevicePose_t[] pGamePoseArray, uint unGamePoseArrayCount);

		// Token: 0x020006C0 RID: 1728
		// (Invoke) Token: 0x06002D07 RID: 11527
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _GetLastPoses([In] [Out] TrackedDevicePose_t[] pRenderPoseArray, uint unRenderPoseArrayCount, [In] [Out] TrackedDevicePose_t[] pGamePoseArray, uint unGamePoseArrayCount);

		// Token: 0x020006C1 RID: 1729
		// (Invoke) Token: 0x06002D0B RID: 11531
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _GetLastPoseForTrackedDeviceIndex(uint unDeviceIndex, ref TrackedDevicePose_t pOutputPose, ref TrackedDevicePose_t pOutputGamePose);

		// Token: 0x020006C2 RID: 1730
		// (Invoke) Token: 0x06002D0F RID: 11535
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _Submit(EVREye eEye, ref Texture_t pTexture, ref VRTextureBounds_t pBounds, EVRSubmitFlags nSubmitFlags);

		// Token: 0x020006C3 RID: 1731
		// (Invoke) Token: 0x06002D13 RID: 11539
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ClearLastSubmittedFrame();

		// Token: 0x020006C4 RID: 1732
		// (Invoke) Token: 0x06002D17 RID: 11543
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _PostPresentHandoff();

		// Token: 0x020006C5 RID: 1733
		// (Invoke) Token: 0x06002D1B RID: 11547
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetFrameTiming(ref Compositor_FrameTiming pTiming, uint unFramesAgo);

		// Token: 0x020006C6 RID: 1734
		// (Invoke) Token: 0x06002D1F RID: 11551
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetFrameTimings(ref Compositor_FrameTiming pTiming, uint nFrames);

		// Token: 0x020006C7 RID: 1735
		// (Invoke) Token: 0x06002D23 RID: 11555
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate float _GetFrameTimeRemaining();

		// Token: 0x020006C8 RID: 1736
		// (Invoke) Token: 0x06002D27 RID: 11559
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetCumulativeStats(ref Compositor_CumulativeStats pStats, uint nStatsSizeInBytes);

		// Token: 0x020006C9 RID: 1737
		// (Invoke) Token: 0x06002D2B RID: 11563
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _FadeToColor(float fSeconds, float fRed, float fGreen, float fBlue, float fAlpha, bool bBackground);

		// Token: 0x020006CA RID: 1738
		// (Invoke) Token: 0x06002D2F RID: 11567
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HmdColor_t _GetCurrentFadeColor(bool bBackground);

		// Token: 0x020006CB RID: 1739
		// (Invoke) Token: 0x06002D33 RID: 11571
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _FadeGrid(float fSeconds, bool bFadeIn);

		// Token: 0x020006CC RID: 1740
		// (Invoke) Token: 0x06002D37 RID: 11575
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate float _GetCurrentGridAlpha();

		// Token: 0x020006CD RID: 1741
		// (Invoke) Token: 0x06002D3B RID: 11579
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _SetSkyboxOverride([In] [Out] Texture_t[] pTextures, uint unTextureCount);

		// Token: 0x020006CE RID: 1742
		// (Invoke) Token: 0x06002D3F RID: 11583
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ClearSkyboxOverride();

		// Token: 0x020006CF RID: 1743
		// (Invoke) Token: 0x06002D43 RID: 11587
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _CompositorBringToFront();

		// Token: 0x020006D0 RID: 1744
		// (Invoke) Token: 0x06002D47 RID: 11591
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _CompositorGoToBack();

		// Token: 0x020006D1 RID: 1745
		// (Invoke) Token: 0x06002D4B RID: 11595
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _CompositorQuit();

		// Token: 0x020006D2 RID: 1746
		// (Invoke) Token: 0x06002D4F RID: 11599
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsFullscreen();

		// Token: 0x020006D3 RID: 1747
		// (Invoke) Token: 0x06002D53 RID: 11603
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetCurrentSceneFocusProcess();

		// Token: 0x020006D4 RID: 1748
		// (Invoke) Token: 0x06002D57 RID: 11607
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetLastFrameRenderer();

		// Token: 0x020006D5 RID: 1749
		// (Invoke) Token: 0x06002D5B RID: 11611
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _CanRenderScene();

		// Token: 0x020006D6 RID: 1750
		// (Invoke) Token: 0x06002D5F RID: 11615
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ShowMirrorWindow();

		// Token: 0x020006D7 RID: 1751
		// (Invoke) Token: 0x06002D63 RID: 11619
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _HideMirrorWindow();

		// Token: 0x020006D8 RID: 1752
		// (Invoke) Token: 0x06002D67 RID: 11623
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsMirrorWindowVisible();

		// Token: 0x020006D9 RID: 1753
		// (Invoke) Token: 0x06002D6B RID: 11627
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _CompositorDumpImages();

		// Token: 0x020006DA RID: 1754
		// (Invoke) Token: 0x06002D6F RID: 11631
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _ShouldAppRenderWithLowResources();

		// Token: 0x020006DB RID: 1755
		// (Invoke) Token: 0x06002D73 RID: 11635
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ForceInterleavedReprojectionOn(bool bOverride);

		// Token: 0x020006DC RID: 1756
		// (Invoke) Token: 0x06002D77 RID: 11639
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ForceReconnectProcess();

		// Token: 0x020006DD RID: 1757
		// (Invoke) Token: 0x06002D7B RID: 11643
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SuspendRendering(bool bSuspend);

		// Token: 0x020006DE RID: 1758
		// (Invoke) Token: 0x06002D7F RID: 11647
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _GetMirrorTextureD3D11(EVREye eEye, IntPtr pD3D11DeviceOrResource, ref IntPtr ppD3D11ShaderResourceView);

		// Token: 0x020006DF RID: 1759
		// (Invoke) Token: 0x06002D83 RID: 11651
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ReleaseMirrorTextureD3D11(IntPtr pD3D11ShaderResourceView);

		// Token: 0x020006E0 RID: 1760
		// (Invoke) Token: 0x06002D87 RID: 11655
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRCompositorError _GetMirrorTextureGL(EVREye eEye, ref uint pglTextureId, IntPtr pglSharedTextureHandle);

		// Token: 0x020006E1 RID: 1761
		// (Invoke) Token: 0x06002D8B RID: 11659
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _ReleaseSharedGLTexture(uint glTextureId, IntPtr glSharedTextureHandle);

		// Token: 0x020006E2 RID: 1762
		// (Invoke) Token: 0x06002D8F RID: 11663
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _LockGLSharedTextureForAccess(IntPtr glSharedTextureHandle);

		// Token: 0x020006E3 RID: 1763
		// (Invoke) Token: 0x06002D93 RID: 11667
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _UnlockGLSharedTextureForAccess(IntPtr glSharedTextureHandle);

		// Token: 0x020006E4 RID: 1764
		// (Invoke) Token: 0x06002D97 RID: 11671
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetVulkanInstanceExtensionsRequired(StringBuilder pchValue, uint unBufferSize);

		// Token: 0x020006E5 RID: 1765
		// (Invoke) Token: 0x06002D9B RID: 11675
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetVulkanDeviceExtensionsRequired(IntPtr pPhysicalDevice, StringBuilder pchValue, uint unBufferSize);
	}
}
