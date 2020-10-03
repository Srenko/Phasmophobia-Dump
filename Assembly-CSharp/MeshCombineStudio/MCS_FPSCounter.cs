using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004B0 RID: 1200
	public class MCS_FPSCounter : MonoBehaviour
	{
		// Token: 0x06002571 RID: 9585 RVA: 0x000BA1D4 File Offset: 0x000B83D4
		private void Awake()
		{
			MCS_FPSCounter.instance = this;
			this.gradient.colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(1f, 0f, 0f, 1f), 0f),
				new GradientColorKey(new Color(1f, 1f, 0f, 1f), 0.5f),
				new GradientColorKey(new Color(0f, 1f, 0f, 1f), 1f)
			};
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000BA276 File Offset: 0x000B8476
		private void OnDestroy()
		{
			if (MCS_FPSCounter.instance == this)
			{
				MCS_FPSCounter.instance = null;
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000BA28C File Offset: 0x000B848C
		private void OnGUI()
		{
			if (this.displayType == MCS_FPSCounter.GUIType.DisplayNothing)
			{
				return;
			}
			if (this.displayType == MCS_FPSCounter.GUIType.DisplayRunning)
			{
				if ((float)Screen.width != this.screenSize.x || (float)Screen.height != this.screenSize.y)
				{
					this.screenSize.x = (float)Screen.width;
					this.screenSize.y = (float)Screen.height;
					this.SetRectsRun();
				}
				GUI.Label(this.rectsRun[0], this.currentFPSText, this.bigStyleShadow);
				GUI.Label(this.rectsRun[1], this.avgFPSText, this.smallStyleShadow);
				GUI.Label(this.rectsRun[2], this.minFPSText, this.smallStyleShadow);
				GUI.Label(this.rectsRun[3], this.maxFSPText, this.smallStyleShadow);
				GUI.Label(this.rectsRun[4], "Avg:", this.smallStyleShadow);
				GUI.Label(this.rectsRun[5], "Min:", this.smallStyleShadow);
				GUI.Label(this.rectsRun[6], "Max:", this.smallStyleShadow);
				GUI.Label(this.rectsRun[7], this.currentFPSText, this.bigStyle);
				GUI.Label(this.rectsRun[8], this.avgFPSText, this.smallStyle);
				GUI.Label(this.rectsRun[9], this.minFPSText, this.smallStyle);
				GUI.Label(this.rectsRun[10], this.maxFSPText, this.smallStyle);
				GUI.Label(this.rectsRun[11], "Avg:", this.smallStyleLabel);
				GUI.Label(this.rectsRun[12], "Min:", this.smallStyleLabel);
				GUI.Label(this.rectsRun[13], "Max:", this.smallStyleLabel);
				return;
			}
			if ((float)Screen.width != this.screenSize.x || (float)Screen.height != this.screenSize.y)
			{
				this.screenSize.x = (float)Screen.width;
				this.screenSize.y = (float)Screen.height;
				this.SetRectsResult();
			}
			if (this.showLogoOnResultsScreen)
			{
				GUI.DrawTexture(this.rectsResult[8], this.logo);
			}
			GUI.Label(this.rectsResult[0], this.resultHeaderGUI, this.headerStyle);
			GUI.DrawTexture(this.rectsResult[1], Texture2D.whiteTexture);
			GUI.Label(this.rectsResult[2], this.reslutLabelAvgGUI, this.smallStyle);
			GUI.Label(this.rectsResult[4], "MINIMUM FPS:", this.smallStyleLabel);
			GUI.Label(this.rectsResult[6], "MAXIMUM FPS:", this.smallStyleLabel);
			GUI.Label(this.rectsResult[3], this.avgTextGUI, this.bigStyle);
			GUI.Label(this.rectsResult[5], this.minFPSText, this.smallStyle);
			GUI.Label(this.rectsResult[7], this.maxFSPText, this.smallStyle);
			GUI.Label(this.rectsResult[9], this.instructions, this.smallStyleLabel);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000BA5FC File Offset: 0x000B87FC
		private void SetRectsRun()
		{
			this.columnRight = (float)Screen.width - 34f;
			this.columnLeft = this.columnRight - 34f;
			float num = 0f;
			this.rectsRun[0].Set((float)Screen.width - 48f + 1f, num + 4f + 2f, 40f, 22f);
			this.rectsRun[1].Set(this.columnRight + 1f, num + 30f + 2f, 26f, 22f);
			this.rectsRun[2].Set(this.columnRight + 1f, num + 44f + 2f, 26f, 22f);
			this.rectsRun[3].Set(this.columnRight + 1f, num + 58f + 2f, 26f, 22f);
			this.rectsRun[4].Set(this.columnLeft + 1f, num + 30f + 2f, 26f, 22f);
			this.rectsRun[5].Set(this.columnLeft + 1f, num + 44f + 2f, 26f, 22f);
			this.rectsRun[6].Set(this.columnLeft + 1f, num + 58f + 2f, 26f, 22f);
			this.rectsRun[7].Set((float)Screen.width - 53f, num + 4f, 45f, 22f);
			this.rectsRun[8].Set(this.columnRight, num + 30f, 26f, 22f);
			this.rectsRun[9].Set(this.columnRight, num + 44f, 26f, 22f);
			this.rectsRun[10].Set(this.columnRight, num + 58f, 26f, 22f);
			this.rectsRun[11].Set(this.columnLeft, num + 30f, 26f, 22f);
			this.rectsRun[12].Set(this.columnLeft, num + 44f, 26f, 22f);
			this.rectsRun[13].Set(this.columnLeft, num + 58f, 26f, 22f);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000BA8C8 File Offset: 0x000B8AC8
		private void SetRectsResult()
		{
			float num = 256f;
			this.rectsResult[8].Set((float)(Screen.width / 2 - this.logo.width / 2), (float)(Screen.height / 2) - num, (float)this.logo.width, (float)this.logo.height);
			Vector2 vector = this.headerStyle.CalcSize(this.resultHeaderGUI);
			this.rectsResult[0].Set((float)(Screen.width / 2) - vector.x / 2f, (float)(Screen.height / 2) - (num - 256f), vector.x, vector.y);
			vector.x += 10f;
			this.rectsResult[1].Set((float)(Screen.width / 2) - vector.x / 2f, (float)(Screen.height / 2) - (num - 256f - 30f), vector.x, 1f);
			this.rectsResult[2].Set((float)(Screen.width / 2 - 200), (float)(Screen.height / 2) - (num - 256f - 30f - 30f), 200f, 24f);
			this.rectsResult[4].Set((float)(Screen.width / 2 - 200), (float)(Screen.height / 2) - (num - 256f - 30f - 30f - 20f), 200f, 24f);
			this.rectsResult[6].Set((float)(Screen.width / 2 - 200), (float)(Screen.height / 2) - (num - 256f - 30f - 30f - 20f - 20f), 200f, 24f);
			this.rectsResult[3].Set((float)(Screen.width / 2), (float)(Screen.height / 2) - (num - 256f - 30f - 18f), 65f, 24f);
			this.rectsResult[5].Set((float)(Screen.width / 2), (float)(Screen.height / 2) - (num - 256f - 30f - 30f - 20f), 65f, 24f);
			this.rectsResult[7].Set((float)(Screen.width / 2), (float)(Screen.height / 2) - (num - 256f - 30f - 30f - 20f - 20f), 65f, 24f);
			vector = this.smallStyleLabel.CalcSize(this.instructions);
			this.rectsResult[9].Set((float)(Screen.width / 2) - vector.x / 2f, (float)(Screen.height / 2) - (num - 256f - 30f - 30f - 20f - 20f - 40f), vector.x, vector.y);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000BABF4 File Offset: 0x000B8DF4
		private void Start()
		{
			this.headerStyle.normal.textColor = this.label;
			this.headerStyle.fontSize = 24;
			this.headerStyle.font = this.fontResult;
			this.headerStyle.alignment = TextAnchor.UpperCenter;
			this.bigStyle.alignment = TextAnchor.UpperRight;
			this.bigStyle.font = this.fontRun;
			this.bigStyle.fontSize = 24;
			this.bigStyle.normal.textColor = Color.green;
			this.bigStyleShadow = new GUIStyle(this.bigStyle);
			this.bigStyleShadow.normal.textColor = this.fontShadow;
			this.smallStyle.alignment = TextAnchor.UpperRight;
			this.smallStyle.font = this.fontRun;
			this.smallStyle.fontSize = 12;
			this.smallStyle.normal.textColor = Color.white;
			this.smallStyleShadow = new GUIStyle(this.smallStyle);
			this.smallStyleShadow.normal.textColor = this.fontShadow;
			this.smallStyleLabel = new GUIStyle(this.smallStyle);
			this.smallStyleLabel.normal.textColor = this.label;
			base.Invoke("Reset", 0.5f);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000BAD44 File Offset: 0x000B8F44
		private void Update()
		{
			if (this.displayType != this.oldDisplayType)
			{
				if (this.displayType == MCS_FPSCounter.GUIType.DisplayResults)
				{
					this.SetRectsResult();
					this.colorAvg = this.EvaluateGradient(this.averageFPS);
					this.bigStyle.normal.textColor = this.colorAvg;
					this.avgTextGUI.text = this.avgFPSText;
				}
				else if (this.displayType == MCS_FPSCounter.GUIType.DisplayRunning)
				{
					this.Reset();
					this.SetRectsRun();
				}
				this.oldDisplayType = this.displayType;
			}
			if (Input.GetKeyDown(this.showHideButton) && this.acceptInput && this.displayType != MCS_FPSCounter.GUIType.DisplayResults)
			{
				if (this.displayType == MCS_FPSCounter.GUIType.DisplayNothing)
				{
					this.displayType = MCS_FPSCounter.GUIType.DisplayRunning;
				}
				else
				{
					this.displayType = MCS_FPSCounter.GUIType.DisplayNothing;
				}
			}
			if (this.displayType == MCS_FPSCounter.GUIType.DisplayNothing)
			{
				return;
			}
			if (this.displayType == MCS_FPSCounter.GUIType.DisplayRunning)
			{
				this.GetFPS();
			}
			if (this.reset)
			{
				this.reset = false;
				this.Reset();
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000BAE2D File Offset: 0x000B902D
		public void StartBenchmark()
		{
			this.Reset();
			this.SetRectsRun();
			this.displayType = MCS_FPSCounter.GUIType.DisplayRunning;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000BAE42 File Offset: 0x000B9042
		public void StopBenchmark()
		{
			this.SetRectsResult();
			this.displayType = MCS_FPSCounter.GUIType.DisplayResults;
			this.colorAvg = this.EvaluateGradient(this.averageFPS);
			this.bigStyle.normal.textColor = this.colorAvg;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000BAE7C File Offset: 0x000B907C
		private void GetFPS()
		{
			this.tempFrameCount++;
			this.totalFrameCount++;
			if ((double)Time.realtimeSinceStartup - this.tStampTemp > (double)this.interval)
			{
				this.currentFPS = (float)((double)this.tempFrameCount / ((double)Time.realtimeSinceStartup - this.tStampTemp));
				this.averageFPS = (float)((double)this.totalFrameCount / ((double)Time.realtimeSinceStartup - this.tStamp));
				if (this.currentFPS < this.minimumFPS)
				{
					this.minimumFPS = this.currentFPS;
				}
				if (this.currentFPS > this.maximumFPS)
				{
					this.maximumFPS = this.currentFPS;
				}
				this.tStampTemp = (double)Time.realtimeSinceStartup;
				this.tempFrameCount = 0;
				this.currentFPSText = "FPS " + this.currentFPS.ToString("0.0");
				this.avgFPSText = this.averageFPS.ToString("0.0");
				this.minFPSText = this.minimumFPS.ToString("0.0");
				this.maxFSPText = this.maximumFPS.ToString("0.0");
				this.colorCurrent = this.EvaluateGradient(this.currentFPS);
				this.bigStyle.normal.textColor = this.colorCurrent;
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000BAFC8 File Offset: 0x000B91C8
		public void Reset()
		{
			this.tStamp = (double)Time.realtimeSinceStartup;
			this.tStampTemp = (double)Time.realtimeSinceStartup;
			this.currentFPS = 0f;
			this.averageFPS = 0f;
			this.minimumFPS = 999.9f;
			this.maximumFPS = 0f;
			this.tempFrameCount = 0;
			this.totalFrameCount = 0;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000BB027 File Offset: 0x000B9227
		private Color EvaluateGradient(float f)
		{
			return this.gradient.Evaluate(Mathf.Clamp01((f - this.gradientRange.x) / (this.gradientRange.y - this.gradientRange.x)));
		}

		// Token: 0x040022AA RID: 8874
		public static MCS_FPSCounter instance;

		// Token: 0x040022AB RID: 8875
		[Header("___ Settings ___________________________________________________________________________________________________________")]
		public float interval = 0.25f;

		// Token: 0x040022AC RID: 8876
		public MCS_FPSCounter.GUIType displayType;

		// Token: 0x040022AD RID: 8877
		public Vector2 gradientRange = new Vector2(15f, 60f);

		// Token: 0x040022AE RID: 8878
		public Font fontRun;

		// Token: 0x040022AF RID: 8879
		public Font fontResult;

		// Token: 0x040022B0 RID: 8880
		public Texture logo;

		// Token: 0x040022B1 RID: 8881
		public bool showLogoOnResultsScreen = true;

		// Token: 0x040022B2 RID: 8882
		public KeyCode showHideButton = KeyCode.Backspace;

		// Token: 0x040022B3 RID: 8883
		public bool acceptInput = true;

		// Token: 0x040022B4 RID: 8884
		public bool reset;

		// Token: 0x040022B5 RID: 8885
		[Header("___ Results ___________________________________________________________________________________________________________")]
		public float currentFPS;

		// Token: 0x040022B6 RID: 8886
		public float averageFPS;

		// Token: 0x040022B7 RID: 8887
		public float minimumFPS;

		// Token: 0x040022B8 RID: 8888
		public float maximumFPS;

		// Token: 0x040022B9 RID: 8889
		private int totalFrameCount;

		// Token: 0x040022BA RID: 8890
		private int tempFrameCount;

		// Token: 0x040022BB RID: 8891
		private double tStamp;

		// Token: 0x040022BC RID: 8892
		private double tStampTemp;

		// Token: 0x040022BD RID: 8893
		private string currentFPSText;

		// Token: 0x040022BE RID: 8894
		private string avgFPSText;

		// Token: 0x040022BF RID: 8895
		private string minFPSText;

		// Token: 0x040022C0 RID: 8896
		private string maxFSPText;

		// Token: 0x040022C1 RID: 8897
		private GUIStyle bigStyle = new GUIStyle();

		// Token: 0x040022C2 RID: 8898
		private GUIStyle bigStyleShadow;

		// Token: 0x040022C3 RID: 8899
		private GUIStyle smallStyle = new GUIStyle();

		// Token: 0x040022C4 RID: 8900
		private GUIStyle smallStyleShadow;

		// Token: 0x040022C5 RID: 8901
		private GUIStyle smallStyleLabel;

		// Token: 0x040022C6 RID: 8902
		private GUIStyle headerStyle = new GUIStyle();

		// Token: 0x040022C7 RID: 8903
		private Rect[] rectsRun = new Rect[14];

		// Token: 0x040022C8 RID: 8904
		private Rect[] rectsResult = new Rect[10];

		// Token: 0x040022C9 RID: 8905
		private Gradient gradient = new Gradient();

		// Token: 0x040022CA RID: 8906
		private const float line1 = 4f;

		// Token: 0x040022CB RID: 8907
		private const float line2 = 30f;

		// Token: 0x040022CC RID: 8908
		private const float line3 = 44f;

		// Token: 0x040022CD RID: 8909
		private const float line4 = 58f;

		// Token: 0x040022CE RID: 8910
		private const float labelWidth = 26f;

		// Token: 0x040022CF RID: 8911
		private const float paddingH = 8f;

		// Token: 0x040022D0 RID: 8912
		private const float lineHeight = 22f;

		// Token: 0x040022D1 RID: 8913
		private float columnRight;

		// Token: 0x040022D2 RID: 8914
		private float columnLeft;

		// Token: 0x040022D3 RID: 8915
		private Color fontShadow = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x040022D4 RID: 8916
		private Color label = new Color(0.8f, 0.8f, 0.8f, 1f);

		// Token: 0x040022D5 RID: 8917
		private Color colorCurrent;

		// Token: 0x040022D6 RID: 8918
		private Color colorAvg;

		// Token: 0x040022D7 RID: 8919
		private const string resultHeader = "BENCHMARK RESULTS";

		// Token: 0x040022D8 RID: 8920
		private const string resultLabelAvg = "AVERAGE FPS:";

		// Token: 0x040022D9 RID: 8921
		private const string resultLabelMin = "MINIMUM FPS:";

		// Token: 0x040022DA RID: 8922
		private const string resultLabelMax = "MAXIMUM FPS:";

		// Token: 0x040022DB RID: 8923
		private GUIContent resultHeaderGUI = new GUIContent("BENCHMARK RESULTS");

		// Token: 0x040022DC RID: 8924
		private GUIContent reslutLabelAvgGUI = new GUIContent("AVERAGE FPS:");

		// Token: 0x040022DD RID: 8925
		private GUIContent avgTextGUI = new GUIContent();

		// Token: 0x040022DE RID: 8926
		private GUIContent instructions = new GUIContent("PRESS SPACEBAR TO RERUN BENCHMARK | PRESS ESCAPE TO RETURN TO MENU");

		// Token: 0x040022DF RID: 8927
		private const string runLabelAvg = "Avg:";

		// Token: 0x040022E0 RID: 8928
		private const string runLabelMin = "Min:";

		// Token: 0x040022E1 RID: 8929
		private const string runLabelMax = "Max:";

		// Token: 0x040022E2 RID: 8930
		private Vector2 screenSize = new Vector2(0f, 0f);

		// Token: 0x040022E3 RID: 8931
		private MCS_FPSCounter.GUIType oldDisplayType = MCS_FPSCounter.GUIType.DisplayNothing;

		// Token: 0x020007B4 RID: 1972
		public enum GUIType
		{
			// Token: 0x04002A26 RID: 10790
			DisplayRunning,
			// Token: 0x04002A27 RID: 10791
			DisplayResults,
			// Token: 0x04002A28 RID: 10792
			DisplayNothing
		}
	}
}
