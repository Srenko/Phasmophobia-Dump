using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200030E RID: 782
	public sealed class VRTK_SDKSetup : MonoBehaviour
	{
		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06001B53 RID: 6995 RVA: 0x0008EE00 File Offset: 0x0008D000
		// (remove) Token: 0x06001B54 RID: 6996 RVA: 0x0008EE38 File Offset: 0x0008D038
		public event VRTK_SDKSetup.LoadEventHandler Loaded;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06001B55 RID: 6997 RVA: 0x0008EE70 File Offset: 0x0008D070
		// (remove) Token: 0x06001B56 RID: 6998 RVA: 0x0008EEA8 File Offset: 0x0008D0A8
		public event VRTK_SDKSetup.LoadEventHandler Unloaded;

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0008EEDD File Offset: 0x0008D0DD
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0008EEE8 File Offset: 0x0008D0E8
		public VRTK_SDKInfo systemSDKInfo
		{
			get
			{
				return this.cachedSystemSDKInfo;
			}
			set
			{
				value = (value ?? VRTK_SDKInfo.Create<SDK_BaseSystem, SDK_FallbackSystem, SDK_FallbackSystem>()[0]);
				if (this.cachedSystemSDKInfo == value)
				{
					return;
				}
				Object.Destroy(this.cachedSystemSDK);
				this.cachedSystemSDK = null;
				this.cachedSystemSDKInfo = new VRTK_SDKInfo(value);
				this.PopulateObjectReferences(false);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0008EF37 File Offset: 0x0008D137
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x0008EF40 File Offset: 0x0008D140
		public VRTK_SDKInfo boundariesSDKInfo
		{
			get
			{
				return this.cachedBoundariesSDKInfo;
			}
			set
			{
				value = (value ?? VRTK_SDKInfo.Create<SDK_BaseBoundaries, SDK_FallbackBoundaries, SDK_FallbackBoundaries>()[0]);
				if (this.cachedBoundariesSDKInfo == value)
				{
					return;
				}
				Object.Destroy(this.cachedBoundariesSDK);
				this.cachedBoundariesSDK = null;
				this.cachedBoundariesSDKInfo = new VRTK_SDKInfo(value);
				this.PopulateObjectReferences(false);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0008EF8F File Offset: 0x0008D18F
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x0008EF98 File Offset: 0x0008D198
		public VRTK_SDKInfo headsetSDKInfo
		{
			get
			{
				return this.cachedHeadsetSDKInfo;
			}
			set
			{
				value = (value ?? VRTK_SDKInfo.Create<SDK_BaseHeadset, SDK_FallbackHeadset, SDK_FallbackHeadset>()[0]);
				if (this.cachedHeadsetSDKInfo == value)
				{
					return;
				}
				Object.Destroy(this.cachedHeadsetSDK);
				this.cachedHeadsetSDK = null;
				this.cachedHeadsetSDKInfo = new VRTK_SDKInfo(value);
				this.PopulateObjectReferences(false);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0008EFE7 File Offset: 0x0008D1E7
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0008EFF0 File Offset: 0x0008D1F0
		public VRTK_SDKInfo controllerSDKInfo
		{
			get
			{
				return this.cachedControllerSDKInfo;
			}
			set
			{
				value = (value ?? VRTK_SDKInfo.Create<SDK_BaseController, SDK_FallbackController, SDK_FallbackController>()[0]);
				if (this.cachedControllerSDKInfo == value)
				{
					return;
				}
				Object.Destroy(this.cachedControllerSDK);
				this.cachedControllerSDK = null;
				this.cachedControllerSDKInfo = new VRTK_SDKInfo(value);
				this.PopulateObjectReferences(false);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0008F040 File Offset: 0x0008D240
		public SDK_BaseSystem systemSDK
		{
			get
			{
				if (this.cachedSystemSDK == null)
				{
					VRTK_SDKSetup.HandleSDKGetter<SDK_BaseSystem>("System", this.systemSDKInfo, VRTK_SDKManager.InstalledSystemSDKInfos);
					this.cachedSystemSDK = (SDK_BaseSystem)ScriptableObject.CreateInstance(this.systemSDKInfo.type);
				}
				return this.cachedSystemSDK;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0008F094 File Offset: 0x0008D294
		public SDK_BaseBoundaries boundariesSDK
		{
			get
			{
				if (this.cachedBoundariesSDK == null)
				{
					VRTK_SDKSetup.HandleSDKGetter<SDK_BaseBoundaries>("Boundaries", this.boundariesSDKInfo, VRTK_SDKManager.InstalledBoundariesSDKInfos);
					this.cachedBoundariesSDK = (SDK_BaseBoundaries)ScriptableObject.CreateInstance(this.boundariesSDKInfo.type);
				}
				return this.cachedBoundariesSDK;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0008F0E8 File Offset: 0x0008D2E8
		public SDK_BaseHeadset headsetSDK
		{
			get
			{
				if (this.cachedHeadsetSDK == null)
				{
					VRTK_SDKSetup.HandleSDKGetter<SDK_BaseHeadset>("Headset", this.headsetSDKInfo, VRTK_SDKManager.InstalledHeadsetSDKInfos);
					this.cachedHeadsetSDK = (SDK_BaseHeadset)ScriptableObject.CreateInstance(this.headsetSDKInfo.type);
				}
				return this.cachedHeadsetSDK;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0008F13C File Offset: 0x0008D33C
		public SDK_BaseController controllerSDK
		{
			get
			{
				if (this.cachedControllerSDK == null)
				{
					VRTK_SDKSetup.HandleSDKGetter<SDK_BaseController>("Controller", this.controllerSDKInfo, VRTK_SDKManager.InstalledControllerSDKInfos);
					this.cachedControllerSDK = (SDK_BaseController)ScriptableObject.CreateInstance(this.controllerSDKInfo.type);
				}
				return this.cachedControllerSDK;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0008F190 File Offset: 0x0008D390
		public string[] usedVRDeviceNames
		{
			get
			{
				return (from info in new VRTK_SDKInfo[]
				{
					this.systemSDKInfo,
					this.boundariesSDKInfo,
					this.headsetSDKInfo,
					this.controllerSDKInfo
				}
				select info.description.vrDeviceName).Distinct<string>().ToArray<string>();
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0008F1F5 File Offset: 0x0008D3F5
		public bool isValid
		{
			get
			{
				return this.GetSimplifiedErrorDescriptions().Length == 0;
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0008F204 File Offset: 0x0008D404
		public void PopulateObjectReferences(bool force)
		{
			if (!force && !this.autoPopulateObjectReferences)
			{
				return;
			}
			VRTK_SDK_Bridge.InvalidateCaches();
			this.actualBoundaries = null;
			this.actualHeadset = null;
			this.actualLeftController = null;
			this.actualRightController = null;
			this.modelAliasLeftController = null;
			this.modelAliasRightController = null;
			Transform playArea = this.boundariesSDK.GetPlayArea();
			Transform headset = this.headsetSDK.GetHeadset();
			this.actualBoundaries = ((playArea == null) ? null : playArea.gameObject);
			this.actualHeadset = ((headset == null) ? null : headset.gameObject);
			this.actualLeftController = this.controllerSDK.GetControllerLeftHand(true);
			this.actualRightController = this.controllerSDK.GetControllerRightHand(true);
			this.modelAliasLeftController = this.controllerSDK.GetControllerModel(SDK_BaseController.ControllerHand.Left);
			this.modelAliasRightController = this.controllerSDK.GetControllerModel(SDK_BaseController.ControllerHand.Right);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0008F2DC File Offset: 0x0008D4DC
		public string[] GetSimplifiedErrorDescriptions()
		{
			List<string> list = new List<string>();
			ReadOnlyCollection<VRTK_SDKInfo>[] array = new ReadOnlyCollection<VRTK_SDKInfo>[]
			{
				VRTK_SDKManager.InstalledSystemSDKInfos,
				VRTK_SDKManager.InstalledBoundariesSDKInfos,
				VRTK_SDKManager.InstalledHeadsetSDKInfos,
				VRTK_SDKManager.InstalledControllerSDKInfos
			};
			VRTK_SDKInfo[] array2 = new VRTK_SDKInfo[]
			{
				this.systemSDKInfo,
				this.boundariesSDKInfo,
				this.headsetSDKInfo,
				this.controllerSDKInfo
			};
			for (int i = 0; i < array.Length; i++)
			{
				ReadOnlyCollection<VRTK_SDKInfo> readOnlyCollection = array[i];
				VRTK_SDKInfo vrtk_SDKInfo = array2[i];
				if (!(vrtk_SDKInfo.type.BaseType == null))
				{
					if (vrtk_SDKInfo.originalTypeNameWhenFallbackIsUsed != null)
					{
						list.Add(string.Format("The SDK '{0}' doesn't exist anymore.", vrtk_SDKInfo.originalTypeNameWhenFallbackIsUsed));
					}
					else if (vrtk_SDKInfo.description.describesFallbackSDK)
					{
						list.Add("A fallback SDK is used. Make sure to set a real SDK.");
					}
					else if (!readOnlyCollection.Contains(vrtk_SDKInfo))
					{
						list.Add(string.Format("The vendor SDK for '{0}' is not installed.", vrtk_SDKInfo.description.prettyName));
					}
				}
			}
			if (this.usedVRDeviceNames.Except(new string[]
			{
				"None"
			}).Count<string>() > 1)
			{
				list.Add("The current SDK selection uses multiple VR Devices. It's not possible to use more than one VR Device at the same time.");
			}
			return list.Distinct<string>().ToArray<string>();
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0008F410 File Offset: 0x0008D610
		public void OnLoaded(VRTK_SDKManager sender)
		{
			List<SDK_Base> list = new SDK_Base[]
			{
				this.systemSDK,
				this.boundariesSDK,
				this.headsetSDK,
				this.controllerSDK
			}.ToList<SDK_Base>();
			list.ForEach(delegate(SDK_Base sdkBase)
			{
				sdkBase.OnBeforeSetupLoad(this);
			});
			base.gameObject.SetActive(true);
			VRTK_SDK_Bridge.InvalidateCaches();
			this.SetupHeadset();
			this.SetupControllers();
			this.boundariesSDK.InitBoundaries();
			list.ForEach(delegate(SDK_Base sdkBase)
			{
				sdkBase.OnAfterSetupLoad(this);
			});
			VRTK_SDKSetup.LoadEventHandler loaded = this.Loaded;
			if (loaded != null)
			{
				loaded(sender, this);
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0008F4AC File Offset: 0x0008D6AC
		public void OnUnloaded(VRTK_SDKManager sender)
		{
			List<SDK_Base> list = new SDK_Base[]
			{
				this.systemSDK,
				this.boundariesSDK,
				this.headsetSDK,
				this.controllerSDK
			}.ToList<SDK_Base>();
			list.ForEach(delegate(SDK_Base sdkBase)
			{
				sdkBase.OnBeforeSetupUnload(this);
			});
			base.gameObject.SetActive(false);
			list.ForEach(delegate(SDK_Base sdkBase)
			{
				sdkBase.OnAfterSetupUnload(this);
			});
			VRTK_SDKSetup.LoadEventHandler unloaded = this.Unloaded;
			if (unloaded != null)
			{
				unloaded(sender, this);
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0008F529 File Offset: 0x0008D729
		private void OnEnable()
		{
			if (!VRTK_SDKManager.instance.persistOnLoad)
			{
				this.PopulateObjectReferences(false);
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0008F540 File Offset: 0x0008D740
		private static void HandleSDKGetter<BaseType>(string prettyName, VRTK_SDKInfo info, IEnumerable<VRTK_SDKInfo> installedInfos) where BaseType : SDK_Base
		{
			if (VRTK_SharedMethods.IsEditTime())
			{
				return;
			}
			string sdkerrorDescription = VRTK_SDKSetup.GetSDKErrorDescription<BaseType>(prettyName, info, installedInfos);
			if (!string.IsNullOrEmpty(sdkerrorDescription))
			{
				VRTK_Logger.Error(sdkerrorDescription);
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0008F56C File Offset: 0x0008D76C
		private static string GetSDKErrorDescription<BaseType>(string prettyName, VRTK_SDKInfo info, IEnumerable<VRTK_SDKInfo> installedInfos) where BaseType : SDK_Base
		{
			Type type = info.type;
			Type typeFromHandle = typeof(BaseType);
			Type type2 = VRTK_SDKManager.SDKFallbackTypesByBaseType[typeFromHandle];
			if (type == type2)
			{
				return string.Format("The fallback {0} SDK is being used because there is no other {0} SDK set in the SDK Setup.", prettyName);
			}
			if (typeFromHandle.IsAssignableFrom(type) && !type2.IsAssignableFrom(type))
			{
				return null;
			}
			string str = string.Format("The fallback {0} SDK is being used despite being set to '{1}'.", prettyName, type.Name);
			if ((from installedInfo in installedInfos
			select installedInfo.type).Contains(type))
			{
				return str + " Its needed scripting define symbols are not added. You can click the GameObject with the `VRTK_SDKManager` script attached to it in Edit Mode and choose to automatically let the manager handle the scripting define symbols.";
			}
			return str + " The needed vendor SDK isn't installed.";
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0008F616 File Offset: 0x0008D816
		private void SetupHeadset()
		{
			if (this.actualHeadset != null && !this.actualHeadset.GetComponent<VRTK_TrackedHeadset>())
			{
				this.actualHeadset.AddComponent<VRTK_TrackedHeadset>();
			}
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0008F644 File Offset: 0x0008D844
		private void SetupControllers()
		{
			Action<GameObject, GameObject> action = delegate(GameObject scriptAliasGameObject, GameObject actualGameObject)
			{
				if (scriptAliasGameObject == null)
				{
					return;
				}
				Transform transform = scriptAliasGameObject.transform;
				Transform transform2 = actualGameObject.transform;
				if (transform.parent != transform2)
				{
					Vector3 localScale = transform.localScale;
					transform.SetParent(transform2);
					transform.localScale = localScale;
				}
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
			};
			if (this.actualLeftController != null)
			{
				action(VRTK_SDKManager.instance.scriptAliasLeftController, this.actualLeftController);
				if (this.actualLeftController.GetComponent<VRTK_TrackedController>() == null)
				{
					this.actualLeftController.AddComponent<VRTK_TrackedController>();
				}
			}
			if (this.actualRightController != null)
			{
				action(VRTK_SDKManager.instance.scriptAliasRightController, this.actualRightController);
				if (this.actualRightController.GetComponent<VRTK_TrackedController>() == null)
				{
					this.actualRightController.AddComponent<VRTK_TrackedController>();
				}
			}
		}

		// Token: 0x04001606 RID: 5638
		[Tooltip("Determines whether the SDK object references are automatically set to the objects of the selected SDKs. If this is true populating is done whenever the selected SDKs change.")]
		public bool autoPopulateObjectReferences = true;

		// Token: 0x04001607 RID: 5639
		[Tooltip("A reference to the GameObject that is the user's boundary or play area, most likely provided by the SDK's Camera Rig.")]
		public GameObject actualBoundaries;

		// Token: 0x04001608 RID: 5640
		[Tooltip("A reference to the GameObject that contains the VR camera, most likely provided by the SDK's Camera Rig Headset.")]
		public GameObject actualHeadset;

		// Token: 0x04001609 RID: 5641
		[Tooltip("A reference to the GameObject that contains the SDK Left Hand Controller.")]
		public GameObject actualLeftController;

		// Token: 0x0400160A RID: 5642
		[Tooltip("A reference to the GameObject that contains the SDK Right Hand Controller.")]
		public GameObject actualRightController;

		// Token: 0x0400160B RID: 5643
		[Tooltip("A reference to the GameObject that models for the Left Hand Controller.")]
		public GameObject modelAliasLeftController;

		// Token: 0x0400160C RID: 5644
		[Tooltip("A reference to the GameObject that models for the Right Hand Controller.")]
		public GameObject modelAliasRightController;

		// Token: 0x0400160F RID: 5647
		[SerializeField]
		private VRTK_SDKInfo cachedSystemSDKInfo = VRTK_SDKInfo.Create<SDK_BaseSystem, SDK_FallbackSystem, SDK_FallbackSystem>()[0];

		// Token: 0x04001610 RID: 5648
		[SerializeField]
		private VRTK_SDKInfo cachedBoundariesSDKInfo = VRTK_SDKInfo.Create<SDK_BaseBoundaries, SDK_FallbackBoundaries, SDK_FallbackBoundaries>()[0];

		// Token: 0x04001611 RID: 5649
		[SerializeField]
		private VRTK_SDKInfo cachedHeadsetSDKInfo = VRTK_SDKInfo.Create<SDK_BaseHeadset, SDK_FallbackHeadset, SDK_FallbackHeadset>()[0];

		// Token: 0x04001612 RID: 5650
		[SerializeField]
		private VRTK_SDKInfo cachedControllerSDKInfo = VRTK_SDKInfo.Create<SDK_BaseController, SDK_FallbackController, SDK_FallbackController>()[0];

		// Token: 0x04001613 RID: 5651
		private SDK_BaseSystem cachedSystemSDK;

		// Token: 0x04001614 RID: 5652
		private SDK_BaseBoundaries cachedBoundariesSDK;

		// Token: 0x04001615 RID: 5653
		private SDK_BaseHeadset cachedHeadsetSDK;

		// Token: 0x04001616 RID: 5654
		private SDK_BaseController cachedControllerSDK;

		// Token: 0x02000606 RID: 1542
		// (Invoke) Token: 0x06002AAB RID: 10923
		public delegate void LoadEventHandler(VRTK_SDKManager sender, VRTK_SDKSetup setup);
	}
}
