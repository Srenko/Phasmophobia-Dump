using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000185 RID: 389
public class LoadingScreenHint : MonoBehaviour
{
	// Token: 0x06000A5B RID: 2651 RVA: 0x0004041C File Offset: 0x0003E61C
	private void Awake()
	{
		this.myText = base.GetComponent<Text>();
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint1"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint2"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint3"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint4"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint5"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint6"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint7"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint8"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint9"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint10"));
		this.hints.Add(LocalisationSystem.GetLocalisedValue("Loading_Hint11"));
		this.myText.text = LocalisationSystem.GetLocalisedValue("Loading_Hint") + ": " + this.hints[Random.Range(0, this.hints.Count)];
	}

	// Token: 0x04000A80 RID: 2688
	private List<string> hints = new List<string>();

	// Token: 0x04000A81 RID: 2689
	private Text myText;
}
