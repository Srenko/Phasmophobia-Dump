using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Photon
{
	// Token: 0x02000457 RID: 1111
	public class PunBehaviour : MonoBehaviour, IPunCallbacks
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnConnectedToPhoton()
		{
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnLeftRoom()
		{
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnMasterClientSwitched(PhotonPlayer newMasterClient)
		{
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonCreateRoomFailed(object[] codeAndMsg)
		{
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonJoinRoomFailed(object[] codeAndMsg)
		{
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnCreatedRoom()
		{
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnJoinedLobby()
		{
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnLeftLobby()
		{
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
		{
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnDisconnectedFromPhoton()
		{
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnConnectionFail(DisconnectCause cause)
		{
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonInstantiate(PhotonMessageInfo info)
		{
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnReceivedRoomListUpdate()
		{
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnJoinedRoom()
		{
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonRandomJoinFailed(object[] codeAndMsg)
		{
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnConnectedToMaster()
		{
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonMaxCccuReached()
		{
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
		{
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
		{
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnUpdatedFriendList()
		{
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnCustomAuthenticationFailed(string debugMessage)
		{
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnWebRpcResponse(OperationResponse response)
		{
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnOwnershipRequest(object[] viewAndPlayer)
		{
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnLobbyStatisticsUpdate()
		{
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
		{
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnOwnershipTransfered(object[] viewAndPlayers)
		{
		}
	}
}
