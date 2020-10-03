using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000398 RID: 920
	public class CVROverlay
	{
		// Token: 0x06001F7C RID: 8060 RVA: 0x0009D87F File Offset: 0x0009BA7F
		internal CVROverlay(IntPtr pInterface)
		{
			this.FnTable = (IVROverlay)Marshal.PtrToStructure(pInterface, typeof(IVROverlay));
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0009D8A2 File Offset: 0x0009BAA2
		public EVROverlayError FindOverlay(string pchOverlayKey, ref ulong pOverlayHandle)
		{
			pOverlayHandle = 0UL;
			return this.FnTable.FindOverlay(pchOverlayKey, ref pOverlayHandle);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0009D8BA File Offset: 0x0009BABA
		public EVROverlayError CreateOverlay(string pchOverlayKey, string pchOverlayName, ref ulong pOverlayHandle)
		{
			pOverlayHandle = 0UL;
			return this.FnTable.CreateOverlay(pchOverlayKey, pchOverlayName, ref pOverlayHandle);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public EVROverlayError DestroyOverlay(ulong ulOverlayHandle)
		{
			return this.FnTable.DestroyOverlay(ulOverlayHandle);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0009D8E6 File Offset: 0x0009BAE6
		public EVROverlayError SetHighQualityOverlay(ulong ulOverlayHandle)
		{
			return this.FnTable.SetHighQualityOverlay(ulOverlayHandle);
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0009D8F9 File Offset: 0x0009BAF9
		public ulong GetHighQualityOverlay()
		{
			return this.FnTable.GetHighQualityOverlay();
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0009D90B File Offset: 0x0009BB0B
		public uint GetOverlayKey(ulong ulOverlayHandle, StringBuilder pchValue, uint unBufferSize, ref EVROverlayError pError)
		{
			return this.FnTable.GetOverlayKey(ulOverlayHandle, pchValue, unBufferSize, ref pError);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0009D922 File Offset: 0x0009BB22
		public uint GetOverlayName(ulong ulOverlayHandle, StringBuilder pchValue, uint unBufferSize, ref EVROverlayError pError)
		{
			return this.FnTable.GetOverlayName(ulOverlayHandle, pchValue, unBufferSize, ref pError);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0009D939 File Offset: 0x0009BB39
		public EVROverlayError SetOverlayName(ulong ulOverlayHandle, string pchName)
		{
			return this.FnTable.SetOverlayName(ulOverlayHandle, pchName);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x0009D94D File Offset: 0x0009BB4D
		public EVROverlayError GetOverlayImageData(ulong ulOverlayHandle, IntPtr pvBuffer, uint unBufferSize, ref uint punWidth, ref uint punHeight)
		{
			punWidth = 0U;
			punHeight = 0U;
			return this.FnTable.GetOverlayImageData(ulOverlayHandle, pvBuffer, unBufferSize, ref punWidth, ref punHeight);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0009D96E File Offset: 0x0009BB6E
		public string GetOverlayErrorNameFromEnum(EVROverlayError error)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetOverlayErrorNameFromEnum(error));
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0009D986 File Offset: 0x0009BB86
		public EVROverlayError SetOverlayRenderingPid(ulong ulOverlayHandle, uint unPID)
		{
			return this.FnTable.SetOverlayRenderingPid(ulOverlayHandle, unPID);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0009D99A File Offset: 0x0009BB9A
		public uint GetOverlayRenderingPid(ulong ulOverlayHandle)
		{
			return this.FnTable.GetOverlayRenderingPid(ulOverlayHandle);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0009D9AD File Offset: 0x0009BBAD
		public EVROverlayError SetOverlayFlag(ulong ulOverlayHandle, VROverlayFlags eOverlayFlag, bool bEnabled)
		{
			return this.FnTable.SetOverlayFlag(ulOverlayHandle, eOverlayFlag, bEnabled);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0009D9C2 File Offset: 0x0009BBC2
		public EVROverlayError GetOverlayFlag(ulong ulOverlayHandle, VROverlayFlags eOverlayFlag, ref bool pbEnabled)
		{
			pbEnabled = false;
			return this.FnTable.GetOverlayFlag(ulOverlayHandle, eOverlayFlag, ref pbEnabled);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0009D9DA File Offset: 0x0009BBDA
		public EVROverlayError SetOverlayColor(ulong ulOverlayHandle, float fRed, float fGreen, float fBlue)
		{
			return this.FnTable.SetOverlayColor(ulOverlayHandle, fRed, fGreen, fBlue);
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0009D9F1 File Offset: 0x0009BBF1
		public EVROverlayError GetOverlayColor(ulong ulOverlayHandle, ref float pfRed, ref float pfGreen, ref float pfBlue)
		{
			pfRed = 0f;
			pfGreen = 0f;
			pfBlue = 0f;
			return this.FnTable.GetOverlayColor(ulOverlayHandle, ref pfRed, ref pfGreen, ref pfBlue);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0009DA1E File Offset: 0x0009BC1E
		public EVROverlayError SetOverlayAlpha(ulong ulOverlayHandle, float fAlpha)
		{
			return this.FnTable.SetOverlayAlpha(ulOverlayHandle, fAlpha);
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0009DA32 File Offset: 0x0009BC32
		public EVROverlayError GetOverlayAlpha(ulong ulOverlayHandle, ref float pfAlpha)
		{
			pfAlpha = 0f;
			return this.FnTable.GetOverlayAlpha(ulOverlayHandle, ref pfAlpha);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0009DA4D File Offset: 0x0009BC4D
		public EVROverlayError SetOverlayTexelAspect(ulong ulOverlayHandle, float fTexelAspect)
		{
			return this.FnTable.SetOverlayTexelAspect(ulOverlayHandle, fTexelAspect);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0009DA61 File Offset: 0x0009BC61
		public EVROverlayError GetOverlayTexelAspect(ulong ulOverlayHandle, ref float pfTexelAspect)
		{
			pfTexelAspect = 0f;
			return this.FnTable.GetOverlayTexelAspect(ulOverlayHandle, ref pfTexelAspect);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0009DA7C File Offset: 0x0009BC7C
		public EVROverlayError SetOverlaySortOrder(ulong ulOverlayHandle, uint unSortOrder)
		{
			return this.FnTable.SetOverlaySortOrder(ulOverlayHandle, unSortOrder);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0009DA90 File Offset: 0x0009BC90
		public EVROverlayError GetOverlaySortOrder(ulong ulOverlayHandle, ref uint punSortOrder)
		{
			punSortOrder = 0U;
			return this.FnTable.GetOverlaySortOrder(ulOverlayHandle, ref punSortOrder);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0009DAA7 File Offset: 0x0009BCA7
		public EVROverlayError SetOverlayWidthInMeters(ulong ulOverlayHandle, float fWidthInMeters)
		{
			return this.FnTable.SetOverlayWidthInMeters(ulOverlayHandle, fWidthInMeters);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0009DABB File Offset: 0x0009BCBB
		public EVROverlayError GetOverlayWidthInMeters(ulong ulOverlayHandle, ref float pfWidthInMeters)
		{
			pfWidthInMeters = 0f;
			return this.FnTable.GetOverlayWidthInMeters(ulOverlayHandle, ref pfWidthInMeters);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0009DAD6 File Offset: 0x0009BCD6
		public EVROverlayError SetOverlayAutoCurveDistanceRangeInMeters(ulong ulOverlayHandle, float fMinDistanceInMeters, float fMaxDistanceInMeters)
		{
			return this.FnTable.SetOverlayAutoCurveDistanceRangeInMeters(ulOverlayHandle, fMinDistanceInMeters, fMaxDistanceInMeters);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0009DAEB File Offset: 0x0009BCEB
		public EVROverlayError GetOverlayAutoCurveDistanceRangeInMeters(ulong ulOverlayHandle, ref float pfMinDistanceInMeters, ref float pfMaxDistanceInMeters)
		{
			pfMinDistanceInMeters = 0f;
			pfMaxDistanceInMeters = 0f;
			return this.FnTable.GetOverlayAutoCurveDistanceRangeInMeters(ulOverlayHandle, ref pfMinDistanceInMeters, ref pfMaxDistanceInMeters);
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0009DB0E File Offset: 0x0009BD0E
		public EVROverlayError SetOverlayTextureColorSpace(ulong ulOverlayHandle, EColorSpace eTextureColorSpace)
		{
			return this.FnTable.SetOverlayTextureColorSpace(ulOverlayHandle, eTextureColorSpace);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0009DB22 File Offset: 0x0009BD22
		public EVROverlayError GetOverlayTextureColorSpace(ulong ulOverlayHandle, ref EColorSpace peTextureColorSpace)
		{
			return this.FnTable.GetOverlayTextureColorSpace(ulOverlayHandle, ref peTextureColorSpace);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0009DB36 File Offset: 0x0009BD36
		public EVROverlayError SetOverlayTextureBounds(ulong ulOverlayHandle, ref VRTextureBounds_t pOverlayTextureBounds)
		{
			return this.FnTable.SetOverlayTextureBounds(ulOverlayHandle, ref pOverlayTextureBounds);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0009DB4A File Offset: 0x0009BD4A
		public EVROverlayError GetOverlayTextureBounds(ulong ulOverlayHandle, ref VRTextureBounds_t pOverlayTextureBounds)
		{
			return this.FnTable.GetOverlayTextureBounds(ulOverlayHandle, ref pOverlayTextureBounds);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0009DB5E File Offset: 0x0009BD5E
		public uint GetOverlayRenderModel(ulong ulOverlayHandle, string pchValue, uint unBufferSize, ref HmdColor_t pColor, ref EVROverlayError pError)
		{
			return this.FnTable.GetOverlayRenderModel(ulOverlayHandle, pchValue, unBufferSize, ref pColor, ref pError);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0009DB77 File Offset: 0x0009BD77
		public EVROverlayError SetOverlayRenderModel(ulong ulOverlayHandle, string pchRenderModel, ref HmdColor_t pColor)
		{
			return this.FnTable.SetOverlayRenderModel(ulOverlayHandle, pchRenderModel, ref pColor);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0009DB8C File Offset: 0x0009BD8C
		public EVROverlayError GetOverlayTransformType(ulong ulOverlayHandle, ref VROverlayTransformType peTransformType)
		{
			return this.FnTable.GetOverlayTransformType(ulOverlayHandle, ref peTransformType);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0009DBA0 File Offset: 0x0009BDA0
		public EVROverlayError SetOverlayTransformAbsolute(ulong ulOverlayHandle, ETrackingUniverseOrigin eTrackingOrigin, ref HmdMatrix34_t pmatTrackingOriginToOverlayTransform)
		{
			return this.FnTable.SetOverlayTransformAbsolute(ulOverlayHandle, eTrackingOrigin, ref pmatTrackingOriginToOverlayTransform);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0009DBB5 File Offset: 0x0009BDB5
		public EVROverlayError GetOverlayTransformAbsolute(ulong ulOverlayHandle, ref ETrackingUniverseOrigin peTrackingOrigin, ref HmdMatrix34_t pmatTrackingOriginToOverlayTransform)
		{
			return this.FnTable.GetOverlayTransformAbsolute(ulOverlayHandle, ref peTrackingOrigin, ref pmatTrackingOriginToOverlayTransform);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0009DBCA File Offset: 0x0009BDCA
		public EVROverlayError SetOverlayTransformTrackedDeviceRelative(ulong ulOverlayHandle, uint unTrackedDevice, ref HmdMatrix34_t pmatTrackedDeviceToOverlayTransform)
		{
			return this.FnTable.SetOverlayTransformTrackedDeviceRelative(ulOverlayHandle, unTrackedDevice, ref pmatTrackedDeviceToOverlayTransform);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0009DBDF File Offset: 0x0009BDDF
		public EVROverlayError GetOverlayTransformTrackedDeviceRelative(ulong ulOverlayHandle, ref uint punTrackedDevice, ref HmdMatrix34_t pmatTrackedDeviceToOverlayTransform)
		{
			punTrackedDevice = 0U;
			return this.FnTable.GetOverlayTransformTrackedDeviceRelative(ulOverlayHandle, ref punTrackedDevice, ref pmatTrackedDeviceToOverlayTransform);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0009DBF7 File Offset: 0x0009BDF7
		public EVROverlayError SetOverlayTransformTrackedDeviceComponent(ulong ulOverlayHandle, uint unDeviceIndex, string pchComponentName)
		{
			return this.FnTable.SetOverlayTransformTrackedDeviceComponent(ulOverlayHandle, unDeviceIndex, pchComponentName);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0009DC0C File Offset: 0x0009BE0C
		public EVROverlayError GetOverlayTransformTrackedDeviceComponent(ulong ulOverlayHandle, ref uint punDeviceIndex, string pchComponentName, uint unComponentNameSize)
		{
			punDeviceIndex = 0U;
			return this.FnTable.GetOverlayTransformTrackedDeviceComponent(ulOverlayHandle, ref punDeviceIndex, pchComponentName, unComponentNameSize);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0009DC26 File Offset: 0x0009BE26
		public EVROverlayError GetOverlayTransformOverlayRelative(ulong ulOverlayHandle, ref ulong ulOverlayHandleParent, ref HmdMatrix34_t pmatParentOverlayToOverlayTransform)
		{
			ulOverlayHandleParent = 0UL;
			return this.FnTable.GetOverlayTransformOverlayRelative(ulOverlayHandle, ref ulOverlayHandleParent, ref pmatParentOverlayToOverlayTransform);
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0009DC3F File Offset: 0x0009BE3F
		public EVROverlayError SetOverlayTransformOverlayRelative(ulong ulOverlayHandle, ulong ulOverlayHandleParent, ref HmdMatrix34_t pmatParentOverlayToOverlayTransform)
		{
			return this.FnTable.SetOverlayTransformOverlayRelative(ulOverlayHandle, ulOverlayHandleParent, ref pmatParentOverlayToOverlayTransform);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0009DC54 File Offset: 0x0009BE54
		public EVROverlayError ShowOverlay(ulong ulOverlayHandle)
		{
			return this.FnTable.ShowOverlay(ulOverlayHandle);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0009DC67 File Offset: 0x0009BE67
		public EVROverlayError HideOverlay(ulong ulOverlayHandle)
		{
			return this.FnTable.HideOverlay(ulOverlayHandle);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0009DC7A File Offset: 0x0009BE7A
		public bool IsOverlayVisible(ulong ulOverlayHandle)
		{
			return this.FnTable.IsOverlayVisible(ulOverlayHandle);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0009DC8D File Offset: 0x0009BE8D
		public EVROverlayError GetTransformForOverlayCoordinates(ulong ulOverlayHandle, ETrackingUniverseOrigin eTrackingOrigin, HmdVector2_t coordinatesInOverlay, ref HmdMatrix34_t pmatTransform)
		{
			return this.FnTable.GetTransformForOverlayCoordinates(ulOverlayHandle, eTrackingOrigin, coordinatesInOverlay, ref pmatTransform);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0009DCA4 File Offset: 0x0009BEA4
		public bool PollNextOverlayEvent(ulong ulOverlayHandle, ref VREvent_t pEvent, uint uncbVREvent)
		{
			if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
			{
				VREvent_t_Packed vrevent_t_Packed = default(VREvent_t_Packed);
				CVROverlay.PollNextOverlayEventUnion pollNextOverlayEventUnion;
				pollNextOverlayEventUnion.pPollNextOverlayEventPacked = null;
				pollNextOverlayEventUnion.pPollNextOverlayEvent = this.FnTable.PollNextOverlayEvent;
				bool result = pollNextOverlayEventUnion.pPollNextOverlayEventPacked(ulOverlayHandle, ref vrevent_t_Packed, (uint)Marshal.SizeOf(typeof(VREvent_t_Packed)));
				vrevent_t_Packed.Unpack(ref pEvent);
				return result;
			}
			return this.FnTable.PollNextOverlayEvent(ulOverlayHandle, ref pEvent, uncbVREvent);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0009DD26 File Offset: 0x0009BF26
		public EVROverlayError GetOverlayInputMethod(ulong ulOverlayHandle, ref VROverlayInputMethod peInputMethod)
		{
			return this.FnTable.GetOverlayInputMethod(ulOverlayHandle, ref peInputMethod);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0009DD3A File Offset: 0x0009BF3A
		public EVROverlayError SetOverlayInputMethod(ulong ulOverlayHandle, VROverlayInputMethod eInputMethod)
		{
			return this.FnTable.SetOverlayInputMethod(ulOverlayHandle, eInputMethod);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0009DD4E File Offset: 0x0009BF4E
		public EVROverlayError GetOverlayMouseScale(ulong ulOverlayHandle, ref HmdVector2_t pvecMouseScale)
		{
			return this.FnTable.GetOverlayMouseScale(ulOverlayHandle, ref pvecMouseScale);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0009DD62 File Offset: 0x0009BF62
		public EVROverlayError SetOverlayMouseScale(ulong ulOverlayHandle, ref HmdVector2_t pvecMouseScale)
		{
			return this.FnTable.SetOverlayMouseScale(ulOverlayHandle, ref pvecMouseScale);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0009DD76 File Offset: 0x0009BF76
		public bool ComputeOverlayIntersection(ulong ulOverlayHandle, ref VROverlayIntersectionParams_t pParams, ref VROverlayIntersectionResults_t pResults)
		{
			return this.FnTable.ComputeOverlayIntersection(ulOverlayHandle, ref pParams, ref pResults);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0009DD8B File Offset: 0x0009BF8B
		public bool HandleControllerOverlayInteractionAsMouse(ulong ulOverlayHandle, uint unControllerDeviceIndex)
		{
			return this.FnTable.HandleControllerOverlayInteractionAsMouse(ulOverlayHandle, unControllerDeviceIndex);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0009DD9F File Offset: 0x0009BF9F
		public bool IsHoverTargetOverlay(ulong ulOverlayHandle)
		{
			return this.FnTable.IsHoverTargetOverlay(ulOverlayHandle);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0009DDB2 File Offset: 0x0009BFB2
		public ulong GetGamepadFocusOverlay()
		{
			return this.FnTable.GetGamepadFocusOverlay();
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0009DDC4 File Offset: 0x0009BFC4
		public EVROverlayError SetGamepadFocusOverlay(ulong ulNewFocusOverlay)
		{
			return this.FnTable.SetGamepadFocusOverlay(ulNewFocusOverlay);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0009DDD7 File Offset: 0x0009BFD7
		public EVROverlayError SetOverlayNeighbor(EOverlayDirection eDirection, ulong ulFrom, ulong ulTo)
		{
			return this.FnTable.SetOverlayNeighbor(eDirection, ulFrom, ulTo);
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0009DDEC File Offset: 0x0009BFEC
		public EVROverlayError MoveGamepadFocusToNeighbor(EOverlayDirection eDirection, ulong ulFrom)
		{
			return this.FnTable.MoveGamepadFocusToNeighbor(eDirection, ulFrom);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0009DE00 File Offset: 0x0009C000
		public EVROverlayError SetOverlayTexture(ulong ulOverlayHandle, ref Texture_t pTexture)
		{
			return this.FnTable.SetOverlayTexture(ulOverlayHandle, ref pTexture);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0009DE14 File Offset: 0x0009C014
		public EVROverlayError ClearOverlayTexture(ulong ulOverlayHandle)
		{
			return this.FnTable.ClearOverlayTexture(ulOverlayHandle);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0009DE27 File Offset: 0x0009C027
		public EVROverlayError SetOverlayRaw(ulong ulOverlayHandle, IntPtr pvBuffer, uint unWidth, uint unHeight, uint unDepth)
		{
			return this.FnTable.SetOverlayRaw(ulOverlayHandle, pvBuffer, unWidth, unHeight, unDepth);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0009DE40 File Offset: 0x0009C040
		public EVROverlayError SetOverlayFromFile(ulong ulOverlayHandle, string pchFilePath)
		{
			return this.FnTable.SetOverlayFromFile(ulOverlayHandle, pchFilePath);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0009DE54 File Offset: 0x0009C054
		public EVROverlayError GetOverlayTexture(ulong ulOverlayHandle, ref IntPtr pNativeTextureHandle, IntPtr pNativeTextureRef, ref uint pWidth, ref uint pHeight, ref uint pNativeFormat, ref ETextureType pAPIType, ref EColorSpace pColorSpace, ref VRTextureBounds_t pTextureBounds)
		{
			pWidth = 0U;
			pHeight = 0U;
			pNativeFormat = 0U;
			return this.FnTable.GetOverlayTexture(ulOverlayHandle, ref pNativeTextureHandle, pNativeTextureRef, ref pWidth, ref pHeight, ref pNativeFormat, ref pAPIType, ref pColorSpace, ref pTextureBounds);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0009DE8C File Offset: 0x0009C08C
		public EVROverlayError ReleaseNativeOverlayHandle(ulong ulOverlayHandle, IntPtr pNativeTextureHandle)
		{
			return this.FnTable.ReleaseNativeOverlayHandle(ulOverlayHandle, pNativeTextureHandle);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0009DEA0 File Offset: 0x0009C0A0
		public EVROverlayError GetOverlayTextureSize(ulong ulOverlayHandle, ref uint pWidth, ref uint pHeight)
		{
			pWidth = 0U;
			pHeight = 0U;
			return this.FnTable.GetOverlayTextureSize(ulOverlayHandle, ref pWidth, ref pHeight);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0009DEBB File Offset: 0x0009C0BB
		public EVROverlayError CreateDashboardOverlay(string pchOverlayKey, string pchOverlayFriendlyName, ref ulong pMainHandle, ref ulong pThumbnailHandle)
		{
			pMainHandle = 0UL;
			pThumbnailHandle = 0UL;
			return this.FnTable.CreateDashboardOverlay(pchOverlayKey, pchOverlayFriendlyName, ref pMainHandle, ref pThumbnailHandle);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0009DEDB File Offset: 0x0009C0DB
		public bool IsDashboardVisible()
		{
			return this.FnTable.IsDashboardVisible();
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0009DEED File Offset: 0x0009C0ED
		public bool IsActiveDashboardOverlay(ulong ulOverlayHandle)
		{
			return this.FnTable.IsActiveDashboardOverlay(ulOverlayHandle);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0009DF00 File Offset: 0x0009C100
		public EVROverlayError SetDashboardOverlaySceneProcess(ulong ulOverlayHandle, uint unProcessId)
		{
			return this.FnTable.SetDashboardOverlaySceneProcess(ulOverlayHandle, unProcessId);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0009DF14 File Offset: 0x0009C114
		public EVROverlayError GetDashboardOverlaySceneProcess(ulong ulOverlayHandle, ref uint punProcessId)
		{
			punProcessId = 0U;
			return this.FnTable.GetDashboardOverlaySceneProcess(ulOverlayHandle, ref punProcessId);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0009DF2B File Offset: 0x0009C12B
		public void ShowDashboard(string pchOverlayToShow)
		{
			this.FnTable.ShowDashboard(pchOverlayToShow);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0009DF3E File Offset: 0x0009C13E
		public uint GetPrimaryDashboardDevice()
		{
			return this.FnTable.GetPrimaryDashboardDevice();
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0009DF50 File Offset: 0x0009C150
		public EVROverlayError ShowKeyboard(int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText, bool bUseMinimalMode, ulong uUserValue)
		{
			return this.FnTable.ShowKeyboard(eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText, bUseMinimalMode, uUserValue);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0009DF70 File Offset: 0x0009C170
		public EVROverlayError ShowKeyboardForOverlay(ulong ulOverlayHandle, int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText, bool bUseMinimalMode, ulong uUserValue)
		{
			return this.FnTable.ShowKeyboardForOverlay(ulOverlayHandle, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText, bUseMinimalMode, uUserValue);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0009DF9A File Offset: 0x0009C19A
		public uint GetKeyboardText(StringBuilder pchText, uint cchText)
		{
			return this.FnTable.GetKeyboardText(pchText, cchText);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0009DFAE File Offset: 0x0009C1AE
		public void HideKeyboard()
		{
			this.FnTable.HideKeyboard();
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0009DFC0 File Offset: 0x0009C1C0
		public void SetKeyboardTransformAbsolute(ETrackingUniverseOrigin eTrackingOrigin, ref HmdMatrix34_t pmatTrackingOriginToKeyboardTransform)
		{
			this.FnTable.SetKeyboardTransformAbsolute(eTrackingOrigin, ref pmatTrackingOriginToKeyboardTransform);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0009DFD4 File Offset: 0x0009C1D4
		public void SetKeyboardPositionForOverlay(ulong ulOverlayHandle, HmdRect2_t avoidRect)
		{
			this.FnTable.SetKeyboardPositionForOverlay(ulOverlayHandle, avoidRect);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0009DFE8 File Offset: 0x0009C1E8
		public EVROverlayError SetOverlayIntersectionMask(ulong ulOverlayHandle, ref VROverlayIntersectionMaskPrimitive_t pMaskPrimitives, uint unNumMaskPrimitives, uint unPrimitiveSize)
		{
			return this.FnTable.SetOverlayIntersectionMask(ulOverlayHandle, ref pMaskPrimitives, unNumMaskPrimitives, unPrimitiveSize);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0009DFFF File Offset: 0x0009C1FF
		public EVROverlayError GetOverlayFlags(ulong ulOverlayHandle, ref uint pFlags)
		{
			pFlags = 0U;
			return this.FnTable.GetOverlayFlags(ulOverlayHandle, ref pFlags);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0009E016 File Offset: 0x0009C216
		public VRMessageOverlayResponse ShowMessageOverlay(string pchText, string pchCaption, string pchButton0Text, string pchButton1Text, string pchButton2Text, string pchButton3Text)
		{
			return this.FnTable.ShowMessageOverlay(pchText, pchCaption, pchButton0Text, pchButton1Text, pchButton2Text, pchButton3Text);
		}

		// Token: 0x04001931 RID: 6449
		private IVROverlay FnTable;

		// Token: 0x02000767 RID: 1895
		// (Invoke) Token: 0x06002F97 RID: 12183
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _PollNextOverlayEventPacked(ulong ulOverlayHandle, ref VREvent_t_Packed pEvent, uint uncbVREvent);

		// Token: 0x02000768 RID: 1896
		[StructLayout(LayoutKind.Explicit)]
		private struct PollNextOverlayEventUnion
		{
			// Token: 0x040028EC RID: 10476
			[FieldOffset(0)]
			public IVROverlay._PollNextOverlayEvent pPollNextOverlayEvent;

			// Token: 0x040028ED RID: 10477
			[FieldOffset(0)]
			public CVROverlay._PollNextOverlayEventPacked pPollNextOverlayEventPacked;
		}
	}
}
