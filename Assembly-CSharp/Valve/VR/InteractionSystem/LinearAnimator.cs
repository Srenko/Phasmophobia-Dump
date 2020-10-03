using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200042A RID: 1066
	public class LinearAnimator : MonoBehaviour
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x000A18F8 File Offset: 0x0009FAF8
		private void Awake()
		{
			if (this.animator == null)
			{
				this.animator = base.GetComponent<Animator>();
			}
			this.animator.speed = 0f;
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000A194C File Offset: 0x0009FB4C
		private void Update()
		{
			if (this.currentLinearMapping != this.linearMapping.value)
			{
				this.currentLinearMapping = this.linearMapping.value;
				this.animator.enabled = true;
				this.animator.Play(0, 0, this.currentLinearMapping);
				this.framesUnchanged = 0;
				return;
			}
			this.framesUnchanged++;
			if (this.framesUnchanged > 2)
			{
				this.animator.enabled = false;
			}
		}

		// Token: 0x04001E40 RID: 7744
		public LinearMapping linearMapping;

		// Token: 0x04001E41 RID: 7745
		public Animator animator;

		// Token: 0x04001E42 RID: 7746
		private float currentLinearMapping = float.NaN;

		// Token: 0x04001E43 RID: 7747
		private int framesUnchanged;
	}
}
