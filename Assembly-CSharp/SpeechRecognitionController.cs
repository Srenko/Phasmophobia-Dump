using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

// Token: 0x02000127 RID: 295
public class SpeechRecognitionController : MonoBehaviour
{
	// Token: 0x060007F1 RID: 2033 RVA: 0x0002FAAE File Offset: 0x0002DCAE
	private void Awake()
	{
		SpeechRecognitionController.instance = this;
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0002FAC2 File Offset: 0x0002DCC2
	public void AddOuijaBoard(OuijaBoard b)
	{
		this.board = b;
		this.StartPhraseRecogniser();
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0002FAD1 File Offset: 0x0002DCD1
	public void AddEVPRecorder(EVPRecorder r)
	{
		r.SetupKeywords();
		this.recorders.Add(r);
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
	public void StartPhraseRecogniser()
	{
		if (this.Recognizer == null)
		{
			this.Recognizer = new KeywordRecognizer(this.Keywords.ToArray(), ConfidenceLevel.Low);
			this.Recognizer.OnPhraseRecognized += this.OnPhraseRecognized;
			this.Recognizer.Start();
		}
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0002FB38 File Offset: 0x0002DD38
	private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		this.board.OnPhraseRecognized(args.text);
		this.listener.OnPhraseRecognized(args.text);
		for (int i = 0; i < this.recorders.Count; i++)
		{
			this.recorders[i].OnPhraseRecognized(args.text);
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0002FB94 File Offset: 0x0002DD94
	public void AddKeyword(string key)
	{
		if (!this.Keywords.Contains(key) && !string.IsNullOrEmpty(key))
		{
			this.Keywords.Add(key);
		}
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0002FBB8 File Offset: 0x0002DDB8
	private void OnDisable()
	{
		if (this.Recognizer != null)
		{
			this.Recognizer.OnPhraseRecognized -= this.OnPhraseRecognized;
			this.Recognizer.Stop();
			this.Recognizer = null;
		}
		PhraseRecognitionSystem.OnError -= this.OnVoiceRecognitionError;
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0002FC07 File Offset: 0x0002DE07
	private void OnEnable()
	{
		PhraseRecognitionSystem.OnError += this.OnVoiceRecognitionError;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0002FC1A File Offset: 0x0002DE1A
	private void OnVoiceRecognitionError(SpeechError error)
	{
		Debug.LogError("Speech Recognition has stopped working: " + error);
	}

	// Token: 0x040007C2 RID: 1986
	public static SpeechRecognitionController instance;

	// Token: 0x040007C3 RID: 1987
	public KeywordRecognizer Recognizer;

	// Token: 0x040007C4 RID: 1988
	private List<string> Keywords = new List<string>();

	// Token: 0x040007C5 RID: 1989
	[SerializeField]
	private PhraseListenerController listener;

	// Token: 0x040007C6 RID: 1990
	private List<EVPRecorder> recorders = new List<EVPRecorder>();

	// Token: 0x040007C7 RID: 1991
	private OuijaBoard board;

	// Token: 0x040007C8 RID: 1992
	[HideInInspector]
	public bool hasSaidGhostsName;

	// Token: 0x040007C9 RID: 1993
	private PhotonView view;
}
