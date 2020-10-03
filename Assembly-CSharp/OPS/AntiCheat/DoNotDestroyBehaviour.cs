using System;
using UnityEngine;

namespace OPS.AntiCheat
{
	// Token: 0x0200046A RID: 1130
	public class DoNotDestroyBehaviour : MonoBehaviour
	{
		// Token: 0x060022F7 RID: 8951 RVA: 0x000AC388 File Offset: 0x000AA588
		private void Awake()
		{
			if (DoNotDestroyBehaviour.Singleton != null)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			DoNotDestroyBehaviour.Singleton = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x04002087 RID: 8327
		private static DoNotDestroyBehaviour Singleton;
	}
}
