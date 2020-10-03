using System;
using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x0200048F RID: 1167
	public class CharacterInstantiation : OnJoinedInstantiate
	{
		// Token: 0x140000CC RID: 204
		// (add) Token: 0x0600245E RID: 9310 RVA: 0x000B1BC0 File Offset: 0x000AFDC0
		// (remove) Token: 0x0600245F RID: 9311 RVA: 0x000B1BF4 File Offset: 0x000AFDF4
		public static event CharacterInstantiation.OnCharacterInstantiated CharacterInstantiated;

		// Token: 0x06002460 RID: 9312 RVA: 0x000B1C28 File Offset: 0x000AFE28
		public new void OnJoinedRoom()
		{
			if (this.PrefabsToInstantiate != null)
			{
				GameObject gameObject = this.PrefabsToInstantiate[(PhotonNetwork.player.ID - 1) % 4];
				Vector3 vector = Vector3.zero;
				if (this.SpawnPosition != null)
				{
					vector = this.SpawnPosition.position;
				}
				Vector3 b = Random.insideUnitSphere;
				b = this.PositionOffset * b.normalized;
				vector += b;
				vector.y = 0f;
				Camera.main.transform.position += vector;
				gameObject = PhotonNetwork.Instantiate(gameObject.name, vector, Quaternion.identity, 0);
				if (CharacterInstantiation.CharacterInstantiated != null)
				{
					CharacterInstantiation.CharacterInstantiated(gameObject);
				}
			}
		}

		// Token: 0x0200079A RID: 1946
		// (Invoke) Token: 0x06003034 RID: 12340
		public delegate void OnCharacterInstantiated(GameObject character);
	}
}
