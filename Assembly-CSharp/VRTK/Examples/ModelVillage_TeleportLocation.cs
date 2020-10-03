using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200035D RID: 861
	public class ModelVillage_TeleportLocation : VRTK_DestinationMarker
	{
		// Token: 0x06001DC1 RID: 7617 RVA: 0x000977D4 File Offset: 0x000959D4
		private void OnTriggerStay(Collider collider)
		{
			VRTK_ControllerEvents vrtk_ControllerEvents = collider.GetComponent<VRTK_ControllerEvents>() ? collider.GetComponent<VRTK_ControllerEvents>() : collider.GetComponentInParent<VRTK_ControllerEvents>();
			if (vrtk_ControllerEvents != null)
			{
				if (this.lastUsePressedState && !vrtk_ControllerEvents.triggerPressed)
				{
					float distance = Vector3.Distance(base.transform.position, this.destination.position);
					VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(vrtk_ControllerEvents.gameObject);
					this.OnDestinationMarkerSet(this.SetDestinationMarkerEvent(distance, this.destination, default(RaycastHit), this.destination.position, controllerReference, false, null));
				}
				this.lastUsePressedState = vrtk_ControllerEvents.triggerPressed;
			}
		}

		// Token: 0x04001774 RID: 6004
		public Transform destination;

		// Token: 0x04001775 RID: 6005
		private bool lastUsePressedState;
	}
}
