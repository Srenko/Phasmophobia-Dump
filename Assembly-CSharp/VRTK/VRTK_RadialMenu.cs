using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x0200025A RID: 602
	[ExecuteInEditMode]
	public class VRTK_RadialMenu : MonoBehaviour
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060011BE RID: 4542 RVA: 0x00066D08 File Offset: 0x00064F08
		// (remove) Token: 0x060011BF RID: 4543 RVA: 0x00066D40 File Offset: 0x00064F40
		public event HapticPulseEventHandler FireHapticPulse;

		// Token: 0x060011C0 RID: 4544 RVA: 0x00066D75 File Offset: 0x00064F75
		public virtual void HoverButton(float angle)
		{
			this.InteractButton(angle, VRTK_RadialMenu.ButtonEvent.hoverOn);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00066D7F File Offset: 0x00064F7F
		public virtual void ClickButton(float angle)
		{
			this.InteractButton(angle, VRTK_RadialMenu.ButtonEvent.click);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00066D89 File Offset: 0x00064F89
		public virtual void UnClickButton(float angle)
		{
			this.InteractButton(angle, VRTK_RadialMenu.ButtonEvent.unclick);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00066D93 File Offset: 0x00064F93
		public virtual void ToggleMenu()
		{
			if (this.isShown)
			{
				this.HideMenu(true);
				return;
			}
			this.ShowMenu();
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00066DAC File Offset: 0x00064FAC
		public virtual void StopTouching()
		{
			if (this.currentHover != -1)
			{
				PointerEventData eventData = new PointerEventData(EventSystem.current);
				ExecuteEvents.Execute<IPointerExitHandler>(this.menuButtons[this.currentHover], eventData, ExecuteEvents.pointerExitHandler);
				this.buttons[this.currentHover].OnHoverExit.Invoke();
				this.currentHover = -1;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00066E0C File Offset: 0x0006500C
		public virtual void ShowMenu()
		{
			if (!this.isShown)
			{
				this.isShown = true;
				base.StopCoroutine("TweenMenuScale");
				base.StartCoroutine("TweenMenuScale", this.isShown);
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00066E3F File Offset: 0x0006503F
		public virtual VRTK_RadialMenu.RadialMenuButton GetButton(int id)
		{
			if (id < this.buttons.Count)
			{
				return this.buttons[id];
			}
			return null;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00066E5D File Offset: 0x0006505D
		public virtual void HideMenu(bool force)
		{
			if (this.isShown && (this.hideOnRelease || force))
			{
				this.isShown = false;
				base.StopCoroutine("TweenMenuScale");
				base.StartCoroutine("TweenMenuScale", this.isShown);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00066E9C File Offset: 0x0006509C
		public void RegenerateButtons()
		{
			this.RemoveAllButtons();
			for (int i = 0; i < this.buttons.Count; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.buttonPrefab);
				gameObject.transform.SetParent(base.transform);
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
				gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
				UICircle component = gameObject.GetComponent<UICircle>();
				if (this.buttonThickness == 1f)
				{
					component.fill = true;
				}
				else
				{
					component.thickness = (int)(this.buttonThickness * (base.GetComponent<RectTransform>().rect.width / 2f));
				}
				int fillPercent = (int)(100f / (float)this.buttons.Count);
				component.fillPercent = fillPercent;
				component.color = this.buttonColor;
				float num = (float)(360 / this.buttons.Count * i) + this.offsetRotation;
				gameObject.transform.localEulerAngles = new Vector3(0f, 0f, num);
				gameObject.layer = 4;
				gameObject.transform.localPosition = Vector3.zero;
				if (component.fillPercent < 55)
				{
					float f = num * 3.14159274f / 180f;
					Vector2 v = new Vector2(-Mathf.Cos(f), -Mathf.Sin(f));
					gameObject.transform.localPosition += v * this.offsetDistance;
				}
				GameObject gameObject2 = gameObject.GetComponentInChildren<RadialButtonIcon>().gameObject;
				if (this.buttons[i].ButtonIcon == null)
				{
					gameObject2.SetActive(false);
				}
				else
				{
					gameObject2.GetComponent<Image>().sprite = this.buttons[i].ButtonIcon;
					gameObject2.transform.localPosition = new Vector2(-1f * (gameObject.GetComponent<RectTransform>().rect.width / 2f - (float)component.thickness / 2f), 0f);
					float a = (float)Mathf.Abs(component.thickness);
					float num2 = Mathf.Abs(gameObject2.transform.localPosition.x);
					float num3 = 359f * (float)component.fillPercent * 0.01f * 3.14159274f / 180f;
					float b = num2 * 2f * Mathf.Sin(num3 / 2f);
					if (component.fillPercent > 24)
					{
						b = float.MaxValue;
					}
					float num4 = Mathf.Min(a, b) - this.iconMargin;
					gameObject2.GetComponent<RectTransform>().sizeDelta = new Vector2(num4, num4);
					if (!this.rotateIcons)
					{
						gameObject2.transform.eulerAngles = base.GetComponentInParent<Canvas>().transform.eulerAngles;
					}
				}
				this.menuButtons.Add(gameObject);
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00067185 File Offset: 0x00065385
		public void AddButton(VRTK_RadialMenu.RadialMenuButton newButton)
		{
			this.buttons.Add(newButton);
			this.RegenerateButtons();
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00067199 File Offset: 0x00065399
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				if (!this.isShown)
				{
					base.transform.localScale = Vector3.zero;
				}
				if (this.generateOnAwake)
				{
					this.RegenerateButtons();
				}
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000671C8 File Offset: 0x000653C8
		protected virtual void Update()
		{
			if (this.currentPress != -1)
			{
				this.buttons[this.currentPress].OnHold.Invoke();
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000671F0 File Offset: 0x000653F0
		protected virtual void InteractButton(float angle, VRTK_RadialMenu.ButtonEvent evt)
		{
			float num = 360f / (float)this.buttons.Count;
			angle = VRTK_SharedMethods.Mod(angle + -this.offsetRotation, 360f);
			int num2 = (int)VRTK_SharedMethods.Mod((angle + num / 2f) / num, (float)this.buttons.Count);
			PointerEventData eventData = new PointerEventData(EventSystem.current);
			if (this.currentHover != num2 && this.currentHover != -1)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(this.menuButtons[this.currentHover], eventData, ExecuteEvents.pointerUpHandler);
				ExecuteEvents.Execute<IPointerExitHandler>(this.menuButtons[this.currentHover], eventData, ExecuteEvents.pointerExitHandler);
				this.buttons[this.currentHover].OnHoverExit.Invoke();
				if (this.executeOnUnclick && this.currentPress != -1)
				{
					ExecuteEvents.Execute<IPointerDownHandler>(this.menuButtons[num2], eventData, ExecuteEvents.pointerDownHandler);
					this.AttempHapticPulse(this.baseHapticStrength * 1.666f);
				}
			}
			if (evt == VRTK_RadialMenu.ButtonEvent.click)
			{
				ExecuteEvents.Execute<IPointerDownHandler>(this.menuButtons[num2], eventData, ExecuteEvents.pointerDownHandler);
				this.currentPress = num2;
				if (!this.executeOnUnclick)
				{
					this.buttons[num2].OnClick.Invoke();
					this.AttempHapticPulse(this.baseHapticStrength * 2.5f);
				}
			}
			else if (evt == VRTK_RadialMenu.ButtonEvent.unclick)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(this.menuButtons[num2], eventData, ExecuteEvents.pointerUpHandler);
				this.currentPress = -1;
				if (this.executeOnUnclick)
				{
					this.AttempHapticPulse(this.baseHapticStrength * 2.5f);
					this.buttons[num2].OnClick.Invoke();
				}
			}
			else if (evt == VRTK_RadialMenu.ButtonEvent.hoverOn && this.currentHover != num2)
			{
				ExecuteEvents.Execute<IPointerEnterHandler>(this.menuButtons[num2], eventData, ExecuteEvents.pointerEnterHandler);
				this.buttons[num2].OnHoverEnter.Invoke();
				this.AttempHapticPulse(this.baseHapticStrength);
			}
			this.currentHover = num2;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000673F3 File Offset: 0x000655F3
		protected virtual IEnumerator TweenMenuScale(bool show)
		{
			float targetScale = 0f;
			Vector3 Dir = -1f * Vector3.one;
			if (show)
			{
				targetScale = 1f;
				Dir = Vector3.one;
			}
			int i = 0;
			while (i < 250 && ((show && base.transform.localScale.x < targetScale) || (!show && base.transform.localScale.x > targetScale)))
			{
				base.transform.localScale += Dir * Time.deltaTime * 4f;
				yield return true;
				int num = i;
				i = num + 1;
			}
			base.transform.localScale = Dir * targetScale;
			base.StopCoroutine("TweenMenuScale");
			yield break;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00067409 File Offset: 0x00065609
		protected virtual void AttempHapticPulse(float strength)
		{
			if (strength > 0f && this.FireHapticPulse != null)
			{
				this.FireHapticPulse(strength);
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00067428 File Offset: 0x00065628
		protected virtual void RemoveAllButtons()
		{
			if (this.menuButtons == null)
			{
				this.menuButtons = new List<GameObject>();
			}
			for (int i = 0; i < this.menuButtons.Count; i++)
			{
				Object.DestroyImmediate(this.menuButtons[i]);
			}
			this.menuButtons = new List<GameObject>();
		}

		// Token: 0x04001061 RID: 4193
		[Tooltip("An array of Buttons that define the interactive buttons required to be displayed as part of the radial menu.")]
		public List<VRTK_RadialMenu.RadialMenuButton> buttons;

		// Token: 0x04001062 RID: 4194
		[Tooltip("The base for each button in the menu, by default set to a dynamic circle arc that will fill up a portion of the menu.")]
		public GameObject buttonPrefab;

		// Token: 0x04001063 RID: 4195
		[Tooltip("If checked, then the buttons will be auto generated on awake.")]
		public bool generateOnAwake = true;

		// Token: 0x04001064 RID: 4196
		[Tooltip("Percentage of the menu the buttons should fill, 1.0 is a pie slice, 0.1 is a thin ring.")]
		[Range(0f, 1f)]
		public float buttonThickness = 0.5f;

		// Token: 0x04001065 RID: 4197
		[Tooltip("The background colour of the buttons, default is white.")]
		public Color buttonColor = Color.white;

		// Token: 0x04001066 RID: 4198
		[Tooltip("The distance the buttons should move away from the centre. This creates space between the individual buttons.")]
		public float offsetDistance = 1f;

		// Token: 0x04001067 RID: 4199
		[Tooltip("The additional rotation of the Radial Menu.")]
		[Range(0f, 359f)]
		public float offsetRotation;

		// Token: 0x04001068 RID: 4200
		[Tooltip("Whether button icons should rotate according to their arc or be vertical compared to the controller.")]
		public bool rotateIcons;

		// Token: 0x04001069 RID: 4201
		[Tooltip("The margin in pixels that the icon should keep within the button.")]
		public float iconMargin;

		// Token: 0x0400106A RID: 4202
		[Tooltip("Whether the buttons are shown")]
		public bool isShown;

		// Token: 0x0400106B RID: 4203
		[Tooltip("Whether the buttons should be visible when not in use.")]
		public bool hideOnRelease;

		// Token: 0x0400106C RID: 4204
		[Tooltip("Whether the button action should happen when the button is released, as opposed to happening immediately when the button is pressed.")]
		public bool executeOnUnclick;

		// Token: 0x0400106D RID: 4205
		[Tooltip("The base strength of the haptic pulses when the selected button is changed, or a button is pressed. Set to zero to disable.")]
		[Range(0f, 1f)]
		public float baseHapticStrength;

		// Token: 0x0400106F RID: 4207
		[Tooltip("The actual GameObjects that make up the radial menu.")]
		public List<GameObject> menuButtons;

		// Token: 0x04001070 RID: 4208
		protected int currentHover = -1;

		// Token: 0x04001071 RID: 4209
		protected int currentPress = -1;

		// Token: 0x020005B8 RID: 1464
		[Serializable]
		public class RadialMenuButton
		{
			// Token: 0x040026F3 RID: 9971
			public Sprite ButtonIcon;

			// Token: 0x040026F4 RID: 9972
			public UnityEvent OnClick = new UnityEvent();

			// Token: 0x040026F5 RID: 9973
			public UnityEvent OnHold = new UnityEvent();

			// Token: 0x040026F6 RID: 9974
			public UnityEvent OnHoverEnter = new UnityEvent();

			// Token: 0x040026F7 RID: 9975
			public UnityEvent OnHoverExit = new UnityEvent();
		}

		// Token: 0x020005B9 RID: 1465
		public enum ButtonEvent
		{
			// Token: 0x040026F9 RID: 9977
			hoverOn,
			// Token: 0x040026FA RID: 9978
			hoverOff,
			// Token: 0x040026FB RID: 9979
			click,
			// Token: 0x040026FC RID: 9980
			unclick
		}
	}
}
