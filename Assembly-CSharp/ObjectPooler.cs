using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class ObjectPooler : Photon.MonoBehaviour
{
	// Token: 0x060007BD RID: 1981 RVA: 0x0002DD54 File Offset: 0x0002BF54
	private void Awake()
	{
		ObjectPooler.instance = this;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0002DD5C File Offset: 0x0002BF5C
	private void Start()
	{
		if (PhotonNetwork.inRoom || MainManager.instance != null)
		{
			this.SetupPools();
		}
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0002DD78 File Offset: 0x0002BF78
	private void OnJoinedRoom()
	{
		this.SetupPools();
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0002DD80 File Offset: 0x0002BF80
	private void SetupPools()
	{
		this.poolDictionary = new Dictionary<string, Queue<GameObject>>();
		foreach (ObjectPooler.Pool pool in this.pools)
		{
			Queue<GameObject> queue = new Queue<GameObject>();
			for (int i = 0; i < pool.size; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(pool.prefab, Vector3.zero, Quaternion.identity);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(base.transform);
				queue.Enqueue(gameObject);
			}
			this.poolDictionary.Add(pool.tag, queue);
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0002DE3C File Offset: 0x0002C03C
	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if (this.poolDictionary == null)
		{
			return null;
		}
		if (!this.poolDictionary.ContainsKey(tag))
		{
			Debug.LogError("Pool with tag: " + tag + " does not exist!");
			return null;
		}
		GameObject gameObject = this.poolDictionary[tag].Dequeue();
		gameObject.SetActive(true);
		gameObject.transform.position = position;
		gameObject.transform.rotation = rotation;
		this.poolDictionary[tag].Enqueue(gameObject);
		return gameObject;
	}

	// Token: 0x04000787 RID: 1927
	public static ObjectPooler instance;

	// Token: 0x04000788 RID: 1928
	public List<ObjectPooler.Pool> pools = new List<ObjectPooler.Pool>();

	// Token: 0x04000789 RID: 1929
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	// Token: 0x02000518 RID: 1304
	[Serializable]
	public class Pool
	{
		// Token: 0x04002483 RID: 9347
		public string tag;

		// Token: 0x04002484 RID: 9348
		public GameObject prefab;

		// Token: 0x04002485 RID: 9349
		public int size;
	}
}
