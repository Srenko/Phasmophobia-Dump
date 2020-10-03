using System;
using UnityEngine;
using Viveport;
using Viveport.Core;

// Token: 0x020001FD RID: 509
public class ViveportDemo_Deeplink : MonoBehaviour
{
	// Token: 0x06000E42 RID: 3650 RVA: 0x0005BD56 File Offset: 0x00059F56
	private void Awake()
	{
		if (!Object.FindObjectOfType<MainThreadDispatcher>())
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<MainThreadDispatcher>();
			gameObject.name = "MainThreadDispatcher";
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x0005BD7A File Offset: 0x00059F7A
	private void OnGUI()
	{
		GUI.Label(new Rect(100f, 20f, 600f, 600f), " Show Message in Console log \n KeyDown \" Q \" Use GoToApp \n KeyDown \" W \" Use GoToApp with BranchName \n KeyDown \" E \" Use GoToStore \n KeyDown \" R \" Use GoToAppOrGoToStore \n KeyDown \" A \" Use GetAppLaunchData \n");
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0005BD9F File Offset: 0x00059F9F
	private void Start()
	{
		Api.Init(new StatusCallback(ViveportDemo_Deeplink.InitStatusHandler), ViveportDemo_Deeplink.VIVEPORT_ID);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0005BDB8 File Offset: 0x00059FB8
	private void OnDestroy()
	{
		Api.Shutdown(new StatusCallback(ViveportDemo_Deeplink.ShutdownHandler));
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0005BDCC File Offset: 0x00059FCC
	private static void InitStatusHandler(int nResult)
	{
		if (nResult == 0)
		{
			Debug.Log("VIVEPORT init pass");
			Deeplink.IsReady(new StatusCallback(ViveportDemo_Deeplink.IsReadyHandler));
			return;
		}
		Debug.Log("VIVEPORT init fail");
		Application.Quit();
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0005BDFC File Offset: 0x00059FFC
	private static void IsReadyHandler(int nResult)
	{
		if (nResult == 0)
		{
			Debug.Log("VIVEPORT Deeplink.IsReady pass");
			ViveportDemo_Deeplink.bInitComplete = true;
			return;
		}
		Debug.Log("VIVEPORT Deeplink.IsReady fail");
		Application.Quit();
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x0005BE21 File Offset: 0x0005A021
	private static void ShutdownHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo_Deeplink.bInitComplete = false;
			Viveport.Core.Logger.Log("ShutdownHandler is successful");
			return;
		}
		Viveport.Core.Logger.Log("ShutdownHandler error: " + nResult);
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0005BE4C File Offset: 0x0005A04C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && ViveportDemo_Deeplink.bInitComplete)
		{
			Deeplink.GoToApp(new StatusCallback2(ViveportDemo_Deeplink.GoToAppHandler), this.GoToApp_Viveport_ID, this.LaunchData);
		}
		if (Input.GetKeyDown(KeyCode.W) && ViveportDemo_Deeplink.bInitComplete)
		{
			Deeplink.GoToApp(new StatusCallback2(ViveportDemo_Deeplink.GoToAppHandler), this.GoToApp_Viveport_ID, this.LaunchData, this.LaunchBranchName);
		}
		if (Input.GetKeyDown(KeyCode.E) && ViveportDemo_Deeplink.bInitComplete)
		{
			Deeplink.GoToStore(new StatusCallback2(ViveportDemo_Deeplink.GoToStoreHandler), this.GoToStore_Viveport_ID);
		}
		if (Input.GetKeyDown(KeyCode.R) && ViveportDemo_Deeplink.bInitComplete)
		{
			Deeplink.GoToAppOrGoToStore(new StatusCallback2(ViveportDemo_Deeplink.GoToAppOrGoToStoreHandler), this.GoToStore_Viveport_ID, this.LaunchData);
		}
		if (Input.GetKeyDown(KeyCode.A) && ViveportDemo_Deeplink.bInitComplete)
		{
			Debug.Log(Deeplink.GetAppLaunchData());
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0005BF28 File Offset: 0x0005A128
	private static void GoToAppHandler(int errorCode, string message)
	{
		if (errorCode == 0)
		{
			MainThreadDispatcher.Instance().Enqueue(delegate()
			{
				Debug.Log("GoToApp is successful");
			});
			return;
		}
		MainThreadDispatcher.Instance().Enqueue(delegate()
		{
			Debug.Log(string.Concat(new object[]
			{
				"GoToApp errorCode : ",
				errorCode,
				" ErrorMessage : ",
				message
			}));
		});
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0005BF94 File Offset: 0x0005A194
	private static void GoToStoreHandler(int errorCode, string message)
	{
		if (errorCode == 0)
		{
			MainThreadDispatcher.Instance().Enqueue(delegate()
			{
				Debug.Log("GoToStore is successful");
			});
			return;
		}
		MainThreadDispatcher.Instance().Enqueue(delegate()
		{
			Debug.Log(string.Concat(new object[]
			{
				"GoToStore errorCode : ",
				errorCode,
				" ErrorMessage : ",
				message
			}));
		});
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x0005C000 File Offset: 0x0005A200
	private static void GoToAppOrGoToStoreHandler(int errorCode, string message)
	{
		if (errorCode == 0)
		{
			MainThreadDispatcher.Instance().Enqueue(delegate()
			{
				Debug.Log("GoToAppOrGoToStore is successful");
			});
			return;
		}
		MainThreadDispatcher.Instance().Enqueue(delegate()
		{
			Debug.Log(string.Concat(new object[]
			{
				"GoToAppOrGoToStore errorCode : ",
				errorCode,
				" ErrorMessage : ",
				message
			}));
		});
	}

	// Token: 0x04000EA7 RID: 3751
	private static string VIVEPORT_ID = "VIVEPORT ID of the content";

	// Token: 0x04000EA8 RID: 3752
	private static string VIVEPORT_KEY = "VIVEPORT Key of the content";

	// Token: 0x04000EA9 RID: 3753
	private string GoToApp_Viveport_ID = "VIVEPORT ID of target APP";

	// Token: 0x04000EAA RID: 3754
	private string GoToStore_Viveport_ID = "VIVEPORT ID of target APP";

	// Token: 0x04000EAB RID: 3755
	private string LaunchData = "Start_Content";

	// Token: 0x04000EAC RID: 3756
	private string LaunchBranchName = "PROD";

	// Token: 0x04000EAD RID: 3757
	private const int SUCCESS = 0;

	// Token: 0x04000EAE RID: 3758
	private static bool bInitComplete = false;
}
