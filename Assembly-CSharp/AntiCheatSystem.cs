using System;

// Token: 0x02000134 RID: 308
public static class AntiCheatSystem
{
	// Token: 0x06000826 RID: 2086 RVA: 0x00030F7A File Offset: 0x0002F17A
	public static void CheckPlayerMoney(int value)
	{
		if (value > 250000)
		{
			FileBasedPrefs.SetInt("PlayersMoney", 100);
		}
	}
}
