using System;
using UnityEngine;
using UnityEngine.Events;
using VRTK.UnityEventHelper;

namespace VRTK.Examples
{
	// Token: 0x0200034F RID: 847
	public class ControlReactor : MonoBehaviour
	{
		// Token: 0x06001D85 RID: 7557 RVA: 0x00096B44 File Offset: 0x00094D44
		private void Start()
		{
			this.controlEvents = base.GetComponent<VRTK_Control_UnityEvents>();
			if (this.controlEvents == null)
			{
				this.controlEvents = base.gameObject.AddComponent<VRTK_Control_UnityEvents>();
			}
			this.controlEvents.OnValueChanged.AddListener(new UnityAction<object, Control3DEventArgs>(this.HandleChange));
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00096B98 File Offset: 0x00094D98
		private void HandleChange(object sender, Control3DEventArgs e)
		{
			this.go.text = e.value.ToString() + "(" + e.normalizedValue.ToString() + "%)";
		}

		// Token: 0x04001748 RID: 5960
		public TextMesh go;

		// Token: 0x04001749 RID: 5961
		private VRTK_Control_UnityEvents controlEvents;
	}
}
