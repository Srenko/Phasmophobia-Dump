using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002A4 RID: 676
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_InteractHaptics")]
	public class VRTK_InteractHaptics : MonoBehaviour
	{
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060015B6 RID: 5558 RVA: 0x00077174 File Offset: 0x00075374
		// (remove) Token: 0x060015B7 RID: 5559 RVA: 0x000771AC File Offset: 0x000753AC
		public event InteractHapticsEventHandler InteractHapticsTouched;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060015B8 RID: 5560 RVA: 0x000771E4 File Offset: 0x000753E4
		// (remove) Token: 0x060015B9 RID: 5561 RVA: 0x0007721C File Offset: 0x0007541C
		public event InteractHapticsEventHandler InteractHapticsGrabbed;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060015BA RID: 5562 RVA: 0x00077254 File Offset: 0x00075454
		// (remove) Token: 0x060015BB RID: 5563 RVA: 0x0007728C File Offset: 0x0007548C
		public event InteractHapticsEventHandler InteractHapticsUsed;

		// Token: 0x060015BC RID: 5564 RVA: 0x000772C1 File Offset: 0x000754C1
		public virtual void OnInteractHapticsTouched(InteractHapticsEventArgs e)
		{
			if (this.InteractHapticsTouched != null)
			{
				this.InteractHapticsTouched(this, e);
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000772D8 File Offset: 0x000754D8
		public virtual void OnInteractHapticsGrabbed(InteractHapticsEventArgs e)
		{
			if (this.InteractHapticsGrabbed != null)
			{
				this.InteractHapticsGrabbed(this, e);
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000772EF File Offset: 0x000754EF
		public virtual void OnInteractHapticsUsed(InteractHapticsEventArgs e)
		{
			if (this.InteractHapticsUsed != null)
			{
				this.InteractHapticsUsed(this, e);
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00077306 File Offset: 0x00075506
		[Obsolete("`VRTK_InteractHaptics.HapticsOnTouch(controllerIndex)` has been replaced with `VRTK_InteractHaptics.HapticsOnTouch(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public virtual void HapticsOnTouch(uint controllerIndex)
		{
			this.HapticsOnTouch(VRTK_ControllerReference.GetControllerReference(controllerIndex));
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00077314 File Offset: 0x00075514
		public virtual void HapticsOnTouch(VRTK_ControllerReference controllerReference)
		{
			if (this.clipOnTouch != null)
			{
				VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, this.clipOnTouch);
			}
			else if (this.strengthOnTouch > 0f && this.durationOnTouch > 0f)
			{
				this.TriggerHapticPulse(controllerReference, this.strengthOnTouch, this.durationOnTouch, this.intervalOnTouch);
			}
			this.OnInteractHapticsTouched(this.SetEventPayload(controllerReference));
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0007737D File Offset: 0x0007557D
		[Obsolete("`VRTK_InteractHaptics.HapticsOnGrab(controllerIndex)` has been replaced with `VRTK_InteractHaptics.HapticsOnGrab(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public virtual void HapticsOnGrab(uint controllerIndex)
		{
			this.HapticsOnGrab(VRTK_ControllerReference.GetControllerReference(controllerIndex));
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0007738C File Offset: 0x0007558C
		public virtual void HapticsOnGrab(VRTK_ControllerReference controllerReference)
		{
			if (this.clipOnGrab != null)
			{
				VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, this.clipOnGrab);
			}
			else if (this.strengthOnGrab > 0f && this.durationOnGrab > 0f)
			{
				this.TriggerHapticPulse(controllerReference, this.strengthOnGrab, this.durationOnGrab, this.intervalOnGrab);
			}
			this.OnInteractHapticsGrabbed(this.SetEventPayload(controllerReference));
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000773F5 File Offset: 0x000755F5
		[Obsolete("`VRTK_InteractHaptics.HapticsOnUse(controllerIndex)` has been replaced with `VRTK_InteractHaptics.HapticsOnUse(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public virtual void HapticsOnUse(uint controllerIndex)
		{
			this.HapticsOnUse(VRTK_ControllerReference.GetControllerReference(controllerIndex));
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00077404 File Offset: 0x00075604
		public virtual void HapticsOnUse(VRTK_ControllerReference controllerReference)
		{
			if (this.clipOnUse != null)
			{
				VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, this.clipOnUse);
			}
			else if (this.strengthOnUse > 0f && this.durationOnUse > 0f)
			{
				this.TriggerHapticPulse(controllerReference, this.strengthOnUse, this.durationOnUse, this.intervalOnUse);
			}
			this.OnInteractHapticsUsed(this.SetEventPayload(controllerReference));
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0007746D File Offset: 0x0007566D
		protected virtual void OnEnable()
		{
			if (!base.GetComponent<VRTK_InteractableObject>())
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_InteractHaptics",
					"VRTK_InteractableObject",
					"the same"
				}));
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000774A5 File Offset: 0x000756A5
		protected virtual void TriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength, float duration, float interval)
		{
			VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, strength, duration, (interval >= 0.05f) ? interval : 0.05f);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000774C4 File Offset: 0x000756C4
		protected virtual InteractHapticsEventArgs SetEventPayload(VRTK_ControllerReference givenControllerReference)
		{
			InteractHapticsEventArgs result;
			result.controllerReference = givenControllerReference;
			return result;
		}

		// Token: 0x0400125B RID: 4699
		[Header("Haptics On Touch")]
		[Tooltip("Denotes the audio clip to use to rumble the controller on touch.")]
		public AudioClip clipOnTouch;

		// Token: 0x0400125C RID: 4700
		[Tooltip("Denotes how strong the rumble in the controller will be on touch.")]
		[Range(0f, 1f)]
		public float strengthOnTouch;

		// Token: 0x0400125D RID: 4701
		[Tooltip("Denotes how long the rumble in the controller will last on touch.")]
		public float durationOnTouch;

		// Token: 0x0400125E RID: 4702
		[Tooltip("Denotes interval betweens rumble in the controller on touch.")]
		public float intervalOnTouch = 0.05f;

		// Token: 0x0400125F RID: 4703
		[Header("Haptics On Grab")]
		[Tooltip("Denotes the audio clip to use to rumble the controller on grab.")]
		public AudioClip clipOnGrab;

		// Token: 0x04001260 RID: 4704
		[Tooltip("Denotes how strong the rumble in the controller will be on grab.")]
		[Range(0f, 1f)]
		public float strengthOnGrab;

		// Token: 0x04001261 RID: 4705
		[Tooltip("Denotes how long the rumble in the controller will last on grab.")]
		public float durationOnGrab;

		// Token: 0x04001262 RID: 4706
		[Tooltip("Denotes interval betweens rumble in the controller on grab.")]
		public float intervalOnGrab = 0.05f;

		// Token: 0x04001263 RID: 4707
		[Header("Haptics On Use")]
		[Tooltip("Denotes the audio clip to use to rumble the controller on use.")]
		public AudioClip clipOnUse;

		// Token: 0x04001264 RID: 4708
		[Tooltip("Denotes how strong the rumble in the controller will be on use.")]
		[Range(0f, 1f)]
		public float strengthOnUse;

		// Token: 0x04001265 RID: 4709
		[Tooltip("Denotes how long the rumble in the controller will last on use.")]
		public float durationOnUse;

		// Token: 0x04001266 RID: 4710
		[Tooltip("Denotes interval betweens rumble in the controller on use.")]
		public float intervalOnUse = 0.05f;

		// Token: 0x0400126A RID: 4714
		protected const float minInterval = 0.05f;
	}
}
