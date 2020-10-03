using System;
using UnityEngine;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200033A RID: 826
	public abstract class VRTK_UnityEvents<T> : MonoBehaviour where T : Component
	{
		// Token: 0x06001CF7 RID: 7415
		protected abstract void AddListeners(T component);

		// Token: 0x06001CF8 RID: 7416
		protected abstract void RemoveListeners(T component);

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0009492C File Offset: 0x00092B2C
		protected virtual void OnEnable()
		{
			this.component = base.GetComponent<T>();
			if (this.component != null)
			{
				this.AddListeners(this.component);
				return;
			}
			string name = base.GetType().Name;
			string name2 = this.component.GetType().Name;
			VRTK_Logger.Error(string.Format("The {0} script requires to be attached to a GameObject that contains a {1} script.", name, name2));
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00094998 File Offset: 0x00092B98
		protected virtual void OnDisable()
		{
			if (this.component != null)
			{
				this.RemoveListeners(this.component);
			}
		}

		// Token: 0x040016FA RID: 5882
		private T component;
	}
}
