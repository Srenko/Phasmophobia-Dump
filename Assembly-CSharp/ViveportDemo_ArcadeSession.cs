using System;
using UnityEngine;
using Viveport;
using Viveport.Arcade;
using Viveport.Core;

// Token: 0x020001FB RID: 507
public class ViveportDemo_ArcadeSession : MonoBehaviour
{
	// Token: 0x06000E35 RID: 3637 RVA: 0x0005B8CF File Offset: 0x00059ACF
	private void Start()
	{
		Viveport.Core.Logger.Log("Version: " + Api.Version());
		this.mListener = new ViveportDemo_ArcadeSession.Result();
		Api.Init(new StatusCallback(this.InitStatusHandler), ViveportDemo_ArcadeSession.VIVEPORT_ARCADE_APP_TEST_ID);
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x00003F60 File Offset: 0x00002160
	private void Update()
	{
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0005B907 File Offset: 0x00059B07
	private void InitStatusHandler(int nResult)
	{
		Viveport.Core.Logger.Log("InitStatusHandler: " + nResult);
		if (nResult != 0)
		{
			Viveport.Core.Logger.Log("Platform setup error ...");
			return;
		}
		Viveport.Core.Logger.Log("Session IsReady");
		Session.IsReady(this.mListener);
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0005B944 File Offset: 0x00059B44
	private void OnGUI()
	{
		if (GUI.Button(new Rect((float)this.nXStart, (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Session Start"))
		{
			Viveport.Core.Logger.Log("Session Start");
			Session.Start(this.mListener);
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "Session Stop"))
		{
			Viveport.Core.Logger.Log("Session Stop");
			Session.Stop(this.mListener);
		}
	}

	// Token: 0x04000E96 RID: 3734
	private int nWidth = 120;

	// Token: 0x04000E97 RID: 3735
	private int nHeight = 40;

	// Token: 0x04000E98 RID: 3736
	private int nXStart = 10;

	// Token: 0x04000E99 RID: 3737
	private int nYStart = 35;

	// Token: 0x04000E9A RID: 3738
	private static string VIVEPORT_ARCADE_APP_TEST_ID = "app_test_id";

	// Token: 0x04000E9B RID: 3739
	private ViveportDemo_ArcadeSession.Result mListener;

	// Token: 0x02000585 RID: 1413
	private class Result : Session.SessionListener
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x000C5355 File Offset: 0x000C3555
		public override void OnSuccess(string pchAppID)
		{
			Viveport.Core.Logger.Log("[Session OnSuccess] pchAppID=" + pchAppID);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000C5367 File Offset: 0x000C3567
		public override void OnStartSuccess(string pchAppID, string pchGuid)
		{
			Viveport.Core.Logger.Log("[Session OnStartSuccess] pchAppID=" + pchAppID + ",pchGuid=" + pchGuid);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000C537F File Offset: 0x000C357F
		public override void OnStopSuccess(string pchAppID, string pchGuid)
		{
			Viveport.Core.Logger.Log("[Session OnStopSuccess] pchAppID=" + pchAppID + ",pchGuid=" + pchGuid);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000C5397 File Offset: 0x000C3597
		public override void OnFailure(int nCode, string pchMessage)
		{
			Viveport.Core.Logger.Log(string.Concat(new object[]
			{
				"[Session OnFailed] nCode=",
				nCode,
				",pchMessage=",
				pchMessage
			}));
			Time.timeScale = 0f;
		}
	}
}
