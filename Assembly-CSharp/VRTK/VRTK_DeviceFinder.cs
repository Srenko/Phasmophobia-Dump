using System;
using UnityEngine;
using UnityEngine.XR;

namespace VRTK
{
	// Token: 0x02000312 RID: 786
	public static class VRTK_DeviceFinder
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x00090B6E File Offset: 0x0008ED6E
		public static SDK_BaseController.ControllerType GetCurrentControllerType()
		{
			return VRTK_SDK_Bridge.GetCurrentControllerType();
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00090B75 File Offset: 0x0008ED75
		public static uint GetControllerIndex(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerIndex(controller);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00090B7D File Offset: 0x0008ED7D
		public static GameObject GetControllerByIndex(uint index, bool getActual)
		{
			return VRTK_SDK_Bridge.GetControllerByIndex(index, getActual);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00090B86 File Offset: 0x0008ED86
		[Obsolete("`VRTK_DeviceFinder.GetControllerOrigin(controller)` has been replaced with `VRTK_DeviceFinder.GetControllerOrigin(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Transform GetControllerOrigin(GameObject controller)
		{
			return VRTK_DeviceFinder.GetControllerOrigin(VRTK_ControllerReference.GetControllerReference(controller));
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00090B93 File Offset: 0x0008ED93
		public static Transform GetControllerOrigin(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerOrigin(controllerReference);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00090B9B File Offset: 0x0008ED9B
		public static Transform DeviceTransform(VRTK_DeviceFinder.Devices device)
		{
			switch (device)
			{
			case VRTK_DeviceFinder.Devices.Headset:
				return VRTK_DeviceFinder.HeadsetTransform();
			case VRTK_DeviceFinder.Devices.LeftController:
				return VRTK_DeviceFinder.GetControllerLeftHand(false).transform;
			case VRTK_DeviceFinder.Devices.RightController:
				return VRTK_DeviceFinder.GetControllerRightHand(false).transform;
			default:
				return null;
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00090BD0 File Offset: 0x0008EDD0
		public static SDK_BaseController.ControllerHand GetControllerHandType(string hand)
		{
			string a = hand.ToLower();
			if (a == "left")
			{
				return SDK_BaseController.ControllerHand.Left;
			}
			if (!(a == "right"))
			{
				return SDK_BaseController.ControllerHand.None;
			}
			return SDK_BaseController.ControllerHand.Right;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00090C05 File Offset: 0x0008EE05
		public static SDK_BaseController.ControllerHand GetControllerHand(GameObject controller)
		{
			if (VRTK_SDK_Bridge.IsControllerLeftHand(controller))
			{
				return SDK_BaseController.ControllerHand.Left;
			}
			if (VRTK_SDK_Bridge.IsControllerRightHand(controller))
			{
				return SDK_BaseController.ControllerHand.Right;
			}
			return SDK_BaseController.ControllerHand.None;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00090C1C File Offset: 0x0008EE1C
		public static GameObject GetControllerLeftHand(bool getActual = false)
		{
			return VRTK_SDK_Bridge.GetControllerLeftHand(getActual);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00090C24 File Offset: 0x0008EE24
		public static GameObject GetControllerRightHand(bool getActual = false)
		{
			return VRTK_SDK_Bridge.GetControllerRightHand(getActual);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00090C2C File Offset: 0x0008EE2C
		public static bool IsControllerOfHand(GameObject checkController, SDK_BaseController.ControllerHand hand)
		{
			if (hand != SDK_BaseController.ControllerHand.Left)
			{
				return hand == SDK_BaseController.ControllerHand.Right && VRTK_DeviceFinder.IsControllerRightHand(checkController);
			}
			return VRTK_DeviceFinder.IsControllerLeftHand(checkController);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00090C47 File Offset: 0x0008EE47
		public static bool IsControllerLeftHand(GameObject checkController)
		{
			return VRTK_SDK_Bridge.IsControllerLeftHand(checkController);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00090C4F File Offset: 0x0008EE4F
		public static bool IsControllerRightHand(GameObject checkController)
		{
			return VRTK_SDK_Bridge.IsControllerRightHand(checkController);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00090C57 File Offset: 0x0008EE57
		public static GameObject GetActualController(GameObject givenController)
		{
			if (VRTK_SDK_Bridge.IsControllerLeftHand(givenController, true) || VRTK_SDK_Bridge.IsControllerRightHand(givenController, true))
			{
				return givenController;
			}
			if (VRTK_SDK_Bridge.IsControllerLeftHand(givenController, false))
			{
				return VRTK_SDK_Bridge.GetControllerLeftHand(true);
			}
			if (VRTK_SDK_Bridge.IsControllerRightHand(givenController, false))
			{
				return VRTK_SDK_Bridge.GetControllerRightHand(true);
			}
			return null;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00090C8E File Offset: 0x0008EE8E
		public static GameObject GetScriptAliasController(GameObject givenController)
		{
			if (VRTK_SDK_Bridge.IsControllerLeftHand(givenController, false) || VRTK_SDK_Bridge.IsControllerRightHand(givenController, false))
			{
				return givenController;
			}
			if (VRTK_SDK_Bridge.IsControllerLeftHand(givenController, true))
			{
				return VRTK_SDK_Bridge.GetControllerLeftHand(false);
			}
			if (VRTK_SDK_Bridge.IsControllerRightHand(givenController, true))
			{
				return VRTK_SDK_Bridge.GetControllerRightHand(false);
			}
			return null;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00090CC5 File Offset: 0x0008EEC5
		public static GameObject GetModelAliasController(GameObject givenController)
		{
			return VRTK_SDK_Bridge.GetControllerModel(givenController);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00090CCD File Offset: 0x0008EECD
		public static SDK_BaseController.ControllerHand GetModelAliasControllerHand(GameObject givenObject)
		{
			if (VRTK_DeviceFinder.GetModelAliasController(VRTK_DeviceFinder.GetControllerLeftHand(false)) == givenObject)
			{
				return SDK_BaseController.ControllerHand.Left;
			}
			if (VRTK_DeviceFinder.GetModelAliasController(VRTK_DeviceFinder.GetControllerRightHand(false)) == givenObject)
			{
				return SDK_BaseController.ControllerHand.Right;
			}
			return SDK_BaseController.ControllerHand.None;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00090CFA File Offset: 0x0008EEFA
		[Obsolete("`VRTK_DeviceFinder.GetControllerVelocity(givenController)` has been replaced with `VRTK_DeviceFinder.GetControllerVelocity(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector3 GetControllerVelocity(GameObject givenController)
		{
			return VRTK_DeviceFinder.GetControllerVelocity(VRTK_ControllerReference.GetControllerReference(givenController));
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00090D07 File Offset: 0x0008EF07
		public static Vector3 GetControllerVelocity(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerVelocity(controllerReference);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00090D0F File Offset: 0x0008EF0F
		[Obsolete("`VRTK_DeviceFinder.GetControllerAngularVelocity(givenController)` has been replaced with `VRTK_DeviceFinder.GetControllerAngularVelocity(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector3 GetControllerAngularVelocity(GameObject givenController)
		{
			return VRTK_DeviceFinder.GetControllerAngularVelocity(VRTK_ControllerReference.GetControllerReference(givenController));
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00090D1C File Offset: 0x0008EF1C
		public static Vector3 GetControllerAngularVelocity(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerAngularVelocity(controllerReference);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00090D24 File Offset: 0x0008EF24
		public static Vector3 GetHeadsetVelocity()
		{
			return VRTK_SDK_Bridge.GetHeadsetVelocity();
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00090D2B File Offset: 0x0008EF2B
		public static Vector3 GetHeadsetAngularVelocity()
		{
			return VRTK_SDK_Bridge.GetHeadsetAngularVelocity();
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00090D32 File Offset: 0x0008EF32
		public static Transform HeadsetTransform()
		{
			return VRTK_SDK_Bridge.GetHeadset();
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00090D39 File Offset: 0x0008EF39
		public static Transform HeadsetCamera()
		{
			return VRTK_SDK_Bridge.GetHeadsetCamera();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00090D40 File Offset: 0x0008EF40
		public static void ResetHeadsetTypeCache()
		{
			VRTK_DeviceFinder.cachedHeadsetType = "";
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00090D4C File Offset: 0x0008EF4C
		public static VRTK_DeviceFinder.Headsets GetHeadsetType(bool summary = false)
		{
			VRTK_DeviceFinder.Headsets headsets = VRTK_DeviceFinder.Headsets.Unknown;
			VRTK_DeviceFinder.cachedHeadsetType = ((VRTK_DeviceFinder.cachedHeadsetType == "") ? XRDevice.model.Replace(" ", "").Replace(".", "").ToLowerInvariant() : VRTK_DeviceFinder.cachedHeadsetType);
			string text = VRTK_DeviceFinder.cachedHeadsetType;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1636145679U)
			{
				if (num <= 151693739U)
				{
					if (num != 16182461U)
					{
						if (num == 151693739U)
						{
							if (text == "index")
							{
								headsets = (summary ? VRTK_DeviceFinder.Headsets.Vive : VRTK_DeviceFinder.Headsets.index);
							}
						}
					}
					else if (text == "hpwindowsmixedrealityheadset0")
					{
						headsets = (summary ? VRTK_DeviceFinder.Headsets.Vive : VRTK_DeviceFinder.Headsets.HPWMR);
					}
				}
				else if (num != 797153218U)
				{
					if (num == 1636145679U)
					{
						if (text == "oculusriftcv1")
						{
							headsets = (summary ? VRTK_DeviceFinder.Headsets.OculusRift : VRTK_DeviceFinder.Headsets.OculusRiftCV1);
						}
					}
				}
				else if (text == "samsungwindowsmixedreality800zba0")
				{
					headsets = (summary ? VRTK_DeviceFinder.Headsets.Vive : VRTK_DeviceFinder.Headsets.samsungOdyssey);
				}
			}
			else if (num <= 2100718906U)
			{
				if (num != 1667887968U)
				{
					if (num == 2100718906U)
					{
						if (text == "vive_promv")
						{
							headsets = (summary ? VRTK_DeviceFinder.Headsets.Vive : VRTK_DeviceFinder.Headsets.VivePro);
						}
					}
				}
				else if (text == "oculusquest")
				{
					headsets = (summary ? VRTK_DeviceFinder.Headsets.OculusRift : VRTK_DeviceFinder.Headsets.OculusQuest);
				}
			}
			else if (num != 2733032458U)
			{
				if (num != 2947161482U)
				{
					if (num == 4142758193U)
					{
						if (text == "vivedvt")
						{
							headsets = (summary ? VRTK_DeviceFinder.Headsets.Vive : VRTK_DeviceFinder.Headsets.ViveDVT);
						}
					}
				}
				else if (text == "oculusrifts")
				{
					headsets = (summary ? VRTK_DeviceFinder.Headsets.OculusRift : VRTK_DeviceFinder.Headsets.OculusRiftS);
				}
			}
			else if (text == "vivemv")
			{
				headsets = (summary ? VRTK_DeviceFinder.Headsets.Vive : VRTK_DeviceFinder.Headsets.ViveMV);
			}
			if (headsets == VRTK_DeviceFinder.Headsets.Unknown)
			{
				VRTK_Logger.Warn(string.Format("Your headset is of type '{0}' which VRTK doesn't know about yet. Please report this headset type to the maintainers of VRTK." + (summary ? " Falling back to a slower check to summarize the headset type now." : ""), VRTK_DeviceFinder.cachedHeadsetType));
				if (summary)
				{
					if (VRTK_DeviceFinder.cachedHeadsetType.Contains("rift") || VRTK_DeviceFinder.cachedHeadsetType.Contains("oculus"))
					{
						return VRTK_DeviceFinder.Headsets.OculusRift;
					}
					if (VRTK_DeviceFinder.cachedHeadsetType.Contains("vive"))
					{
						return VRTK_DeviceFinder.Headsets.Vive;
					}
					if (VRTK_DeviceFinder.cachedHeadsetType.Contains("samsungwindowsmixedreality"))
					{
						return VRTK_DeviceFinder.Headsets.samsungOdyssey;
					}
				}
			}
			return headsets;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00090FA1 File Offset: 0x0008F1A1
		public static Transform PlayAreaTransform()
		{
			return VRTK_SDK_Bridge.GetPlayArea();
		}

		// Token: 0x0400163F RID: 5695
		private static string cachedHeadsetType = "";

		// Token: 0x02000612 RID: 1554
		public enum Devices
		{
			// Token: 0x04002894 RID: 10388
			Headset,
			// Token: 0x04002895 RID: 10389
			LeftController,
			// Token: 0x04002896 RID: 10390
			RightController
		}

		// Token: 0x02000613 RID: 1555
		public enum Headsets
		{
			// Token: 0x04002898 RID: 10392
			Unknown,
			// Token: 0x04002899 RID: 10393
			OculusRift,
			// Token: 0x0400289A RID: 10394
			OculusRiftCV1,
			// Token: 0x0400289B RID: 10395
			OculusRiftS,
			// Token: 0x0400289C RID: 10396
			OculusQuest,
			// Token: 0x0400289D RID: 10397
			Vive,
			// Token: 0x0400289E RID: 10398
			ViveMV,
			// Token: 0x0400289F RID: 10399
			ViveDVT,
			// Token: 0x040028A0 RID: 10400
			VivePro,
			// Token: 0x040028A1 RID: 10401
			index,
			// Token: 0x040028A2 RID: 10402
			samsungOdyssey,
			// Token: 0x040028A3 RID: 10403
			HPWMR
		}
	}
}
