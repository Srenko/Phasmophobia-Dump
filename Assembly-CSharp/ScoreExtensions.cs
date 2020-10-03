using System;
using ExitGames.Client.Photon;

// Token: 0x020000DA RID: 218
public static class ScoreExtensions
{
	// Token: 0x060005FF RID: 1535 RVA: 0x000220AC File Offset: 0x000202AC
	public static void SetScore(this PhotonPlayer player, int newScore)
	{
		Hashtable hashtable = new Hashtable();
		hashtable["score"] = newScore;
		player.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x000220DC File Offset: 0x000202DC
	public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
	{
		int num = player.GetScore();
		num += scoreToAddToCurrent;
		Hashtable hashtable = new Hashtable();
		hashtable["score"] = num;
		player.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x00022114 File Offset: 0x00020314
	public static int GetScore(this PhotonPlayer player)
	{
		object obj;
		if (player.CustomProperties.TryGetValue("score", out obj))
		{
			return (int)obj;
		}
		return 0;
	}
}
