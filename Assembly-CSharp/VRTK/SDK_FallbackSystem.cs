using System;

namespace VRTK
{
	// Token: 0x02000271 RID: 625
	[SDK_Description("Fallback", null, null, null, 0)]
	public class SDK_FallbackSystem : SDK_BaseSystem
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsDisplayOnDesktop()
		{
			return false;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool ShouldAppRenderWithLowResources()
		{
			return false;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ForceInterleavedReprojectionOn(bool force)
		{
		}
	}
}
