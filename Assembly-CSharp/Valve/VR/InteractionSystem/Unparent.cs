using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200043B RID: 1083
	public class Unparent : MonoBehaviour
	{
		// Token: 0x06002120 RID: 8480 RVA: 0x000A3638 File Offset: 0x000A1838
		private void Start()
		{
			this.oldParent = base.transform.parent;
			base.transform.parent = null;
			base.gameObject.name = this.oldParent.gameObject.name + "." + base.gameObject.name;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000A3692 File Offset: 0x000A1892
		private void Update()
		{
			if (this.oldParent == null)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000A36AD File Offset: 0x000A18AD
		public Transform GetOldParent()
		{
			return this.oldParent;
		}

		// Token: 0x04001EA8 RID: 7848
		private Transform oldParent;
	}
}
