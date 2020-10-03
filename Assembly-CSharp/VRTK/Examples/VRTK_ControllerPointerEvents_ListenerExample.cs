using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000370 RID: 880
	public class VRTK_ControllerPointerEvents_ListenerExample : MonoBehaviour
	{
		// Token: 0x06001E60 RID: 7776 RVA: 0x00099D6C File Offset: 0x00097F6C
		private void Start()
		{
			if (base.GetComponent<VRTK_DestinationMarker>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_ControllerPointerEvents_ListenerExample",
					"VRTK_DestinationMarker",
					"the Controller Alias"
				}));
				return;
			}
			base.GetComponent<VRTK_DestinationMarker>().DestinationMarkerEnter += this.DoPointerIn;
			if (this.showHoverState)
			{
				base.GetComponent<VRTK_DestinationMarker>().DestinationMarkerHover += this.DoPointerHover;
			}
			base.GetComponent<VRTK_DestinationMarker>().DestinationMarkerExit += this.DoPointerOut;
			base.GetComponent<VRTK_DestinationMarker>().DestinationMarkerSet += this.DoPointerDestinationSet;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00099E18 File Offset: 0x00098018
		private void DebugLogger(uint index, string action, Transform target, RaycastHit raycastHit, float distance, Vector3 tipPosition)
		{
			string text = target ? target.name : "<NO VALID TARGET>";
			string text2 = raycastHit.collider ? raycastHit.collider.name : "<NO VALID COLLIDER>";
			VRTK_Logger.Info(string.Concat(new object[]
			{
				"Controller on index '",
				index,
				"' is ",
				action,
				" at a distance of ",
				distance,
				" on object named [",
				text,
				"] on the collider named [",
				text2,
				"] - the pointer tip position is/was: ",
				tipPosition
			}));
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00099ECA File Offset: 0x000980CA
		private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER IN", e.target, e.raycastHit, e.distance, e.destinationPosition);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00099EFA File Offset: 0x000980FA
		private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER OUT", e.target, e.raycastHit, e.distance, e.destinationPosition);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00099F2A File Offset: 0x0009812A
		private void DoPointerHover(object sender, DestinationMarkerEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER HOVER", e.target, e.raycastHit, e.distance, e.destinationPosition);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00099F5A File Offset: 0x0009815A
		private void DoPointerDestinationSet(object sender, DestinationMarkerEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER DESTINATION", e.target, e.raycastHit, e.distance, e.destinationPosition);
		}

		// Token: 0x040017C4 RID: 6084
		public bool showHoverState;
	}
}
