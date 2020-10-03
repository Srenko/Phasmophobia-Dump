using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200044A RID: 1098
	[RequireComponent(typeof(Interactable))]
	public class Longbow : MonoBehaviour
	{
		// Token: 0x060021B9 RID: 8633 RVA: 0x000A6F20 File Offset: 0x000A5120
		private void OnAttachedToHand(Hand attachedHand)
		{
			this.hand = attachedHand;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000A6F29 File Offset: 0x000A5129
		private void Awake()
		{
			this.newPosesAppliedAction = SteamVR_Events.NewPosesAppliedAction(new UnityAction(this.OnNewPosesApplied));
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000A6F42 File Offset: 0x000A5142
		private void OnEnable()
		{
			this.newPosesAppliedAction.enabled = true;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000A6F50 File Offset: 0x000A5150
		private void OnDisable()
		{
			this.newPosesAppliedAction.enabled = false;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000A6F5E File Offset: 0x000A515E
		private void LateUpdate()
		{
			if (this.deferNewPoses)
			{
				this.lateUpdatePos = base.transform.position;
				this.lateUpdateRot = base.transform.rotation;
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000A6F8A File Offset: 0x000A518A
		private void OnNewPosesApplied()
		{
			if (this.deferNewPoses)
			{
				base.transform.position = this.lateUpdatePos;
				base.transform.rotation = this.lateUpdateRot;
				this.deferNewPoses = false;
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000A6FC0 File Offset: 0x000A51C0
		private void HandAttachedUpdate(Hand hand)
		{
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			this.EvaluateHandedness();
			if (this.nocked)
			{
				this.deferNewPoses = true;
				Vector3 lhs = this.arrowHand.arrowNockTransform.parent.position - this.nockRestTransform.position;
				float num = Util.RemapNumberClamped(Time.time, this.nockLerpStartTime, this.nockLerpStartTime + this.lerpDuration, 0f, 1f);
				float d = Util.RemapNumberClamped(lhs.magnitude, 0.05f, 0.5f, 0f, 1f);
				Vector3 normalized = (Player.instance.hmdTransform.position + Vector3.down * 0.05f - this.arrowHand.arrowNockTransform.parent.position).normalized;
				Vector3 normalized2 = (this.arrowHand.arrowNockTransform.parent.position + normalized * this.drawOffset * d - this.pivotTransform.position).normalized;
				Vector3 normalized3 = (this.handleTransform.position - this.pivotTransform.position).normalized;
				this.bowLeftVector = -Vector3.Cross(normalized3, normalized2);
				this.pivotTransform.rotation = Quaternion.Lerp(this.nockLerpStartRotation, Quaternion.LookRotation(normalized2, this.bowLeftVector), num);
				if (Vector3.Dot(lhs, -this.nockTransform.forward) <= 0f)
				{
					this.nockTransform.localPosition = new Vector3(0f, 0f, 0f);
					this.bowDrawLinearMapping.value = 0f;
					return;
				}
				float num2 = lhs.magnitude * num;
				this.nockTransform.localPosition = new Vector3(0f, 0f, Mathf.Clamp(-num2, -0.5f, 0f));
				this.nockDistanceTravelled = -this.nockTransform.localPosition.z;
				this.arrowVelocity = Util.RemapNumber(this.nockDistanceTravelled, 0.05f, 0.5f, this.arrowMinVelocity, this.arrowMaxVelocity);
				this.drawTension = Util.RemapNumberClamped(this.nockDistanceTravelled, 0f, 0.5f, 0f, 1f);
				this.bowDrawLinearMapping.value = this.drawTension;
				if (this.nockDistanceTravelled > 0.05f)
				{
					this.pulled = true;
				}
				else
				{
					this.pulled = false;
				}
				if (this.nockDistanceTravelled > this.lastTickDistance + this.hapticDistanceThreshold || this.nockDistanceTravelled < this.lastTickDistance - this.hapticDistanceThreshold)
				{
					ushort durationMicroSec = (ushort)Util.RemapNumber(this.nockDistanceTravelled, 0f, 0.5f, 100f, 500f);
					hand.controller.TriggerHapticPulse(durationMicroSec, EVRButtonId.k_EButton_Axis0);
					hand.otherHand.controller.TriggerHapticPulse(durationMicroSec, EVRButtonId.k_EButton_Axis0);
					this.drawSound.PlayBowTensionClicks(this.drawTension);
					this.lastTickDistance = this.nockDistanceTravelled;
				}
				if (this.nockDistanceTravelled >= 0.5f && Time.time > this.nextStrainTick)
				{
					hand.controller.TriggerHapticPulse(400, EVRButtonId.k_EButton_Axis0);
					hand.otherHand.controller.TriggerHapticPulse(400, EVRButtonId.k_EButton_Axis0);
					this.drawSound.PlayBowTensionClicks(this.drawTension);
					this.nextStrainTick = Time.time + Random.Range(this.minStrainTickTime, this.maxStrainTickTime);
					return;
				}
			}
			else if (this.lerpBackToZeroRotation)
			{
				float num3 = Util.RemapNumber(Time.time, this.lerpStartTime, this.lerpStartTime + this.lerpDuration, 0f, 1f);
				this.pivotTransform.localRotation = Quaternion.Lerp(this.lerpStartRotation, Quaternion.identity, num3);
				if (num3 >= 1f)
				{
					this.lerpBackToZeroRotation = false;
				}
			}
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000A73DC File Offset: 0x000A55DC
		public void ArrowReleased()
		{
			this.nocked = false;
			this.hand.HoverUnlock(base.GetComponent<Interactable>());
			this.hand.otherHand.HoverUnlock(this.arrowHand.GetComponent<Interactable>());
			if (this.releaseSound != null)
			{
				this.releaseSound.Play();
			}
			base.StartCoroutine(this.ResetDrawAnim());
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000A7442 File Offset: 0x000A5642
		private IEnumerator ResetDrawAnim()
		{
			float startTime = Time.time;
			float startLerp = this.drawTension;
			while (Time.time < startTime + 0.02f)
			{
				float value = Util.RemapNumberClamped(Time.time, startTime, startTime + 0.02f, startLerp, 0f);
				this.bowDrawLinearMapping.value = value;
				yield return null;
			}
			this.bowDrawLinearMapping.value = 0f;
			yield break;
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000A7451 File Offset: 0x000A5651
		public float GetArrowVelocity()
		{
			return this.arrowVelocity;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000A7459 File Offset: 0x000A5659
		public void StartRotationLerp()
		{
			this.lerpStartTime = Time.time;
			this.lerpBackToZeroRotation = true;
			this.lerpStartRotation = this.pivotTransform.localRotation;
			Util.ResetTransform(this.nockTransform, true);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000A748C File Offset: 0x000A568C
		public void StartNock(ArrowHand currentArrowHand)
		{
			this.arrowHand = currentArrowHand;
			this.hand.HoverLock(base.GetComponent<Interactable>());
			this.nocked = true;
			this.nockLerpStartTime = Time.time;
			this.nockLerpStartRotation = this.pivotTransform.rotation;
			this.arrowSlideSound.Play();
			this.DoHandednessCheck();
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000A74E8 File Offset: 0x000A56E8
		private void EvaluateHandedness()
		{
			if (this.hand.GuessCurrentHandType() == Hand.HandType.Left)
			{
				if (this.possibleHandSwitch && this.currentHandGuess == Longbow.Handedness.Left)
				{
					this.possibleHandSwitch = false;
				}
				if (!this.possibleHandSwitch && this.currentHandGuess == Longbow.Handedness.Right)
				{
					this.possibleHandSwitch = true;
					this.timeOfPossibleHandSwitch = Time.time;
				}
				if (this.possibleHandSwitch && Time.time > this.timeOfPossibleHandSwitch + this.timeBeforeConfirmingHandSwitch)
				{
					this.currentHandGuess = Longbow.Handedness.Left;
					this.possibleHandSwitch = false;
					return;
				}
			}
			else
			{
				if (this.possibleHandSwitch && this.currentHandGuess == Longbow.Handedness.Right)
				{
					this.possibleHandSwitch = false;
				}
				if (!this.possibleHandSwitch && this.currentHandGuess == Longbow.Handedness.Left)
				{
					this.possibleHandSwitch = true;
					this.timeOfPossibleHandSwitch = Time.time;
				}
				if (this.possibleHandSwitch && Time.time > this.timeOfPossibleHandSwitch + this.timeBeforeConfirmingHandSwitch)
				{
					this.currentHandGuess = Longbow.Handedness.Right;
					this.possibleHandSwitch = false;
				}
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000A75D0 File Offset: 0x000A57D0
		private void DoHandednessCheck()
		{
			if (this.currentHandGuess == Longbow.Handedness.Left)
			{
				this.pivotTransform.localScale = new Vector3(1f, 1f, 1f);
				return;
			}
			this.pivotTransform.localScale = new Vector3(1f, -1f, 1f);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000A7624 File Offset: 0x000A5824
		public void ArrowInPosition()
		{
			this.DoHandednessCheck();
			if (this.nockSound != null)
			{
				this.nockSound.Play();
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000A7645 File Offset: 0x000A5845
		public void ReleaseNock()
		{
			this.nocked = false;
			this.hand.HoverUnlock(base.GetComponent<Interactable>());
			base.StartCoroutine(this.ResetDrawAnim());
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000A766C File Offset: 0x000A586C
		private void ShutDown()
		{
			if (this.hand != null && this.hand.otherHand.currentAttachedObject != null && this.hand.otherHand.currentAttachedObject.GetComponent<ItemPackageReference>() != null && this.hand.otherHand.currentAttachedObject.GetComponent<ItemPackageReference>().itemPackage == this.arrowHandItemPackage)
			{
				this.hand.otherHand.DetachObject(this.hand.otherHand.currentAttachedObject, true);
			}
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0000AC1C File Offset: 0x00008E1C
		private void OnHandFocusLost(Hand hand)
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000A7704 File Offset: 0x000A5904
		private void OnHandFocusAcquired(Hand hand)
		{
			base.gameObject.SetActive(true);
			this.OnAttachedToHand(hand);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00021808 File Offset: 0x0001FA08
		private void OnDetachedFromHand(Hand hand)
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000A7719 File Offset: 0x000A5919
		private void OnDestroy()
		{
			this.ShutDown();
		}

		// Token: 0x04001F27 RID: 7975
		public Longbow.Handedness currentHandGuess;

		// Token: 0x04001F28 RID: 7976
		private float timeOfPossibleHandSwitch;

		// Token: 0x04001F29 RID: 7977
		private float timeBeforeConfirmingHandSwitch = 1.5f;

		// Token: 0x04001F2A RID: 7978
		private bool possibleHandSwitch;

		// Token: 0x04001F2B RID: 7979
		public Transform pivotTransform;

		// Token: 0x04001F2C RID: 7980
		public Transform handleTransform;

		// Token: 0x04001F2D RID: 7981
		private Hand hand;

		// Token: 0x04001F2E RID: 7982
		private ArrowHand arrowHand;

		// Token: 0x04001F2F RID: 7983
		public Transform nockTransform;

		// Token: 0x04001F30 RID: 7984
		public Transform nockRestTransform;

		// Token: 0x04001F31 RID: 7985
		public bool autoSpawnArrowHand = true;

		// Token: 0x04001F32 RID: 7986
		public ItemPackage arrowHandItemPackage;

		// Token: 0x04001F33 RID: 7987
		public GameObject arrowHandPrefab;

		// Token: 0x04001F34 RID: 7988
		public bool nocked;

		// Token: 0x04001F35 RID: 7989
		public bool pulled;

		// Token: 0x04001F36 RID: 7990
		private const float minPull = 0.05f;

		// Token: 0x04001F37 RID: 7991
		private const float maxPull = 0.5f;

		// Token: 0x04001F38 RID: 7992
		private float nockDistanceTravelled;

		// Token: 0x04001F39 RID: 7993
		private float hapticDistanceThreshold = 0.01f;

		// Token: 0x04001F3A RID: 7994
		private float lastTickDistance;

		// Token: 0x04001F3B RID: 7995
		private const float bowPullPulseStrengthLow = 100f;

		// Token: 0x04001F3C RID: 7996
		private const float bowPullPulseStrengthHigh = 500f;

		// Token: 0x04001F3D RID: 7997
		private Vector3 bowLeftVector;

		// Token: 0x04001F3E RID: 7998
		public float arrowMinVelocity = 3f;

		// Token: 0x04001F3F RID: 7999
		public float arrowMaxVelocity = 30f;

		// Token: 0x04001F40 RID: 8000
		private float arrowVelocity = 30f;

		// Token: 0x04001F41 RID: 8001
		private float minStrainTickTime = 0.1f;

		// Token: 0x04001F42 RID: 8002
		private float maxStrainTickTime = 0.5f;

		// Token: 0x04001F43 RID: 8003
		private float nextStrainTick;

		// Token: 0x04001F44 RID: 8004
		private bool lerpBackToZeroRotation;

		// Token: 0x04001F45 RID: 8005
		private float lerpStartTime;

		// Token: 0x04001F46 RID: 8006
		private float lerpDuration = 0.15f;

		// Token: 0x04001F47 RID: 8007
		private Quaternion lerpStartRotation;

		// Token: 0x04001F48 RID: 8008
		private float nockLerpStartTime;

		// Token: 0x04001F49 RID: 8009
		private Quaternion nockLerpStartRotation;

		// Token: 0x04001F4A RID: 8010
		public float drawOffset = 0.06f;

		// Token: 0x04001F4B RID: 8011
		public LinearMapping bowDrawLinearMapping;

		// Token: 0x04001F4C RID: 8012
		private bool deferNewPoses;

		// Token: 0x04001F4D RID: 8013
		private Vector3 lateUpdatePos;

		// Token: 0x04001F4E RID: 8014
		private Quaternion lateUpdateRot;

		// Token: 0x04001F4F RID: 8015
		public SoundBowClick drawSound;

		// Token: 0x04001F50 RID: 8016
		private float drawTension;

		// Token: 0x04001F51 RID: 8017
		public SoundPlayOneshot arrowSlideSound;

		// Token: 0x04001F52 RID: 8018
		public SoundPlayOneshot releaseSound;

		// Token: 0x04001F53 RID: 8019
		public SoundPlayOneshot nockSound;

		// Token: 0x04001F54 RID: 8020
		private SteamVR_Events.Action newPosesAppliedAction;

		// Token: 0x02000788 RID: 1928
		public enum Handedness
		{
			// Token: 0x04002968 RID: 10600
			Left,
			// Token: 0x04002969 RID: 10601
			Right
		}
	}
}
