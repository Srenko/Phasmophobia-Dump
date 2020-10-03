using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200029C RID: 668
	public class VRTK_ControllerHaptics : MonoBehaviour
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x00074C93 File Offset: 0x00072E93
		public static void TriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength)
		{
			VRTK_ControllerHaptics.SetupInstance();
			if (VRTK_ControllerHaptics.instance != null)
			{
				VRTK_ControllerHaptics.instance.InternalTriggerHapticPulse(controllerReference, strength);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00074CB3 File Offset: 0x00072EB3
		public static void TriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength, float duration, float pulseInterval)
		{
			VRTK_ControllerHaptics.SetupInstance();
			if (VRTK_ControllerHaptics.instance != null)
			{
				VRTK_ControllerHaptics.instance.InternalTriggerHapticPulse(controllerReference, strength, duration, pulseInterval);
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00074CD5 File Offset: 0x00072ED5
		public static void TriggerHapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			VRTK_ControllerHaptics.SetupInstance();
			if (VRTK_ControllerHaptics.instance != null)
			{
				VRTK_ControllerHaptics.instance.InternalTriggerHapticPulse(controllerReference, clip);
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00074CF5 File Offset: 0x00072EF5
		public static void CancelHapticPulse(VRTK_ControllerReference controllerReference)
		{
			VRTK_ControllerHaptics.SetupInstance();
			if (VRTK_ControllerHaptics.instance != null)
			{
				VRTK_ControllerHaptics.instance.InternalCancelHapticPulse(controllerReference);
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00074D14 File Offset: 0x00072F14
		protected virtual void OnDisable()
		{
			base.StopAllCoroutines();
			this.hapticLoopCoroutines.Clear();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00074D27 File Offset: 0x00072F27
		protected static void SetupInstance()
		{
			if (VRTK_ControllerHaptics.instance == null && VRTK_SDKManager.instance != null)
			{
				VRTK_ControllerHaptics.instance = VRTK_SDKManager.instance.gameObject.AddComponent<VRTK_ControllerHaptics>();
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00074D58 File Offset: 0x00072F58
		protected virtual void InternalTriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength)
		{
			this.InternalCancelHapticPulse(controllerReference);
			float strength2 = Mathf.Clamp(strength, 0f, 1f);
			VRTK_SDK_Bridge.HapticPulse(controllerReference, strength2);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00074D84 File Offset: 0x00072F84
		protected virtual void InternalTriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength, float duration, float pulseInterval)
		{
			this.InternalCancelHapticPulse(controllerReference);
			float hapticPulseStrength = Mathf.Clamp(strength, 0f, 1f);
			SDK_ControllerHapticModifiers hapticModifiers = VRTK_SDK_Bridge.GetHapticModifiers();
			Coroutine value = base.StartCoroutine(this.SimpleHapticPulseRoutine(controllerReference, duration * hapticModifiers.durationModifier, hapticPulseStrength, pulseInterval * hapticModifiers.intervalModifier));
			if (!this.hapticLoopCoroutines.ContainsKey(controllerReference))
			{
				this.hapticLoopCoroutines.Add(controllerReference, value);
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00074DEC File Offset: 0x00072FEC
		protected virtual void InternalTriggerHapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			this.InternalCancelHapticPulse(controllerReference);
			if (!VRTK_SDK_Bridge.HapticPulse(controllerReference, clip))
			{
				Coroutine value = base.StartCoroutine(this.AudioClipHapticsRoutine(controllerReference, clip));
				if (!this.hapticLoopCoroutines.ContainsKey(controllerReference))
				{
					this.hapticLoopCoroutines.Add(controllerReference, value);
				}
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00074E33 File Offset: 0x00073033
		protected virtual void InternalCancelHapticPulse(VRTK_ControllerReference controllerReference)
		{
			if (this.hapticLoopCoroutines.ContainsKey(controllerReference) && this.hapticLoopCoroutines[controllerReference] != null)
			{
				base.StopCoroutine(this.hapticLoopCoroutines[controllerReference]);
				this.hapticLoopCoroutines.Remove(controllerReference);
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00074E70 File Offset: 0x00073070
		protected virtual IEnumerator SimpleHapticPulseRoutine(VRTK_ControllerReference controllerReference, float duration, float hapticPulseStrength, float pulseInterval)
		{
			if (pulseInterval <= 0f)
			{
				yield break;
			}
			while (duration > 0f)
			{
				VRTK_SDK_Bridge.HapticPulse(controllerReference, hapticPulseStrength);
				yield return new WaitForSeconds(pulseInterval);
				duration -= pulseInterval;
			}
			yield break;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00074E95 File Offset: 0x00073095
		protected virtual IEnumerator AudioClipHapticsRoutine(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			SDK_ControllerHapticModifiers hapticModifiers = VRTK_SDK_Bridge.GetHapticModifiers();
			float hapticScalar = (float)hapticModifiers.maxHapticVibration;
			float[] audioData = new float[hapticModifiers.hapticsBufferSize];
			int sampleOffset = -hapticModifiers.hapticsBufferSize;
			float startTime = Time.time;
			float length = clip.length / 1f;
			float endTime = startTime + length;
			float sampleRate = (float)clip.samples;
			while (Time.time <= endTime)
			{
				float num = (Time.time - startTime) / length;
				int num2 = (int)(sampleRate * num);
				if (num2 >= sampleOffset + hapticModifiers.hapticsBufferSize)
				{
					clip.GetData(audioData, num2);
					sampleOffset = num2;
				}
				float num3 = Mathf.Abs(audioData[num2 - sampleOffset]);
				ushort num4 = (ushort)(hapticScalar * num3);
				VRTK_SDK_Bridge.HapticPulse(controllerReference, (float)num4);
				yield return null;
			}
			yield break;
		}

		// Token: 0x04001207 RID: 4615
		protected static VRTK_ControllerHaptics instance;

		// Token: 0x04001208 RID: 4616
		protected Dictionary<VRTK_ControllerReference, Coroutine> hapticLoopCoroutines = new Dictionary<VRTK_ControllerReference, Coroutine>();
	}
}
