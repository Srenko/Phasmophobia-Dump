using System;
using System.Collections;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200043D RID: 1085
	[Serializable]
	public class AfterTimer_Component : MonoBehaviour
	{
		// Token: 0x06002152 RID: 8530 RVA: 0x000A4050 File Offset: 0x000A2250
		public void Init(float _time, Action _callback, bool earlydestroy)
		{
			this.triggerTime = _time;
			this.callback = _callback;
			this.triggerOnEarlyDestroy = earlydestroy;
			this.timerActive = true;
			base.StartCoroutine(this.Wait());
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000A407B File Offset: 0x000A227B
		private IEnumerator Wait()
		{
			yield return new WaitForSeconds(this.triggerTime);
			this.timerActive = false;
			this.callback();
			Object.Destroy(this);
			yield break;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000A408A File Offset: 0x000A228A
		private void OnDestroy()
		{
			if (this.timerActive)
			{
				base.StopCoroutine(this.Wait());
				this.timerActive = false;
				if (this.triggerOnEarlyDestroy)
				{
					this.callback();
				}
			}
		}

		// Token: 0x04001EB3 RID: 7859
		private Action callback;

		// Token: 0x04001EB4 RID: 7860
		private float triggerTime;

		// Token: 0x04001EB5 RID: 7861
		private bool timerActive;

		// Token: 0x04001EB6 RID: 7862
		private bool triggerOnEarlyDestroy;
	}
}
