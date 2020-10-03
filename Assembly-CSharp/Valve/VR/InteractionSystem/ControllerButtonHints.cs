using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200043F RID: 1087
	public class ControllerButtonHints : MonoBehaviour
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x000A4295 File Offset: 0x000A2495
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x000A429D File Offset: 0x000A249D
		public bool initialized { get; private set; }

		// Token: 0x06002160 RID: 8544 RVA: 0x000A42A6 File Offset: 0x000A24A6
		private void Awake()
		{
			this.renderModelLoadedAction = SteamVR_Events.RenderModelLoadedAction(new UnityAction<SteamVR_RenderModel, bool>(this.OnRenderModelLoaded));
			this.colorID = Shader.PropertyToID("_Color");
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000A42CF File Offset: 0x000A24CF
		private void Start()
		{
			this.player = Player.instance;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000A42DC File Offset: 0x000A24DC
		private void HintDebugLog(string msg)
		{
			if (this.debugHints)
			{
				Debug.Log("Hints: " + msg);
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000A42F6 File Offset: 0x000A24F6
		private void OnEnable()
		{
			this.renderModelLoadedAction.enabled = true;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000A4304 File Offset: 0x000A2504
		private void OnDisable()
		{
			this.renderModelLoadedAction.enabled = false;
			this.Clear();
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000A4318 File Offset: 0x000A2518
		private void OnParentHandInputFocusLost()
		{
			this.HideAllButtonHints();
			this.HideAllText();
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000A4328 File Offset: 0x000A2528
		private void OnHandInitialized(int deviceIndex)
		{
			this.renderModel = new GameObject("SteamVR_RenderModel").AddComponent<SteamVR_RenderModel>();
			this.renderModel.transform.parent = base.transform;
			this.renderModel.transform.localPosition = Vector3.zero;
			this.renderModel.transform.localRotation = Quaternion.identity;
			this.renderModel.transform.localScale = Vector3.one;
			this.renderModel.SetDeviceIndex(deviceIndex);
			if (!this.initialized)
			{
				this.renderModel.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000A43C4 File Offset: 0x000A25C4
		private void OnRenderModelLoaded(SteamVR_RenderModel renderModel, bool succeess)
		{
			if (renderModel == this.renderModel)
			{
				this.textHintParent = new GameObject("Text Hints").transform;
				this.textHintParent.SetParent(base.transform);
				this.textHintParent.localPosition = Vector3.zero;
				this.textHintParent.localRotation = Quaternion.identity;
				this.textHintParent.localScale = Vector3.one;
				using (SteamVR_RenderModel.RenderModelInterfaceHolder renderModelInterfaceHolder = new SteamVR_RenderModel.RenderModelInterfaceHolder())
				{
					CVRRenderModels instance = renderModelInterfaceHolder.instance;
					if (instance != null)
					{
						string text = "Components for render model " + renderModel.index;
						foreach (object obj in renderModel.transform)
						{
							Transform transform = (Transform)obj;
							ulong componentButtonMask = instance.GetComponentButtonMask(renderModel.renderModelName, transform.name);
							this.componentButtonMasks.Add(new KeyValuePair<string, ulong>(transform.name, componentButtonMask));
							text = string.Concat(new object[]
							{
								text,
								"\n\t",
								transform.name,
								": ",
								componentButtonMask
							});
						}
						this.HintDebugLog(text);
					}
				}
				this.buttonHintInfos = new Dictionary<EVRButtonId, ControllerButtonHints.ButtonHintInfo>();
				this.CreateAndAddButtonInfo(EVRButtonId.k_EButton_Axis1);
				this.CreateAndAddButtonInfo(EVRButtonId.k_EButton_ApplicationMenu);
				this.CreateAndAddButtonInfo(EVRButtonId.k_EButton_System);
				this.CreateAndAddButtonInfo(EVRButtonId.k_EButton_Grip);
				this.CreateAndAddButtonInfo(EVRButtonId.k_EButton_Axis0);
				this.CreateAndAddButtonInfo(EVRButtonId.k_EButton_A);
				this.ComputeTextEndTransforms();
				this.initialized = true;
				renderModel.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000A457C File Offset: 0x000A277C
		private void CreateAndAddButtonInfo(EVRButtonId buttonID)
		{
			Transform transform = null;
			List<MeshRenderer> list = new List<MeshRenderer>();
			string text = "Looking for button: " + buttonID;
			EVRButtonId evrbuttonId = buttonID;
			if (buttonID == EVRButtonId.k_EButton_Grip && SteamVR.instance.hmd_TrackingSystemName.ToLowerInvariant().Contains("oculus"))
			{
				evrbuttonId = EVRButtonId.k_EButton_Axis2;
			}
			ulong num = 1UL << (int)evrbuttonId;
			foreach (KeyValuePair<string, ulong> keyValuePair in this.componentButtonMasks)
			{
				if ((keyValuePair.Value & num) == num)
				{
					text = string.Concat(new object[]
					{
						text,
						"\nFound component: ",
						keyValuePair.Key,
						" ",
						keyValuePair.Value
					});
					Transform transform2 = this.renderModel.FindComponent(keyValuePair.Key);
					transform = transform2;
					text = string.Concat(new object[]
					{
						text,
						"\nFound componentTransform: ",
						transform2,
						" buttonTransform: ",
						transform
					});
					list.AddRange(transform2.GetComponentsInChildren<MeshRenderer>());
				}
			}
			text = string.Concat(new object[]
			{
				text,
				"\nFound ",
				list.Count,
				" renderers for ",
				buttonID
			});
			foreach (MeshRenderer meshRenderer in list)
			{
				text = text + "\n\t" + meshRenderer.name;
			}
			this.HintDebugLog(text);
			if (transform == null)
			{
				this.HintDebugLog("Couldn't find buttonTransform for " + buttonID);
				return;
			}
			ControllerButtonHints.ButtonHintInfo buttonHintInfo = new ControllerButtonHints.ButtonHintInfo();
			this.buttonHintInfos.Add(buttonID, buttonHintInfo);
			buttonHintInfo.componentName = transform.name;
			buttonHintInfo.renderers = list;
			buttonHintInfo.localTransform = transform.Find("attach");
			ControllerButtonHints.OffsetType offsetType = ControllerButtonHints.OffsetType.Right;
			switch (buttonID)
			{
			case EVRButtonId.k_EButton_System:
				offsetType = ControllerButtonHints.OffsetType.Right;
				break;
			case EVRButtonId.k_EButton_ApplicationMenu:
				offsetType = ControllerButtonHints.OffsetType.Right;
				break;
			case EVRButtonId.k_EButton_Grip:
				offsetType = ControllerButtonHints.OffsetType.Forward;
				break;
			default:
				if (buttonID != EVRButtonId.k_EButton_Axis0)
				{
					if (buttonID == EVRButtonId.k_EButton_Axis1)
					{
						offsetType = ControllerButtonHints.OffsetType.Right;
					}
				}
				else
				{
					offsetType = ControllerButtonHints.OffsetType.Up;
				}
				break;
			}
			switch (offsetType)
			{
			case ControllerButtonHints.OffsetType.Up:
				buttonHintInfo.textEndOffsetDir = buttonHintInfo.localTransform.up;
				break;
			case ControllerButtonHints.OffsetType.Right:
				buttonHintInfo.textEndOffsetDir = buttonHintInfo.localTransform.right;
				break;
			case ControllerButtonHints.OffsetType.Forward:
				buttonHintInfo.textEndOffsetDir = buttonHintInfo.localTransform.forward;
				break;
			case ControllerButtonHints.OffsetType.Back:
				buttonHintInfo.textEndOffsetDir = -buttonHintInfo.localTransform.forward;
				break;
			}
			Vector3 position = buttonHintInfo.localTransform.position + buttonHintInfo.localTransform.forward * 0.01f;
			buttonHintInfo.textHintObject = Object.Instantiate<GameObject>(this.textHintPrefab, position, Quaternion.identity);
			buttonHintInfo.textHintObject.name = "Hint_" + buttonHintInfo.componentName + "_Start";
			buttonHintInfo.textHintObject.transform.SetParent(this.textHintParent);
			buttonHintInfo.textHintObject.layer = base.gameObject.layer;
			buttonHintInfo.textHintObject.tag = base.gameObject.tag;
			buttonHintInfo.textStartAnchor = buttonHintInfo.textHintObject.transform.Find("Start");
			buttonHintInfo.textEndAnchor = buttonHintInfo.textHintObject.transform.Find("End");
			buttonHintInfo.canvasOffset = buttonHintInfo.textHintObject.transform.Find("CanvasOffset");
			buttonHintInfo.line = buttonHintInfo.textHintObject.transform.Find("Line").GetComponent<LineRenderer>();
			buttonHintInfo.textCanvas = buttonHintInfo.textHintObject.GetComponentInChildren<Canvas>();
			buttonHintInfo.text = buttonHintInfo.textCanvas.GetComponentInChildren<Text>();
			buttonHintInfo.textMesh = buttonHintInfo.textCanvas.GetComponentInChildren<TextMesh>();
			buttonHintInfo.textHintObject.SetActive(false);
			buttonHintInfo.textStartAnchor.position = position;
			if (buttonHintInfo.text != null)
			{
				buttonHintInfo.text.text = buttonHintInfo.componentName;
			}
			if (buttonHintInfo.textMesh != null)
			{
				buttonHintInfo.textMesh.text = buttonHintInfo.componentName;
			}
			this.centerPosition += buttonHintInfo.textStartAnchor.position;
			buttonHintInfo.textCanvas.transform.localScale = Vector3.Scale(buttonHintInfo.textCanvas.transform.localScale, this.player.transform.localScale);
			buttonHintInfo.textStartAnchor.transform.localScale = Vector3.Scale(buttonHintInfo.textStartAnchor.transform.localScale, this.player.transform.localScale);
			buttonHintInfo.textEndAnchor.transform.localScale = Vector3.Scale(buttonHintInfo.textEndAnchor.transform.localScale, this.player.transform.localScale);
			buttonHintInfo.line.transform.localScale = Vector3.Scale(buttonHintInfo.line.transform.localScale, this.player.transform.localScale);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000A4AEC File Offset: 0x000A2CEC
		private void ComputeTextEndTransforms()
		{
			this.centerPosition /= (float)this.buttonHintInfos.Count;
			float num = 0f;
			foreach (KeyValuePair<EVRButtonId, ControllerButtonHints.ButtonHintInfo> keyValuePair in this.buttonHintInfos)
			{
				keyValuePair.Value.distanceFromCenter = Vector3.Distance(keyValuePair.Value.textStartAnchor.position, this.centerPosition);
				if (keyValuePair.Value.distanceFromCenter > num)
				{
					num = keyValuePair.Value.distanceFromCenter;
				}
			}
			foreach (KeyValuePair<EVRButtonId, ControllerButtonHints.ButtonHintInfo> keyValuePair2 in this.buttonHintInfos)
			{
				Vector3 vector = keyValuePair2.Value.textStartAnchor.position - this.centerPosition;
				vector.Normalize();
				vector = Vector3.Project(vector, this.renderModel.transform.forward);
				float num2 = keyValuePair2.Value.distanceFromCenter / num;
				float d = keyValuePair2.Value.distanceFromCenter * Mathf.Pow(2f, 10f * (num2 - 1f)) * 20f;
				float d2 = 0.1f;
				Vector3 position = keyValuePair2.Value.textStartAnchor.position + keyValuePair2.Value.textEndOffsetDir * d2 + vector * d * 0.1f;
				keyValuePair2.Value.textEndAnchor.position = position;
				keyValuePair2.Value.canvasOffset.position = position;
				keyValuePair2.Value.canvasOffset.localRotation = Quaternion.identity;
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000A4CFC File Offset: 0x000A2EFC
		private void ShowButtonHint(params EVRButtonId[] buttons)
		{
			this.renderModel.gameObject.SetActive(true);
			this.renderModel.GetComponentsInChildren<MeshRenderer>(this.renderers);
			for (int i = 0; i < this.renderers.Count; i++)
			{
				Texture mainTexture = this.renderers[i].material.mainTexture;
				this.renderers[i].sharedMaterial = this.controllerMaterial;
				this.renderers[i].material.mainTexture = mainTexture;
				this.renderers[i].material.renderQueue = this.controllerMaterial.shader.renderQueue;
			}
			for (int j = 0; j < buttons.Length; j++)
			{
				if (this.buttonHintInfos.ContainsKey(buttons[j]))
				{
					foreach (MeshRenderer item in this.buttonHintInfos[buttons[j]].renderers)
					{
						if (!this.flashingRenderers.Contains(item))
						{
							this.flashingRenderers.Add(item);
						}
					}
				}
			}
			this.startTime = Time.realtimeSinceStartup;
			this.tickCount = 0f;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000A4E4C File Offset: 0x000A304C
		private void HideAllButtonHints()
		{
			this.Clear();
			this.renderModel.gameObject.SetActive(false);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000A4E68 File Offset: 0x000A3068
		private void HideButtonHint(params EVRButtonId[] buttons)
		{
			Color color = this.controllerMaterial.GetColor(this.colorID);
			for (int i = 0; i < buttons.Length; i++)
			{
				if (this.buttonHintInfos.ContainsKey(buttons[i]))
				{
					foreach (MeshRenderer meshRenderer in this.buttonHintInfos[buttons[i]].renderers)
					{
						meshRenderer.material.color = color;
						this.flashingRenderers.Remove(meshRenderer);
					}
				}
			}
			if (this.flashingRenderers.Count == 0)
			{
				this.renderModel.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000A4F28 File Offset: 0x000A3128
		private bool IsButtonHintActive(EVRButtonId button)
		{
			if (this.buttonHintInfos.ContainsKey(button))
			{
				foreach (MeshRenderer item in this.buttonHintInfos[button].renderers)
				{
					if (this.flashingRenderers.Contains(item))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000A4FA4 File Offset: 0x000A31A4
		private IEnumerator TestButtonHints()
		{
			for (;;)
			{
				this.ShowButtonHint(new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
				yield return new WaitForSeconds(1f);
				this.ShowButtonHint(new EVRButtonId[]
				{
					EVRButtonId.k_EButton_ApplicationMenu
				});
				yield return new WaitForSeconds(1f);
				this.ShowButtonHint(new EVRButtonId[1]);
				yield return new WaitForSeconds(1f);
				this.ShowButtonHint(new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Grip
				});
				yield return new WaitForSeconds(1f);
				this.ShowButtonHint(new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis0
				});
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000A4FB3 File Offset: 0x000A31B3
		private IEnumerator TestTextHints()
		{
			for (;;)
			{
				this.ShowText(EVRButtonId.k_EButton_Axis1, "Trigger", true);
				yield return new WaitForSeconds(3f);
				this.ShowText(EVRButtonId.k_EButton_ApplicationMenu, "Application", true);
				yield return new WaitForSeconds(3f);
				this.ShowText(EVRButtonId.k_EButton_System, "System", true);
				yield return new WaitForSeconds(3f);
				this.ShowText(EVRButtonId.k_EButton_Grip, "Grip", true);
				yield return new WaitForSeconds(3f);
				this.ShowText(EVRButtonId.k_EButton_Axis0, "Touchpad", true);
				yield return new WaitForSeconds(3f);
				this.HideAllText();
				yield return new WaitForSeconds(3f);
			}
			yield break;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000A4FC4 File Offset: 0x000A31C4
		private void Update()
		{
			if (this.renderModel != null && this.renderModel.gameObject.activeInHierarchy && this.flashingRenderers.Count > 0)
			{
				Color color = this.controllerMaterial.GetColor(this.colorID);
				float num = (Time.realtimeSinceStartup - this.startTime) * 3.14159274f * 2f;
				num = Mathf.Cos(num);
				num = Util.RemapNumberClamped(num, -1f, 1f, 0f, 1f);
				if (Time.realtimeSinceStartup - this.startTime - this.tickCount > 1f)
				{
					this.tickCount += 1f;
					SteamVR_Controller.Device device = SteamVR_Controller.Input((int)this.renderModel.index);
					if (device != null)
					{
						device.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
					}
				}
				for (int i = 0; i < this.flashingRenderers.Count; i++)
				{
					this.flashingRenderers[i].material.SetColor(this.colorID, Color.Lerp(color, this.flashColor, num));
				}
				if (this.initialized)
				{
					foreach (KeyValuePair<EVRButtonId, ControllerButtonHints.ButtonHintInfo> keyValuePair in this.buttonHintInfos)
					{
						if (keyValuePair.Value.textHintActive)
						{
							this.UpdateTextHint(keyValuePair.Value);
						}
					}
				}
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000A5148 File Offset: 0x000A3348
		private void UpdateTextHint(ControllerButtonHints.ButtonHintInfo hintInfo)
		{
			Transform hmdTransform = this.player.hmdTransform;
			Vector3 forward = hmdTransform.position - hintInfo.canvasOffset.position;
			Quaternion a = Quaternion.LookRotation(forward, Vector3.up);
			Quaternion b = Quaternion.LookRotation(forward, hmdTransform.up);
			float t;
			if (hmdTransform.forward.y > 0f)
			{
				t = Util.RemapNumberClamped(hmdTransform.forward.y, 0.6f, 0.4f, 1f, 0f);
			}
			else
			{
				t = Util.RemapNumberClamped(hmdTransform.forward.y, -0.8f, -0.6f, 1f, 0f);
			}
			hintInfo.canvasOffset.rotation = Quaternion.Slerp(a, b, t);
			Transform transform = hintInfo.line.transform;
			hintInfo.line.useWorldSpace = false;
			hintInfo.line.SetPosition(0, transform.InverseTransformPoint(hintInfo.textStartAnchor.position));
			hintInfo.line.SetPosition(1, transform.InverseTransformPoint(hintInfo.textEndAnchor.position));
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000A5255 File Offset: 0x000A3455
		private void Clear()
		{
			this.renderers.Clear();
			this.flashingRenderers.Clear();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000A5270 File Offset: 0x000A3470
		private void ShowText(EVRButtonId button, string text, bool highlightButton = true)
		{
			if (this.buttonHintInfos.ContainsKey(button))
			{
				ControllerButtonHints.ButtonHintInfo buttonHintInfo = this.buttonHintInfos[button];
				buttonHintInfo.textHintObject.SetActive(true);
				buttonHintInfo.textHintActive = true;
				if (buttonHintInfo.text != null)
				{
					buttonHintInfo.text.text = text;
				}
				if (buttonHintInfo.textMesh != null)
				{
					buttonHintInfo.textMesh.text = text;
				}
				this.UpdateTextHint(buttonHintInfo);
				if (highlightButton)
				{
					this.ShowButtonHint(new EVRButtonId[]
					{
						button
					});
				}
				this.renderModel.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000A530A File Offset: 0x000A350A
		private void HideText(EVRButtonId button)
		{
			if (this.buttonHintInfos.ContainsKey(button))
			{
				ControllerButtonHints.ButtonHintInfo buttonHintInfo = this.buttonHintInfos[button];
				buttonHintInfo.textHintObject.SetActive(false);
				buttonHintInfo.textHintActive = false;
				this.HideButtonHint(new EVRButtonId[]
				{
					button
				});
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000A5348 File Offset: 0x000A3548
		private void HideAllText()
		{
			foreach (KeyValuePair<EVRButtonId, ControllerButtonHints.ButtonHintInfo> keyValuePair in this.buttonHintInfos)
			{
				keyValuePair.Value.textHintObject.SetActive(false);
				keyValuePair.Value.textHintActive = false;
			}
			this.HideAllButtonHints();
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000A53BC File Offset: 0x000A35BC
		private string GetActiveHintText(EVRButtonId button)
		{
			if (this.buttonHintInfos.ContainsKey(button))
			{
				ControllerButtonHints.ButtonHintInfo buttonHintInfo = this.buttonHintInfos[button];
				if (buttonHintInfo.textHintActive)
				{
					return buttonHintInfo.text.text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000A5400 File Offset: 0x000A3600
		private static ControllerButtonHints GetControllerButtonHints(Hand hand)
		{
			if (hand != null)
			{
				ControllerButtonHints componentInChildren = hand.GetComponentInChildren<ControllerButtonHints>();
				if (componentInChildren != null && componentInChildren.initialized)
				{
					return componentInChildren;
				}
			}
			return null;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000A5434 File Offset: 0x000A3634
		public static void ShowButtonHint(Hand hand, params EVRButtonId[] buttons)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				controllerButtonHints.ShowButtonHint(buttons);
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000A5458 File Offset: 0x000A3658
		public static void HideButtonHint(Hand hand, params EVRButtonId[] buttons)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				controllerButtonHints.HideButtonHint(buttons);
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000A547C File Offset: 0x000A367C
		public static void HideAllButtonHints(Hand hand)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				controllerButtonHints.HideAllButtonHints();
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000A54A0 File Offset: 0x000A36A0
		public static bool IsButtonHintActive(Hand hand, EVRButtonId button)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			return controllerButtonHints != null && controllerButtonHints.IsButtonHintActive(button);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000A54C8 File Offset: 0x000A36C8
		public static void ShowTextHint(Hand hand, EVRButtonId button, string text, bool highlightButton = true)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				controllerButtonHints.ShowText(button, text, highlightButton);
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000A54F0 File Offset: 0x000A36F0
		public static void HideTextHint(Hand hand, EVRButtonId button)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				controllerButtonHints.HideText(button);
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000A5514 File Offset: 0x000A3714
		public static void HideAllTextHints(Hand hand)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				controllerButtonHints.HideAllText();
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000A5538 File Offset: 0x000A3738
		public static string GetActiveHintText(Hand hand, EVRButtonId button)
		{
			ControllerButtonHints controllerButtonHints = ControllerButtonHints.GetControllerButtonHints(hand);
			if (controllerButtonHints != null)
			{
				return controllerButtonHints.GetActiveHintText(button);
			}
			return string.Empty;
		}

		// Token: 0x04001EBE RID: 7870
		public Material controllerMaterial;

		// Token: 0x04001EBF RID: 7871
		public Color flashColor = new Color(1f, 0.557f, 0f);

		// Token: 0x04001EC0 RID: 7872
		public GameObject textHintPrefab;

		// Token: 0x04001EC1 RID: 7873
		[Header("Debug")]
		public bool debugHints;

		// Token: 0x04001EC2 RID: 7874
		private SteamVR_RenderModel renderModel;

		// Token: 0x04001EC3 RID: 7875
		private Player player;

		// Token: 0x04001EC4 RID: 7876
		private List<MeshRenderer> renderers = new List<MeshRenderer>();

		// Token: 0x04001EC5 RID: 7877
		private List<MeshRenderer> flashingRenderers = new List<MeshRenderer>();

		// Token: 0x04001EC6 RID: 7878
		private float startTime;

		// Token: 0x04001EC7 RID: 7879
		private float tickCount;

		// Token: 0x04001EC8 RID: 7880
		private Dictionary<EVRButtonId, ControllerButtonHints.ButtonHintInfo> buttonHintInfos;

		// Token: 0x04001EC9 RID: 7881
		private Transform textHintParent;

		// Token: 0x04001ECA RID: 7882
		private List<KeyValuePair<string, ulong>> componentButtonMasks = new List<KeyValuePair<string, ulong>>();

		// Token: 0x04001ECB RID: 7883
		private int colorID;

		// Token: 0x04001ECD RID: 7885
		private Vector3 centerPosition = Vector3.zero;

		// Token: 0x04001ECE RID: 7886
		private SteamVR_Events.Action renderModelLoadedAction;

		// Token: 0x02000781 RID: 1921
		private enum OffsetType
		{
			// Token: 0x04002936 RID: 10550
			Up,
			// Token: 0x04002937 RID: 10551
			Right,
			// Token: 0x04002938 RID: 10552
			Forward,
			// Token: 0x04002939 RID: 10553
			Back
		}

		// Token: 0x02000782 RID: 1922
		private class ButtonHintInfo
		{
			// Token: 0x0400293A RID: 10554
			public string componentName;

			// Token: 0x0400293B RID: 10555
			public List<MeshRenderer> renderers;

			// Token: 0x0400293C RID: 10556
			public Transform localTransform;

			// Token: 0x0400293D RID: 10557
			public GameObject textHintObject;

			// Token: 0x0400293E RID: 10558
			public Transform textStartAnchor;

			// Token: 0x0400293F RID: 10559
			public Transform textEndAnchor;

			// Token: 0x04002940 RID: 10560
			public Vector3 textEndOffsetDir;

			// Token: 0x04002941 RID: 10561
			public Transform canvasOffset;

			// Token: 0x04002942 RID: 10562
			public Text text;

			// Token: 0x04002943 RID: 10563
			public TextMesh textMesh;

			// Token: 0x04002944 RID: 10564
			public Canvas textCanvas;

			// Token: 0x04002945 RID: 10565
			public LineRenderer line;

			// Token: 0x04002946 RID: 10566
			public float distanceFromCenter;

			// Token: 0x04002947 RID: 10567
			public bool textHintActive;
		}
	}
}
