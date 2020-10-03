using System;
using Valve.VR;

namespace VRTK
{
	// Token: 0x02000281 RID: 641
	[SDK_Description("SteamVR", "VRTK_DEFINE_SDK_STEAMVR", "OpenVR", "Standalone", 0)]
	public class SDK_SteamVRSystem : SDK_BaseSystem
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x0006BF6C File Offset: 0x0006A16C
		public override bool IsDisplayOnDesktop()
		{
			return OpenVR.System == null || OpenVR.System.IsDisplayOnDesktop();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0006BF81 File Offset: 0x0006A181
		public override bool ShouldAppRenderWithLowResources()
		{
			return OpenVR.Compositor != null && OpenVR.Compositor.ShouldAppRenderWithLowResources();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006BF96 File Offset: 0x0006A196
		public override void ForceInterleavedReprojectionOn(bool force)
		{
			if (OpenVR.Compositor != null)
			{
				OpenVR.Compositor.ForceInterleavedReprojectionOn(force);
			}
		}
	}
}
