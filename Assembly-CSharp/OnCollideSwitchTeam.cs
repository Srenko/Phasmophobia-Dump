using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
[RequireComponent(typeof(Collider))]
public class OnCollideSwitchTeam : MonoBehaviour
{
	// Token: 0x060001E5 RID: 485 RVA: 0x0000D254 File Offset: 0x0000B454
	public void OnTriggerEnter(Collider other)
	{
		PhotonView component = other.GetComponent<PhotonView>();
		if (component != null && component.isMine)
		{
			PhotonNetwork.player.SetTeam(this.TeamToSwitchTo);
		}
	}

	// Token: 0x04000200 RID: 512
	public PunTeams.Team TeamToSwitchTo;
}
