using System;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000336 RID: 822
	public sealed class VRTK_SDKSetup_UnityEvents : VRTK_UnityEvents<VRTK_SDKSetup>
	{
		// Token: 0x06001CDA RID: 7386 RVA: 0x00094499 File Offset: 0x00092699
		protected override void AddListeners(VRTK_SDKSetup component)
		{
			component.Loaded += this.Loaded;
			component.Unloaded += this.Unloaded;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000944BF File Offset: 0x000926BF
		protected override void RemoveListeners(VRTK_SDKSetup component)
		{
			component.Loaded -= this.Loaded;
			component.Unloaded -= this.Unloaded;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000944E5 File Offset: 0x000926E5
		private void Loaded(VRTK_SDKManager sender, VRTK_SDKSetup setup)
		{
			this.OnLoaded.Invoke(sender, setup);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000944F4 File Offset: 0x000926F4
		private void Unloaded(VRTK_SDKManager sender, VRTK_SDKSetup setup)
		{
			this.OnUnloaded.Invoke(sender, setup);
		}

		// Token: 0x040016E9 RID: 5865
		public VRTK_SDKSetup_UnityEvents.LoadEvent OnLoaded = new VRTK_SDKSetup_UnityEvents.LoadEvent();

		// Token: 0x040016EA RID: 5866
		public VRTK_SDKSetup_UnityEvents.LoadEvent OnUnloaded = new VRTK_SDKSetup_UnityEvents.LoadEvent();

		// Token: 0x02000639 RID: 1593
		[Serializable]
		public sealed class LoadEvent : UnityEvent<VRTK_SDKManager, VRTK_SDKSetup>
		{
		}
	}
}
