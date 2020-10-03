using System;
using UnityEngine;
using UnityEngine.Events;
using VRTK.UnityEventHelper;

namespace VRTK.Examples
{
	// Token: 0x0200034E RID: 846
	public class ButtonReactor : MonoBehaviour
	{
		// Token: 0x06001D82 RID: 7554 RVA: 0x00096ABC File Offset: 0x00094CBC
		private void Start()
		{
			this.buttonEvents = base.GetComponent<VRTK_Button_UnityEvents>();
			if (this.buttonEvents == null)
			{
				this.buttonEvents = base.gameObject.AddComponent<VRTK_Button_UnityEvents>();
			}
			this.buttonEvents.OnPushed.AddListener(new UnityAction<object, Control3DEventArgs>(this.handlePush));
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00096B10 File Offset: 0x00094D10
		private void handlePush(object sender, Control3DEventArgs e)
		{
			VRTK_Logger.Info("Pushed");
			Object.Destroy(Object.Instantiate<GameObject>(this.go, this.dispenseLocation.position, Quaternion.identity), 10f);
		}

		// Token: 0x04001745 RID: 5957
		public GameObject go;

		// Token: 0x04001746 RID: 5958
		public Transform dispenseLocation;

		// Token: 0x04001747 RID: 5959
		private VRTK_Button_UnityEvents buttonEvents;
	}
}
