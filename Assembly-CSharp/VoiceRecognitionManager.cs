﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

// Token: 0x02000198 RID: 408
public class VoiceRecognitionManager : MonoBehaviour
{
	// Token: 0x06000B18 RID: 2840 RVA: 0x000452D8 File Offset: 0x000434D8
	private void OnEnable()
	{
		this.phraseHasBeenRecognised = false;
		this.phraseText.text = LocalisationSystem.GetLocalisedValue("Options_Say") + " \"" + LocalisationSystem.GetLocalisedValue("Q_Give us a sign") + "\"";
		this.listeningString = LocalisationSystem.GetLocalisedValue("Options_Listening");
		this.answerText.text = this.listeningString;
		if (!this.keywords.Contains(LocalisationSystem.GetLocalisedValue("Q_Give us a sign")))
		{
			this.keywords.Add(LocalisationSystem.GetLocalisedValue("Q_Give us a sign"));
		}
		if (this.recognizer == null)
		{
			this.recognizer = new KeywordRecognizer(this.keywords.ToArray(), ConfidenceLevel.Low);
			this.recognizer.OnPhraseRecognized += this.OnPhraseRecognized;
			this.recognizer.Start();
			this.statusText.text = "Voice recognition is setup correctly.";
			return;
		}
		this.statusText.text = "Error: Voice recognition is not setup on your PC correctly.";
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x000453C9 File Offset: 0x000435C9
	private void OnDisable()
	{
		if (this.recognizer != null)
		{
			this.recognizer.OnPhraseRecognized -= this.OnPhraseRecognized;
			this.recognizer.Stop();
			this.recognizer = null;
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x000453FC File Offset: 0x000435FC
	private void Update()
	{
		if (this.phraseHasBeenRecognised)
		{
			return;
		}
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			this.count++;
			if (this.count > 3)
			{
				this.count = 0;
			}
			if (this.count == 0)
			{
				this.answerText.text = this.listeningString;
			}
			else if (this.count == 1)
			{
				this.answerText.text = this.listeningString + ".";
			}
			else if (this.count == 2)
			{
				this.answerText.text = this.listeningString + "..";
			}
			else
			{
				this.answerText.text = this.listeningString + "...";
			}
			this.timer = 0.5f;
		}
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x000454DF File Offset: 0x000436DF
	private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		this.phraseHasBeenRecognised = true;
		this.answerText.text = LocalisationSystem.GetLocalisedValue("Options_Heard");
	}

	// Token: 0x04000B69 RID: 2921
	private KeywordRecognizer recognizer;

	// Token: 0x04000B6A RID: 2922
	private List<string> keywords = new List<string>();

	// Token: 0x04000B6B RID: 2923
	[SerializeField]
	private Text phraseText;

	// Token: 0x04000B6C RID: 2924
	[SerializeField]
	private Text answerText;

	// Token: 0x04000B6D RID: 2925
	[SerializeField]
	private Text statusText;

	// Token: 0x04000B6E RID: 2926
	private bool phraseHasBeenRecognised;

	// Token: 0x04000B6F RID: 2927
	private string listeningString = "Listening...";

	// Token: 0x04000B70 RID: 2928
	private float timer = 0.5f;

	// Token: 0x04000B71 RID: 2929
	private int count;

	// Token: 0x04000B72 RID: 2930
	private const string quote = "\"";
}
