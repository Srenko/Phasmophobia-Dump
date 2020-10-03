using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Photon Networking/Photon Rigidbody View")]
public class PhotonRigidbodyView : MonoBehaviour, IPunObservable
{
	// Token: 0x0600057A RID: 1402 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
	private void Awake()
	{
		this.m_Body = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
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
				this.m_Body.velocity = (Vector3)stream.ReceiveNext();
			}
			if (this.m_SynchronizeAngularVelocity)
			{
				this.m_Body.angularVelocity = (Vector3)stream.ReceiveNext();
			}
		}
	}

	// Token: 0x0400058C RID: 1420
	[SerializeField]
	private bool m_SynchronizeVelocity = true;

	// Token: 0x0400058D RID: 1421
	[SerializeField]
	private bool m_SynchronizeAngularVelocity = true;

	// Token: 0x0400058E RID: 1422
	private Rigidbody m_Body;
}
