using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000351 RID: 849
	public class Controller_Menu : MonoBehaviour
	{
		// Token: 0x06001D90 RID: 7568 RVA: 0x00096E1C File Offset: 0x0009501C
		private void Start()
		{
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += this.DoMenuOn;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += this.DoMenuOff;
			this.menuInit = false;
			this.menuActive = false;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00096E5A File Offset: 0x0009505A
		private void InitMenu()
		{
			this.clonedMenuObject = Object.Instantiate<GameObject>(this.menuObject, base.transform.position, Quaternion.identity);
			this.clonedMenuObject.SetActive(true);
			this.menuInit = true;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00096E90 File Offset: 0x00095090
		private void DoMenuOn(object sender, ControllerInteractionEventArgs e)
		{
			if (!this.menuInit)
			{
				this.InitMenu();
			}
			if (this.clonedMenuObject != null)
			{
				this.clonedMenuObject.SetActive(true);
				this.menuActive = true;
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00096EC1 File Offset: 0x000950C1
		private void DoMenuOff(object sender, ControllerInteractionEventArgs e)
		{
			if (this.clonedMenuObject != null)
			{
				this.clonedMenuObject.SetActive(false);
				this.menuActive = false;
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00096EE4 File Offset: 0x000950E4
		private void Update()
		{
			if (this.clonedMenuObject != null && this.menuActive)
			{
				this.clonedMenuObject.transform.rotation = base.transform.rotation;
				this.clonedMenuObject.transform.position = base.transform.position;
			}
		}

		// Token: 0x04001752 RID: 5970
		public GameObject menuObject;

		// Token: 0x04001753 RID: 5971
		private GameObject clonedMenuObject;

		// Token: 0x04001754 RID: 5972
		private bool menuInit;

		// Token: 0x04001755 RID: 5973
		private bool menuActive;
	}
}
