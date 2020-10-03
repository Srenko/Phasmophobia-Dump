using System;

// Token: 0x0200007D RID: 125
public enum PhotonNetworkingMessage
{
	// Token: 0x04000304 RID: 772
	OnConnectedToPhoton,
	// Token: 0x04000305 RID: 773
	OnLeftRoom,
	// Token: 0x04000306 RID: 774
	OnMasterClientSwitched,
	// Token: 0x04000307 RID: 775
	OnPhotonCreateRoomFailed,
	// Token: 0x04000308 RID: 776
	OnPhotonJoinRoomFailed,
	// Token: 0x04000309 RID: 777
	OnCreatedRoom,
	// Token: 0x0400030A RID: 778
	OnJoinedLobby,
	// Token: 0x0400030B RID: 779
	OnLeftLobby,
	// Token: 0x0400030C RID: 780
	OnDisconnectedFromPhoton,
	// Token: 0x0400030D RID: 781
	OnConnectionFail,
	// Token: 0x0400030E RID: 782
	OnFailedToConnectToPhoton,
	// Token: 0x0400030F RID: 783
	OnReceivedRoomListUpdate,
	// Token: 0x04000310 RID: 784
	OnJoinedRoom,
	// Token: 0x04000311 RID: 785
	OnPhotonPlayerConnected,
	// Token: 0x04000312 RID: 786
	OnPhotonPlayerDisconnected,
	// Token: 0x04000313 RID: 787
	OnPhotonRandomJoinFailed,
	// Token: 0x04000314 RID: 788
	OnConnectedToMaster,
	// Token: 0x04000315 RID: 789
	OnPhotonSerializeView,
	// Token: 0x04000316 RID: 790
	OnPhotonInstantiate,
	// Token: 0x04000317 RID: 791
	OnPhotonMaxCccuReached,
	// Token: 0x04000318 RID: 792
	OnPhotonCustomRoomPropertiesChanged,
	// Token: 0x04000319 RID: 793
	OnPhotonPlayerPropertiesChanged,
	// Token: 0x0400031A RID: 794
	OnUpdatedFriendList,
	// Token: 0x0400031B RID: 795
	OnCustomAuthenticationFailed,
	// Token: 0x0400031C RID: 796
	OnCustomAuthenticationResponse,
	// Token: 0x0400031D RID: 797
	OnWebRpcResponse,
	// Token: 0x0400031E RID: 798
	OnOwnershipRequest,
	// Token: 0x0400031F RID: 799
	OnLobbyStatisticsUpdate,
	// Token: 0x04000320 RID: 800
	OnPhotonPlayerActivityChanged,
	// Token: 0x04000321 RID: 801
	OnOwnershipTransfered
}
