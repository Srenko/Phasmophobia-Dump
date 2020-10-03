using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000179 RID: 377
public class ChallengesManager : MonoBehaviour
{
	// Token: 0x060009F5 RID: 2549 RVA: 0x0003D575 File Offset: 0x0003B775
	private void OnEnable()
	{
		this.resetLocalisedValue = LocalisationSystem.GetLocalisedValue("Challenge_ResetTimer");
		this.UpdateChallengeValues();
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0003D58D File Offset: 0x0003B78D
	public void ForceResetChallenges()
	{
		DailyChallengesController.Instance.ForceReset();
		this.UpdateChallengeValues();
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
	public void UpdateChallengeValues()
	{
		this.contract1Name.text = DailyChallengesController.Instance.currentChallenges[0].GetLocalisedName();
		this.contract1Reward.text = "$" + DailyChallengesController.Instance.currentChallenges[0].myChallenge.reward;
		int num = Mathf.Clamp(DailyChallengesController.Instance.currentChallenges[0].progressionValue, 0, DailyChallengesController.Instance.currentChallenges[0].myChallenge.progressionMaxValue);
		this.contract1SliderValue.text = num + "/" + DailyChallengesController.Instance.currentChallenges[0].myChallenge.progressionMaxValue;
		this.contract1Slider.maxValue = (float)DailyChallengesController.Instance.currentChallenges[0].myChallenge.progressionMaxValue;
		this.contract1Slider.value = (float)num;
		this.contract2Name.text = DailyChallengesController.Instance.currentChallenges[1].GetLocalisedName();
		this.contract2Reward.text = "$" + DailyChallengesController.Instance.currentChallenges[1].myChallenge.reward;
		int num2 = Mathf.Clamp(DailyChallengesController.Instance.currentChallenges[1].progressionValue, 0, DailyChallengesController.Instance.currentChallenges[1].myChallenge.progressionMaxValue);
		this.contract2SliderValue.text = num2 + "/" + DailyChallengesController.Instance.currentChallenges[1].myChallenge.progressionMaxValue;
		this.contract2Slider.maxValue = (float)DailyChallengesController.Instance.currentChallenges[1].myChallenge.progressionMaxValue;
		this.contract2Slider.value = (float)num2;
		this.contract3Name.text = DailyChallengesController.Instance.currentChallenges[2].GetLocalisedName();
		this.contract3Reward.text = "$" + DailyChallengesController.Instance.currentChallenges[2].myChallenge.reward;
		int num3 = Mathf.Clamp(DailyChallengesController.Instance.currentChallenges[2].progressionValue, 0, DailyChallengesController.Instance.currentChallenges[2].myChallenge.progressionMaxValue);
		this.contract3SliderValue.text = num3 + "/" + DailyChallengesController.Instance.currentChallenges[1].myChallenge.progressionMaxValue;
		this.contract3Slider.maxValue = (float)DailyChallengesController.Instance.currentChallenges[2].myChallenge.progressionMaxValue;
		this.contract3Slider.value = (float)num3;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0003D894 File Offset: 0x0003BA94
	private void Update()
	{
		this.resetTimerText.text = string.Concat(new string[]
		{
			this.resetLocalisedValue,
			": ",
			(23 - DateTime.UtcNow.Hour).ToString("00"),
			":",
			(59 - DateTime.UtcNow.Minute).ToString("00"),
			":",
			(59 - DateTime.UtcNow.Second).ToString("00")
		});
	}

	// Token: 0x04000A13 RID: 2579
	[SerializeField]
	private Text contract1Name;

	// Token: 0x04000A14 RID: 2580
	[SerializeField]
	private Text contract1Reward;

	// Token: 0x04000A15 RID: 2581
	[SerializeField]
	private Slider contract1Slider;

	// Token: 0x04000A16 RID: 2582
	[SerializeField]
	private Text contract1SliderValue;

	// Token: 0x04000A17 RID: 2583
	[SerializeField]
	private Text contract2Name;

	// Token: 0x04000A18 RID: 2584
	[SerializeField]
	private Text contract2Reward;

	// Token: 0x04000A19 RID: 2585
	[SerializeField]
	private Slider contract2Slider;

	// Token: 0x04000A1A RID: 2586
	[SerializeField]
	private Text contract2SliderValue;

	// Token: 0x04000A1B RID: 2587
	[SerializeField]
	private Text contract3Name;

	// Token: 0x04000A1C RID: 2588
	[SerializeField]
	private Text contract3Reward;

	// Token: 0x04000A1D RID: 2589
	[SerializeField]
	private Slider contract3Slider;

	// Token: 0x04000A1E RID: 2590
	[SerializeField]
	private Text contract3SliderValue;

	// Token: 0x04000A1F RID: 2591
	[SerializeField]
	private Text resetTimerText;

	// Token: 0x04000A20 RID: 2592
	private string resetLocalisedValue;
}
