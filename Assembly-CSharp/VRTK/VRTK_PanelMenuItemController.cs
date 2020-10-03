using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000256 RID: 598
	public class VRTK_PanelMenuItemController : MonoBehaviour
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600118E RID: 4494 RVA: 0x0006665C File Offset: 0x0006485C
		// (remove) Token: 0x0600118F RID: 4495 RVA: 0x00066694 File Offset: 0x00064894
		public event PanelMenuItemControllerEventHandler PanelMenuItemShowing;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001190 RID: 4496 RVA: 0x000666CC File Offset: 0x000648CC
		// (remove) Token: 0x06001191 RID: 4497 RVA: 0x00066704 File Offset: 0x00064904
		public event PanelMenuItemControllerEventHandler PanelMenuItemHiding;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001192 RID: 4498 RVA: 0x0006673C File Offset: 0x0006493C
		// (remove) Token: 0x06001193 RID: 4499 RVA: 0x00066774 File Offset: 0x00064974
		public event PanelMenuItemControllerEventHandler PanelMenuItemSwipeLeft;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001194 RID: 4500 RVA: 0x000667AC File Offset: 0x000649AC
		// (remove) Token: 0x06001195 RID: 4501 RVA: 0x000667E4 File Offset: 0x000649E4
		public event PanelMenuItemControllerEventHandler PanelMenuItemSwipeRight;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001196 RID: 4502 RVA: 0x0006681C File Offset: 0x00064A1C
		// (remove) Token: 0x06001197 RID: 4503 RVA: 0x00066854 File Offset: 0x00064A54
		public event PanelMenuItemControllerEventHandler PanelMenuItemSwipeTop;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001198 RID: 4504 RVA: 0x0006688C File Offset: 0x00064A8C
		// (remove) Token: 0x06001199 RID: 4505 RVA: 0x000668C4 File Offset: 0x00064AC4
		public event PanelMenuItemControllerEventHandler PanelMenuItemSwipeBottom;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600119A RID: 4506 RVA: 0x000668FC File Offset: 0x00064AFC
		// (remove) Token: 0x0600119B RID: 4507 RVA: 0x00066934 File Offset: 0x00064B34
		public event PanelMenuItemControllerEventHandler PanelMenuItemTriggerPressed;

		// Token: 0x0600119C RID: 4508 RVA: 0x00066969 File Offset: 0x00064B69
		public virtual void OnPanelMenuItemShowing(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemShowing != null)
			{
				this.PanelMenuItemShowing(this, e);
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00066980 File Offset: 0x00064B80
		public virtual void OnPanelMenuItemHiding(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemHiding != null)
			{
				this.PanelMenuItemHiding(this, e);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00066997 File Offset: 0x00064B97
		public virtual void OnPanelMenuItemSwipeLeft(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemSwipeLeft != null)
			{
				this.PanelMenuItemSwipeLeft(this, e);
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000669AE File Offset: 0x00064BAE
		public virtual void OnPanelMenuItemSwipeRight(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemSwipeRight != null)
			{
				this.PanelMenuItemSwipeRight(this, e);
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000669C5 File Offset: 0x00064BC5
		public virtual void OnPanelMenuItemSwipeTop(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemSwipeTop != null)
			{
				this.PanelMenuItemSwipeTop(this, e);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000669DC File Offset: 0x00064BDC
		public virtual void OnPanelMenuItemSwipeBottom(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemSwipeBottom != null)
			{
				this.PanelMenuItemSwipeBottom(this, e);
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000669F4 File Offset: 0x00064BF4
		public virtual PanelMenuItemControllerEventArgs SetPanelMenuItemEvent(GameObject interactableObject)
		{
			PanelMenuItemControllerEventArgs result;
			result.interactableObject = interactableObject;
			return result;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00066A0A File Offset: 0x00064C0A
		public virtual void Show(GameObject interactableObject)
		{
			base.gameObject.SetActive(true);
			this.OnPanelMenuItemShowing(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00066A25 File Offset: 0x00064C25
		public virtual void Hide(GameObject interactableObject)
		{
			base.gameObject.SetActive(false);
			this.OnPanelMenuItemHiding(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00066A40 File Offset: 0x00064C40
		public virtual void SwipeLeft(GameObject interactableObject)
		{
			this.OnPanelMenuItemSwipeLeft(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00066A4F File Offset: 0x00064C4F
		public virtual void SwipeRight(GameObject interactableObject)
		{
			this.OnPanelMenuItemSwipeRight(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00066A5E File Offset: 0x00064C5E
		public virtual void SwipeTop(GameObject interactableObject)
		{
			this.OnPanelMenuItemSwipeTop(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00066A6D File Offset: 0x00064C6D
		public virtual void SwipeBottom(GameObject interactableObject)
		{
			this.OnPanelMenuItemSwipeBottom(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00066A7C File Offset: 0x00064C7C
		public virtual void TriggerPressed(GameObject interactableObject)
		{
			this.OnPanelMenuItemTriggerPressed(this.SetPanelMenuItemEvent(interactableObject));
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00066A8B File Offset: 0x00064C8B
		protected virtual void OnPanelMenuItemTriggerPressed(PanelMenuItemControllerEventArgs e)
		{
			if (this.PanelMenuItemTriggerPressed != null)
			{
				this.PanelMenuItemTriggerPressed(this, e);
			}
		}
	}
}
