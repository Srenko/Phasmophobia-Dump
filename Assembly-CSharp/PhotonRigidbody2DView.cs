using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Photon Networking/Photon Rigidbody 2D View")]
public class PhotonRigidbody2DView : MonoBehaviour, IPunObservable
{
	// Token: 0x06000577 RID: 1399 RVA: 0x0001F40E File Offset: 0x0001D60E
	private void Awake()
	{
		this.m_Body = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0001F41C File Offset: 0x0001D61C
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			if (this.m_SynchronizeVelocity)
			{
				stream.SendNext(this.m_Body.velocity);
			}
			if (this.m_SynchronizeAngularVelocity)
			{
				stream.SendNext(this.m_Body.angularVelocity);
				return;
			}
		}
		else
		{
			if (this.m_SynchronizeVelocity)
			{
				this.m_Body.velocity = (Vector2)stream.ReceiveNext();
			}
			if (this.m_SynchronizeAngularVelocity)
			{
				this.m_Body.angularVelocity = (float)stream.ReceiveNext();
			}
		}
	}

	// Token: 0x04000589 RID: 1417
	[SerializeField]
	private bool m_SynchronizeVelocity = true;

	// Token: 0x0400058A RID: 1418
	[SerializeField]
	private bool m_SynchronizeAngularVelocity = true;

	// Token: 0x0400058B RID: 1419
	private Rigidbody2D m_Body;
}
