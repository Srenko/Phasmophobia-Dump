using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
[SerializeField]
public struct GhostTraits
{
	// Token: 0x0400071F RID: 1823
	public GhostTraits.Type ghostType;

	// Token: 0x04000720 RID: 1824
	public int ghostAge;

	// Token: 0x04000721 RID: 1825
	public bool isMale;

	// Token: 0x04000722 RID: 1826
	public string ghostName;

	// Token: 0x04000723 RID: 1827
	public bool isShy;

	// Token: 0x04000724 RID: 1828
	public int deathLength;

	// Token: 0x04000725 RID: 1829
	public int favouriteRoomID;

	// Token: 0x02000512 RID: 1298
	public enum Type
	{
		// Token: 0x0400245C RID: 9308
		none,
		// Token: 0x0400245D RID: 9309
		Spirit,
		// Token: 0x0400245E RID: 9310
		Wraith,
		// Token: 0x0400245F RID: 9311
		Phantom,
		// Token: 0x04002460 RID: 9312
		Poltergeist,
		// Token: 0x04002461 RID: 9313
		Banshee,
		// Token: 0x04002462 RID: 9314
		Jinn,
		// Token: 0x04002463 RID: 9315
		Mare,
		// Token: 0x04002464 RID: 9316
		Revenant,
		// Token: 0x04002465 RID: 9317
		Shade,
		// Token: 0x04002466 RID: 9318
		Demon,
		// Token: 0x04002467 RID: 9319
		Yurei,
		// Token: 0x04002468 RID: 9320
		Oni
	}
}
