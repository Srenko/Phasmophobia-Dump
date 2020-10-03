using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
[ExecuteInEditMode]
public class SteamVR_GameView : MonoBehaviour
{
	// Token: 0x06000D8E RID: 3470 RVA: 0x0005559D File Offset: 0x0005379D
	private void Awake()
	{
		Debug.Log("SteamVR_GameView is deprecated in Unity 5.4 - REMOVING");
		Object.DestroyImmediate(this);
	}
}
