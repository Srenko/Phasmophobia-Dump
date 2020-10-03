using System;
using UnityEngine;

namespace VRTK.Examples.PanelMenu
{
	// Token: 0x02000378 RID: 888
	public class PanelMenuDemoFlyingSaucer : MonoBehaviour
	{
		// Token: 0x06001E99 RID: 7833 RVA: 0x0009B8F8 File Offset: 0x00099AF8
		public void UpdateGridLayoutValue(int selectedIndex)
		{
			base.transform.GetChild(1).GetComponent<MeshRenderer>().materials[0].color = this.colors[selectedIndex];
		}

		// Token: 0x040017E0 RID: 6112
		private readonly Color[] colors = new Color[]
		{
			Color.black,
			Color.blue,
			Color.cyan,
			Color.gray,
			Color.green,
			Color.magenta,
			Color.red,
			Color.white
		};
	}
}
