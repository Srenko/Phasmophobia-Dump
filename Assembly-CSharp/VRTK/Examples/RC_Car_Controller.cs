using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000360 RID: 864
	public class RC_Car_Controller : MonoBehaviour
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x00097CEC File Offset: 0x00095EEC
		private void Start()
		{
			this.rcCarScript = this.rcCar.GetComponent<RC_Car>();
			base.GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += this.DoTriggerAxisChanged;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += this.DoTouchpadAxisChanged;
			base.GetComponent<VRTK_ControllerEvents>().TriggerReleased += this.DoTriggerReleased;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += this.DoTouchpadTouchEnd;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += this.DoCarReset;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00097D7D File Offset: 0x00095F7D
		private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.rcCarScript.SetTouchAxis(e.touchpadAxis);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00097D90 File Offset: 0x00095F90
		private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.rcCarScript.SetTriggerAxis(e.buttonPressure);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00097DA3 File Offset: 0x00095FA3
		private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.rcCarScript.SetTouchAxis(Vector2.zero);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00097DB5 File Offset: 0x00095FB5
		private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.rcCarScript.SetTriggerAxis(0f);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00097DC7 File Offset: 0x00095FC7
		private void DoCarReset(object sender, ControllerInteractionEventArgs e)
		{
			this.rcCarScript.ResetCar();
		}

		// Token: 0x0400178A RID: 6026
		public GameObject rcCar;

		// Token: 0x0400178B RID: 6027
		private RC_Car rcCarScript;
	}
}
