using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200038B RID: 907
	public struct IVRRenderModels
	{
		// Token: 0x040018FF RID: 6399
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._LoadRenderModel_Async LoadRenderModel_Async;

		// Token: 0x04001900 RID: 6400
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._FreeRenderModel FreeRenderModel;

		// Token: 0x04001901 RID: 6401
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._LoadTexture_Async LoadTexture_Async;

		// Token: 0x04001902 RID: 6402
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._FreeTexture FreeTexture;

		// Token: 0x04001903 RID: 6403
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._LoadTextureD3D11_Async LoadTextureD3D11_Async;

		// Token: 0x04001904 RID: 6404
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._LoadIntoTextureD3D11_Async LoadIntoTextureD3D11_Async;

		// Token: 0x04001905 RID: 6405
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._FreeTextureD3D11 FreeTextureD3D11;

		// Token: 0x04001906 RID: 6406
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetRenderModelName GetRenderModelName;

		// Token: 0x04001907 RID: 6407
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetRenderModelCount GetRenderModelCount;

		// Token: 0x04001908 RID: 6408
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetComponentCount GetComponentCount;

		// Token: 0x04001909 RID: 6409
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetComponentName GetComponentName;

		// Token: 0x0400190A RID: 6410
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetComponentButtonMask GetComponentButtonMask;

		// Token: 0x0400190B RID: 6411
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetComponentRenderModelName GetComponentRenderModelName;

		// Token: 0x0400190C RID: 6412
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetComponentState GetComponentState;

		// Token: 0x0400190D RID: 6413
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._RenderModelHasComponent RenderModelHasComponent;

		// Token: 0x0400190E RID: 6414
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetRenderModelThumbnailURL GetRenderModelThumbnailURL;

		// Token: 0x0400190F RID: 6415
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetRenderModelOriginalPath GetRenderModelOriginalPath;

		// Token: 0x04001910 RID: 6416
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRRenderModels._GetRenderModelErrorNameFromEnum GetRenderModelErrorNameFromEnum;

		// Token: 0x02000736 RID: 1846
		// (Invoke) Token: 0x06002EDF RID: 11999
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRRenderModelError _LoadRenderModel_Async(string pchRenderModelName, ref IntPtr ppRenderModel);

		// Token: 0x02000737 RID: 1847
		// (Invoke) Token: 0x06002EE3 RID: 12003
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _FreeRenderModel(IntPtr pRenderModel);

		// Token: 0x02000738 RID: 1848
		// (Invoke) Token: 0x06002EE7 RID: 12007
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRRenderModelError _LoadTexture_Async(int textureId, ref IntPtr ppTexture);

		// Token: 0x02000739 RID: 1849
		// (Invoke) Token: 0x06002EEB RID: 12011
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _FreeTexture(IntPtr pTexture);

		// Token: 0x0200073A RID: 1850
		// (Invoke) Token: 0x06002EEF RID: 12015
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRRenderModelError _LoadTextureD3D11_Async(int textureId, IntPtr pD3D11Device, ref IntPtr ppD3D11Texture2D);

		// Token: 0x0200073B RID: 1851
		// (Invoke) Token: 0x06002EF3 RID: 12019
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRRenderModelError _LoadIntoTextureD3D11_Async(int textureId, IntPtr pDstTexture);

		// Token: 0x0200073C RID: 1852
		// (Invoke) Token: 0x06002EF7 RID: 12023
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _FreeTextureD3D11(IntPtr pD3D11Texture2D);

		// Token: 0x0200073D RID: 1853
		// (Invoke) Token: 0x06002EFB RID: 12027
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetRenderModelName(uint unRenderModelIndex, StringBuilder pchRenderModelName, uint unRenderModelNameLen);

		// Token: 0x0200073E RID: 1854
		// (Invoke) Token: 0x06002EFF RID: 12031
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetRenderModelCount();

		// Token: 0x0200073F RID: 1855
		// (Invoke) Token: 0x06002F03 RID: 12035
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetComponentCount(string pchRenderModelName);

		// Token: 0x02000740 RID: 1856
		// (Invoke) Token: 0x06002F07 RID: 12039
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetComponentName(string pchRenderModelName, uint unComponentIndex, StringBuilder pchComponentName, uint unComponentNameLen);

		// Token: 0x02000741 RID: 1857
		// (Invoke) Token: 0x06002F0B RID: 12043
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ulong _GetComponentButtonMask(string pchRenderModelName, string pchComponentName);

		// Token: 0x02000742 RID: 1858
		// (Invoke) Token: 0x06002F0F RID: 12047
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetComponentRenderModelName(string pchRenderModelName, string pchComponentName, StringBuilder pchComponentRenderModelName, uint unComponentRenderModelNameLen);

		// Token: 0x02000743 RID: 1859
		// (Invoke) Token: 0x06002F13 RID: 12051
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetComponentState(string pchRenderModelName, string pchComponentName, ref VRControllerState_t pControllerState, ref RenderModel_ControllerMode_State_t pState, ref RenderModel_ComponentState_t pComponentState);

		// Token: 0x02000744 RID: 1860
		// (Invoke) Token: 0x06002F17 RID: 12055
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _RenderModelHasComponent(string pchRenderModelName, string pchComponentName);

		// Token: 0x02000745 RID: 1861
		// (Invoke) Token: 0x06002F1B RID: 12059
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetRenderModelThumbnailURL(string pchRenderModelName, StringBuilder pchThumbnailURL, uint unThumbnailURLLen, ref EVRRenderModelError peError);

		// Token: 0x02000746 RID: 1862
		// (Invoke) Token: 0x06002F1F RID: 12063
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetRenderModelOriginalPath(string pchRenderModelName, StringBuilder pchOriginalPath, uint unOriginalPathLen, ref EVRRenderModelError peError);

		// Token: 0x02000747 RID: 1863
		// (Invoke) Token: 0x06002F23 RID: 12067
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetRenderModelErrorNameFromEnum(EVRRenderModelError error);
	}
}
