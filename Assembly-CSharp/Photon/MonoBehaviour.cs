using System;
using UnityEngine;

namespace Photon
{
	// Token: 0x02000456 RID: 1110
	public class MonoBehaviour : MonoBehaviour
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000AA329 File Offset: 0x000A8529
		public PhotonView photonView
		{
			get
			{
				if (this.pvCache == null)
				{
					this.pvCache = PhotonView.Get(this);
				}
				return this.pvCache;
			}
		}

		// Token: 0x04001FEB RID: 8171
		private PhotonView pvCache;
	}
}
