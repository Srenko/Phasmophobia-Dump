using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200044F RID: 1103
	public class ChaperoneInfo : MonoBehaviour
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000A7AA0 File Offset: 0x000A5CA0
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x000A7AA8 File Offset: 0x000A5CA8
		public bool initialized { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000A7AB1 File Offset: 0x000A5CB1
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x000A7AB9 File Offset: 0x000A5CB9
		public float playAreaSizeX { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000A7AC2 File Offset: 0x000A5CC2
		// (set) Token: 0x060021E8 RID: 8680 RVA: 0x000A7ACA File Offset: 0x000A5CCA
		public float playAreaSizeZ { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000A7AD3 File Offset: 0x000A5CD3
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x000A7ADB File Offset: 0x000A5CDB
		public bool roomscale { get; private set; }

		// Token: 0x060021EB RID: 8683 RVA: 0x000A7AE4 File Offset: 0x000A5CE4
		public static SteamVR_Events.Action InitializedAction(UnityAction action)
		{
			return new SteamVR_Events.ActionNoArgs(ChaperoneInfo.Initialized, action);
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000A7AF4 File Offset: 0x000A5CF4
		public static ChaperoneInfo instance
		{
			get
			{
				if (ChaperoneInfo._instance == null)
				{
					ChaperoneInfo._instance = new GameObject("[ChaperoneInfo]").AddComponent<ChaperoneInfo>();
					ChaperoneInfo._instance.initialized = false;
					ChaperoneInfo._instance.playAreaSizeX = 1f;
					ChaperoneInfo._instance.playAreaSizeZ = 1f;
					ChaperoneInfo._instance.roomscale = false;
					Object.DontDestroyOnLoad(ChaperoneInfo._instance.gameObject);
				}
				return ChaperoneInfo._instance;
			}
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000A7B6A File Offset: 0x000A5D6A
		private IEnumerator Start()
		{
			CVRChaperone chaperone = OpenVR.Chaperone;
			if (chaperone == null)
			{
				Debug.LogWarning("Failed to get IVRChaperone interface.");
				this.initialized = true;
				yield break;
			}
			float num;
			float num2;
			for (;;)
			{
				num = 0f;
				num2 = 0f;
				if (chaperone.GetPlayAreaSize(ref num, ref num2))
				{
					break;
				}
				yield return null;
			}
			this.initialized = true;
			this.playAreaSizeX = num;
			this.playAreaSizeZ = num2;
			this.roomscale = (Mathf.Max(num, num2) > 1.01f);
			Debug.LogFormat("ChaperoneInfo initialized. {2} play area {0:0.00}m x {1:0.00}m", new object[]
			{
				num,
				num2,
				this.roomscale ? "Roomscale" : "Standing"
			});
			ChaperoneInfo.Initialized.Send();
			yield break;
			yield break;
		}

		// Token: 0x04001F67 RID: 8039
		public static SteamVR_Events.Event Initialized = new SteamVR_Events.Event();

		// Token: 0x04001F68 RID: 8040
		private static ChaperoneInfo _instance;
	}
}
