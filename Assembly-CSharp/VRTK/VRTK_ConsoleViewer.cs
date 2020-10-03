using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x02000245 RID: 581
	public class VRTK_ConsoleViewer : MonoBehaviour
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x000636A7 File Offset: 0x000618A7
		public void SetCollapse(bool state)
		{
			this.collapseLog = state;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000636B0 File Offset: 0x000618B0
		public void ClearLog()
		{
			this.consoleOutput.text = "";
			this.currentBuffer = 0;
			this.lastMessage = "";
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000636D4 File Offset: 0x000618D4
		protected virtual void Awake()
		{
			this.logTypeColors = new Dictionary<LogType, Color>
			{
				{
					LogType.Assert,
					this.assertMessage
				},
				{
					LogType.Error,
					this.errorMessage
				},
				{
					LogType.Exception,
					this.exceptionMessage
				},
				{
					LogType.Log,
					this.infoMessage
				},
				{
					LogType.Warning,
					this.warningMessage
				}
			};
			this.scrollWindow = base.transform.Find("Panel/Scroll View").GetComponent<ScrollRect>();
			this.consoleRect = base.transform.Find("Panel/Scroll View/Viewport/Content").GetComponent<RectTransform>();
			this.consoleOutput = base.transform.Find("Panel/Scroll View/Viewport/Content/ConsoleOutput").GetComponent<Text>();
			this.consoleOutput.fontSize = this.fontSize;
			this.ClearLog();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00063795 File Offset: 0x00061995
		protected virtual void OnEnable()
		{
			Application.logMessageReceived += this.HandleLog;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000637A8 File Offset: 0x000619A8
		protected virtual void OnDisable()
		{
			Application.logMessageReceived -= this.HandleLog;
			this.consoleRect.sizeDelta = Vector2.zero;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000637CC File Offset: 0x000619CC
		private string GetMessage(string message, LogType type)
		{
			string text = ColorUtility.ToHtmlStringRGBA(this.logTypeColors[type]);
			return string.Concat(new string[]
			{
				"<color=#",
				text,
				">",
				message,
				"</color>\n"
			});
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00063818 File Offset: 0x00061A18
		private void HandleLog(string message, string stackTrace, LogType type)
		{
			string message2 = this.GetMessage(message, type);
			if (!this.collapseLog || this.lastMessage != message2)
			{
				Text text = this.consoleOutput;
				text.text += message2;
				this.lastMessage = message2;
			}
			this.consoleRect.sizeDelta = new Vector2(this.consoleOutput.preferredWidth, this.consoleOutput.preferredHeight);
			this.scrollWindow.verticalNormalizedPosition = 0f;
			this.currentBuffer++;
			if (this.currentBuffer >= this.lineBuffer)
			{
				IEnumerable<string> source = Regex.Split(this.consoleOutput.text, "\n").Skip(this.lineBuffer / 2);
				this.consoleOutput.text = string.Join("\n", source.ToArray<string>());
				this.currentBuffer = this.lineBuffer / 2;
			}
		}

		// Token: 0x04000FC4 RID: 4036
		[Tooltip("The size of the font the log text is displayed in.")]
		public int fontSize = 14;

		// Token: 0x04000FC5 RID: 4037
		[Tooltip("The colour of the text for an info log message.")]
		public Color infoMessage = Color.black;

		// Token: 0x04000FC6 RID: 4038
		[Tooltip("The colour of the text for an assertion log message.")]
		public Color assertMessage = Color.black;

		// Token: 0x04000FC7 RID: 4039
		[Tooltip("The colour of the text for a warning log message.")]
		public Color warningMessage = Color.yellow;

		// Token: 0x04000FC8 RID: 4040
		[Tooltip("The colour of the text for an error log message.")]
		public Color errorMessage = Color.red;

		// Token: 0x04000FC9 RID: 4041
		[Tooltip("The colour of the text for an exception log message.")]
		public Color exceptionMessage = Color.red;

		// Token: 0x04000FCA RID: 4042
		private Dictionary<LogType, Color> logTypeColors;

		// Token: 0x04000FCB RID: 4043
		private ScrollRect scrollWindow;

		// Token: 0x04000FCC RID: 4044
		private RectTransform consoleRect;

		// Token: 0x04000FCD RID: 4045
		private Text consoleOutput;

		// Token: 0x04000FCE RID: 4046
		private const string NEWLINE = "\n";

		// Token: 0x04000FCF RID: 4047
		private int lineBuffer = 50;

		// Token: 0x04000FD0 RID: 4048
		private int currentBuffer;

		// Token: 0x04000FD1 RID: 4049
		private string lastMessage;

		// Token: 0x04000FD2 RID: 4050
		private bool collapseLog;
	}
}
