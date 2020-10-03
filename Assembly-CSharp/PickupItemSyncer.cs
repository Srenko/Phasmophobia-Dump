using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x020000D7 RID: 215
[RequireComponent(typeof(PhotonView))]
public class PickupItemSyncer : Photon.MonoBehaviour
{
	// Token: 0x060005F4 RID: 1524 RVA: 0x00021C2F File Offset: 0x0001FE2F
	public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.SendPickedUpItems(newPlayer);
		}
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00021C40 File Offset: 0x0001FE40
	public void OnJoinedRoom()
	{
		Debug.Log(string.Concat(new object[]
		{
			"Joined Room. isMasterClient: ",
			PhotonNetwork.isMasterClient.ToString(),
			" id: ",
			PhotonNetwork.player.ID
		}));
		this.IsWaitingForPickupInit = !PhotonNetwork.isMasterClient;
		if (PhotonNetwork.playerList.Length >= 2)
		{
			base.Invoke("AskForPickupItemSpawnTimes", 2f);
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00021CB8 File Offset: 0x0001FEB8
	public void AskForPickupItemSpawnTimes()
	{
		if (this.IsWaitingForPickupInit)
		{
			if (PhotonNetwork.playerList.Length < 2)
			{
				Debug.Log("Cant ask anyone else for PickupItem spawn times.");
				this.IsWaitingForPickupInit = false;
				return;
			}
			PhotonPlayer next = PhotonNetwork.masterClient.GetNext();
			if (next == null || next.Equals(PhotonNetwork.player))
			{
				next = PhotonNetwork.player.GetNext();
			}
			if (next != null && !next.Equals(PhotonNetwork.player))
			{
				base.photonView.RPC("RequestForPickupItems", next, Array.Empty<object>());
				return;
			}
			Debug.Log("No player left to ask");
			this.IsWaitingForPickupInit = false;
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00021D47 File Offset: 0x0001FF47
	[PunRPC]
	[Obsolete("Use RequestForPickupItems(PhotonMessageInfo msgInfo) with corrected typing instead.")]
	public void RequestForPickupTimes(PhotonMessageInfo msgInfo)
	{
		this.RequestForPickupItems(msgInfo);
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00021D50 File Offset: 0x0001FF50
	[PunRPC]
	public void RequestForPickupItems(PhotonMessageInfo msgInfo)
	{
		if (msgInfo.sender == null)
		{
			Debug.LogError("Unknown player asked for PickupItems");
			return;
		}
		this.SendPickedUpItems(msgInfo.sender);
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00021D74 File Offset: 0x0001FF74
	private void SendPickedUpItems(PhotonPlayer targetPlayer)
	{
		if (targetPlayer == null)
		{
			Debug.LogWarning("Cant send PickupItem spawn times to unknown targetPlayer.");
			return;
		}
		double time = PhotonNetwork.time;
		double num = time + 0.20000000298023224;
		PickupItem[] array = new PickupItem[PickupItem.DisabledPickupItems.Count];
		PickupItem.DisabledPickupItems.CopyTo(array);
		List<float> list = new List<float>(array.Length * 2);
		foreach (PickupItem pickupItem in array)
		{
			if (pickupItem.SecondsBeforeRespawn <= 0f)
			{
				list.Add((float)pickupItem.ViewID);
				list.Add(0f);
			}
			else
			{
				double num2 = pickupItem.TimeOfRespawn - PhotonNetwork.time;
				if (pickupItem.TimeOfRespawn > num)
				{
					Debug.Log(string.Concat(new object[]
					{
						pickupItem.ViewID,
						" respawn: ",
						pickupItem.TimeOfRespawn,
						" timeUntilRespawn: ",
						num2,
						" (now: ",
						PhotonNetwork.time,
						")"
					}));
					list.Add((float)pickupItem.ViewID);
					list.Add((float)num2);
				}
			}
		}
		Debug.Log(string.Concat(new object[]
		{
			"Sent count: ",
			list.Count,
			" now: ",
			time
		}));
		base.photonView.RPC("PickupItemInit", targetPlayer, new object[]
		{
			PhotonNetwork.time,
			list.ToArray()
		});
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00021F0C File Offset: 0x0002010C
	[PunRPC]
	public void PickupItemInit(double timeBase, float[] inactivePickupsAndTimes)
	{
		this.IsWaitingForPickupInit = false;
		for (int i = 0; i < inactivePickupsAndTimes.Length / 2; i++)
		{
			int num = i * 2;
			int viewID = (int)inactivePickupsAndTimes[num];
			float num2 = inactivePickupsAndTimes[num + 1];
			PhotonView photonView = PhotonView.Find(viewID);
			PickupItem component = photonView.GetComponent<PickupItem>();
			if (num2 <= 0f)
			{
				component.PickedUp(0f);
			}
			else
			{
				double num3 = (double)num2 + timeBase;
				Debug.Log(string.Concat(new object[]
				{
					photonView.viewID,
					" respawn: ",
					num3,
					" timeUntilRespawnBasedOnTimeBase:",
					num2,
					" SecondsBeforeRespawn: ",
					component.SecondsBeforeRespawn
				}));
				double num4 = num3 - PhotonNetwork.time;
				if (num2 <= 0f)
				{
					num4 = 0.0;
				}
				component.PickedUp((float)num4);
			}
		}
	}

	// Token: 0x04000607 RID: 1543
	public bool IsWaitingForPickupInit;

	// Token: 0x04000608 RID: 1544
	private const float TimeDeltaToIgnore = 0.2f;
}
