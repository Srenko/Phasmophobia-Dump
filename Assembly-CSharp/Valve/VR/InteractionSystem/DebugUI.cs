using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000416 RID: 1046
	public class DebugUI : MonoBehaviour
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0009FB2C File Offset: 0x0009DD2C
		public static DebugUI instance
		{
			get
			{
				if (DebugUI._instance == null)
				{
					DebugUI._instance = Object.FindObjectOfType<DebugUI>();
				}
				return DebugUI._instance;
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0009FB4A File Offset: 0x0009DD4A
		private void Start()
		{
			this.player = Player.instance;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0009FB57 File Offset: 0x0009DD57
		private void OnGUI()
		{
			this.player.Draw2DDebug();
		}

		// Token: 0x04001DE3 RID: 7651
		private Player player;

		// Token: 0x04001DE4 RID: 7652
		private static DebugUI _instance;
	}
}
