using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000372 RID: 882
	public class VRTK_RoomExtender_ControllerExample : MonoBehaviour
	{
		// Token: 0x06001E6E RID: 7790 RVA: 0x0009A0A0 File Offset: 0x000982A0
		private void Start()
		{
			if (base.GetComponent<VRTK_ControllerEvents>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_RoomExtender_ControllerExample",
					"VRTK_ControllerEvents",
					"the Controller Alias"
				}));
				return;
			}
			if (Object.FindObjectOfType<VRTK_RoomExtender>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_RoomExtender_ControllerExample",
					"VRTK_RoomExtender"
				}));
				return;
			}
			this.roomExtender = Object.FindObjectOfType<VRTK_RoomExtender>();
			base.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += this.DoTouchpadPressed;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadReleased += this.DoTouchpadReleased;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += this.DoSwitchMovementFunction;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0009A164 File Offset: 0x00098364
		private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.roomExtender.additionalMovementMultiplier = ((e.touchpadAxis.magnitude * 5f > 1f) ? (e.touchpadAxis.magnitude * 5f) : 1f);
			if (this.roomExtender.additionalMovementEnabledOnButtonPress)
			{
				this.EnableAdditionalMovement();
				return;
			}
			this.DisableAdditionalMovement();
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0009A1C8 File Offset: 0x000983C8
		private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
		{
			if (this.roomExtender.additionalMovementEnabledOnButtonPress)
			{
				this.DisableAdditionalMovement();
				return;
			}
			this.EnableAdditionalMovement();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0009A1E4 File Offset: 0x000983E4
		private void DoSwitchMovementFunction(object sender, ControllerInteractionEventArgs e)
		{
			VRTK_RoomExtender.MovementFunction movementFunction = this.roomExtender.movementFunction;
			if (movementFunction == VRTK_RoomExtender.MovementFunction.Nonlinear)
			{
				this.roomExtender.movementFunction = VRTK_RoomExtender.MovementFunction.LinearDirect;
				return;
			}
			if (movementFunction != VRTK_RoomExtender.MovementFunction.LinearDirect)
			{
				return;
			}
			this.roomExtender.movementFunction = VRTK_RoomExtender.MovementFunction.Nonlinear;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0009A21E File Offset: 0x0009841E
		private void EnableAdditionalMovement()
		{
			this.roomExtender.additionalMovementEnabled = true;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0009A22C File Offset: 0x0009842C
		private void DisableAdditionalMovement()
		{
			this.roomExtender.additionalMovementEnabled = false;
		}

		// Token: 0x040017C6 RID: 6086
		protected VRTK_RoomExtender roomExtender;
	}
}
