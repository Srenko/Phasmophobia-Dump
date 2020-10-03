using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019A RID: 410
public class Mission : MonoBehaviour
{
	// Token: 0x06000B22 RID: 2850 RVA: 0x000455CC File Offset: 0x000437CC
	public void Completed()
	{
		this.completed = true;
		this.myText.color = new Color(this.myText.color.r, this.myText.color.r, this.myText.color.r, 0.3f);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00045628 File Offset: 0x00043828
	public void SetUIText()
	{
		this.myText.text = string.Concat(new object[]
		{
			LocalisationSystem.GetLocalisedValue("WhiteBoard_Objective"),
			" ",
			this.sideMissionID + 1,
			": ",
			this.missionName
		});
	}

	// Token: 0x04000B74 RID: 2932
	[HideInInspector]
	public PhotonView view;

	// Token: 0x04000B75 RID: 2933
	public Mission.MissionType type;

	// Token: 0x04000B76 RID: 2934
	public bool completed;

	// Token: 0x04000B77 RID: 2935
	public string missionName = "Mission name has not been set";

	// Token: 0x04000B78 RID: 2936
	public Text myText;

	// Token: 0x04000B79 RID: 2937
	public int sideMissionID;

	// Token: 0x0200054E RID: 1358
	public enum MissionType
	{
		// Token: 0x04002564 RID: 9572
		none,
		// Token: 0x04002565 RID: 9573
		main,
		// Token: 0x04002566 RID: 9574
		side
	}
}
