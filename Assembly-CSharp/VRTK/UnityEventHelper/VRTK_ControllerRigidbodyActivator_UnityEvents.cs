using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200031F RID: 799
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ControllerRigidbodyActivator_UnityEvents")]
	public sealed class VRTK_ControllerRigidbodyActivator_UnityEvents : VRTK_UnityEvents<VRTK_ControllerRigidbodyActivator>
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x00092E5A File Offset: 0x0009105A
		protected override void AddListeners(VRTK_ControllerRigidbodyActivator component)
		{
			component.ControllerRigidbodyOn += this.ControllerRigidbodyOn;
			component.ControllerRigidbodyOff += this.ControllerRigidbodyOff;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00092E80 File Offset: 0x00091080
		protected override void RemoveListeners(VRTK_ControllerRigidbodyActivator component)
		{
			component.ControllerRigidbodyOn -= this.ControllerRigidbodyOn;
			component.ControllerRigidbodyOff -= this.ControllerRigidbodyOff;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00092EA6 File Offset: 0x000910A6
		private void ControllerRigidbodyOn(object o, ControllerRigidbodyActivatorEventArgs e)
		{
			this.OnControllerRigidbodyOn.Invoke(o, e);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00092EB5 File Offset: 0x000910B5
		private void ControllerRigidbodyOff(object o, ControllerRigidbodyActivatorEventArgs e)
		{
			this.OnControllerRigidbodyOff.Invoke(o, e);
		}

		// Token: 0x04001697 RID: 5783
		public VRTK_ControllerRigidbodyActivator_UnityEvents.ControllerRigidbodyActivatorEvent OnControllerRigidbodyOn = new VRTK_ControllerRigidbodyActivator_UnityEvents.ControllerRigidbodyActivatorEvent();

		// Token: 0x04001698 RID: 5784
		public VRTK_ControllerRigidbodyActivator_UnityEvents.ControllerRigidbodyActivatorEvent OnControllerRigidbodyOff = new VRTK_ControllerRigidbodyActivator_UnityEvents.ControllerRigidbodyActivatorEvent();

		// Token: 0x02000623 RID: 1571
		[Serializable]
		public sealed class ControllerRigidbodyActivatorEvent : UnityEvent<object, ControllerRigidbodyActivatorEventArgs>
		{
		}
	}
}
