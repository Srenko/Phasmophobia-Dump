using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRTK.Examples
{
	// Token: 0x0200036A RID: 874
	public class UI_Interactions : MonoBehaviour
	{
		// Token: 0x06001E0E RID: 7694 RVA: 0x00098A45 File Offset: 0x00096C45
		public void Button_Red()
		{
			VRTK_Logger.Info("Red Button Clicked");
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00098A51 File Offset: 0x00096C51
		public void Button_Pink()
		{
			VRTK_Logger.Info("Pink Button Clicked");
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00098A5D File Offset: 0x00096C5D
		public void Toggle(bool state)
		{
			VRTK_Logger.Info("The toggle state is " + (state ? "on" : "off"));
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00098A7D File Offset: 0x00096C7D
		public void Dropdown(int value)
		{
			VRTK_Logger.Info("Dropdown option selected was ID " + value);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00098A94 File Offset: 0x00096C94
		public void SetDropText(BaseEventData data)
		{
			PointerEventData pointerEventData = data as PointerEventData;
			GameObject gameObject = GameObject.Find("ActionText");
			if (gameObject)
			{
				gameObject.GetComponent<Text>().text = pointerEventData.pointerDrag.name + " Dropped On " + pointerEventData.pointerEnter.name;
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00098AE6 File Offset: 0x00096CE6
		public void CreateCanvas()
		{
			base.StartCoroutine(this.CreateCanvasOnNextFrame());
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00098AF5 File Offset: 0x00096CF5
		private IEnumerator CreateCanvasOnNextFrame()
		{
			yield return null;
			int num = Object.FindObjectsOfType<Canvas>().Length - 4;
			GameObject gameObject = new GameObject("TempCanvas");
			gameObject.layer = 5;
			RectTransform component = gameObject.AddComponent<Canvas>().GetComponent<RectTransform>();
			component.position = new Vector3(-4f, 2f, 3f + (float)num);
			component.sizeDelta = new Vector2(300f, 400f);
			component.localScale = new Vector3(0.005f, 0.005f, 0.005f);
			component.eulerAngles = new Vector3(0f, 270f, 0f);
			GameObject gameObject2 = new GameObject("TempButton", new Type[]
			{
				typeof(RectTransform)
			});
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.layer = 5;
			RectTransform component2 = gameObject2.GetComponent<RectTransform>();
			component2.position = new Vector3(0f, 0f, 0f);
			component2.anchoredPosition = new Vector3(0f, 0f, 0f);
			component2.localPosition = new Vector3(0f, 0f, 0f);
			component2.sizeDelta = new Vector2(180f, 60f);
			component2.localScale = new Vector3(1f, 1f, 1f);
			component2.localEulerAngles = new Vector3(0f, 0f, 0f);
			gameObject2.AddComponent<Image>();
			Button button = gameObject2.AddComponent<Button>();
			ColorBlock colors = button.colors;
			colors.highlightedColor = Color.red;
			button.colors = colors;
			GameObject gameObject3 = new GameObject("BtnText", new Type[]
			{
				typeof(RectTransform)
			});
			gameObject3.transform.SetParent(gameObject2.transform);
			gameObject3.layer = 5;
			RectTransform component3 = gameObject3.GetComponent<RectTransform>();
			component3.position = new Vector3(0f, 0f, 0f);
			component3.anchoredPosition = new Vector3(0f, 0f, 0f);
			component3.localPosition = new Vector3(0f, 0f, 0f);
			component3.sizeDelta = new Vector2(180f, 60f);
			component3.localScale = new Vector3(1f, 1f, 1f);
			component3.localEulerAngles = new Vector3(0f, 0f, 0f);
			Text text = gameObject3.AddComponent<Text>();
			text.text = "New Button";
			text.color = Color.black;
			text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
			gameObject.AddComponent<VRTK_UICanvas>();
			yield break;
		}

		// Token: 0x040017B0 RID: 6064
		private const int EXISTING_CANVAS_COUNT = 4;
	}
}
