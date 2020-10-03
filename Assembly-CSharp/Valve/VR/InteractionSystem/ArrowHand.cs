using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000442 RID: 1090
	public class ArrowHand : MonoBehaviour
	{
		// Token: 0x0600218D RID: 8589 RVA: 0x000A5CE7 File Offset: 0x000A3EE7
		private void Awake()
		{
			this.allowTeleport = base.GetComponent<AllowTeleportWhileAttachedToHand>();
			this.allowTeleport.teleportAllowed = true;
			this.allowTeleport.overrideHoverLock = false;
			this.arrowList = new List<GameObject>();
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000A5D18 File Offset: 0x000A3F18
		private void OnAttachedToHand(Hand attachedHand)
		{
			this.hand = attachedHand;
			this.FindBow();
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000A5D28 File Offset: 0x000A3F28
		private GameObject InstantiateArrow()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.arrowPrefab, this.arrowNockTransform.position, this.arrowNockTransform.rotation);
			gameObject.name = "Bow Arrow";
			gameObject.transform.parent = this.arrowNockTransform;
			Util.ResetTransform(gameObject.transform, true);
			this.arrowList.Add(gameObject);
			while (this.arrowList.Count > this.maxArrowCount)
			{
				GameObject gameObject2 = this.arrowList[0];
				this.arrowList.RemoveAt(0);
				if (gameObject2)
				{
					Object.Destroy(gameObject2);
				}
			}
			return gameObject;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000A5DC8 File Offset: 0x000A3FC8
		private void HandAttachedUpdate(Hand hand)
		{
			if (this.bow == null)
			{
				this.FindBow();
			}
			if (this.bow == null)
			{
				return;
			}
			if (this.allowArrowSpawn && this.currentArrow == null)
			{
				this.currentArrow = this.InstantiateArrow();
				this.arrowSpawnSound.Play();
			}
			float num = Vector3.Distance(base.transform.parent.position, this.bow.nockTransform.position);
			if (!this.nocked)
			{
				if (num < this.rotationLerpThreshold)
				{
					float t = Util.RemapNumber(num, this.rotationLerpThreshold, this.lerpCompleteDistance, 0f, 1f);
					this.arrowNockTransform.rotation = Quaternion.Lerp(this.arrowNockTransform.parent.rotation, this.bow.nockRestTransform.rotation, t);
				}
				else
				{
					this.arrowNockTransform.localRotation = Quaternion.identity;
				}
				if (num < this.positionLerpThreshold)
				{
					float num2 = Util.RemapNumber(num, this.positionLerpThreshold, this.lerpCompleteDistance, 0f, 1f);
					num2 = Mathf.Clamp(num2, 0f, 1f);
					this.arrowNockTransform.position = Vector3.Lerp(this.arrowNockTransform.parent.position, this.bow.nockRestTransform.position, num2);
				}
				else
				{
					this.arrowNockTransform.position = this.arrowNockTransform.parent.position;
				}
				if (num < this.lerpCompleteDistance)
				{
					if (!this.arrowLerpComplete)
					{
						this.arrowLerpComplete = true;
						hand.controller.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
					}
				}
				else if (this.arrowLerpComplete)
				{
					this.arrowLerpComplete = false;
				}
				if (num < this.nockDistance)
				{
					if (!this.inNockRange)
					{
						this.inNockRange = true;
						this.bow.ArrowInPosition();
					}
				}
				else if (this.inNockRange)
				{
					this.inNockRange = false;
				}
				if (num < this.nockDistance && hand.controller.GetPress(8589934592UL) && !this.nocked)
				{
					if (this.currentArrow == null)
					{
						this.currentArrow = this.InstantiateArrow();
					}
					this.nocked = true;
					this.bow.StartNock(this);
					hand.HoverLock(base.GetComponent<Interactable>());
					this.allowTeleport.teleportAllowed = false;
					this.currentArrow.transform.parent = this.bow.nockTransform;
					Util.ResetTransform(this.currentArrow.transform, true);
					Util.ResetTransform(this.arrowNockTransform, true);
				}
			}
			if (this.nocked && (!hand.controller.GetPress(8589934592UL) || hand.controller.GetPressUp(8589934592UL)))
			{
				if (this.bow.pulled)
				{
					this.FireArrow();
				}
				else
				{
					this.arrowNockTransform.rotation = this.currentArrow.transform.rotation;
					this.currentArrow.transform.parent = this.arrowNockTransform;
					Util.ResetTransform(this.currentArrow.transform, true);
					this.nocked = false;
					this.bow.ReleaseNock();
					hand.HoverUnlock(base.GetComponent<Interactable>());
					this.allowTeleport.teleportAllowed = true;
				}
				this.bow.StartRotationLerp();
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00021808 File Offset: 0x0001FA08
		private void OnDetachedFromHand(Hand hand)
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000A6124 File Offset: 0x000A4324
		private void FireArrow()
		{
			this.currentArrow.transform.parent = null;
			Arrow component = this.currentArrow.GetComponent<Arrow>();
			component.shaftRB.isKinematic = false;
			component.shaftRB.useGravity = true;
			component.shaftRB.transform.GetComponent<BoxCollider>().enabled = true;
			component.arrowHeadRB.isKinematic = false;
			component.arrowHeadRB.useGravity = true;
			component.arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = true;
			component.arrowHeadRB.AddForce(this.currentArrow.transform.forward * this.bow.GetArrowVelocity(), ForceMode.VelocityChange);
			component.arrowHeadRB.AddTorque(this.currentArrow.transform.forward * 10f);
			this.nocked = false;
			this.currentArrow.GetComponent<Arrow>().ArrowReleased(this.bow.GetArrowVelocity());
			this.bow.ArrowReleased();
			this.allowArrowSpawn = false;
			base.Invoke("EnableArrowSpawn", 0.5f);
			base.StartCoroutine(this.ArrowReleaseHaptics());
			this.currentArrow = null;
			this.allowTeleport.teleportAllowed = true;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000A625D File Offset: 0x000A445D
		private void EnableArrowSpawn()
		{
			this.allowArrowSpawn = true;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000A6266 File Offset: 0x000A4466
		private IEnumerator ArrowReleaseHaptics()
		{
			yield return new WaitForSeconds(0.05f);
			this.hand.otherHand.controller.TriggerHapticPulse(1500, EVRButtonId.k_EButton_Axis0);
			yield return new WaitForSeconds(0.05f);
			this.hand.otherHand.controller.TriggerHapticPulse(800, EVRButtonId.k_EButton_Axis0);
			yield return new WaitForSeconds(0.05f);
			this.hand.otherHand.controller.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
			yield return new WaitForSeconds(0.05f);
			this.hand.otherHand.controller.TriggerHapticPulse(300, EVRButtonId.k_EButton_Axis0);
			yield break;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0000AC1C File Offset: 0x00008E1C
		private void OnHandFocusLost(Hand hand)
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000A6275 File Offset: 0x000A4475
		private void OnHandFocusAcquired(Hand hand)
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000A6283 File Offset: 0x000A4483
		private void FindBow()
		{
			this.bow = this.hand.otherHand.GetComponentInChildren<Longbow>();
		}

		// Token: 0x04001EE8 RID: 7912
		private Hand hand;

		// Token: 0x04001EE9 RID: 7913
		private Longbow bow;

		// Token: 0x04001EEA RID: 7914
		private GameObject currentArrow;

		// Token: 0x04001EEB RID: 7915
		public GameObject arrowPrefab;

		// Token: 0x04001EEC RID: 7916
		public Transform arrowNockTransform;

		// Token: 0x04001EED RID: 7917
		public float nockDistance = 0.1f;

		// Token: 0x04001EEE RID: 7918
		public float lerpCompleteDistance = 0.08f;

		// Token: 0x04001EEF RID: 7919
		public float rotationLerpThreshold = 0.15f;

		// Token: 0x04001EF0 RID: 7920
		public float positionLerpThreshold = 0.15f;

		// Token: 0x04001EF1 RID: 7921
		private bool allowArrowSpawn = true;

		// Token: 0x04001EF2 RID: 7922
		private bool nocked;

		// Token: 0x04001EF3 RID: 7923
		private bool inNockRange;

		// Token: 0x04001EF4 RID: 7924
		private bool arrowLerpComplete;

		// Token: 0x04001EF5 RID: 7925
		public SoundPlayOneshot arrowSpawnSound;

		// Token: 0x04001EF6 RID: 7926
		private AllowTeleportWhileAttachedToHand allowTeleport;

		// Token: 0x04001EF7 RID: 7927
		public int maxArrowCount = 10;

		// Token: 0x04001EF8 RID: 7928
		private List<GameObject> arrowList;
	}
}
