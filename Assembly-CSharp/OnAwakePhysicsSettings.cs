using System;
using Photon;
using UnityEngine;

// Token: 0x02000040 RID: 64
[RequireComponent(typeof(PhotonView))]
public class OnAwakePhysicsSettings : Photon.MonoBehaviour
{
	// Token: 0x0600016D RID: 365 RVA: 0x0000A5E8 File Offset: 0x000087E8
	public void Awake()
	{
		if (!base.photonView.isMine)
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component != null)
			{
				component.isKinematic = true;
				return;
			}
			Rigidbody2D component2 = base.GetComponent<Rigidbody2D>();
			if (component2 != null)
			{
				component2.isKinematic = true;
			}
		}
	}
}
