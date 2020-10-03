using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200035A RID: 858
	public class Menu_Color_Changer : VRTK_InteractableObject
	{
		// Token: 0x06001DB5 RID: 7605 RVA: 0x00097614 File Offset: 0x00095814
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			base.transform.parent.gameObject.GetComponent<Menu_Container_Object_Colors>().SetSelectedColor(this.newMenuColor);
			this.ResetMenuItems();
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00097643 File Offset: 0x00095843
		public override void OnEnable()
		{
			base.OnEnable();
			base.gameObject.GetComponent<MeshRenderer>().material.color = this.newMenuColor;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00097668 File Offset: 0x00095868
		private void ResetMenuItems()
		{
			Menu_Color_Changer[] array = Object.FindObjectsOfType<Menu_Color_Changer>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].StopUsing(null);
			}
		}

		// Token: 0x04001771 RID: 6001
		public Color newMenuColor = Color.black;
	}
}
