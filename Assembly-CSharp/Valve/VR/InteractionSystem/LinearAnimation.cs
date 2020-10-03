using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000429 RID: 1065
	public class LinearAnimation : MonoBehaviour
	{
		// Token: 0x060020BA RID: 8378 RVA: 0x000A1820 File Offset: 0x0009FA20
		private void Awake()
		{
			if (this.animation == null)
			{
				this.animation = base.GetComponent<Animation>();
			}
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
			this.animation.playAutomatically = true;
			this.animState = this.animation[this.animation.clip.name];
			this.animState.wrapMode = WrapMode.PingPong;
			this.animState.speed = 0f;
			this.animLength = this.animState.length;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000A18BC File Offset: 0x0009FABC
		private void Update()
		{
			float value = this.linearMapping.value;
			if (value != this.lastValue)
			{
				this.animState.time = value / this.animLength;
			}
			this.lastValue = value;
		}

		// Token: 0x04001E3B RID: 7739
		public LinearMapping linearMapping;

		// Token: 0x04001E3C RID: 7740
		public Animation animation;

		// Token: 0x04001E3D RID: 7741
		private AnimationState animState;

		// Token: 0x04001E3E RID: 7742
		private float animLength;

		// Token: 0x04001E3F RID: 7743
		private float lastValue;
	}
}
