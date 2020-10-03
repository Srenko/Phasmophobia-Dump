using System;

// Token: 0x020000DE RID: 222
public interface IPunTurnManagerCallbacks
{
	// Token: 0x0600061C RID: 1564
	void OnTurnBegins(int turn);

	// Token: 0x0600061D RID: 1565
	void OnTurnCompleted(int turn);

	// Token: 0x0600061E RID: 1566
	void OnPlayerMove(PhotonPlayer player, int turn, object move);

	// Token: 0x0600061F RID: 1567
	void OnPlayerFinished(PhotonPlayer player, int turn, object move);

	// Token: 0x06000620 RID: 1568
	void OnTurnTimeEnds(int turn);
}
