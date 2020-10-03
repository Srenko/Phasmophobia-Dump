using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200042C RID: 1068
	public class LinearBlendshape : MonoBehaviour
	{
		// Token: 0x060020C4 RID: 8388 RVA: 0x000A1A70 File Offset: 0x0009FC70
		private void Awake()
		{
			if (this.skinnedMesh == null)
			{
				this.skinnedMesh = base.GetComponent<SkinnedMeshRenderer>();
			}
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000A1AA8 File Offset: 0x0009FCA8
		private void Update()
		{
			float value = this.linearMapping.value;
			if (value != this.lastValue)
			{
				float value2 = Util.RemapNumberClamped(value, 0f, 1f, 1f, 100f);
				this.skinnedMesh.SetBlendShapeWeight(0, value2);
			}
			this.lastValue = value;
		}

		// Token: 0x04001E4A RID: 7754
		public LinearMapping linearMapping;

		// Token: 0x04001E4B RID: 7755
		public SkinnedMeshRenderer skinnedMesh;

		// Token: 0x04001E4C RID: 7756
		private float lastValue;
	}
}
