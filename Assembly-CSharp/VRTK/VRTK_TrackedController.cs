using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002BF RID: 703
	public class VRTK_TrackedController : MonoBehaviour
	{
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06001744 RID: 5956 RVA: 0x0007C9F8 File Offset: 0x0007ABF8
		// (remove) Token: 0x06001745 RID: 5957 RVA: 0x0007CA30 File Offset: 0x0007AC30
		public event VRTKTrackedControllerEventHandler ControllerEnabled;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06001746 RID: 5958 RVA: 0x0007CA68 File Offset: 0x0007AC68
		// (remove) Token: 0x06001747 RID: 5959 RVA: 0x0007CAA0 File Offset: 0x0007ACA0
		public event VRTKTrackedControllerEventHandler ControllerDisabled;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06001748 RID: 5960 RVA: 0x0007CAD8 File Offset: 0x0007ACD8
		// (remove) Token: 0x06001749 RID: 5961 RVA: 0x0007CB10 File Offset: 0x0007AD10
		public event VRTKTrackedControllerEventHandler ControllerIndexChanged;

		// Token: 0x0600174A RID: 5962 RVA: 0x0007CB45 File Offset: 0x0007AD45
		public virtual void OnControllerEnabled(VRTKTrackedControllerEventArgs e)
		{
			if (this.ControllerEnabled != null)
			{
				this.ControllerEnabled(this, e);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0007CB5C File Offset: 0x0007AD5C
		public virtual void OnControllerDisabled(VRTKTrackedControllerEventArgs e)
		{
			if (this.ControllerDisabled != null)
			{
				this.ControllerDisabled(this, e);
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0007CB73 File Offset: 0x0007AD73
		public virtual void OnControllerIndexChanged(VRTKTrackedControllerEventArgs e)
		{
			if (this.ControllerIndexChanged != null)
			{
				this.ControllerIndexChanged(this, e);
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0007CB8C File Offset: 0x0007AD8C
		protected virtual VRTKTrackedControllerEventArgs SetEventPayload(uint previousIndex = 4294967295U)
		{
			VRTKTrackedControllerEventArgs result;
			result.currentIndex = this.index;
			result.previousIndex = previousIndex;
			return result;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
		protected virtual void OnEnable()
		{
			this.aliasController = VRTK_DeviceFinder.GetScriptAliasController(base.gameObject);
			if (this.aliasController == null)
			{
				this.aliasController = base.gameObject;
			}
			this.index = VRTK_DeviceFinder.GetControllerIndex(base.gameObject);
			this.OnControllerEnabled(this.SetEventPayload(uint.MaxValue));
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0007CC06 File Offset: 0x0007AE06
		protected virtual void OnDisable()
		{
			this.OnControllerDisabled(this.SetEventPayload(uint.MaxValue));
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0007CC15 File Offset: 0x0007AE15
		protected virtual void FixedUpdate()
		{
			VRTK_SDK_Bridge.ControllerProcessFixedUpdate(VRTK_ControllerReference.GetControllerReference(this.index), null);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0007CC28 File Offset: 0x0007AE28
		protected virtual void Update()
		{
			uint controllerIndex = VRTK_DeviceFinder.GetControllerIndex(base.gameObject);
			if (controllerIndex != this.index)
			{
				uint eventPayload = this.index;
				this.index = controllerIndex;
				this.OnControllerIndexChanged(this.SetEventPayload(eventPayload));
			}
			VRTK_SDK_Bridge.ControllerProcessUpdate(VRTK_ControllerReference.GetControllerReference(this.index), null);
			if (this.aliasController != null && base.gameObject.activeInHierarchy && !this.aliasController.activeSelf)
			{
				this.aliasController.SetActive(true);
			}
		}

		// Token: 0x0400130B RID: 4875
		public uint index = uint.MaxValue;

		// Token: 0x0400130F RID: 4879
		protected GameObject aliasController;
	}
}
