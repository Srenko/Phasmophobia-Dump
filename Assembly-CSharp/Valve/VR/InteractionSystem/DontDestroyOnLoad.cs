using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200041B RID: 1051
	public class DontDestroyOnLoad : MonoBehaviour
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x0009FC69 File Offset: 0x0009DE69
		private void Awake()
		{
			Object.DontDestroyOnLoad(this);
		}
	}
}
