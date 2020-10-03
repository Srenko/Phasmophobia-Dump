using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000331 RID: 817
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_PlayerClimb_UnityEvents")]
	public sealed class VRTK_PlayerClimb_UnityEvents : VRTK_UnityEvents<VRTK_PlayerClimb>
	{
		// Token: 0x06001CC0 RID: 7360 RVA: 0x0009418E File Offset: 0x0009238E
		protected override void AddListeners(VRTK_PlayerClimb component)
		{
			component.PlayerClimbStarted += this.PlayerClimbStarted;
			component.PlayerClimbEnded += this.PlayerClimbEnded;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000941B4 File Offset: 0x000923B4
		protected override void RemoveListeners(VRTK_PlayerClimb component)
		{
			component.PlayerClimbStarted -= this.PlayerClimbStarted;
			component.PlayerClimbEnded -= this.PlayerClimbEnded;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000941DA File Offset: 0x000923DA
		private void PlayerClimbStarted(object o, PlayerClimbEventArgs e)
		{
			this.OnPlayerClimbStarted.Invoke(o, e);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000941E9 File Offset: 0x000923E9
		private void PlayerClimbEnded(object o, PlayerClimbEventArgs e)
		{
			this.OnPlayerClimbEnded.Invoke(o, e);
		}

		// Token: 0x040016DE RID: 5854
		public VRTK_PlayerClimb_UnityEvents.PlayerClimbEvent OnPlayerClimbStarted = new VRTK_PlayerClimb_UnityEvents.PlayerClimbEvent();

		// Token: 0x040016DF RID: 5855
		public VRTK_PlayerClimb_UnityEvents.PlayerClimbEvent OnPlayerClimbEnded = new VRTK_PlayerClimb_UnityEvents.PlayerClimbEvent();

		// Token: 0x02000635 RID: 1589
		[Serializable]
		public sealed class PlayerClimbEvent : UnityEvent<object, PlayerClimbEventArgs>
		{
		}
	}
}
