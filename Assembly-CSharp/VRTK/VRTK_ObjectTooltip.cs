using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x02000252 RID: 594
	public class VRTK_ObjectTooltip : MonoBehaviour
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001158 RID: 4440 RVA: 0x000658FC File Offset: 0x00063AFC
		// (remove) Token: 0x06001159 RID: 4441 RVA: 0x00065934 File Offset: 0x00063B34
		public event ObjectTooltipEventHandler ObjectTooltipReset;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600115A RID: 4442 RVA: 0x0006596C File Offset: 0x00063B6C
		// (remove) Token: 0x0600115B RID: 4443 RVA: 0x000659A4 File Offset: 0x00063BA4
		public event ObjectTooltipEventHandler ObjectTooltipTextUpdated;

		// Token: 0x0600115C RID: 4444 RVA: 0x000659D9 File Offset: 0x00063BD9
		public virtual void OnObjectTooltipReset(ObjectTooltipEventArgs e)
		{
			if (this.ObjectTooltipReset != null)
			{
				this.ObjectTooltipReset(this, e);
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000659F0 File Offset: 0x00063BF0
		public virtual void OnObjectTooltipTextUpdated(ObjectTooltipEventArgs e)
		{
			if (this.ObjectTooltipTextUpdated != null)
			{
				this.ObjectTooltipTextUpdated(this, e);
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00065A08 File Offset: 0x00063C08
		public virtual void ResetTooltip()
		{
			this.SetContainer();
			this.SetText("UITextFront");
			this.SetText("UITextReverse");
			this.SetLine();
			if (this.drawLineTo == null && base.transform.parent != null)
			{
				this.drawLineTo = base.transform.parent;
			}
			this.OnObjectTooltipReset(this.SetEventPayload(""));
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00065A7A File Offset: 0x00063C7A
		public virtual void UpdateText(string newText)
		{
			this.displayText = newText;
			this.OnObjectTooltipTextUpdated(this.SetEventPayload(newText));
			this.ResetTooltip();
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00065A96 File Offset: 0x00063C96
		protected virtual void OnEnable()
		{
			this.ResetTooltip();
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00065AA9 File Offset: 0x00063CA9
		protected virtual void Update()
		{
			this.DrawLine();
			if (this.alwaysFaceHeadset)
			{
				base.transform.LookAt(this.headset);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00065ACC File Offset: 0x00063CCC
		protected virtual ObjectTooltipEventArgs SetEventPayload(string newText = "")
		{
			ObjectTooltipEventArgs result;
			result.newText = newText;
			return result;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00065AE4 File Offset: 0x00063CE4
		protected virtual void SetContainer()
		{
			base.transform.Find("TooltipCanvas").GetComponent<RectTransform>().sizeDelta = this.containerSize;
			Transform transform = base.transform.Find("TooltipCanvas/UIContainer");
			transform.GetComponent<RectTransform>().sizeDelta = this.containerSize;
			transform.GetComponent<Image>().color = this.containerColor;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00065B44 File Offset: 0x00063D44
		protected virtual void SetText(string name)
		{
			Text component = base.transform.Find("TooltipCanvas/" + name).GetComponent<Text>();
			component.material = (Resources.Load("UIText") as Material);
			component.text = this.displayText.Replace("\\n", "\n");
			component.color = this.fontColor;
			component.fontSize = this.fontSize;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00065BB4 File Offset: 0x00063DB4
		protected virtual void SetLine()
		{
			this.line = base.transform.Find("Line").GetComponent<LineRenderer>();
			this.line.material = (Resources.Load("TooltipLine") as Material);
			this.line.material.color = this.lineColor;
			this.line.startColor = this.lineColor;
			this.line.endColor = this.lineColor;
			this.line.startWidth = this.lineWidth;
			this.line.endWidth = this.lineWidth;
			if (this.drawLineFrom == null)
			{
				this.drawLineFrom = base.transform;
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00065C6A File Offset: 0x00063E6A
		protected virtual void DrawLine()
		{
			if (this.drawLineTo != null)
			{
				this.line.SetPosition(0, this.drawLineFrom.position);
				this.line.SetPosition(1, this.drawLineTo.position);
			}
		}

		// Token: 0x04001029 RID: 4137
		[Tooltip("The text that is displayed on the tooltip.")]
		public string displayText;

		// Token: 0x0400102A RID: 4138
		[Tooltip("The size of the text that is displayed.")]
		public int fontSize = 14;

		// Token: 0x0400102B RID: 4139
		[Tooltip("The size of the tooltip container where `x = width` and `y = height`.")]
		public Vector2 containerSize = new Vector2(0.1f, 0.03f);

		// Token: 0x0400102C RID: 4140
		[Tooltip("An optional transform of where to start drawing the line from. If one is not provided the centre of the tooltip is used for the initial line position.")]
		public Transform drawLineFrom;

		// Token: 0x0400102D RID: 4141
		[Tooltip("A transform of another object in the scene that a line will be drawn from the tooltip to, this helps denote what the tooltip is in relation to. If no transform is provided and the tooltip is a child of another object, then the parent object's transform will be used as this destination position.")]
		public Transform drawLineTo;

		// Token: 0x0400102E RID: 4142
		[Tooltip("The width of the line drawn between the tooltip and the destination transform.")]
		public float lineWidth = 0.001f;

		// Token: 0x0400102F RID: 4143
		[Tooltip("The colour to use for the text on the tooltip.")]
		public Color fontColor = Color.black;

		// Token: 0x04001030 RID: 4144
		[Tooltip("The colour to use for the background container of the tooltip.")]
		public Color containerColor = Color.black;

		// Token: 0x04001031 RID: 4145
		[Tooltip("The colour to use for the line drawn between the tooltip and the destination transform.")]
		public Color lineColor = Color.black;

		// Token: 0x04001032 RID: 4146
		[Tooltip("If this is checked then the tooltip will be rotated so it always face the headset.")]
		public bool alwaysFaceHeadset;

		// Token: 0x04001035 RID: 4149
		protected LineRenderer line;

		// Token: 0x04001036 RID: 4150
		protected Transform headset;
	}
}
