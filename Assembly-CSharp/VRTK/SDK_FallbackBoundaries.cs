using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200026E RID: 622
	[SDK_Description(typeof(SDK_FallbackSystem), 0)]
	public class SDK_FallbackBoundaries : SDK_BaseBoundaries
	{
		// Token: 0x06001284 RID: 4740 RVA: 0x00003F60 File Offset: 0x00002160
		public override void InitBoundaries()
		{
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0006949E File Offset: 0x0006769E
		public override Transform GetPlayArea()
		{
			return null;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0006949E File Offset: 0x0006769E
		public override Vector3[] GetPlayAreaVertices()
		{
			return null;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000694A1 File Offset: 0x000676A1
		public override float GetPlayAreaBorderThickness()
		{
			return 0f;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsPlayAreaSizeCalibrated()
		{
			return false;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool GetDrawAtRuntime()
		{
			return false;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00003F60 File Offset: 0x00002160
		public override void SetDrawAtRuntime(bool value)
		{
		}
	}
}
