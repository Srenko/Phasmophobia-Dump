using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
[ExecuteInEditMode]
public class SteamVR_UpdatePoses : MonoBehaviour
{
	// Token: 0x06000E0D RID: 3597 RVA: 0x00059686 File Offset: 0x00057886
	private void Awake()
	{
		Debug.Log("SteamVR_UpdatePoses has been deprecated - REMOVING");
		Object.DestroyImmediate(this);
	}
}
