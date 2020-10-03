using System;
using Photon;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class Evidence : Photon.MonoBehaviour
{
	// Token: 0x06000814 RID: 2068 RVA: 0x0003072A File Offset: 0x0002E92A
	private void Awake()
	{
		if (base.GetComponent<PhotonView>())
		{
			this.view = base.GetComponent<PhotonView>();
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00030745 File Offset: 0x0002E945
	private void OnEnable()
	{
		if (base.GetComponent<EMF>())
		{
			this.SetValuesLocal();
			return;
		}
		this.SetValuesNetworked();
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00030761 File Offset: 0x0002E961
	private void OnDisable()
	{
		if (EvidenceController.instance && EvidenceController.instance.evidenceInLevel.Contains(this))
		{
			EvidenceController.instance.evidenceInLevel.Remove(this);
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00030745 File Offset: 0x0002E945
	private void OnJoinedRoom()
	{
		if (base.GetComponent<EMF>())
		{
			this.SetValuesLocal();
			return;
		}
		this.SetValuesNetworked();
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00030794 File Offset: 0x0002E994
	private void SetValuesNetworked()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		switch (this.EvidenceType)
		{
		case Evidence.Type.emfSpot:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(30, 50),
				"Evidence_Interaction"
			});
			return;
		case Evidence.Type.ouijaBoard:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(50, 100),
				"Evidence_OuijaBoard"
			});
			return;
		case Evidence.Type.fingerprint:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(15, 60),
				"Evidence_Fingerprints"
			});
			return;
		case Evidence.Type.footstep:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(20, 40),
				"Evidence_Footstep"
			});
			return;
		case Evidence.Type.DNA:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(40, 80),
				"Evidence_DNA"
			});
			return;
		case Evidence.Type.ghost:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				100,
				"Evidence_Ghost"
			});
			return;
		case Evidence.Type.deadBody:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(10, 30),
				"Evidence_DeadBody"
			});
			return;
		case Evidence.Type.dirtyWater:
			this.view.RPC("SyncEvidenceAmount", PhotonTargets.All, new object[]
			{
				Random.Range(15, 30),
				"Evidence_DirtyWater"
			});
			return;
		default:
			Debug.LogError("Evidence: " + base.gameObject.name + " has no evidence type.");
			return;
		}
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0003097C File Offset: 0x0002EB7C
	private void SetValuesLocal()
	{
		switch (this.EvidenceType)
		{
		case Evidence.Type.emfSpot:
			this.SyncEvidenceAmount(Random.Range(20, 40), "Evidence_Interaction");
			return;
		case Evidence.Type.ouijaBoard:
			this.SyncEvidenceAmount(Random.Range(30, 60), "Evidence_OuijaBoard");
			return;
		case Evidence.Type.fingerprint:
			this.SyncEvidenceAmount(Random.Range(15, 50), "Evidence_Fingerprints");
			return;
		case Evidence.Type.footstep:
			this.SyncEvidenceAmount(Random.Range(20, 40), "Evidence_Footstep");
			return;
		case Evidence.Type.DNA:
			this.SyncEvidenceAmount(Random.Range(40, 70), "Evidence_DNA");
			return;
		case Evidence.Type.ghost:
			this.SyncEvidenceAmount(100, "Evidence_Ghost");
			return;
		case Evidence.Type.deadBody:
			this.SyncEvidenceAmount(Random.Range(10, 30), "Evidence_Dead Body");
			return;
		case Evidence.Type.dirtyWater:
			this.SyncEvidenceAmount(Random.Range(15, 30), "Evidence_DirtyWater");
			return;
		default:
			Debug.LogError("Evidence: " + base.gameObject.name + " has no evidence type.");
			return;
		}
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00030A7C File Offset: 0x0002EC7C
	[PunRPC]
	private void SyncEvidenceAmount(int amount, string name)
	{
		this.paranormalEvidenceAmount = amount;
		this.evidenceName = LocalisationSystem.GetLocalisedValue(name);
		if (EvidenceController.instance && !EvidenceController.instance.evidenceInLevel.Contains(this))
		{
			EvidenceController.instance.evidenceInLevel.Add(this);
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00030ACA File Offset: 0x0002ECCA
	public int GetEvidenceAmount()
	{
		this.hasAlreadyTakenPhoto = true;
		if (this.view)
		{
			this.view.RPC("SyncHasTakenPhoto", PhotonTargets.All, Array.Empty<object>());
		}
		return this.paranormalEvidenceAmount;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00030AFC File Offset: 0x0002ECFC
	[PunRPC]
	private void SyncHasTakenPhoto()
	{
		this.hasAlreadyTakenPhoto = true;
	}

	// Token: 0x0400082A RID: 2090
	private PhotonView view;

	// Token: 0x0400082B RID: 2091
	public bool showsGhostVictim;

	// Token: 0x0400082C RID: 2092
	[HideInInspector]
	public bool hasAlreadyTakenPhoto;

	// Token: 0x0400082D RID: 2093
	[HideInInspector]
	public int paranormalEvidenceAmount;

	// Token: 0x0400082E RID: 2094
	public Evidence.Type EvidenceType;

	// Token: 0x0400082F RID: 2095
	public string evidenceName;

	// Token: 0x0200051F RID: 1311
	public enum Type
	{
		// Token: 0x040024A1 RID: 9377
		emfSpot,
		// Token: 0x040024A2 RID: 9378
		ouijaBoard,
		// Token: 0x040024A3 RID: 9379
		fingerprint,
		// Token: 0x040024A4 RID: 9380
		footstep,
		// Token: 0x040024A5 RID: 9381
		DNA,
		// Token: 0x040024A6 RID: 9382
		ghost,
		// Token: 0x040024A7 RID: 9383
		deadBody,
		// Token: 0x040024A8 RID: 9384
		dirtyWater
	}
}
