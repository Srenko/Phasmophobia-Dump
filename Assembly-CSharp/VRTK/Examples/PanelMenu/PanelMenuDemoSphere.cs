using System;
using UnityEngine;

namespace VRTK.Examples.PanelMenu
{
	// Token: 0x02000379 RID: 889
	public class PanelMenuDemoSphere : MonoBehaviour
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x0009B9A3 File Offset: 0x00099BA3
		public void UpdateSliderValue(float value)
		{
			base.GetComponent<MeshRenderer>().materials[0].color = this.colors[(int)(value - 1f)];
		}

		// Token: 0x040017E1 RID: 6113
		private readonly Color[] colors = new Color[]
		{
			Color.black,
			Color.blue,
			Color.cyan,
			Color.gray,
			Color.green,
			Color.magenta,
			Color.red,
			Color.white,
			Color.yellow,
			Color.black
		};
	}
}
