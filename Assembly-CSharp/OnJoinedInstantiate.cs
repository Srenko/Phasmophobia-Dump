using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class OnJoinedInstantiate : MonoBehaviour
{
	// Token: 0x060005DF RID: 1503 RVA: 0x00021744 File Offset: 0x0001F944
	public void OnJoinedRoom()
	{
		if (this.PrefabsToInstantiate != null)
		{
			foreach (GameObject gameObject in this.PrefabsToInstantiate)
			{
				Debug.Log("Instantiating: " + gameObject.name);
				Vector3 a = Vector3.up;
				if (this.SpawnPosition != null)
				{
					a = this.SpawnPosition.position;
				}
				Vector3 a2 = Random.insideUnitSphere;
				a2.y = 0f;
				a2 = a2.normalized;
				Vector3 position = a + this.PositionOffset * a2;
				PhotonNetwork.Instantiate(gameObject.name, position, Quaternion.identity, 0);
			}
		}
	}

	// Token: 0x040005FA RID: 1530
	public Transform SpawnPosition;

	// Token: 0x040005FB RID: 1531
	public float PositionOffset = 2f;

	// Token: 0x040005FC RID: 1532
	public GameObject[] PrefabsToInstantiate;
}
