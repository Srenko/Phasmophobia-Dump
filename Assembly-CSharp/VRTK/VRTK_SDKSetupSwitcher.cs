using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x0200030F RID: 783
	public class VRTK_SDKSetupSwitcher : MonoBehaviour
	{
		// Token: 0x06001B73 RID: 7027 RVA: 0x0008F76A File Offset: 0x0008D96A
		protected virtual void Awake()
		{
			this.fallbackCamera.gameObject.SetActive(false);
			this.eventSystem.gameObject.SetActive(false);
			this.chooseButton.gameObject.SetActive(false);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0008F7A0 File Offset: 0x0008D9A0
		protected virtual void OnEnable()
		{
			this.sdkManager = VRTK_SDKManager.instance;
			this.sdkManager.LoadedSetupChanged += this.OnLoadedSetupChanged;
			this.switchButton.onClick.AddListener(new UnityAction(this.OnSwitchButtonClick));
			this.cancelButton.onClick.AddListener(new UnityAction(this.OnCancelButtonClick));
			this.Show(VRTK_SDKSetupSwitcher.ViewingState.Status);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0008F814 File Offset: 0x0008DA14
		protected virtual void OnDisable()
		{
			this.sdkManager.LoadedSetupChanged -= this.OnLoadedSetupChanged;
			this.switchButton.onClick.RemoveListener(new UnityAction(this.OnSwitchButtonClick));
			this.cancelButton.onClick.RemoveListener(new UnityAction(this.OnCancelButtonClick));
			this.Show(VRTK_SDKSetupSwitcher.ViewingState.Status);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0008F87A File Offset: 0x0008DA7A
		protected virtual void OnLoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
		{
			this.Show(VRTK_SDKSetupSwitcher.ViewingState.Status);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0008F883 File Offset: 0x0008DA83
		protected virtual void OnSwitchButtonClick()
		{
			this.Show(VRTK_SDKSetupSwitcher.ViewingState.Selection);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0008F87A File Offset: 0x0008DA7A
		protected virtual void OnCancelButtonClick()
		{
			this.Show(VRTK_SDKSetupSwitcher.ViewingState.Status);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0008F88C File Offset: 0x0008DA8C
		protected virtual void Show(VRTK_SDKSetupSwitcher.ViewingState viewingState)
		{
			if (viewingState != VRTK_SDKSetupSwitcher.ViewingState.Status)
			{
				if (viewingState != VRTK_SDKSetupSwitcher.ViewingState.Selection)
				{
					VRTK_Logger.Fatal(new ArgumentOutOfRangeException("viewingState", viewingState, null));
					return;
				}
				this.AddSelectionButtons();
				this.selectionPanel.gameObject.SetActive(true);
				this.statusPanel.gameObject.SetActive(false);
			}
			else
			{
				this.RemoveCreatedChooseButtons();
				this.UpdateCurrentText();
				this.selectionPanel.gameObject.SetActive(false);
				this.statusPanel.gameObject.SetActive(true);
			}
			this.fallbackCamera.gameObject.SetActive(VRTK_DeviceFinder.HeadsetCamera() == null);
			this.eventSystem.gameObject.SetActive(EventSystem.current == null || EventSystem.current == this.eventSystem);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0008F960 File Offset: 0x0008DB60
		protected virtual void UpdateCurrentText()
		{
			VRTK_SDKSetup loadedSetup = this.sdkManager.loadedSetup;
			this.currentText.text = ((loadedSetup == null) ? "None" : loadedSetup.name);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0008F99C File Offset: 0x0008DB9C
		protected virtual void AddSelectionButtons()
		{
			VRTK_SDKSetup loadedSetup = this.sdkManager.loadedSetup;
			if (loadedSetup != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.chooseButton.gameObject, this.chooseButton.transform.parent);
				gameObject.GetComponentInChildren<Text>().text = "None";
				gameObject.name = "ChooseNoneButton";
				gameObject.SetActive(true);
				gameObject.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.sdkManager.UnloadSDKSetup(true);
				});
				this.chooseButtonGameObjects.Add(gameObject);
			}
			VRTK_SDKSetup[] setups = this.sdkManager.setups;
			for (int i = 0; i < setups.Length; i++)
			{
				VRTK_SDKSetup vrtk_SDKSetup = setups[i];
				if (!(vrtk_SDKSetup == null) && !(vrtk_SDKSetup == loadedSetup))
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.chooseButton.gameObject, this.chooseButton.transform.parent);
					gameObject2.GetComponentInChildren<Text>().text = vrtk_SDKSetup.name;
					gameObject2.name = string.Format("Choose{0}Button", vrtk_SDKSetup.name);
					gameObject2.SetActive(true);
					int indexCopy = i;
					Button component = gameObject2.GetComponent<Button>();
					component.onClick.AddListener(delegate()
					{
						this.sdkManager.TryLoadSDKSetup(indexCopy, true, setups);
					});
					ColorBlock colors = component.colors;
					colors.colorMultiplier = (vrtk_SDKSetup.isValid ? 1f : 0.8f);
					component.colors = colors;
					this.chooseButtonGameObjects.Add(gameObject2);
				}
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0008FB4D File Offset: 0x0008DD4D
		protected virtual void RemoveCreatedChooseButtons()
		{
			this.chooseButtonGameObjects.ForEach(new Action<GameObject>(Object.Destroy));
			this.chooseButtonGameObjects.Clear();
		}

		// Token: 0x04001617 RID: 5655
		[Header("Fallback Objects")]
		[SerializeField]
		private Camera fallbackCamera;

		// Token: 0x04001618 RID: 5656
		[SerializeField]
		private EventSystem eventSystem;

		// Token: 0x04001619 RID: 5657
		[Header("Object References")]
		[SerializeField]
		private Text currentText;

		// Token: 0x0400161A RID: 5658
		[SerializeField]
		private RectTransform statusPanel;

		// Token: 0x0400161B RID: 5659
		[SerializeField]
		private RectTransform selectionPanel;

		// Token: 0x0400161C RID: 5660
		[SerializeField]
		private Button switchButton;

		// Token: 0x0400161D RID: 5661
		[SerializeField]
		private Button cancelButton;

		// Token: 0x0400161E RID: 5662
		[SerializeField]
		private Button chooseButton;

		// Token: 0x0400161F RID: 5663
		private VRTK_SDKManager sdkManager;

		// Token: 0x04001620 RID: 5664
		private readonly List<GameObject> chooseButtonGameObjects = new List<GameObject>();

		// Token: 0x02000609 RID: 1545
		protected enum ViewingState
		{
			// Token: 0x04002872 RID: 10354
			Status,
			// Token: 0x04002873 RID: 10355
			Selection
		}
	}
}
