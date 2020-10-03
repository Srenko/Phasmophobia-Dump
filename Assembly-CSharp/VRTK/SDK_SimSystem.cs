using System;

namespace VRTK
{
	// Token: 0x0200027C RID: 636
	[SDK_Description("Simulator", null, null, "Standalone", 0)]
	public class SDK_SimSystem : SDK_BaseSystem
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsDisplayOnDesktop()
		{
			return false;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool ShouldAppRenderWithLowResources()
		{
			return false;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ForceInterleavedReprojectionOn(bool force)
		{
		}
	}
}
