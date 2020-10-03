using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000270 RID: 624
	[SDK_Description(typeof(SDK_FallbackSystem), 0)]
	public class SDK_FallbackHeadset : SDK_BaseHeadset
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessUpdate(Dictionary<string, object> options)
		{
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessFixedUpdate(Dictionary<string, object> options)
		{
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0006949E File Offset: 0x0006769E
		public override Transform GetHeadset()
		{
			return null;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0006949E File Offset: 0x0006769E
		public override Transform GetHeadsetCamera()
		{
			return null;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000694C0 File Offset: 0x000676C0
		public override Vector3 GetHeadsetVelocity()
		{
			return Vector3.zero;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000694C0 File Offset: 0x000676C0
		public override Vector3 GetHeadsetAngularVelocity()
		{
			return Vector3.zero;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00003F60 File Offset: 0x00002160
		public override void HeadsetFade(Color color, float duration, bool fadeOverlay = false)
		{
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool HasHeadsetFade(Transform obj)
		{
			return false;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00003F60 File Offset: 0x00002160
		public override void AddHeadsetFade(Transform camera)
		{
		}
	}
}
