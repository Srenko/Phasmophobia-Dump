using System;
using UnityEngine;
using Viveport;
using Viveport.Core;

// Token: 0x02000200 RID: 512
public class ViveportDemo_Subscription : MonoBehaviour
{
	// Token: 0x06000E5A RID: 3674 RVA: 0x0005CB2F File Offset: 0x0005AD2F
	private void Start()
	{
		Api.Init(new StatusCallback(ViveportDemo_Subscription.InitStatusHandler), ViveportDemo_Subscription.APP_ID);
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0005CB48 File Offset: 0x0005AD48
	private void OnGUI()
	{
		GUIStyle style = new GUIStyle("button");
		if (!ViveportDemo_Subscription.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.gray;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Init", style) && !ViveportDemo_Subscription.bInit)
		{
			Api.Init(new StatusCallback(ViveportDemo_Subscription.InitStatusHandler), ViveportDemo_Subscription.APP_ID);
		}
		if (ViveportDemo_Subscription.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Shutdown", style) && ViveportDemo_Subscription.bInit)
		{
			Api.Shutdown(new StatusCallback(ViveportDemo_Subscription.ShutdownHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "IsReady", style) && ViveportDemo_Subscription.bInit)
		{
			Subscription.IsReady(new StatusCallback2(ViveportDemo_Subscription.IsReadyHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "GetUserStatus", style) && ViveportDemo_Subscription.bInit && ViveportDemo_Subscription.bIsReady)
		{
			SubscriptionStatus userStatus = Subscription.GetUserStatus();
			string text = userStatus.Platforms.Contains(SubscriptionStatus.Platform.Windows) ? "true" : "false";
			string text2 = userStatus.Platforms.Contains(SubscriptionStatus.Platform.Android) ? "true" : "false";
			string text3;
			switch (userStatus.Type)
			{
			case SubscriptionStatus.TransactionType.Unknown:
				text3 = "Unknown";
				break;
			case SubscriptionStatus.TransactionType.Paid:
				text3 = "Paid";
				break;
			case SubscriptionStatus.TransactionType.Redeem:
				text3 = "Redeem";
				break;
			case SubscriptionStatus.TransactionType.FreeTrial:
				text3 = "FreeTrial";
				break;
			default:
				text3 = "Unknown";
				break;
			}
			Viveport.Core.Logger.Log(string.Concat(new string[]
			{
				"User is a Windows subscriber: ",
				text,
				".  User is a Android subscriber: ",
				text2,
				".  transactionType :",
				text3
			}));
		}
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0005CD9A File Offset: 0x0005AF9A
	private static void InitStatusHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo_Subscription.bInit = true;
			ViveportDemo_Subscription.bIsReady = false;
			Viveport.Core.Logger.Log("InitStatusHandler is successful");
			return;
		}
		ViveportDemo_Subscription.bInit = false;
		Viveport.Core.Logger.Log("InitStatusHandler error : " + nResult);
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0005CDD1 File Offset: 0x0005AFD1
	private static void ShutdownHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo_Subscription.bInit = false;
			ViveportDemo_Subscription.bIsReady = false;
			Viveport.Core.Logger.Log("ShutdownHandler is successful");
			return;
		}
		Viveport.Core.Logger.Log("ShutdownHandler error: " + nResult);
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0005CE04 File Offset: 0x0005B004
	private static void IsReadyHandler(int nResult, string message)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("Subscription is ready");
			ViveportDemo_Subscription.bIsReady = true;
			return;
		}
		Viveport.Core.Logger.Log(string.Concat(new object[]
		{
			"Subscription IsReadyHandler error: ",
			nResult,
			" Message : ",
			message
		}));
	}

	// Token: 0x04000EBF RID: 3775
	private int nWidth = 140;

	// Token: 0x04000EC0 RID: 3776
	private int nHeight = 40;

	// Token: 0x04000EC1 RID: 3777
	private static bool bIsReady = false;

	// Token: 0x04000EC2 RID: 3778
	private int nXStart = 10;

	// Token: 0x04000EC3 RID: 3779
	private int nYStart = 35;

	// Token: 0x04000EC4 RID: 3780
	private static string APP_ID = "76d0898e-8772-49a9-aa55-1ec251a21686";

	// Token: 0x04000EC5 RID: 3781
	private static bool bInit = true;
}
