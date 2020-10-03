using System;
using System.Collections;
using Steamworks;
using UnityEngine;
using Viveport;

// Token: 0x020001C1 RID: 449
public class StoreSDKManager : MonoBehaviour
{
	// Token: 0x06000C51 RID: 3153 RVA: 0x0004D41C File Offset: 0x0004B61C
	private void Awake()
	{
		this.storeBranchType = StoreSDKManager.StoreBranchType.normal;
		if (this.storeSDKType == StoreSDKManager.StoreSDKType.steam)
		{
			this.steamManager.enabled = true;
			this.viveportInitialiser.enabled = false;
			return;
		}
		if (this.storeSDKType == StoreSDKManager.StoreSDKType.viveport)
		{
			this.steamManager.enabled = false;
			this.viveportInitialiser.enabled = true;
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0004D472 File Offset: 0x0004B672
	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		if (this.storeSDKType == StoreSDKManager.StoreSDKType.steam)
		{
			if (SteamApps.BIsCybercafe())
			{
				this.storeBranchType = StoreSDKManager.StoreBranchType.youtube;
			}
			string a = "public";
			if (SteamApps.GetCurrentBetaName(out a, 20))
			{
				if (a == "beta")
				{
					this.storeBranchType = StoreSDKManager.StoreBranchType.beta;
				}
				else if (a == "youtube")
				{
					this.storeBranchType = StoreSDKManager.StoreBranchType.youtube;
				}
				else if (a == "developer")
				{
					this.storeBranchType = StoreSDKManager.StoreBranchType.developer;
				}
				else
				{
					this.storeBranchType = StoreSDKManager.StoreBranchType.normal;
				}
			}
			if (Application.isEditor)
			{
				this.storeBranchType = this.storeBranchEditorOverride;
			}
			if (SteamUser.GetSteamID().m_SteamID.ToString() == "76561198366613395")
			{
				PlayerPrefs.SetInt("isCrackedVersion", 1);
				PhotonNetwork.offlineMode = true;
				PlayerPrefs.SetString("ErrorMessage", "Please consider purchasing the game to help with server costs.");
				this.errorScreen.SetActive(true);
				this.mainManager.gameObject.SetActive(false);
			}
			else if (PlayerPrefs.HasKey("isCrackedVersion"))
			{
				PlayerPrefs.SetInt("isCrackedVersion", 0);
			}
		}
		PlayerPrefs.SetInt("isYoutuberVersion", (this.storeBranchType == StoreSDKManager.StoreBranchType.youtube || this.storeBranchType == StoreSDKManager.StoreBranchType.developer) ? 1 : 0);
		PlayerPrefs.SetInt("isDeveloperVersion", (this.storeBranchType == StoreSDKManager.StoreBranchType.developer) ? 1 : 0);
		this.storeManager.StartStore();
		this.mainManager.SetPlayerName();
		Debug.Log("Connecting to server version: " + this.serverVersion + this.storeBranchType.ToString());
		yield break;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0004D481 File Offset: 0x0004B681
	public void QueryArcadeLicense()
	{
		if (this.storeSDKType == StoreSDKManager.StoreSDKType.viveport)
		{
			Api.QueryRuntimeMode(new QueryRuntimeModeCallback(this.QueryRunTimeHandler));
		}
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0004D49D File Offset: 0x0004B69D
	private void QueryRunTimeHandler(int nResult, int nMode)
	{
		if (nResult == 0 && nMode == 2)
		{
			this.storeBranchType = StoreSDKManager.StoreBranchType.youtube;
		}
	}

	// Token: 0x04000CD9 RID: 3289
	public StoreSDKManager.StoreSDKType storeSDKType;

	// Token: 0x04000CDA RID: 3290
	[HideInInspector]
	public StoreSDKManager.StoreBranchType storeBranchType;

	// Token: 0x04000CDB RID: 3291
	public StoreSDKManager.StoreBranchType storeBranchEditorOverride;

	// Token: 0x04000CDC RID: 3292
	public string serverVersion;

	// Token: 0x04000CDD RID: 3293
	[SerializeField]
	private ViveportInitialiser viveportInitialiser;

	// Token: 0x04000CDE RID: 3294
	[SerializeField]
	private SteamManager steamManager;

	// Token: 0x04000CDF RID: 3295
	[SerializeField]
	private MainManager mainManager;

	// Token: 0x04000CE0 RID: 3296
	[SerializeField]
	private StoreManager storeManager;

	// Token: 0x04000CE1 RID: 3297
	[SerializeField]
	private GameObject errorScreen;

	// Token: 0x0200055D RID: 1373
	public enum StoreSDKType
	{
		// Token: 0x04002592 RID: 9618
		steam,
		// Token: 0x04002593 RID: 9619
		viveport
	}

	// Token: 0x0200055E RID: 1374
	public enum StoreBranchType
	{
		// Token: 0x04002595 RID: 9621
		normal,
		// Token: 0x04002596 RID: 9622
		beta,
		// Token: 0x04002597 RID: 9623
		youtube,
		// Token: 0x04002598 RID: 9624
		developer
	}
}
