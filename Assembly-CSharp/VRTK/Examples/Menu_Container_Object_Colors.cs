using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200035B RID: 859
	public class Menu_Container_Object_Colors : VRTK_InteractableObject
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x000976A8 File Offset: 0x000958A8
		public void SetSelectedColor(Color color)
		{
			Menu_Object_Spawner[] componentsInChildren = base.gameObject.GetComponentsInChildren<Menu_Object_Spawner>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetSelectedColor(color);
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000976D8 File Offset: 0x000958D8
		protected void Start()
		{
			this.SetSelectedColor(Color.red);
			this.SaveCurrentState();
		}
	}
}
