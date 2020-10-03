using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000279 RID: 633
	[SDK_Description(typeof(SDK_SimSystem), 0)]
	public class SDK_SimBoundaries : SDK_BaseBoundaries
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x00003F60 File Offset: 0x00002160
		public override void InitBoundaries()
		{
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0006A7A0 File Offset: 0x000689A0
		public override Transform GetPlayArea()
		{
			if (this.area == null)
			{
				GameObject gameObject = SDK_InputSimulator.FindInScene();
				if (gameObject)
				{
					this.area = gameObject.transform;
				}
			}
			return this.area;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0006A7DC File Offset: 0x000689DC
		public override Vector3[] GetPlayAreaVertices()
		{
			float num = 0.9f;
			float num2 = 1f;
			return new Vector3[]
			{
				new Vector3(num, 0f, -num),
				new Vector3(-num, 0f, -num),
				new Vector3(-num, 0f, num),
				new Vector3(num, 0f, num),
				new Vector3(num2, 0f, -num2),
				new Vector3(-num2, 0f, -num2),
				new Vector3(-num2, 0f, num2),
				new Vector3(num2, 0f, num2)
			};
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0006A89B File Offset: 0x00068A9B
		public override float GetPlayAreaBorderThickness()
		{
			return 0.1f;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000694B6 File Offset: 0x000676B6
		public override bool IsPlayAreaSizeCalibrated()
		{
			return true;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool GetDrawAtRuntime()
		{
			return false;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00003F60 File Offset: 0x00002160
		public override void SetDrawAtRuntime(bool value)
		{
		}

		// Token: 0x040010E6 RID: 4326
		private Transform area;
	}
}
