using System;
using UnityEngine;

// Token: 0x020001E0 RID: 480
[ExecuteInEditMode]
public class SteamVR_CameraFlip : MonoBehaviour
{
	// Token: 0x06000D4C RID: 3404 RVA: 0x00053866 File Offset: 0x00051A66
	private void Awake()
	{
		Debug.Log("SteamVR_CameraFlip is deprecated in Unity 5.4 - REMOVING");
		Object.DestroyImmediate(this);
	}
}
