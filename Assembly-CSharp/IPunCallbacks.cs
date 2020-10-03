using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

// Token: 0x020000A5 RID: 165
public interface IPunCallbacks
{
	// Token: 0x060003C1 RID: 961
	void OnConnectedToPhoton();

	// Token: 0x060003C2 RID: 962
	void OnLeftRoom();

	// Token: 0x060003C3 RID: 963
	void OnMasterClientSwitched(PhotonPlayer newMasterClient);

	// Token: 0x060003C4 RID: 964
	void OnPhotonCreateRoomFailed(object[] codeAndMsg);

	// Token: 0x060003C5 RID: 965
	void OnPhotonJoinRoomFailed(object[] codeAndMsg);

	// Token: 0x060003C6 RID: 966
	void OnCreatedRoom();

	// Token: 0x060003C7 RID: 967
	void OnJoinedLobby();

	// Token: 0x060003C8 RID: 968
	void OnLeftLobby();

	// Token: 0x060003C9 RID: 969
	void OnFailedToConnectToPhoton(DisconnectCause cause);

	// Token: 0x060003CA RID: 970
	void OnConnectionFail(DisconnectCause cause);

	// Token: 0x060003CB RID: 971
	void OnDisconnectedFromPhoton();

	// Token: 0x060003CC RID: 972
	void OnPhotonInstantiate(PhotonMessageInfo info);

	// Token: 0x060003CD RID: 973
	void OnReceivedRoomListUpdate();

	// Token: 0x060003CE RID: 974
	void OnJoinedRoom();

	// Token: 0x060003CF RID: 975
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer);

	// Token: 0x060003D0 RID: 976
	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer);

	// Token: 0x060003D1 RID: 977
	void OnPhotonRandomJoinFailed(object[] codeAndMsg);

	// Token: 0x060003D2 RID: 978
	void OnConnectedToMaster();

	// Token: 0x060003D3 RID: 979
	void OnPhotonMaxCccuReached();

	// Token: 0x060003D4 RID: 980
	void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged);

	// Token: 0x060003D5 RID: 981
	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps);

	// Token: 0x060003D6 RID: 982
	void OnUpdatedFriendList();

	// Token: 0x060003D7 RID: 983
	void OnCustomAuthenticationFailed(string debugMessage);

	// Token: 0x060003D8 RID: 984
	void OnCustomAuthenticationResponse(Dictionary<string, object> data);

	// Token: 0x060003D9 RID: 985
	void OnWebRpcResponse(OperationResponse response);

	// Token: 0x060003DA RID: 986
	void OnOwnershipRequest(object[] viewAndPlayer);

	// Token: 0x060003DB RID: 987
	void OnLobbyStatisticsUpdate();

	// Token: 0x060003DC RID: 988
	void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer);

	// Token: 0x060003DD RID: 989
	void OnOwnershipTransfered(object[] viewAndPlayers);
}
