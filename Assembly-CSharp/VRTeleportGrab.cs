using System;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

// Token: 0x020001D0 RID: 464
public class VRTeleportGrab : MonoBehaviour
{
	// Token: 0x06000CAE RID: 3246 RVA: 0x00050DF8 File Offset: 0x0004EFF8
	private void Awake()
	{
		this.interactGrab = base.GetComponent<VRTK_InteractGrab>();
		this.interactTouch = base.GetComponent<VRTK_InteractTouch>();
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00050E12 File Offset: 0x0004F012
	private void OnEnable()
	{
		this.interactGrab.OnGrabPressed.AddListener(new UnityAction(this.GrabPressed));
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00050E30 File Offset: 0x0004F030
	private void OnDisable()
	{
		this.interactGrab.OnGrabPressed.RemoveListener(new UnityAction(this.GrabPressed));
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00050E4E File Offset: 0x0004F04E
	private void GrabPressed()
	{
		if (this.interactGrab.GetGrabbedObject() == null)
		{
			this.AttemptGrab();
			return;
		}
		if (!this.interactGrab.GetGrabbedObject().GetComponent<PhotonObjectInteract>().isGrabbed)
		{
			this.AttemptGrab();
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00050E88 File Offset: 0x0004F088
	private void AttemptGrab()
	{
		for (int i = 0; i < this.raycastPoints.Length; i++)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.raycastPoints[i].position, this.raycastPoints[i].forward, out raycastHit, 1.5f, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.GetComponent<Prop>())
			{
				PhotonObjectInteract component = raycastHit.collider.GetComponent<PhotonObjectInteract>();
				if (!component.isGrabbed)
				{
					if (!component.view.isMine)
					{
						component.view.RequestOwnership();
					}
					component.transform.position = base.transform.position;
					this.interactTouch.ForceStopTouching();
					this.interactTouch.ForceTouch(component.gameObject);
					this.interactGrab.AttemptGrab();
				}
			}
		}
	}

	// Token: 0x04000D5A RID: 3418
	[SerializeField]
	private Transform[] raycastPoints;

	// Token: 0x04000D5B RID: 3419
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000D5C RID: 3420
	private VRTK_InteractGrab interactGrab;

	// Token: 0x04000D5D RID: 3421
	private VRTK_InteractTouch interactTouch;
}
