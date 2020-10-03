using System;
using UnityEngine;
using Viveport;
using Viveport.Core;

// Token: 0x020001FC RID: 508
public class ViveportDemo_DLC : MonoBehaviour
{
	// Token: 0x06000E3B RID: 3643 RVA: 0x0005BA13 File Offset: 0x00059C13
	private void Start()
	{
		Api.Init(new StatusCallback(ViveportDemo_DLC.InitStatusHandler), ViveportDemo_DLC.APP_ID);
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0005BA2C File Offset: 0x00059C2C
	private void OnGUI()
	{
		GUIStyle style = new GUIStyle("button");
		if (!ViveportDemo_DLC.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.gray;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Init", style) && !ViveportDemo_DLC.bInit)
		{
			Api.Init(new StatusCallback(ViveportDemo_DLC.InitStatusHandler), ViveportDemo_DLC.APP_ID);
		}
		if (ViveportDemo_DLC.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Shutdown", style) && ViveportDemo_DLC.bInit)
		{
			Api.Shutdown(new StatusCallback(ViveportDemo_DLC.ShutdownHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "IsDLCReady", style) && ViveportDemo_DLC.bInit)
		{
			DLC.IsDlcReady(new StatusCallback(ViveportDemo_DLC.IsDLCReadyHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "GetDLCCount", style) && ViveportDemo_DLC.bInit && ViveportDemo_DLC.bIsReady)
		{
			ViveportDemo_DLC.dlcCount = DLC.GetCount();
			Viveport.Core.Logger.Log("DLC count: " + ViveportDemo_DLC.dlcCount);
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 4 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "GetIsAvailable", style) && ViveportDemo_DLC.bInit && ViveportDemo_DLC.bIsReady && DLC.GetIsAvailable(this.dlcIndex, out ViveportDemo_DLC.dlcAppId, out ViveportDemo_DLC.bIsDLCAvailable))
		{
			Viveport.Core.Logger.Log("Is DLC available: " + ViveportDemo_DLC.bIsDLCAvailable.ToString());
			Viveport.Core.Logger.Log("DLC APP ID: " + ViveportDemo_DLC.dlcAppId);
		}
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0005BC6A File Offset: 0x00059E6A
	private static void InitStatusHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo_DLC.bInit = true;
			ViveportDemo_DLC.bIsReady = false;
			Viveport.Core.Logger.Log("InitStatusHandler is successful");
			return;
		}
		ViveportDemo_DLC.bInit = false;
		Viveport.Core.Logger.Log("InitStatusHandler error : " + nResult);
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0005BCA1 File Offset: 0x00059EA1
	private static void ShutdownHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo_DLC.bInit = false;
			ViveportDemo_DLC.bIsReady = false;
			Viveport.Core.Logger.Log("ShutdownHandler is successful");
			return;
		}
		Viveport.Core.Logger.Log("ShutdownHandler error: " + nResult);
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0005BCD2 File Offset: 0x00059ED2
	private static void IsDLCReadyHandler(int nResult)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("DLC is ready");
			ViveportDemo_DLC.bIsReady = true;
			return;
		}
		Viveport.Core.Logger.Log("IsDLCReadyHandler error: " + nResult);
	}

	// Token: 0x04000E9C RID: 3740
	private int nWidth = 140;

	// Token: 0x04000E9D RID: 3741
	private int nHeight = 40;

	// Token: 0x04000E9E RID: 3742
	private static bool bIsReady = false;

	// Token: 0x04000E9F RID: 3743
	private static bool bIsDLCAvailable = false;

	// Token: 0x04000EA0 RID: 3744
	private static int dlcCount = -1;

	// Token: 0x04000EA1 RID: 3745
	private static string dlcAppId = "";

	// Token: 0x04000EA2 RID: 3746
	private int dlcIndex;

	// Token: 0x04000EA3 RID: 3747
	private int nXStart = 10;

	// Token: 0x04000EA4 RID: 3748
	private int nYStart = 35;

	// Token: 0x04000EA5 RID: 3749
	private static string APP_ID = "76d0898e-8772-49a9-aa55-1ec251a21686";

	// Token: 0x04000EA6 RID: 3750
	private static bool bInit = true;
}
