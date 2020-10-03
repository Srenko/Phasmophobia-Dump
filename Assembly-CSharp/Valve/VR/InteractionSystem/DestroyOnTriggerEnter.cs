using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000419 RID: 1049
	public class DestroyOnTriggerEnter : MonoBehaviour
	{
		// Token: 0x0600205B RID: 8283 RVA: 0x0009FBA1 File Offset: 0x0009DDA1
		private void Start()
		{
			if (!string.IsNullOrEmpty(this.tagFilter))
			{
				this.useTag = true;
			}
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0009FBB8 File Offset: 0x0009DDB8
		private void OnTriggerEnter(Collider collider)
		{
			if (!this.useTag || (this.useTag && collider.gameObject.tag == this.tagFilter))
			{
				Object.Destroy(collider.gameObject.transform.root.gameObject);
			}
		}

		// Token: 0x04001DE6 RID: 7654
		public string tagFilter;

		// Token: 0x04001DE7 RID: 7655
		private bool useTag;
	}
}
