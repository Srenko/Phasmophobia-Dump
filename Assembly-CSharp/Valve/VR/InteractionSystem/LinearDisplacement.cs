using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200042D RID: 1069
	public class LinearDisplacement : MonoBehaviour
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x000A1AF9 File Offset: 0x0009FCF9
		private void Start()
		{
			this.initialPosition = base.transform.localPosition;
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000A1B26 File Offset: 0x0009FD26
		private void Update()
		{
			if (this.linearMapping)
			{
				base.transform.localPosition = this.initialPosition + this.linearMapping.value * this.displacement;
			}
		}

		// Token: 0x04001E4D RID: 7757
		public Vector3 displacement;

		// Token: 0x04001E4E RID: 7758
		public LinearMapping linearMapping;

		// Token: 0x04001E4F RID: 7759
		private Vector3 initialPosition;
	}
}
