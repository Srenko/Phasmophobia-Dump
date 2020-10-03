using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000440 RID: 1088
	public class ArcheryTarget : MonoBehaviour
	{
		// Token: 0x06002181 RID: 8577 RVA: 0x000A55BD File Offset: 0x000A37BD
		private void ApplyDamage()
		{
			this.OnDamageTaken();
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000A55BD File Offset: 0x000A37BD
		private void FireExposure()
		{
			this.OnDamageTaken();
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000A55C5 File Offset: 0x000A37C5
		private void OnDamageTaken()
		{
			if (this.targetEnabled)
			{
				this.onTakeDamage.Invoke();
				base.StartCoroutine(this.FallDown());
				if (this.onceOnly)
				{
					this.targetEnabled = false;
				}
			}
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000A55F6 File Offset: 0x000A37F6
		private IEnumerator FallDown()
		{
			if (this.baseTransform)
			{
				Quaternion startingRot = this.baseTransform.rotation;
				float startTime = Time.time;
				float rotLerp = 0f;
				while (rotLerp < 1f)
				{
					rotLerp = Util.RemapNumberClamped(Time.time, startTime, startTime + this.fallTime, 0f, 1f);
					this.baseTransform.rotation = Quaternion.Lerp(startingRot, this.fallenDownTransform.rotation, rotLerp);
					yield return null;
				}
				startingRot = default(Quaternion);
			}
			yield return null;
			yield break;
		}

		// Token: 0x04001ECF RID: 7887
		public UnityEvent onTakeDamage;

		// Token: 0x04001ED0 RID: 7888
		public bool onceOnly;

		// Token: 0x04001ED1 RID: 7889
		public Transform targetCenter;

		// Token: 0x04001ED2 RID: 7890
		public Transform baseTransform;

		// Token: 0x04001ED3 RID: 7891
		public Transform fallenDownTransform;

		// Token: 0x04001ED4 RID: 7892
		public float fallTime = 0.5f;

		// Token: 0x04001ED5 RID: 7893
		private const float targetRadius = 0.25f;

		// Token: 0x04001ED6 RID: 7894
		private bool targetEnabled = true;
	}
}
