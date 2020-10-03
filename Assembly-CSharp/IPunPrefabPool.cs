using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public interface IPunPrefabPool
{
	// Token: 0x060003DE RID: 990
	GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation);

	// Token: 0x060003DF RID: 991
	void Destroy(GameObject gameObject);
}
