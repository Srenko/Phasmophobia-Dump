using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000399 RID: 921
	public class CVRRenderModels
	{
		// Token: 0x06001FCD RID: 8141 RVA: 0x0009E031 File Offset: 0x0009C231
		internal CVRRenderModels(IntPtr pInterface)
		{
			this.FnTable = (IVRRenderModels)Marshal.PtrToStructure(pInterface, typeof(IVRRenderModels));
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0009E054 File Offset: 0x0009C254
		public EVRRenderModelError LoadRenderModel_Async(string pchRenderModelName, ref IntPtr ppRenderModel)
		{
			return this.FnTable.LoadRenderModel_Async(pchRenderModelName, ref ppRenderModel);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0009E068 File Offset: 0x0009C268
		public void FreeRenderModel(IntPtr pRenderModel)
		{
			this.FnTable.FreeRenderModel(pRenderModel);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0009E07B File Offset: 0x0009C27B
		public EVRRenderModelError LoadTexture_Async(int textureId, ref IntPtr ppTexture)
		{
			return this.FnTable.LoadTexture_Async(textureId, ref ppTexture);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0009E08F File Offset: 0x0009C28F
		public void FreeTexture(IntPtr pTexture)
		{
			this.FnTable.FreeTexture(pTexture);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0009E0A2 File Offset: 0x0009C2A2
		public EVRRenderModelError LoadTextureD3D11_Async(int textureId, IntPtr pD3D11Device, ref IntPtr ppD3D11Texture2D)
		{
			return this.FnTable.LoadTextureD3D11_Async(textureId, pD3D11Device, ref ppD3D11Texture2D);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0009E0B7 File Offset: 0x0009C2B7
		public EVRRenderModelError LoadIntoTextureD3D11_Async(int textureId, IntPtr pDstTexture)
		{
			return this.FnTable.LoadIntoTextureD3D11_Async(textureId, pDstTexture);
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0009E0CB File Offset: 0x0009C2CB
		public void FreeTextureD3D11(IntPtr pD3D11Texture2D)
		{
			this.FnTable.FreeTextureD3D11(pD3D11Texture2D);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0009E0DE File Offset: 0x0009C2DE
		public uint GetRenderModelName(uint unRenderModelIndex, StringBuilder pchRenderModelName, uint unRenderModelNameLen)
		{
			return this.FnTable.GetRenderModelName(unRenderModelIndex, pchRenderModelName, unRenderModelNameLen);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0009E0F3 File Offset: 0x0009C2F3
		public uint GetRenderModelCount()
		{
			return this.FnTable.GetRenderModelCount();
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0009E105 File Offset: 0x0009C305
		public uint GetComponentCount(string pchRenderModelName)
		{
			return this.FnTable.GetComponentCount(pchRenderModelName);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0009E118 File Offset: 0x0009C318
		public uint GetComponentName(string pchRenderModelName, uint unComponentIndex, StringBuilder pchComponentName, uint unComponentNameLen)
		{
			return this.FnTable.GetComponentName(pchRenderModelName, unComponentIndex, pchComponentName, unComponentNameLen);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0009E12F File Offset: 0x0009C32F
		public ulong GetComponentButtonMask(string pchRenderModelName, string pchComponentName)
		{
			return this.FnTable.GetComponentButtonMask(pchRenderModelName, pchComponentName);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0009E143 File Offset: 0x0009C343
		public uint GetComponentRenderModelName(string pchRenderModelName, string pchComponentName, StringBuilder pchComponentRenderModelName, uint unComponentRenderModelNameLen)
		{
			return this.FnTable.GetComponentRenderModelName(pchRenderModelName, pchComponentName, pchComponentRenderModelName, unComponentRenderModelNameLen);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0009E15C File Offset: 0x0009C35C
		public bool GetComponentState(string pchRenderModelName, string pchComponentName, ref VRControllerState_t pControllerState, ref RenderModel_ControllerMode_State_t pState, ref RenderModel_ComponentState_t pComponentState)
		{
			if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
			{
				VRControllerState_t_Packed vrcontrollerState_t_Packed = new VRControllerState_t_Packed(pControllerState);
				CVRRenderModels.GetComponentStateUnion getComponentStateUnion;
				getComponentStateUnion.pGetComponentStatePacked = null;
				getComponentStateUnion.pGetComponentState = this.FnTable.GetComponentState;
				bool result = getComponentStateUnion.pGetComponentStatePacked(pchRenderModelName, pchComponentName, ref vrcontrollerState_t_Packed, ref pState, ref pComponentState);
				vrcontrollerState_t_Packed.Unpack(ref pControllerState);
				return result;
			}
			return this.FnTable.GetComponentState(pchRenderModelName, pchComponentName, ref pControllerState, ref pState, ref pComponentState);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0009E1DD File Offset: 0x0009C3DD
		public bool RenderModelHasComponent(string pchRenderModelName, string pchComponentName)
		{
			return this.FnTable.RenderModelHasComponent(pchRenderModelName, pchComponentName);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0009E1F1 File Offset: 0x0009C3F1
		public uint GetRenderModelThumbnailURL(string pchRenderModelName, StringBuilder pchThumbnailURL, uint unThumbnailURLLen, ref EVRRenderModelError peError)
		{
			return this.FnTable.GetRenderModelThumbnailURL(pchRenderModelName, pchThumbnailURL, unThumbnailURLLen, ref peError);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0009E208 File Offset: 0x0009C408
		public uint GetRenderModelOriginalPath(string pchRenderModelName, StringBuilder pchOriginalPath, uint unOriginalPathLen, ref EVRRenderModelError peError)
		{
			return this.FnTable.GetRenderModelOriginalPath(pchRenderModelName, pchOriginalPath, unOriginalPathLen, ref peError);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0009E21F File Offset: 0x0009C41F
		public string GetRenderModelErrorNameFromEnum(EVRRenderModelError error)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetRenderModelErrorNameFromEnum(error));
		}

		// Token: 0x04001932 RID: 6450
		private IVRRenderModels FnTable;

		// Token: 0x02000769 RID: 1897
		// (Invoke) Token: 0x06002F9B RID: 12187
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetComponentStatePacked(string pchRenderModelName, string pchComponentName, ref VRControllerState_t_Packed pControllerState, ref RenderModel_ControllerMode_State_t pState, ref RenderModel_ComponentState_t pComponentState);

		// Token: 0x0200076A RID: 1898
		[StructLayout(LayoutKind.Explicit)]
		private struct GetComponentStateUnion
		{
			// Token: 0x040028EE RID: 10478
			[FieldOffset(0)]
			public IVRRenderModels._GetComponentState pGetComponentState;

			// Token: 0x040028EF RID: 10479
			[FieldOffset(0)]
			public CVRRenderModels._GetComponentStatePacked pGetComponentStatePacked;
		}
	}
}
