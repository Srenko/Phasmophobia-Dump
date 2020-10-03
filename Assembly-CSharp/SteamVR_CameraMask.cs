using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
[ExecuteInEditMode]
public class SteamVR_CameraMask : MonoBehaviour
{
	// Token: 0x06000D4E RID: 3406 RVA: 0x00053878 File Offset: 0x00051A78
	private void Awake()
	{
		Debug.Log("SteamVR_CameraMask is deprecated in Unity 5.4 - REMOVING");
		Object.DestroyImmediate(this);
	}
}
