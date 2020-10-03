using System;
using UnityEngine.SceneManagement;

// Token: 0x020000AA RID: 170
public class SceneManagerHelper
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00019E88 File Offset: 0x00018088
	public static string ActiveSceneName
	{
		get
		{
			return SceneManager.GetActiveScene().name;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00019EA4 File Offset: 0x000180A4
	public static int ActiveSceneBuildIndex
	{
		get
		{
			return SceneManager.GetActiveScene().buildIndex;
		}
	}
}
