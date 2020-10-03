using System;

namespace VRTK
{
	// Token: 0x02000266 RID: 614
	public abstract class SDK_BaseSystem : SDK_Base
	{
		// Token: 0x06001271 RID: 4721
		public abstract bool IsDisplayOnDesktop();

		// Token: 0x06001272 RID: 4722
		public abstract bool ShouldAppRenderWithLowResources();

		// Token: 0x06001273 RID: 4723
		public abstract void ForceInterleavedReprojectionOn(bool force);
	}
}
