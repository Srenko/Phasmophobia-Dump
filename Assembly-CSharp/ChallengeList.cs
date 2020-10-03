﻿using System;
using System.Collections.Generic;

// Token: 0x0200012E RID: 302
public class ChallengeList
{
	// Token: 0x06000806 RID: 2054 RVA: 0x0002FFE8 File Offset: 0x0002E1E8
	public void CreateList()
	{
		this.listOfChallenges.Clear();
		this.listOfChallenges.Add(this.PlayContracts);
		this.listOfChallenges.Add(this.PlayTogether);
		this.listOfChallenges.Add(this.PlayWithDefaultItems);
		this.listOfChallenges.Add(this.PlayMediumContract);
		this.listOfChallenges.Add(this.CompleteObjectives);
		this.listOfChallenges.Add(this.DiscoverGhostType);
		this.listOfChallenges.Add(this.FindDNAEvidence);
		this.listOfChallenges.Add(this.GetRewardWithPhotos);
		this.listOfChallenges.Add(this.BuyStoreItem);
		this.listOfChallenges.Add(this.OuijaBoardResponse);
		this.listOfChallenges.Add(this.TakeGhostPhoto);
		this.listOfChallenges.Add(this.SurviveHuntingPhase);
		this.listOfChallenges.Add(this.ReachZeroSanity);
		this.listOfChallenges.Add(this.SpiritBoxResponse);
	}

	// Token: 0x04000813 RID: 2067
	public List<Challenge.ChallengeValues> listOfChallenges = new List<Challenge.ChallengeValues>();

	// Token: 0x04000814 RID: 2068
	public Challenge.ChallengeValues PlayContracts = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 0,
		reward = 35,
		challengeName = "Challenge_PlayContracts",
		progressionMaxValue = 3
	};

	// Token: 0x04000815 RID: 2069
	public Challenge.ChallengeValues PlayTogether = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 1,
		reward = 15,
		challengeName = "Challenge_PlayTogether",
		progressionMaxValue = 1
	};

	// Token: 0x04000816 RID: 2070
	public Challenge.ChallengeValues PlayWithDefaultItems = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 2,
		reward = 15,
		challengeName = "Challenge_PlayWithDefaultItems",
		progressionMaxValue = 1
	};

	// Token: 0x04000817 RID: 2071
	public Challenge.ChallengeValues PlayMediumContract = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 3,
		reward = 20,
		challengeName = "Challenge_PlayMediumContract",
		progressionMaxValue = 1
	};

	// Token: 0x04000818 RID: 2072
	public Challenge.ChallengeValues CompleteObjectives = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 4,
		reward = 30,
		challengeName = "Challenge_CompleteObjectives",
		progressionMaxValue = 5
	};

	// Token: 0x04000819 RID: 2073
	public Challenge.ChallengeValues DiscoverGhostType = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 5,
		reward = 25,
		challengeName = "Challenge_DiscoverGhostType",
		progressionMaxValue = 1
	};

	// Token: 0x0400081A RID: 2074
	public Challenge.ChallengeValues FindDNAEvidence = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 6,
		reward = 10,
		challengeName = "Challenge_FindDNAEvidence",
		progressionMaxValue = 1
	};

	// Token: 0x0400081B RID: 2075
	public Challenge.ChallengeValues GetRewardWithPhotos = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 7,
		reward = 15,
		challengeName = "Challenge_GetPhotoReward",
		progressionMaxValue = 1
	};

	// Token: 0x0400081C RID: 2076
	public Challenge.ChallengeValues BuyStoreItem = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 8,
		reward = 10,
		challengeName = "Challenge_BuyStoreItem",
		progressionMaxValue = 1
	};

	// Token: 0x0400081D RID: 2077
	public Challenge.ChallengeValues OuijaBoardResponse = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 9,
		reward = 20,
		challengeName = "Challenge_OuijaBoardResponse",
		progressionMaxValue = 1
	};

	// Token: 0x0400081E RID: 2078
	public Challenge.ChallengeValues TakeGhostPhoto = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 10,
		reward = 25,
		challengeName = "Challenge_TakeGhostPhoto",
		progressionMaxValue = 1
	};

	// Token: 0x0400081F RID: 2079
	public Challenge.ChallengeValues SurviveHuntingPhase = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 11,
		reward = 15,
		challengeName = "Challenge_SurviveHuntingPhase",
		progressionMaxValue = 1
	};

	// Token: 0x04000820 RID: 2080
	public Challenge.ChallengeValues ReachZeroSanity = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 12,
		reward = 10,
		challengeName = "Challenge_ReachZeroSanity",
		progressionMaxValue = 1
	};

	// Token: 0x04000821 RID: 2081
	public Challenge.ChallengeValues SpiritBoxResponse = new Challenge.ChallengeValues
	{
		uniqueChallengeID = 13,
		reward = 15,
		challengeName = "Challenge_SpiritBoxEvidence",
		progressionMaxValue = 1
	};
}
