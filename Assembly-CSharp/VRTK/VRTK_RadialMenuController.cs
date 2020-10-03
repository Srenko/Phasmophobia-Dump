using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200025B RID: 603
	[RequireComponent(typeof(VRTK_RadialMenu))]
	public class VRTK_RadialMenuController : MonoBehaviour
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x000674B8 File Offset: 0x000656B8
		protected virtual void Awake()
		{
			this.menu = base.GetComponent<VRTK_RadialMenu>();
			this.Initialize();
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000674CC File Offset: 0x000656CC
		protected virtual void Initialize()
		{
			if (this.events == null)
			{
				this.events = base.GetComponentInParent<VRTK_ControllerEvents>();
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000674E8 File Offset: 0x000656E8
		protected virtual void OnEnable()
		{
			if (this.events == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"RadialMenuController",
					"VRTK_ControllerEvents",
					"events",
					"the parent"
				}));
				return;
			}
			this.events.TouchpadPressed += this.DoTouchpadClicked;
			this.events.TouchpadReleased += this.DoTouchpadUnclicked;
			this.events.TouchpadTouchStart += this.DoTouchpadTouched;
			this.events.TouchpadTouchEnd += this.DoTouchpadUntouched;
			this.events.TouchpadAxisChanged += this.DoTouchpadAxisChanged;
			this.menu.FireHapticPulse += this.AttemptHapticPulse;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000675C8 File Offset: 0x000657C8
		protected virtual void OnDisable()
		{
			this.events.TouchpadPressed -= this.DoTouchpadClicked;
			this.events.TouchpadReleased -= this.DoTouchpadUnclicked;
			this.events.TouchpadTouchStart -= this.DoTouchpadTouched;
			this.events.TouchpadTouchEnd -= this.DoTouchpadUntouched;
			this.events.TouchpadAxisChanged -= this.DoTouchpadAxisChanged;
			this.menu.FireHapticPulse -= this.AttemptHapticPulse;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00067665 File Offset: 0x00065865
		protected virtual void DoClickButton(object sender = null)
		{
			this.menu.ClickButton(this.currentAngle);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00067678 File Offset: 0x00065878
		protected virtual void DoUnClickButton(object sender = null)
		{
			this.menu.UnClickButton(this.currentAngle);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0006768B File Offset: 0x0006588B
		protected virtual void DoShowMenu(float initialAngle, object sender = null)
		{
			this.menu.ShowMenu();
			this.DoChangeAngle(initialAngle, null);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000676A0 File Offset: 0x000658A0
		protected virtual void DoHideMenu(bool force, object sender = null)
		{
			this.menu.StopTouching();
			this.menu.HideMenu(force);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000676B9 File Offset: 0x000658B9
		protected virtual void DoChangeAngle(float angle, object sender = null)
		{
			this.currentAngle = angle;
			this.menu.HoverButton(this.currentAngle);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000676D3 File Offset: 0x000658D3
		protected virtual void AttemptHapticPulse(float strength)
		{
			if (this.events)
			{
				VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.events.gameObject), strength);
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000676F8 File Offset: 0x000658F8
		protected virtual void DoTouchpadClicked(object sender, ControllerInteractionEventArgs e)
		{
			this.DoClickButton(null);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00067701 File Offset: 0x00065901
		protected virtual void DoTouchpadUnclicked(object sender, ControllerInteractionEventArgs e)
		{
			this.DoUnClickButton(null);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0006770A File Offset: 0x0006590A
		protected virtual void DoTouchpadTouched(object sender, ControllerInteractionEventArgs e)
		{
			this.touchpadTouched = true;
			this.DoShowMenu(this.CalculateAngle(e), null);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00067721 File Offset: 0x00065921
		protected virtual void DoTouchpadUntouched(object sender, ControllerInteractionEventArgs e)
		{
			this.touchpadTouched = false;
			this.DoHideMenu(false, null);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00067732 File Offset: 0x00065932
		protected virtual void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			if (this.touchpadTouched)
			{
				this.DoChangeAngle(this.CalculateAngle(e), null);
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0006774A File Offset: 0x0006594A
		protected virtual float CalculateAngle(ControllerInteractionEventArgs e)
		{
			return 360f - e.touchpadAngle;
		}

		// Token: 0x04001072 RID: 4210
		[Tooltip("The controller to listen to the controller events on.")]
		public VRTK_ControllerEvents events;

		// Token: 0x04001073 RID: 4211
		protected VRTK_RadialMenu menu;

		// Token: 0x04001074 RID: 4212
		protected float currentAngle;

		// Token: 0x04001075 RID: 4213
		protected bool touchpadTouched;
	}
}
