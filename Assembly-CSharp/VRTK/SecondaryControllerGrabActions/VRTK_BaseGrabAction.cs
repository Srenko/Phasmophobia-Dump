using System;
using UnityEngine;

namespace VRTK.SecondaryControllerGrabActions
{
	// Token: 0x0200033C RID: 828
	public abstract class VRTK_BaseGrabAction : MonoBehaviour
	{
		// Token: 0x06001D04 RID: 7428 RVA: 0x00094C6F File Offset: 0x00092E6F
		public virtual void Initialise(VRTK_InteractableObject currentGrabbdObject, VRTK_InteractGrab currentPrimaryGrabbingObject, VRTK_InteractGrab currentSecondaryGrabbingObject, Transform primaryGrabPoint, Transform secondaryGrabPoint)
		{
			this.grabbedObject = currentGrabbdObject;
			this.primaryGrabbingObject = currentPrimaryGrabbingObject;
			this.secondaryGrabbingObject = currentSecondaryGrabbingObject;
			this.primaryInitialGrabPoint = primaryGrabPoint;
			this.secondaryInitialGrabPoint = secondaryGrabPoint;
			this.initialised = true;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00094C9D File Offset: 0x00092E9D
		public virtual void ResetAction()
		{
			this.grabbedObject = null;
			this.primaryGrabbingObject = null;
			this.secondaryGrabbingObject = null;
			this.primaryInitialGrabPoint = null;
			this.secondaryInitialGrabPoint = null;
			this.initialised = false;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00094CC9 File Offset: 0x00092EC9
		public virtual bool IsActionable()
		{
			return this.isActionable;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00094CD1 File Offset: 0x00092ED1
		public virtual bool IsSwappable()
		{
			return this.isSwappable;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void ProcessUpdate()
		{
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void ProcessFixedUpdate()
		{
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnDropAction()
		{
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00094CD9 File Offset: 0x00092ED9
		protected virtual void CheckForceStopDistance(float ungrabDistance)
		{
			if (this.initialised && Vector3.Distance(this.secondaryGrabbingObject.transform.position, this.secondaryInitialGrabPoint.position) > ungrabDistance)
			{
				this.grabbedObject.ForceStopSecondaryGrabInteraction();
			}
		}

		// Token: 0x04001703 RID: 5891
		protected VRTK_InteractableObject grabbedObject;

		// Token: 0x04001704 RID: 5892
		protected VRTK_InteractGrab primaryGrabbingObject;

		// Token: 0x04001705 RID: 5893
		protected VRTK_InteractGrab secondaryGrabbingObject;

		// Token: 0x04001706 RID: 5894
		protected Transform primaryInitialGrabPoint;

		// Token: 0x04001707 RID: 5895
		protected Transform secondaryInitialGrabPoint;

		// Token: 0x04001708 RID: 5896
		protected bool initialised;

		// Token: 0x04001709 RID: 5897
		protected bool isActionable = true;

		// Token: 0x0400170A RID: 5898
		protected bool isSwappable;
	}
}
