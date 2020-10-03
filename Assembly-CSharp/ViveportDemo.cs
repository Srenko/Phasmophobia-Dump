using System;
using UnityEngine;
using Viveport;
using Viveport.Core;

// Token: 0x020001FA RID: 506
public class ViveportDemo : MonoBehaviour
{
	// Token: 0x06000E23 RID: 3619 RVA: 0x0005A2DB File Offset: 0x000584DB
	private static void Log(string msg)
	{
		ViveportDemo.msgBuffer = msg + "\n" + ViveportDemo.msgBuffer;
		Viveport.Core.Logger.Log(msg);
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0005A2F8 File Offset: 0x000584F8
	private void Start()
	{
		Api.Init(new StatusCallback(ViveportDemo.InitStatusHandler), ViveportDemo.APP_ID);
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x00003F60 File Offset: 0x00002160
	private void Update()
	{
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0005A314 File Offset: 0x00058514
	private void OnGUI()
	{
		GUIStyle style = new GUIStyle("button");
		GUIStyle guistyle = new GUIStyle("button");
		guistyle.fontSize = 10;
		if (!ViveportDemo.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Init", style) && !ViveportDemo.bInit)
		{
			Api.Init(new StatusCallback(ViveportDemo.InitStatusHandler), ViveportDemo.APP_ID);
		}
		if (ViveportDemo.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Shutdown", style) && ViveportDemo.bInit)
		{
			Api.Shutdown(new StatusCallback(ViveportDemo.ShutdownHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "Version", style) && ViveportDemo.bInit)
		{
			Viveport.Core.Logger.Log("Version: " + Api.Version());
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "QueryRunMode", style) && ViveportDemo.bInit)
		{
			Api.QueryRuntimeMode(new QueryRuntimeModeCallback(ViveportDemo.QueryRunTimeHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 4 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "StatIsReady", style) && ViveportDemo.bInit)
		{
			UserStats.IsReady(new StatusCallback(ViveportDemo.IsReadyHandler));
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 6 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "DRM", style))
		{
			if (ViveportDemo.bInit)
			{
				Api.GetLicense(new ViveportDemo.MyLicenseChecker(), ViveportDemo.APP_ID, ViveportDemo.APP_KEY);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 5 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "ArcadeIsReady", style) && ViveportDemo.bInit)
		{
			ArcadeLeaderboard.IsReady(new StatusCallback(ViveportDemo.IsArcadeLeaderboardReadyHandler));
		}
		if (ViveportDemo.bInit && ViveportDemo.bIsReady)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 7 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "UserProfileIsReady", guistyle))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				User.IsReady(new StatusCallback(ViveportDemo.UserProfileIsReadyHandler));
			}
			else
			{
				ViveportDemo.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 8 * (this.nWidth + 10)), (float)this.nYStart, (float)this.nWidth, (float)this.nHeight), "UserProfile", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady && ViveportDemo.bUserProfileIsReady)
			{
				ViveportDemo.Log("UserId: " + User.GetUserId());
				ViveportDemo.Log("userName: " + User.GetUserName());
				ViveportDemo.Log("userAvatarUrl: " + User.GetUserAvatarUrl());
			}
			else
			{
				ViveportDemo.Log("Please make sure init & isReady are successful.");
			}
		}
		this.stringToEdit = GUI.TextField(new Rect(10f, (float)(this.nWidth + 10), 120f, 20f), this.stringToEdit, 50);
		this.StatsCount = GUI.TextField(new Rect(130f, (float)(this.nWidth + 10), 220f, 20f), this.StatsCount, 50);
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "DownloadStat", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.DownloadStats(new StatusCallback(ViveportDemo.DownloadStatsHandler));
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)(this.nYStart + this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "UploadStat", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.UploadStats(new StatusCallback(ViveportDemo.UploadStatsHandler));
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)(this.nYStart + this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "GetStat", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				this.nResult = UserStats.GetStat(this.stringToEdit, this.nInitValue);
				Viveport.Core.Logger.Log(string.Concat(new object[]
				{
					"Get ",
					this.stringToEdit,
					" stat name as => ",
					this.nResult
				}));
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)(this.nYStart + this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "SetStat", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				Viveport.Core.Logger.Log("MaxStep is => " + int.Parse(this.StatsCount));
				this.nResult = int.Parse(this.StatsCount);
				UserStats.SetStat(this.stringToEdit, this.nResult);
				Viveport.Core.Logger.Log(string.Concat(new object[]
				{
					"Set",
					this.stringToEdit,
					" stat name as =>",
					this.nResult
				}));
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		this.achivToEdit = GUI.TextField(new Rect(10f, (float)(2 * this.nWidth + 15), 120f, 20f), this.achivToEdit, 50);
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 2 * this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "GetAchieve", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				bool achievement = UserStats.GetAchievement(this.achivToEdit);
				Viveport.Core.Logger.Log("Get achievement => " + this.achivToEdit + " , and value is => " + achievement.ToString());
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + this.nWidth + 10), (float)(this.nYStart + 2 * this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "SetAchieve", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.SetAchievement(this.achivToEdit);
				Viveport.Core.Logger.Log("Set achievement => " + this.achivToEdit);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)(this.nYStart + 2 * this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "ClearAchieve", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.ClearAchievement(this.achivToEdit);
				Viveport.Core.Logger.Log("Clear achievement => " + this.achivToEdit);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)(this.nYStart + 2 * this.nWidth + 10), (float)this.nWidth, (float)this.nHeight), "Achieve&Time", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				int achievementUnlockTime = UserStats.GetAchievementUnlockTime(this.achivToEdit);
				Viveport.Core.Logger.Log("The achievement's unlock time is =>" + achievementUnlockTime);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		this.leaderboardToEdit = GUI.TextField(new Rect(10f, (float)(3 * this.nWidth + 20), 160f, 20f), this.leaderboardToEdit, 150);
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "DL Around", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.DownloadLeaderboardScores(new StatusCallback(ViveportDemo.DownloadLeaderboardHandler), this.leaderboardToEdit, UserStats.LeaderBoardRequestType.GlobalDataAroundUser, UserStats.LeaderBoardTimeRange.AllTime, -5, 5);
				Viveport.Core.Logger.Log("DownloadLeaderboardScores");
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "DL not Around", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.DownloadLeaderboardScores(new StatusCallback(ViveportDemo.DownloadLeaderboardHandler), this.leaderboardToEdit, UserStats.LeaderBoardRequestType.GlobalData, UserStats.LeaderBoardTimeRange.AllTime, 0, 10);
				Viveport.Core.Logger.Log("DownloadLeaderboardScores");
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		this.leaderboardScore = GUI.TextField(new Rect(170f, (float)(3 * this.nWidth + 20), 160f, 20f), this.leaderboardScore, 50);
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Upload LB", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				UserStats.UploadLeaderboardScore(new StatusCallback(ViveportDemo.UploadLeaderboardScoreHandler), this.leaderboardToEdit, int.Parse(this.leaderboardScore));
				Viveport.Core.Logger.Log("UploadLeaderboardScore");
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get LB count", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				this.nResult = UserStats.GetLeaderboardScoreCount();
				Viveport.Core.Logger.Log("GetLeaderboardScoreCount=> " + this.nResult);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 4 * (this.nWidth + 10)), (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get LB Score", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				int leaderboardScoreCount = UserStats.GetLeaderboardScoreCount();
				Viveport.Core.Logger.Log("GetLeaderboardScoreCount => " + leaderboardScoreCount);
				for (int i = 0; i < leaderboardScoreCount; i++)
				{
					Leaderboard leaderboard = UserStats.GetLeaderboardScore(i);
					Viveport.Core.Logger.Log(string.Concat(new object[]
					{
						"UserName = ",
						leaderboard.UserName,
						", Score = ",
						leaderboard.Score,
						", Rank = ",
						leaderboard.Rank
					}));
				}
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 5 * (this.nWidth + 10)), (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get Sort Method", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				int leaderboardSortMethod = (int)UserStats.GetLeaderboardSortMethod();
				Viveport.Core.Logger.Log("GetLeaderboardSortMethod=> " + leaderboardSortMethod);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 6 * (this.nWidth + 10)), (float)(this.nYStart + 3 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get Disp Type", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bIsReady)
			{
				int leaderboardDisplayType = (int)UserStats.GetLeaderboardDisplayType();
				Viveport.Core.Logger.Log("GetLeaderboardDisplayType=> " + leaderboardDisplayType);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		this.leaderboardToEdit = GUI.TextField(new Rect(10f, (float)(4 * this.nWidth + 20), 160f, 20f), this.leaderboardToEdit, 150);
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 4 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "DL Arca LB", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
			{
				ArcadeLeaderboard.DownloadLeaderboardScores(new StatusCallback(ViveportDemo.DownloadLeaderboardHandler), this.leaderboardToEdit, ArcadeLeaderboard.LeaderboardTimeRange.AllTime, 10);
				Viveport.Core.Logger.Log("DownloadLeaderboardScores");
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		this.leaderboardUserName = GUI.TextField(new Rect(170f, (float)(4 * this.nWidth + 20), 160f, 20f), this.leaderboardUserName, 150);
		this.leaderboardScore = GUI.TextField(new Rect(330f, (float)(4 * this.nWidth + 20), 160f, 20f), this.leaderboardScore, 50);
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)(this.nYStart + 4 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "UL Arca LB", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
			{
				ArcadeLeaderboard.UploadLeaderboardScore(new StatusCallback(ViveportDemo.UploadLeaderboardScoreHandler), this.leaderboardToEdit, this.leaderboardUserName, int.Parse(this.leaderboardScore));
				Viveport.Core.Logger.Log("UploadLeaderboardScore");
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 2 * (this.nWidth + 10)), (float)(this.nYStart + 4 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get Arca Count", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
			{
				this.nResult = ArcadeLeaderboard.GetLeaderboardScoreCount();
				Viveport.Core.Logger.Log("GetLeaderboardScoreCount=> " + this.nResult);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & Arcade isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 3 * (this.nWidth + 10)), (float)(this.nYStart + 4 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get Arca Score", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
			{
				int leaderboardScoreCount2 = ArcadeLeaderboard.GetLeaderboardScoreCount();
				Viveport.Core.Logger.Log("GetLeaderboardScoreCount => " + leaderboardScoreCount2);
				for (int j = 0; j < leaderboardScoreCount2; j++)
				{
					Leaderboard leaderboard2 = ArcadeLeaderboard.GetLeaderboardScore(j);
					Viveport.Core.Logger.Log(string.Concat(new object[]
					{
						"UserName = ",
						leaderboard2.UserName,
						", Score = ",
						leaderboard2.Score,
						", Rank = ",
						leaderboard2.Rank
					}));
				}
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 4 * (this.nWidth + 10)), (float)(this.nYStart + 4 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get AC UScore", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
			{
				int leaderboardUserScore = ArcadeLeaderboard.GetLeaderboardUserScore();
				Viveport.Core.Logger.Log("GetLeaderboardUserScore=> " + leaderboardUserScore);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (GUI.Button(new Rect((float)(this.nXStart + 5 * (this.nWidth + 10)), (float)(this.nYStart + 4 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "Get AC URank", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bArcadeIsReady)
			{
				int leaderboardUserRank = ArcadeLeaderboard.GetLeaderboardUserRank();
				Viveport.Core.Logger.Log("GetLeaderboardUserRank=> " + leaderboardUserRank);
			}
			else
			{
				Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
			}
		}
		if (ViveportDemo.bInit)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)this.nXStart, (float)(this.nYStart + 5 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "TokenIsReady", style) && ViveportDemo.bInit)
		{
			Token.IsReady(new StatusCallback(ViveportDemo.IsTokenReadyHandler));
		}
		if (ViveportDemo.bInit && ViveportDemo.bTokenIsReady)
		{
			GUI.contentColor = Color.white;
		}
		else
		{
			GUI.contentColor = Color.grey;
		}
		if (GUI.Button(new Rect((float)(this.nXStart + (this.nWidth + 10)), (float)(this.nYStart + 5 * this.nWidth + 20), (float)this.nWidth, (float)this.nHeight), "SessionToken", style))
		{
			if (ViveportDemo.bInit && ViveportDemo.bTokenIsReady)
			{
				Token.GetSessionToken(new StatusCallback2(ViveportDemo.GetSessionTokenHandler));
				return;
			}
			Viveport.Core.Logger.Log("Please make sure init & tokenIsReady are successful.");
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0005B583 File Offset: 0x00059783
	private static void InitStatusHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo.bInit = true;
			ViveportDemo.bIsReady = false;
			ViveportDemo.bArcadeIsReady = false;
			Viveport.Core.Logger.Log("InitStatusHandler is successful");
			return;
		}
		ViveportDemo.bInit = false;
		Viveport.Core.Logger.Log("InitStatusHandler error : " + nResult);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0005B5C0 File Offset: 0x000597C0
	private static void IsReadyHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo.bIsReady = true;
			ViveportDemo.bArcadeIsReady = false;
			Viveport.Core.Logger.Log("IsReadyHandler is successful");
			return;
		}
		ViveportDemo.bIsReady = false;
		Viveport.Core.Logger.Log("IsReadyHandler error: " + nResult);
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0005B5F7 File Offset: 0x000597F7
	private static void IsTokenReadyHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo.bTokenIsReady = true;
			Viveport.Core.Logger.Log("IsTokenReadyHandler is successful");
			return;
		}
		ViveportDemo.bTokenIsReady = false;
		Viveport.Core.Logger.Log("IsTokenReadyHandler error: " + nResult);
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0005B628 File Offset: 0x00059828
	private static void UserProfileIsReadyHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo.bUserProfileIsReady = true;
			ViveportDemo.Log("UserProfileIsReadyHandler is successful");
			return;
		}
		ViveportDemo.bUserProfileIsReady = false;
		ViveportDemo.Log("UserProfileIsReadyHandler error: " + nResult);
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x0005B65C File Offset: 0x0005985C
	private static void GetSessionTokenHandler(int nResult, string message)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("GetSessionTokenHandler is successful, token:" + message);
			return;
		}
		if (message.Length != 0)
		{
			Viveport.Core.Logger.Log(string.Concat(new object[]
			{
				"GetSessionTokenHandler error: ",
				nResult,
				", message:",
				message
			}));
			return;
		}
		Viveport.Core.Logger.Log("GetSessionTokenHandler error: " + nResult);
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0005B6C8 File Offset: 0x000598C8
	private static void QueryRunTimeHandler(int nResult, int nMode)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log(string.Concat(new object[]
			{
				"QueryRunTimeHandler is successful",
				nResult,
				"Running mode is ",
				nMode
			}));
			return;
		}
		Viveport.Core.Logger.Log("QueryRunTimeHandler error: " + nResult);
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0005B720 File Offset: 0x00059920
	private static void IsArcadeLeaderboardReadyHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo.bArcadeIsReady = true;
			ViveportDemo.bIsReady = false;
			Viveport.Core.Logger.Log("IsArcadeLeaderboardReadyHandler is successful");
			return;
		}
		ViveportDemo.bArcadeIsReady = false;
		Viveport.Core.Logger.Log("IsArcadeLeaderboardReadyHandler error: " + nResult);
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0005B757 File Offset: 0x00059957
	private static void ShutdownHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportDemo.bInit = false;
			ViveportDemo.bIsReady = false;
			Viveport.Core.Logger.Log("ShutdownHandler is successful");
			return;
		}
		Viveport.Core.Logger.Log("ShutdownHandler error: " + nResult);
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0005B788 File Offset: 0x00059988
	private static void DownloadStatsHandler(int nResult)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("DownloadStatsHandler is successful ");
			return;
		}
		Viveport.Core.Logger.Log("DownloadStatsHandler error: " + nResult);
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0005B7AD File Offset: 0x000599AD
	private static void UploadStatsHandler(int nResult)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("UploadStatsHandler is successful");
			return;
		}
		Viveport.Core.Logger.Log("UploadStatsHandler error: " + nResult);
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0005B7D2 File Offset: 0x000599D2
	private static void DownloadLeaderboardHandler(int nResult)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("DownloadLeaderboardHandler is successful");
			return;
		}
		Viveport.Core.Logger.Log("DownloadLeaderboardHandler error: " + nResult);
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0005B7F7 File Offset: 0x000599F7
	private static void UploadLeaderboardScoreHandler(int nResult)
	{
		if (nResult == 0)
		{
			Viveport.Core.Logger.Log("UploadLeaderboardScoreHandler is successful.");
			return;
		}
		Viveport.Core.Logger.Log("UploadLeaderboardScoreHandler error : " + nResult);
	}

	// Token: 0x04000E82 RID: 3714
	private int nInitValue;

	// Token: 0x04000E83 RID: 3715
	private int nResult;

	// Token: 0x04000E84 RID: 3716
	private int nWidth = 110;

	// Token: 0x04000E85 RID: 3717
	private int nHeight = 40;

	// Token: 0x04000E86 RID: 3718
	private int nXStart = 10;

	// Token: 0x04000E87 RID: 3719
	private int nYStart = 35;

	// Token: 0x04000E88 RID: 3720
	private string stringToEdit = "ID_Stat1";

	// Token: 0x04000E89 RID: 3721
	private string StatsCount = "80";

	// Token: 0x04000E8A RID: 3722
	private string achivToEdit = "ID_Achievement1";

	// Token: 0x04000E8B RID: 3723
	private string leaderboardToEdit = "ID_Leaderboard1";

	// Token: 0x04000E8C RID: 3724
	private string leaderboardUserName = "Karl";

	// Token: 0x04000E8D RID: 3725
	private string leaderboardScore = "1000";

	// Token: 0x04000E8E RID: 3726
	private static bool bInit = true;

	// Token: 0x04000E8F RID: 3727
	private static bool bIsReady = false;

	// Token: 0x04000E90 RID: 3728
	private static bool bUserProfileIsReady = false;

	// Token: 0x04000E91 RID: 3729
	private static bool bArcadeIsReady = false;

	// Token: 0x04000E92 RID: 3730
	private static bool bTokenIsReady = false;

	// Token: 0x04000E93 RID: 3731
	private static string msgBuffer = "";

	// Token: 0x04000E94 RID: 3732
	private static string APP_ID = "bd67b286-aafc-449d-8896-bb7e9b351876";

	// Token: 0x04000E95 RID: 3733
	private static string APP_KEY = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDFypCg0OHfBC+VZLSWPbNSgDo9qg/yQORDwGy1rKIboMj3IXn4Zy6h6bgn8kiMY7VI0lPwIj9lijT3ZxkzuTsI5GsK//Y1bqeTol4OUFR+47gj+TUuekAS2WMtglKox+/7mO6CA1gV+jZrAKo6YSVmPd+oFsgisRcqEgNh5MIURQIDAQAB";

	// Token: 0x02000584 RID: 1412
	private class MyLicenseChecker : Api.LicenseChecker
	{
		// Token: 0x060028D8 RID: 10456 RVA: 0x000C52C4 File Offset: 0x000C34C4
		public override void OnSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired)
		{
			Viveport.Core.Logger.Log("[MyLicenseChecker] issueTime: " + issueTime);
			Viveport.Core.Logger.Log("[MyLicenseChecker] expirationTime: " + expirationTime);
			Viveport.Core.Logger.Log("[MyLicenseChecker] latestVersion: " + latestVersion);
			Viveport.Core.Logger.Log("[MyLicenseChecker] updateRequired: " + updateRequired.ToString());
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000C5326 File Offset: 0x000C3526
		public override void OnFailure(int errorCode, string errorMessage)
		{
			Viveport.Core.Logger.Log("[MyLicenseChecker] errorCode: " + errorCode);
			Viveport.Core.Logger.Log("[MyLicenseChecker] errorMessage: " + errorMessage);
		}
	}
}
