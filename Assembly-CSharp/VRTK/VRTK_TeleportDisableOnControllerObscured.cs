using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D7 RID: 727
	[RequireComponent(typeof(VRTK_HeadsetControllerAware))]
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_TeleportDisableOnControllerObscured")]
	public class VRTK_TeleportDisableOnControllerObscured : MonoBehaviour
	{
		// Token: 0x06001837 RID: 6199 RVA: 0x00080DD3 File Offset: 0x0007EFD3
		protected virtual void OnEnable()
		{
			this.basicTeleport = base.GetComponent<VRTK_BasicTeleport>();
			base.StartCoroutine(this.EnableAtEndOfFrame());
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00080DF0 File Offset: 0x0007EFF0
		protected virtual void OnDisable()
		{
			if (this.basicTeleport == null)
			{
				return;
			}
			if (this.headset)
			{
				this.headset.ControllerObscured -= this.DisableTeleport;
				this.headset.ControllerUnobscured -= this.EnableTeleport;
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00080E49 File Offset: 0x0007F049
		protected virtual IEnumerator EnableAtEndOfFrame()
		{
			if (this.basicTeleport == null)
			{
				yield break;
			}
			yield return new WaitForEndOfFrame();
			this.headset = VRTK_ObjectCache.registeredHeadsetControllerAwareness;
			if (this.headset)
			{
				this.headset.ControllerObscured += this.DisableTeleport;
				this.headset.ControllerUnobscured += this.EnableTeleport;
			}
			yield break;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00080E58 File Offset: 0x0007F058
		protected virtual void DisableTeleport(object sender, HeadsetControllerAwareEventArgs e)
		{
			this.basicTeleport.ToggleTeleportEnabled(false);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00080E66 File Offset: 0x0007F066
		protected virtual void EnableTeleport(object sender, HeadsetControllerAwareEventArgs e)
		{
			this.basicTeleport.ToggleTeleportEnabled(true);
		}

		// Token: 0x040013BD RID: 5053
		protected VRTK_BasicTeleport basicTeleport;

		// Token: 0x040013BE RID: 5054
		protected VRTK_HeadsetControllerAware headset;
	}
}
