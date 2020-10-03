using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000366 RID: 870
	public class RendererOffOnDash : MonoBehaviour
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x00098528 File Offset: 0x00096728
		private void OnEnable()
		{
			foreach (VRTK_BasicTeleport vrtk_BasicTeleport in VRTK_ObjectCache.registeredTeleporters)
			{
				VRTK_DashTeleport component = vrtk_BasicTeleport.GetComponent<VRTK_DashTeleport>();
				if (component)
				{
					this.dashTeleporters.Add(component);
				}
			}
			foreach (VRTK_DashTeleport vrtk_DashTeleport in this.dashTeleporters)
			{
				vrtk_DashTeleport.WillDashThruObjects += this.RendererOff;
				vrtk_DashTeleport.DashedThruObjects += this.RendererOn;
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000985EC File Offset: 0x000967EC
		private void OnDisable()
		{
			foreach (VRTK_DashTeleport vrtk_DashTeleport in this.dashTeleporters)
			{
				vrtk_DashTeleport.WillDashThruObjects -= this.RendererOff;
				vrtk_DashTeleport.DashedThruObjects -= this.RendererOn;
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0009865C File Offset: 0x0009685C
		private void RendererOff(object sender, DashTeleportEventArgs e)
		{
			GameObject gameObject = base.transform.gameObject;
			foreach (RaycastHit raycastHit in e.hits)
			{
				if (raycastHit.collider.gameObject == gameObject)
				{
					this.SwitchRenderer(gameObject, false);
					return;
				}
			}
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000986B0 File Offset: 0x000968B0
		private void RendererOn(object sender, DashTeleportEventArgs e)
		{
			GameObject gameObject = base.transform.gameObject;
			if (this.wasSwitchedOff)
			{
				this.SwitchRenderer(gameObject, true);
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000986D9 File Offset: 0x000968D9
		private void SwitchRenderer(GameObject go, bool enable)
		{
			go.GetComponent<Renderer>().enabled = enable;
			this.wasSwitchedOff = !enable;
		}

		// Token: 0x040017A6 RID: 6054
		private bool wasSwitchedOff;

		// Token: 0x040017A7 RID: 6055
		private List<VRTK_DashTeleport> dashTeleporters = new List<VRTK_DashTeleport>();
	}
}
