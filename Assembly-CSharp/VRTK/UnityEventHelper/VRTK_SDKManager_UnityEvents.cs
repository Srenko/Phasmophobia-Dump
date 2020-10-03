using System;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000335 RID: 821
	public sealed class VRTK_SDKManager_UnityEvents : VRTK_UnityEvents<VRTK_SDKManager>
	{
		// Token: 0x06001CD6 RID: 7382 RVA: 0x0009444F File Offset: 0x0009264F
		protected override void AddListeners(VRTK_SDKManager component)
		{
			component.LoadedSetupChanged += this.LoadedSetupChanged;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00094463 File Offset: 0x00092663
		protected override void RemoveListeners(VRTK_SDKManager component)
		{
			component.LoadedSetupChanged -= this.LoadedSetupChanged;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00094477 File Offset: 0x00092677
		private void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
		{
			this.OnLoadedSetupChanged.Invoke(sender, e);
		}

		// Token: 0x040016E8 RID: 5864
		public VRTK_SDKManager_UnityEvents.LoadedSetupChangeEvent OnLoadedSetupChanged = new VRTK_SDKManager_UnityEvents.LoadedSetupChangeEvent();

		// Token: 0x02000638 RID: 1592
		[Serializable]
		public sealed class LoadedSetupChangeEvent : UnityEvent<VRTK_SDKManager, VRTK_SDKManager.LoadedSetupChangeEventArgs>
		{
		}
	}
}
