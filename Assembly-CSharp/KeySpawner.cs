using System;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class KeySpawner : MonoBehaviour
{
	// Token: 0x0600088F RID: 2191 RVA: 0x00033363 File Offset: 0x00031563
	private void Start()
	{
		if (this.type != Key.KeyType.main)
		{
			this.SpawnKey(this.type);
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00033379 File Offset: 0x00031579
	public void Spawn()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.SpawnKey(this.type);
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00033390 File Offset: 0x00031590
	private void SpawnKey(Key.KeyType keyType)
	{
		if (PhotonNetwork.inRoom)
		{
			if (PhotonNetwork.isMasterClient)
			{
				if (keyType == Key.KeyType.basement)
				{
					this.key = PhotonNetwork.InstantiateSceneObject("BasementKey", base.transform.position, Quaternion.identity, 0, null);
					return;
				}
				if (keyType == Key.KeyType.Car)
				{
					this.key = PhotonNetwork.InstantiateSceneObject("CarKey", base.transform.position, Quaternion.identity, 0, null);
					return;
				}
				if (keyType == Key.KeyType.garage)
				{
					this.key = PhotonNetwork.InstantiateSceneObject("GarageKey", base.transform.position, Quaternion.identity, 0, null);
					return;
				}
				if (keyType == Key.KeyType.main)
				{
					this.key = PhotonNetwork.InstantiateSceneObject("MainKey", base.transform.position, Quaternion.identity, 0, null);
					return;
				}
			}
		}
		else
		{
			if (keyType == Key.KeyType.basement)
			{
				Object.Instantiate(Resources.Load("BasementKey"), base.transform.position, Quaternion.identity);
				return;
			}
			if (keyType == Key.KeyType.Car)
			{
				Object.Instantiate(Resources.Load("CarKey"), base.transform.position, Quaternion.identity);
				return;
			}
			if (keyType == Key.KeyType.garage)
			{
				Object.Instantiate(Resources.Load("GarageKey"), base.transform.position, Quaternion.identity);
				return;
			}
			if (keyType == Key.KeyType.main)
			{
				Object.Instantiate(Resources.Load("MainKey"), base.transform.position, Quaternion.identity);
			}
		}
	}

	// Token: 0x040008B4 RID: 2228
	public Key.KeyType type;

	// Token: 0x040008B5 RID: 2229
	private GameObject key;
}
