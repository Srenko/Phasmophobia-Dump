using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000411 RID: 1041
	[RequireComponent(typeof(CapsuleCollider))]
	public class BodyCollider : MonoBehaviour
	{
		// Token: 0x06002029 RID: 8233 RVA: 0x0009E850 File Offset: 0x0009CA50
		private void Awake()
		{
			this.capsuleCollider = base.GetComponent<CapsuleCollider>();
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0009E860 File Offset: 0x0009CA60
		private void FixedUpdate()
		{
			float num = Vector3.Dot(this.head.localPosition, Vector3.up);
			this.capsuleCollider.height = Mathf.Max(this.capsuleCollider.radius, num);
			base.transform.localPosition = this.head.localPosition - 0.5f * num * Vector3.up;
		}

		// Token: 0x04001DAB RID: 7595
		public Transform head;

		// Token: 0x04001DAC RID: 7596
		private CapsuleCollider capsuleCollider;
	}
}
