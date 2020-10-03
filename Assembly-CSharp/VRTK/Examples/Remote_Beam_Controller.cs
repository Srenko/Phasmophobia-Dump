using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000365 RID: 869
	public class Remote_Beam_Controller : MonoBehaviour
	{
		// Token: 0x06001DF3 RID: 7667 RVA: 0x000984B4 File Offset: 0x000966B4
		private void Start()
		{
			this.remoteBeamScript = this.remoteBeam.GetComponent<Remote_Beam>();
			base.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += this.DoTouchpadAxisChanged;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += this.DoTouchpadTouchEnd;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00098500 File Offset: 0x00096700
		private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.remoteBeamScript.SetTouchAxis(e.touchpadAxis);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00098513 File Offset: 0x00096713
		private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.remoteBeamScript.SetTouchAxis(Vector2.zero);
		}

		// Token: 0x040017A4 RID: 6052
		public GameObject remoteBeam;

		// Token: 0x040017A5 RID: 6053
		private Remote_Beam remoteBeamScript;
	}
}
