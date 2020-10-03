using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002AF RID: 687
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_ObjectTouchAutoInteract")]
	public class VRTK_ObjectTouchAutoInteract : MonoBehaviour
	{
		// Token: 0x060016BD RID: 5821 RVA: 0x0007AA5C File Offset: 0x00078C5C
		protected virtual void OnEnable()
		{
			this.regrabTimer = 0f;
			this.reuseTimer = 0f;
			this.touchers = new List<GameObject>();
			this.interactableObject = ((this.interactableObject != null) ? this.interactableObject : base.GetComponent<VRTK_InteractableObject>());
			if (this.interactableObject != null)
			{
				this.interactableObject.InteractableObjectTouched += this.InteractableObjectTouched;
				this.interactableObject.InteractableObjectUntouched += this.InteractableObjectUntouched;
				this.interactableObject.InteractableObjectUngrabbed += this.InteractableObjectUngrabbed;
				this.interactableObject.InteractableObjectUnused += this.InteractableObjectUnused;
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0007AB1C File Offset: 0x00078D1C
		protected virtual void OnDisable()
		{
			if (this.interactableObject != null)
			{
				this.interactableObject.InteractableObjectTouched -= this.InteractableObjectTouched;
				this.interactableObject.InteractableObjectUntouched -= this.InteractableObjectUntouched;
				this.interactableObject.InteractableObjectUngrabbed -= this.InteractableObjectUngrabbed;
				this.interactableObject.InteractableObjectUnused -= this.InteractableObjectUnused;
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0007AB98 File Offset: 0x00078D98
		protected virtual void Update()
		{
			if (this.interactableObject != null && (this.continuousGrabCheck || this.continuousUseCheck))
			{
				for (int i = 0; i < this.touchers.Count; i++)
				{
					if (this.continuousGrabCheck)
					{
						this.CheckGrab(this.touchers[i]);
					}
					if (this.continuousUseCheck)
					{
						this.CheckUse(this.touchers[i]);
					}
				}
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0007AC0D File Offset: 0x00078E0D
		protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
		{
			this.ManageTouchers(e.interactingObject, true);
			this.CheckGrab(e.interactingObject);
			this.CheckUse(e.interactingObject);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0007AC34 File Offset: 0x00078E34
		protected virtual void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
		{
			this.ManageTouchers(e.interactingObject, false);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0007AC43 File Offset: 0x00078E43
		protected virtual void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.regrabTimer = this.regrabDelay + Time.time;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0007AC57 File Offset: 0x00078E57
		protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
		{
			this.reuseTimer = this.reuseDelay + Time.time;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0007AC6B File Offset: 0x00078E6B
		protected virtual void ManageTouchers(GameObject interactingObject, bool add)
		{
			if (add && !this.touchers.Contains(interactingObject))
			{
				this.touchers.Add(interactingObject);
				return;
			}
			if (!add && this.touchers.Contains(interactingObject))
			{
				this.touchers.Remove(interactingObject);
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0007ACAC File Offset: 0x00078EAC
		protected virtual void CheckGrab(GameObject interactingObject)
		{
			if (this.grabOnTouchWhen != VRTK_ObjectTouchAutoInteract.AutoInteractions.Never && this.regrabTimer < Time.time)
			{
				VRTK_InteractGrab component = interactingObject.GetComponent<VRTK_InteractGrab>();
				if (component != null && (this.grabOnTouchWhen == VRTK_ObjectTouchAutoInteract.AutoInteractions.NoButtonHeld || (this.grabOnTouchWhen == VRTK_ObjectTouchAutoInteract.AutoInteractions.ButtonHeld && component.IsGrabButtonPressed())))
				{
					component.AttemptGrab();
				}
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0007AD00 File Offset: 0x00078F00
		protected virtual void CheckUse(GameObject interactingObject)
		{
			if (this.useOnTouchWhen != VRTK_ObjectTouchAutoInteract.AutoInteractions.Never && this.reuseTimer < Time.time)
			{
				VRTK_InteractUse component = interactingObject.GetComponent<VRTK_InteractUse>();
				if (component != null && (this.useOnTouchWhen == VRTK_ObjectTouchAutoInteract.AutoInteractions.NoButtonHeld || (this.useOnTouchWhen == VRTK_ObjectTouchAutoInteract.AutoInteractions.ButtonHeld && component.IsUseButtonPressed())))
				{
					if (!this.interactableObject.holdButtonToUse && this.interactableObject.IsUsing(null))
					{
						this.interactableObject.ForceStopInteracting();
						return;
					}
					component.AttemptUse();
				}
			}
		}

		// Token: 0x040012CC RID: 4812
		[Header("Auto Grab")]
		[Tooltip("Determines when a grab on touch should occur.")]
		public VRTK_ObjectTouchAutoInteract.AutoInteractions grabOnTouchWhen;

		// Token: 0x040012CD RID: 4813
		[Tooltip("After being ungrabbed, another auto grab on touch can only occur after this time.")]
		public float regrabDelay = 0.1f;

		// Token: 0x040012CE RID: 4814
		[Tooltip("If this is checked then the grab on touch check will happen every frame and not only on the first touch of the object.")]
		public bool continuousGrabCheck;

		// Token: 0x040012CF RID: 4815
		[Header("Auto Use")]
		[Tooltip("Determines when a use on touch should occur.")]
		public VRTK_ObjectTouchAutoInteract.AutoInteractions useOnTouchWhen;

		// Token: 0x040012D0 RID: 4816
		[Tooltip("After being unused, another auto use on touch can only occur after this time.")]
		public float reuseDelay = 0.1f;

		// Token: 0x040012D1 RID: 4817
		[Tooltip("If this is checked then the use on touch check will happen every frame and not only on the first touch of the object.")]
		public bool continuousUseCheck;

		// Token: 0x040012D2 RID: 4818
		[Header("Custom Settings")]
		[Tooltip("The interactable object that the auto interaction will occur on. If this is blank then the script must be on the same GameObject as the Interactable Object script.")]
		public VRTK_InteractableObject interactableObject;

		// Token: 0x040012D3 RID: 4819
		protected float regrabTimer;

		// Token: 0x040012D4 RID: 4820
		protected float reuseTimer;

		// Token: 0x040012D5 RID: 4821
		protected List<GameObject> touchers;

		// Token: 0x020005DB RID: 1499
		public enum AutoInteractions
		{
			// Token: 0x040027AB RID: 10155
			Never,
			// Token: 0x040027AC RID: 10156
			NoButtonHeld,
			// Token: 0x040027AD RID: 10157
			ButtonHeld
		}
	}
}
