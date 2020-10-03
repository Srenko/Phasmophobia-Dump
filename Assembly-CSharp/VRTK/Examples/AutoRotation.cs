using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200034C RID: 844
	public class AutoRotation : MonoBehaviour
	{
		// Token: 0x06001D79 RID: 7545 RVA: 0x00096901 File Offset: 0x00094B01
		private void Start()
		{
			this.rotAxis.Normalize();
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0009690E File Offset: 0x00094B0E
		private void Update()
		{
			base.transform.Rotate(this.rotAxis, this.degPerSec * Time.deltaTime);
		}

		// Token: 0x04001742 RID: 5954
		[Tooltip("Angular velocity in degrees per seconds")]
		public float degPerSec = 60f;

		// Token: 0x04001743 RID: 5955
		[Tooltip("Rotation axis")]
		public Vector3 rotAxis = Vector3.up;
	}
}
