using System;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x020001C0 RID: 448
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0004D28A File Offset: 0x0004B48A
	private static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0004D2AE File Offset: 0x0004B4AE
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0004D2BA File Offset: 0x0004B4BA
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0004D2C4 File Offset: 0x0004B4C4
	private void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException arg)
		{
			Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + arg, this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		SteamManager.s_EverInialized = true;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0004D398 File Offset: 0x0004B598
	private void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0004D3E6 File Offset: 0x0004B5E6
	private void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0004D40A File Offset: 0x0004B60A
	private void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x04000CD5 RID: 3285
	private static SteamManager s_instance;

	// Token: 0x04000CD6 RID: 3286
	private static bool s_EverInialized;

	// Token: 0x04000CD7 RID: 3287
	private bool m_bInitialized;

	// Token: 0x04000CD8 RID: 3288
	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
