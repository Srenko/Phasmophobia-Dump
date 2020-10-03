using System;
using UnityEngine;

namespace VRTK.SecondaryControllerGrabActions
{
	// Token: 0x0200033E RID: 830
	[AddComponentMenu("VRTK/Scripts/Interactions/Secondary Controller Grab Actions/VRTK_SwapControllerGrabAction")]
	public class VRTK_SwapControllerGrabAction : VRTK_BaseGrabAction
	{
		// Token: 0x06001D17 RID: 7447 RVA: 0x0009502D File Offset: 0x0009322D
		protected virtual void Awake()
		{
			this.isActionable = false;
			this.isSwappable = true;
		}
	}
}
