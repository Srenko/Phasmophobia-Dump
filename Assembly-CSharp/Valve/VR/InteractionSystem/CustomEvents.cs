using System;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000415 RID: 1045
	public static class CustomEvents
	{
		// Token: 0x0200076F RID: 1903
		[Serializable]
		public class UnityEventSingleFloat : UnityEvent<float>
		{
		}

		// Token: 0x02000770 RID: 1904
		[Serializable]
		public class UnityEventHand : UnityEvent<Hand>
		{
		}
	}
}
