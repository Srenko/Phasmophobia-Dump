using System;
using AOT;
using Viveport.Internal;

namespace Viveport
{
	// Token: 0x0200020C RID: 524
	public class UserStats
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0005DB28 File Offset: 0x0005BD28
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void IsReadyIl2cppCallback(int errorCode)
		{
			UserStats.isReadyIl2cppCallback(errorCode);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0005DB38 File Offset: 0x0005BD38
		public static int IsReady(StatusCallback callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			UserStats.isReadyIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(UserStats.IsReadyIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				return UserStats.IsReady_64(new StatusCallback(UserStats.IsReadyIl2cppCallback));
			}
			return UserStats.IsReady(new StatusCallback(UserStats.IsReadyIl2cppCallback));
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0005DBA5 File Offset: 0x0005BDA5
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void DownloadStatsIl2cppCallback(int errorCode)
		{
			UserStats.downloadStatsIl2cppCallback(errorCode);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0005DBB4 File Offset: 0x0005BDB4
		public static int DownloadStats(StatusCallback callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			UserStats.downloadStatsIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(UserStats.DownloadStatsIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				return UserStats.DownloadStats_64(new StatusCallback(UserStats.DownloadStatsIl2cppCallback));
			}
			return UserStats.DownloadStats(new StatusCallback(UserStats.DownloadStatsIl2cppCallback));
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0005DC24 File Offset: 0x0005BE24
		public static int GetStat(string name, int defaultValue)
		{
			int result = defaultValue;
			if (IntPtr.Size == 8)
			{
				UserStats.GetStat_64(name, ref result);
			}
			else
			{
				UserStats.GetStat(name, ref result);
			}
			return result;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0005DC50 File Offset: 0x0005BE50
		public static float GetStat(string name, float defaultValue)
		{
			float result = defaultValue;
			if (IntPtr.Size == 8)
			{
				UserStats.GetStat_64(name, ref result);
			}
			else
			{
				UserStats.GetStat(name, ref result);
			}
			return result;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0005DC7C File Offset: 0x0005BE7C
		public static void SetStat(string name, int value)
		{
			if (IntPtr.Size == 8)
			{
				UserStats.SetStat_64(name, value);
				return;
			}
			UserStats.SetStat(name, value);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0005DC97 File Offset: 0x0005BE97
		public static void SetStat(string name, float value)
		{
			if (IntPtr.Size == 8)
			{
				UserStats.SetStat_64(name, value);
				return;
			}
			UserStats.SetStat(name, value);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0005DCB2 File Offset: 0x0005BEB2
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void UploadStatsIl2cppCallback(int errorCode)
		{
			UserStats.uploadStatsIl2cppCallback(errorCode);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0005DCC0 File Offset: 0x0005BEC0
		public static int UploadStats(StatusCallback callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			UserStats.uploadStatsIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(UserStats.UploadStatsIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				return UserStats.UploadStats_64(new StatusCallback(UserStats.UploadStatsIl2cppCallback));
			}
			return UserStats.UploadStats(new StatusCallback(UserStats.UploadStatsIl2cppCallback));
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0005DD30 File Offset: 0x0005BF30
		public static bool GetAchievement(string pchName)
		{
			int num = 0;
			if (IntPtr.Size == 8)
			{
				UserStats.GetAchievement_64(pchName, ref num);
			}
			else
			{
				UserStats.GetAchievement(pchName, ref num);
			}
			return num == 1;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005DD60 File Offset: 0x0005BF60
		public static int GetAchievementUnlockTime(string pchName)
		{
			int result = 0;
			if (IntPtr.Size == 8)
			{
				UserStats.GetAchievementUnlockTime_64(pchName, ref result);
			}
			else
			{
				UserStats.GetAchievementUnlockTime(pchName, ref result);
			}
			return result;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0005DD8C File Offset: 0x0005BF8C
		public static string GetAchievementIcon(string pchName)
		{
			return "";
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0005DD8C File Offset: 0x0005BF8C
		public static string GetAchievementDisplayAttribute(string pchName, UserStats.AchievementDisplayAttribute attr)
		{
			return "";
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0005DD8C File Offset: 0x0005BF8C
		public static string GetAchievementDisplayAttribute(string pchName, UserStats.AchievementDisplayAttribute attr, Locale locale)
		{
			return "";
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0005DD93 File Offset: 0x0005BF93
		public static int SetAchievement(string pchName)
		{
			if (IntPtr.Size == 8)
			{
				return UserStats.SetAchievement_64(pchName);
			}
			return UserStats.SetAchievement(pchName);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0005DDAA File Offset: 0x0005BFAA
		public static int ClearAchievement(string pchName)
		{
			if (IntPtr.Size == 8)
			{
				return UserStats.ClearAchievement_64(pchName);
			}
			return UserStats.ClearAchievement(pchName);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0005DDC1 File Offset: 0x0005BFC1
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void DownloadLeaderboardScoresIl2cppCallback(int errorCode)
		{
			UserStats.downloadLeaderboardScoresIl2cppCallback(errorCode);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0005DDD0 File Offset: 0x0005BFD0
		public static int DownloadLeaderboardScores(StatusCallback callback, string pchLeaderboardName, UserStats.LeaderBoardRequestType eLeaderboardDataRequest, UserStats.LeaderBoardTimeRange eLeaderboardDataTimeRange, int nRangeStart, int nRangeEnd)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			UserStats.downloadLeaderboardScoresIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(UserStats.DownloadLeaderboardScoresIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				return UserStats.DownloadLeaderboardScores_64(new StatusCallback(UserStats.DownloadLeaderboardScoresIl2cppCallback), pchLeaderboardName, (ELeaderboardDataRequest)eLeaderboardDataRequest, (ELeaderboardDataTimeRange)eLeaderboardDataTimeRange, nRangeStart, nRangeEnd);
			}
			return UserStats.DownloadLeaderboardScores(new StatusCallback(UserStats.DownloadLeaderboardScoresIl2cppCallback), pchLeaderboardName, (ELeaderboardDataRequest)eLeaderboardDataRequest, (ELeaderboardDataTimeRange)eLeaderboardDataTimeRange, nRangeStart, nRangeEnd);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0005DE4B File Offset: 0x0005C04B
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void UploadLeaderboardScoreIl2cppCallback(int errorCode)
		{
			UserStats.uploadLeaderboardScoreIl2cppCallback(errorCode);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0005DE58 File Offset: 0x0005C058
		public static int UploadLeaderboardScore(StatusCallback callback, string pchLeaderboardName, int nScore)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			UserStats.uploadLeaderboardScoreIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(UserStats.UploadLeaderboardScoreIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				return UserStats.UploadLeaderboardScore_64(new StatusCallback(UserStats.UploadLeaderboardScoreIl2cppCallback), pchLeaderboardName, nScore);
			}
			return UserStats.UploadLeaderboardScore(new StatusCallback(UserStats.UploadLeaderboardScoreIl2cppCallback), pchLeaderboardName, nScore);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0005DECC File Offset: 0x0005C0CC
		public static Leaderboard GetLeaderboardScore(int index)
		{
			LeaderboardEntry_t leaderboardEntry_t;
			leaderboardEntry_t.m_nGlobalRank = 0;
			leaderboardEntry_t.m_nScore = 0;
			leaderboardEntry_t.m_pUserName = "";
			if (IntPtr.Size == 8)
			{
				UserStats.GetLeaderboardScore_64(index, ref leaderboardEntry_t);
			}
			else
			{
				UserStats.GetLeaderboardScore(index, ref leaderboardEntry_t);
			}
			return new Leaderboard
			{
				Rank = leaderboardEntry_t.m_nGlobalRank,
				Score = leaderboardEntry_t.m_nScore,
				UserName = leaderboardEntry_t.m_pUserName
			};
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0005DF3A File Offset: 0x0005C13A
		public static int GetLeaderboardScoreCount()
		{
			if (IntPtr.Size == 8)
			{
				return UserStats.GetLeaderboardScoreCount_64();
			}
			return UserStats.GetLeaderboardScoreCount();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0005DF4F File Offset: 0x0005C14F
		public static UserStats.LeaderBoardSortMethod GetLeaderboardSortMethod()
		{
			if (IntPtr.Size == 8)
			{
				return (UserStats.LeaderBoardSortMethod)UserStats.GetLeaderboardSortMethod_64();
			}
			return (UserStats.LeaderBoardSortMethod)UserStats.GetLeaderboardSortMethod();
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0005DF64 File Offset: 0x0005C164
		public static UserStats.LeaderBoardDiaplayType GetLeaderboardDisplayType()
		{
			if (IntPtr.Size == 8)
			{
				return (UserStats.LeaderBoardDiaplayType)UserStats.GetLeaderboardDisplayType_64();
			}
			return (UserStats.LeaderBoardDiaplayType)UserStats.GetLeaderboardDisplayType();
		}

		// Token: 0x04000EEB RID: 3819
		private static StatusCallback isReadyIl2cppCallback;

		// Token: 0x04000EEC RID: 3820
		private static StatusCallback downloadStatsIl2cppCallback;

		// Token: 0x04000EED RID: 3821
		private static StatusCallback uploadStatsIl2cppCallback;

		// Token: 0x04000EEE RID: 3822
		private static StatusCallback downloadLeaderboardScoresIl2cppCallback;

		// Token: 0x04000EEF RID: 3823
		private static StatusCallback uploadLeaderboardScoreIl2cppCallback;

		// Token: 0x02000598 RID: 1432
		public enum LeaderBoardRequestType
		{
			// Token: 0x04002669 RID: 9833
			GlobalData,
			// Token: 0x0400266A RID: 9834
			GlobalDataAroundUser,
			// Token: 0x0400266B RID: 9835
			LocalData,
			// Token: 0x0400266C RID: 9836
			LocalDataAroundUser
		}

		// Token: 0x02000599 RID: 1433
		public enum LeaderBoardTimeRange
		{
			// Token: 0x0400266E RID: 9838
			AllTime,
			// Token: 0x0400266F RID: 9839
			Daily,
			// Token: 0x04002670 RID: 9840
			Weekly,
			// Token: 0x04002671 RID: 9841
			Monthly
		}

		// Token: 0x0200059A RID: 1434
		public enum LeaderBoardSortMethod
		{
			// Token: 0x04002673 RID: 9843
			None,
			// Token: 0x04002674 RID: 9844
			Ascending,
			// Token: 0x04002675 RID: 9845
			Descending
		}

		// Token: 0x0200059B RID: 1435
		public enum LeaderBoardDiaplayType
		{
			// Token: 0x04002677 RID: 9847
			None,
			// Token: 0x04002678 RID: 9848
			Numeric,
			// Token: 0x04002679 RID: 9849
			TimeSeconds,
			// Token: 0x0400267A RID: 9850
			TimeMilliSeconds
		}

		// Token: 0x0200059C RID: 1436
		public enum LeaderBoardScoreMethod
		{
			// Token: 0x0400267C RID: 9852
			None,
			// Token: 0x0400267D RID: 9853
			KeepBest,
			// Token: 0x0400267E RID: 9854
			ForceUpdate
		}

		// Token: 0x0200059D RID: 1437
		public enum AchievementDisplayAttribute
		{
			// Token: 0x04002680 RID: 9856
			Name,
			// Token: 0x04002681 RID: 9857
			Desc,
			// Token: 0x04002682 RID: 9858
			Hidden
		}
	}
}
