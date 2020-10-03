using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
public static class GameObjectExtensions
{
	// Token: 0x060002D7 RID: 727 RVA: 0x000127D5 File Offset: 0x000109D5
	public static bool GetActive(this GameObject target)
	{
		return target.activeInHierarchy;
	}
}
