using System;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x02000380 RID: 896
	public class BowAnimation : MonoBehaviour
	{
		// Token: 0x06001ED0 RID: 7888 RVA: 0x0009C770 File Offset: 0x0009A970
		public void SetFrame(float frame)
		{
			this.animationTimeline["BowPullAnimation"].speed = 0f;
			this.animationTimeline["BowPullAnimation"].time = frame;
			this.animationTimeline.Play("BowPullAnimation");
		}

		// Token: 0x04001808 RID: 6152
		public Animation animationTimeline;
	}
}
