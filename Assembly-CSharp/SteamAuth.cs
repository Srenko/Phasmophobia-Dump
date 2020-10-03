using System;
using System.Text;
using Photon;
using Steamworks;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class SteamAuth : Photon.MonoBehaviour
{
	// Token: 0x06000C45 RID: 3141 RVA: 0x0004D188 File Offset: 0x0004B388
	private string GetSteamAuthTicket(out HAuthTicket hAuthTicket)
	{
		byte[] array = new byte[1024];
		uint num;
		hAuthTicket = SteamUser.GetAuthSessionTicket(array, array.Length, out num);
		Array.Resize<byte>(ref array, (int)num);
		StringBuilder stringBuilder = new StringBuilder();
		int num2 = 0;
		while ((long)num2 < (long)((ulong)num))
		{
			stringBuilder.AppendFormat("{0:x2}", array[num2]);
			num2++;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0004D1E8 File Offset: 0x0004B3E8
	public void ConnectViaSteamAuthenticator()
	{
		string steamAuthTicket = this.GetSteamAuthTicket(out this.hAuthTicket);
		PhotonNetwork.AuthValues = new AuthenticationValues();
		PhotonNetwork.AuthValues.UserId = SteamUser.GetSteamID().ToString();
		PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Steam;
		PhotonNetwork.AuthValues.AddAuthParameter("ticket", steamAuthTicket);
		PhotonNetwork.ConnectUsingSettings(this.storeSDKManager.serverVersion + this.storeSDKManager.storeBranchType.ToString());
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0004D270 File Offset: 0x0004B470
	private void OnApplicationQuit()
	{
		if (this.storeSDKManager.storeSDKType == StoreSDKManager.StoreSDKType.steam)
		{
			SteamUser.CancelAuthTicket(this.hAuthTicket);
		}
	}

	// Token: 0x04000CD3 RID: 3283
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x04000CD4 RID: 3284
	private HAuthTicket hAuthTicket;
}
