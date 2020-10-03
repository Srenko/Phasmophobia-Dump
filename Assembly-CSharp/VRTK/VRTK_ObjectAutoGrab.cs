using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002AE RID: 686
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_ObjectAutoGrab")]
	public class VRTK_ObjectAutoGrab : MonoBehaviour
	{
		// Token: 0x14000086 RID: 134
		// (add) Token: 0x060016B6 RID: 5814 RVA: 0x0007A9A0 File Offset: 0x00078BA0
		// (remove) Token: 0x060016B7 RID: 5815 RVA: 0x0007A9D8 File Offset: 0x00078BD8
		public event ObjectAutoGrabEventHandler ObjectAutoGrabCompleted;

		// Token: 0x060016B8 RID: 5816 RVA: 0x0007AA0D File Offset: 0x00078C0D
		public virtual void OnObjectAutoGrabCompleted()
		{
			if (this.ObjectAutoGrabCompleted != null)
			{
				this.ObjectAutoGrabCompleted(this);
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0007AA23 File Offset: 0x00078C23
		public virtual void ClearPreviousClone()
		{
			this.previousClonedObject = null;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0007AA2C File Offset: 0x00078C2C
		protected virtual void OnEnable()
		{
			if (this.objectIsPrefab)
			{
				this.cloneGrabbedObject = true;
			}
			base.StartCoroutine(this.AutoGrab());
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0007AA4A File Offset: 0x00078C4A
		protected virtual IEnumerator AutoGrab()
		{
			yield return new WaitForEndOfFrame();
			this.interactTouch = ((this.interactTouch != null) ? this.interactTouch : base.GetComponentInParent<VRTK_InteractTouch>());
			this.interactGrab = ((this.interactGrab != null) ? this.interactGrab : base.GetComponentInParent<VRTK_InteractGrab>());
			if (this.interactTouch == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_ObjectAutoGrab",
					"VRTK_InteractTouch",
					"interactTouch",
					"the same or parent"
				}));
			}
			if (this.interactGrab == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_ObjectAutoGrab",
					"VRTK_InteractGrab",
					"interactGrab",
					"the same or parent"
				}));
			}
			if (this.objectToGrab == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.NOT_DEFINED, new object[]
				{
					"objectToGrab"
				}));
				yield break;
			}
			while (this.interactGrab.controllerAttachPoint == null)
			{
				yield return true;
			}
			bool disableWhenIdle = this.objectToGrab.disableWhenIdle;
			if (this.objectIsPrefab)
			{
				this.objectToGrab.disableWhenIdle = false;
			}
			VRTK_InteractableObject vrtk_InteractableObject = this.objectToGrab;
			if (this.alwaysCloneOnEnable)
			{
				this.ClearPreviousClone();
			}
			if (!this.interactGrab.GetGrabbedObject())
			{
				if (this.cloneGrabbedObject)
				{
					if (this.previousClonedObject == null)
					{
						vrtk_InteractableObject = Object.Instantiate<VRTK_InteractableObject>(this.objectToGrab);
						this.previousClonedObject = vrtk_InteractableObject;
					}
					else
					{
						vrtk_InteractableObject = this.previousClonedObject;
					}
				}
				if (vrtk_InteractableObject.isGrabbable && !vrtk_InteractableObject.IsGrabbed(null))
				{
					vrtk_InteractableObject.transform.position = base.transform.position;
					this.interactTouch.ForceStopTouching();
					this.interactTouch.ForceTouch(vrtk_InteractableObject.gameObject);
					this.interactGrab.AttemptGrab();
					this.OnObjectAutoGrabCompleted();
				}
			}
			this.objectToGrab.disableWhenIdle = disableWhenIdle;
			vrtk_InteractableObject.disableWhenIdle = disableWhenIdle;
			yield break;
		}

		// Token: 0x040012C4 RID: 4804
		[Tooltip("A game object (either within the scene or a prefab) that will be grabbed by the controller on game start.")]
		public VRTK_InteractableObject objectToGrab;

		// Token: 0x040012C5 RID: 4805
		[Tooltip("If the `Object To Grab` is a prefab then this needs to be checked, if the `Object To Grab` already exists in the scene then this needs to be unchecked.")]
		public bool objectIsPrefab;

		// Token: 0x040012C6 RID: 4806
		[Tooltip("If this is checked then the Object To Grab will be cloned into a new object and attached to the controller leaving the existing object in the scene. This is required if the same object is to be grabbed to both controllers as a single object cannot be grabbed by different controllers at the same time. It is also required to clone a grabbed object if it is a prefab as it needs to exist within the scene to be grabbed.")]
		public bool cloneGrabbedObject;

		// Token: 0x040012C7 RID: 4807
		[Tooltip("If `Clone Grabbed Object` is checked and this is checked, then whenever this script is disabled and re-enabled, it will always create a new clone of the object to grab. If this is false then the original cloned object will attempt to be grabbed again. If the original cloned object no longer exists then a new clone will be created.")]
		public bool alwaysCloneOnEnable;

		// Token: 0x040012C8 RID: 4808
		[Header("Custom Settings")]
		[Tooltip("The Interact Touch to listen for touches on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_InteractTouch interactTouch;

		// Token: 0x040012C9 RID: 4809
		[Tooltip("The Interact Grab to listen for grab actions on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_InteractGrab interactGrab;

		// Token: 0x040012CB RID: 4811
		protected VRTK_InteractableObject previousClonedObject;
	}
}
