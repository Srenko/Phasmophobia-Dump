using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x02000278 RID: 632
	public class SDK_InputSimulator : MonoBehaviour
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x000697CC File Offset: 0x000679CC
		public static GameObject FindInScene()
		{
			if (SDK_InputSimulator.cachedCameraRig == null && !SDK_InputSimulator.destroyed)
			{
				SDK_InputSimulator.cachedCameraRig = VRTK_SharedMethods.FindEvenInactiveGameObject<SDK_InputSimulator>(null);
				if (!SDK_InputSimulator.cachedCameraRig)
				{
					VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
					{
						"VRSimulatorCameraRig",
						"SDK_InputSimulator",
						". check that the `VRTK/Prefabs/VRSimulatorCameraRig` prefab been added to the scene."
					}));
				}
			}
			return SDK_InputSimulator.cachedCameraRig;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00064607 File Offset: 0x00062807
		private void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00069834 File Offset: 0x00067A34
		private void OnEnable()
		{
			this.hintCanvas = base.transform.Find("Canvas/Control Hints").gameObject;
			this.crossHairPanel = base.transform.Find("Canvas/CrosshairPanel").gameObject;
			this.hintText = this.hintCanvas.GetComponentInChildren<Text>();
			this.hintCanvas.SetActive(this.showControlHints);
			this.rightHand = base.transform.Find("RightHand");
			this.rightHand.gameObject.SetActive(false);
			this.leftHand = base.transform.Find("LeftHand");
			this.leftHand.gameObject.SetActive(false);
			this.currentHand = this.rightHand;
			this.oldPos = Input.mousePosition;
			this.neck = base.transform.Find("Neck");
			this.leftHand.Find("Hand").GetComponent<Renderer>().material.color = Color.red;
			this.rightHand.Find("Hand").GetComponent<Renderer>().material.color = Color.green;
			this.rightController = this.rightHand.GetComponent<SDK_ControllerSim>();
			this.leftController = this.leftHand.GetComponent<SDK_ControllerSim>();
			this.rightController.Selected = true;
			this.leftController.Selected = false;
			SDK_InputSimulator.destroyed = false;
			SDK_SimController sdk_SimController = VRTK_SDK_Bridge.GetControllerSDK() as SDK_SimController;
			if (sdk_SimController != null)
			{
				Dictionary<string, KeyCode> keyMappings = new Dictionary<string, KeyCode>
				{
					{
						"Trigger",
						this.triggerAlias
					},
					{
						"Grip",
						this.gripAlias
					},
					{
						"TouchpadPress",
						this.touchpadAlias
					},
					{
						"ButtonOne",
						this.buttonOneAlias
					},
					{
						"ButtonTwo",
						this.buttonTwoAlias
					},
					{
						"StartMenu",
						this.startMenuAlias
					},
					{
						"TouchModifier",
						this.touchModifier
					},
					{
						"HairTouchModifier",
						this.hairTouchModifier
					}
				};
				sdk_SimController.SetKeyMappings(keyMappings);
			}
			this.rightHand.gameObject.SetActive(true);
			this.leftHand.gameObject.SetActive(true);
			this.crossHairPanel.SetActive(false);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00069A76 File Offset: 0x00067C76
		private void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
			SDK_InputSimulator.destroyed = true;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00069A8C File Offset: 0x00067C8C
		private void Update()
		{
			if (Input.GetKeyDown(this.toggleControlHints))
			{
				this.showControlHints = !this.showControlHints;
				this.hintCanvas.SetActive(this.showControlHints);
			}
			if (this.mouseMovementInput == SDK_InputSimulator.MouseInputMode.RequiresButtonPress)
			{
				if (this.lockMouseToView)
				{
					Cursor.lockState = (Input.GetKey(this.mouseMovementKey) ? CursorLockMode.Locked : CursorLockMode.None);
				}
				else if (Input.GetKeyDown(this.mouseMovementKey))
				{
					this.oldPos = Input.mousePosition;
				}
			}
			if (Input.GetKeyDown(this.handsOnOff))
			{
				if (this.isHand)
				{
					this.SetMove();
				}
				else
				{
					this.SetHand();
				}
			}
			if (Input.GetKeyDown(this.changeHands))
			{
				if (this.currentHand.name == "LeftHand")
				{
					this.currentHand = this.rightHand;
					this.rightController.Selected = true;
					this.leftController.Selected = false;
				}
				else
				{
					this.currentHand = this.leftHand;
					this.rightController.Selected = false;
					this.leftController.Selected = true;
				}
			}
			if (this.isHand)
			{
				this.UpdateHands();
			}
			else
			{
				this.UpdateRotation();
				if (Input.GetKeyDown(this.distancePickupRight) && Input.GetKey(this.distancePickupModifier))
				{
					this.TryPickup(true);
				}
				else if (Input.GetKeyDown(this.distancePickupLeft) && Input.GetKey(this.distancePickupModifier))
				{
					this.TryPickup(false);
				}
				if (Input.GetKey(this.sprint))
				{
					this.sprintMultiplier = this.playerSprintMultiplier;
				}
				else
				{
					this.sprintMultiplier = 1f;
				}
				if (Input.GetKeyDown(this.distancePickupModifier))
				{
					this.crossHairPanel.SetActive(true);
				}
				else if (Input.GetKeyUp(this.distancePickupModifier))
				{
					this.crossHairPanel.SetActive(false);
				}
			}
			this.UpdatePosition();
			if (this.showControlHints)
			{
				this.UpdateHints();
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00069C64 File Offset: 0x00067E64
		private void TryPickup(bool rightHand)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit) && raycastHit.collider.gameObject.GetComponent<VRTK_InteractableObject>())
			{
				GameObject gameObject;
				if (rightHand)
				{
					gameObject = VRTK_DeviceFinder.GetControllerRightHand(false);
				}
				else
				{
					gameObject = VRTK_DeviceFinder.GetControllerLeftHand(false);
				}
				VRTK_InteractGrab component = gameObject.GetComponent<VRTK_InteractGrab>();
				if (component.GetGrabbedObject() == null)
				{
					gameObject.GetComponent<VRTK_InteractTouch>().ForceTouch(raycastHit.collider.gameObject);
					component.AttemptGrab();
				}
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00069CF8 File Offset: 0x00067EF8
		private void UpdateHands()
		{
			Vector3 mouseDelta = this.GetMouseDelta();
			if (this.IsAcceptingMouseInput())
			{
				if (Input.GetKey(this.rotationPosition))
				{
					if (Input.GetKey(this.changeAxis))
					{
						Vector3 zero = Vector3.zero;
						zero.x += (mouseDelta * this.handRotationMultiplier).y;
						zero.y += (mouseDelta * this.handRotationMultiplier).x;
						this.currentHand.transform.Rotate(zero * Time.deltaTime);
						return;
					}
					Vector3 zero2 = Vector3.zero;
					zero2.z += (mouseDelta * this.handRotationMultiplier).x;
					zero2.x += (mouseDelta * this.handRotationMultiplier).y;
					this.currentHand.transform.Rotate(zero2 * Time.deltaTime);
					return;
				}
				else
				{
					if (Input.GetKey(this.changeAxis))
					{
						Vector3 a = Vector3.zero;
						a += mouseDelta * this.handMoveMultiplier;
						this.currentHand.transform.Translate(a * Time.deltaTime);
						return;
					}
					Vector3 zero3 = Vector3.zero;
					zero3.x += (mouseDelta * this.handMoveMultiplier).x;
					zero3.z += (mouseDelta * this.handMoveMultiplier).y;
					this.currentHand.transform.Translate(zero3 * Time.deltaTime);
				}
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00069E88 File Offset: 0x00068088
		private void UpdateRotation()
		{
			Vector3 mouseDelta = this.GetMouseDelta();
			if (this.IsAcceptingMouseInput())
			{
				Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
				eulerAngles.y += (mouseDelta * this.playerRotationMultiplier).x;
				base.transform.localRotation = Quaternion.Euler(eulerAngles);
				eulerAngles = this.neck.rotation.eulerAngles;
				if (eulerAngles.x > 180f)
				{
					eulerAngles.x -= 360f;
				}
				if (eulerAngles.x < 80f && eulerAngles.x > -80f)
				{
					eulerAngles.x += (mouseDelta * this.playerRotationMultiplier).y * -1f;
					eulerAngles.x = Mathf.Clamp(eulerAngles.x, -79f, 79f);
					this.neck.rotation = Quaternion.Euler(eulerAngles);
				}
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00069F84 File Offset: 0x00068184
		private void UpdatePosition()
		{
			float d = Time.deltaTime * this.playerMoveMultiplier * this.sprintMultiplier;
			if (Input.GetKey(this.moveForward))
			{
				base.transform.Translate(base.transform.forward * d, Space.World);
			}
			else if (Input.GetKey(this.moveBackward))
			{
				base.transform.Translate(-base.transform.forward * d, Space.World);
			}
			if (Input.GetKey(this.moveLeft))
			{
				base.transform.Translate(-base.transform.right * d, Space.World);
				return;
			}
			if (Input.GetKey(this.moveRight))
			{
				base.transform.Translate(base.transform.right * d, Space.World);
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0006A05C File Offset: 0x0006825C
		private void SetHand()
		{
			Cursor.visible = false;
			this.isHand = true;
			this.rightHand.gameObject.SetActive(true);
			this.leftHand.gameObject.SetActive(true);
			this.oldPos = Input.mousePosition;
			if (this.resetHandsAtSwitch)
			{
				this.rightHand.transform.localPosition = new Vector3(0.2f, 1.2f, 0.5f);
				this.rightHand.transform.localRotation = Quaternion.identity;
				this.leftHand.transform.localPosition = new Vector3(-0.2f, 1.2f, 0.5f);
				this.leftHand.transform.localRotation = Quaternion.identity;
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0006A11D File Offset: 0x0006831D
		private void SetMove()
		{
			Cursor.visible = true;
			this.isHand = false;
			if (this.hideHandsAtSwitch)
			{
				this.rightHand.gameObject.SetActive(false);
				this.leftHand.gameObject.SetActive(false);
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0006A158 File Offset: 0x00068358
		private void UpdateHints()
		{
			string text = "";
			Func<KeyCode, string> func = (KeyCode k) => "<b>" + k.ToString() + "</b>";
			string str = "";
			if (this.mouseMovementInput == SDK_InputSimulator.MouseInputMode.RequiresButtonPress)
			{
				str = " (" + func(this.mouseMovementKey) + ")";
			}
			string str2 = this.moveForward.ToString() + this.moveLeft.ToString() + this.moveBackward.ToString() + this.moveRight.ToString();
			text = text + "Toggle Control Hints: " + func(this.toggleControlHints) + "\n\n";
			text = text + "Move Player/Playspace: <b>" + str2 + "</b>\n";
			text = text + "Sprint Modifier: (" + func(this.sprint) + ")\n\n";
			if (this.isHand)
			{
				if (Input.GetKey(this.rotationPosition))
				{
					text = text + "Mouse: <b>Controller Rotation" + str + "</b>\n";
				}
				else
				{
					text = text + "Mouse: <b>Controller Position" + str + "</b>\n";
				}
				text = string.Concat(new string[]
				{
					text,
					"Modes: HMD (",
					func(this.handsOnOff),
					"), Rotation (",
					func(this.rotationPosition),
					")\n"
				});
				text = string.Concat(new string[]
				{
					text,
					"Controller Hand: ",
					this.currentHand.name.Replace("Hand", ""),
					" (",
					func(this.changeHands),
					")\n"
				});
				string text2 = Input.GetKey(this.changeAxis) ? "X/Y" : "X/Z";
				text = string.Concat(new string[]
				{
					text,
					"Axis: ",
					text2,
					" (",
					func(this.changeAxis),
					")\n"
				});
				string text3 = "Press";
				if (Input.GetKey(this.hairTouchModifier))
				{
					text3 = "Hair Touch";
				}
				else if (Input.GetKey(this.touchModifier))
				{
					text3 = "Touch";
				}
				text = string.Concat(new string[]
				{
					text,
					"\nButton Press Mode Modifiers: Touch (",
					func(this.touchModifier),
					"), Hair Touch (",
					func(this.hairTouchModifier),
					")\n"
				});
				text = string.Concat(new string[]
				{
					text,
					"Trigger ",
					text3,
					": ",
					func(this.triggerAlias),
					"\n"
				});
				text = string.Concat(new string[]
				{
					text,
					"Grip ",
					text3,
					": ",
					func(this.gripAlias),
					"\n"
				});
				if (!Input.GetKey(this.hairTouchModifier))
				{
					text = string.Concat(new string[]
					{
						text,
						"Touchpad ",
						text3,
						": ",
						func(this.touchpadAlias),
						"\n"
					});
					text = string.Concat(new string[]
					{
						text,
						"Button One ",
						text3,
						": ",
						func(this.buttonOneAlias),
						"\n"
					});
					text = string.Concat(new string[]
					{
						text,
						"Button Two ",
						text3,
						": ",
						func(this.buttonTwoAlias),
						"\n"
					});
					text = string.Concat(new string[]
					{
						text,
						"Start Menu ",
						text3,
						": ",
						func(this.startMenuAlias),
						"\n"
					});
				}
			}
			else
			{
				text = text + "Mouse: <b>HMD Rotation" + str + "</b>\n";
				text = text + "Modes: Controller (" + func(this.handsOnOff) + ")\n";
				text = text + "Distance Pickup Modifier: (" + func(this.distancePickupModifier) + ")\n";
				text = text + "Distance Pickup Left Hand: (" + func(this.distancePickupLeft) + ")\n";
				text = text + "Distance Pickup Right Hand: (" + func(this.distancePickupRight) + ")\n";
			}
			this.hintText.text = text.TrimEnd(Array.Empty<char>());
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0006A60B File Offset: 0x0006880B
		private bool IsAcceptingMouseInput()
		{
			return this.mouseMovementInput == SDK_InputSimulator.MouseInputMode.Always || Input.GetKey(this.mouseMovementKey);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0006A622 File Offset: 0x00068822
		private Vector3 GetMouseDelta()
		{
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				return new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			}
			Vector3 result = Input.mousePosition - this.oldPos;
			this.oldPos = Input.mousePosition;
			return result;
		}

		// Token: 0x040010B8 RID: 4280
		[Tooltip("Show control information in the upper left corner of the screen.")]
		public bool showControlHints = true;

		// Token: 0x040010B9 RID: 4281
		[Tooltip("Hide hands when disabling them.")]
		public bool hideHandsAtSwitch;

		// Token: 0x040010BA RID: 4282
		[Tooltip("Reset hand position and rotation when enabling them.")]
		public bool resetHandsAtSwitch = true;

		// Token: 0x040010BB RID: 4283
		[Tooltip("Whether mouse movement always acts as input or requires a button press.")]
		public SDK_InputSimulator.MouseInputMode mouseMovementInput;

		// Token: 0x040010BC RID: 4284
		[Tooltip("Lock the mouse cursor to the game window when the mouse movement key is pressed.")]
		public bool lockMouseToView = true;

		// Token: 0x040010BD RID: 4285
		[Header("Adjustments")]
		[Tooltip("Adjust hand movement speed.")]
		public float handMoveMultiplier = 0.002f;

		// Token: 0x040010BE RID: 4286
		[Tooltip("Adjust hand rotation speed.")]
		public float handRotationMultiplier = 0.5f;

		// Token: 0x040010BF RID: 4287
		[Tooltip("Adjust player movement speed.")]
		public float playerMoveMultiplier = 5f;

		// Token: 0x040010C0 RID: 4288
		[Tooltip("Adjust player rotation speed.")]
		public float playerRotationMultiplier = 0.5f;

		// Token: 0x040010C1 RID: 4289
		[Tooltip("Adjust player sprint speed.")]
		public float playerSprintMultiplier = 2f;

		// Token: 0x040010C2 RID: 4290
		[Header("Operation Key Bindings")]
		[Tooltip("Key used to enable mouse input if a button press is required.")]
		public KeyCode mouseMovementKey = KeyCode.Mouse1;

		// Token: 0x040010C3 RID: 4291
		[Tooltip("Key used to toggle control hints on/off.")]
		public KeyCode toggleControlHints = KeyCode.F1;

		// Token: 0x040010C4 RID: 4292
		[Tooltip("Key used to switch between left and righ hand.")]
		public KeyCode changeHands = KeyCode.Tab;

		// Token: 0x040010C5 RID: 4293
		[Tooltip("Key used to switch hands On/Off.")]
		public KeyCode handsOnOff = KeyCode.LeftAlt;

		// Token: 0x040010C6 RID: 4294
		[Tooltip("Key used to switch between positional and rotational movement.")]
		public KeyCode rotationPosition = KeyCode.LeftShift;

		// Token: 0x040010C7 RID: 4295
		[Tooltip("Key used to switch between X/Y and X/Z axis.")]
		public KeyCode changeAxis = KeyCode.LeftControl;

		// Token: 0x040010C8 RID: 4296
		[Tooltip("Key used to distance pickup with left hand.")]
		public KeyCode distancePickupLeft = KeyCode.Mouse0;

		// Token: 0x040010C9 RID: 4297
		[Tooltip("Key used to distance pickup with right hand.")]
		public KeyCode distancePickupRight = KeyCode.Mouse1;

		// Token: 0x040010CA RID: 4298
		[Tooltip("Key used to enable distance pickup.")]
		public KeyCode distancePickupModifier = KeyCode.LeftControl;

		// Token: 0x040010CB RID: 4299
		[Header("Movement Key Bindings")]
		[Tooltip("Key used to move forward.")]
		public KeyCode moveForward = KeyCode.W;

		// Token: 0x040010CC RID: 4300
		[Tooltip("Key used to move to the left.")]
		public KeyCode moveLeft = KeyCode.A;

		// Token: 0x040010CD RID: 4301
		[Tooltip("Key used to move backwards.")]
		public KeyCode moveBackward = KeyCode.S;

		// Token: 0x040010CE RID: 4302
		[Tooltip("Key used to move to the right.")]
		public KeyCode moveRight = KeyCode.D;

		// Token: 0x040010CF RID: 4303
		[Tooltip("Key used to sprint.")]
		public KeyCode sprint = KeyCode.LeftShift;

		// Token: 0x040010D0 RID: 4304
		[Header("Controller Key Bindings")]
		[Tooltip("Key used to simulate trigger button.")]
		public KeyCode triggerAlias = KeyCode.Mouse1;

		// Token: 0x040010D1 RID: 4305
		[Tooltip("Key used to simulate grip button.")]
		public KeyCode gripAlias = KeyCode.Mouse0;

		// Token: 0x040010D2 RID: 4306
		[Tooltip("Key used to simulate touchpad button.")]
		public KeyCode touchpadAlias = KeyCode.Q;

		// Token: 0x040010D3 RID: 4307
		[Tooltip("Key used to simulate button one.")]
		public KeyCode buttonOneAlias = KeyCode.E;

		// Token: 0x040010D4 RID: 4308
		[Tooltip("Key used to simulate button two.")]
		public KeyCode buttonTwoAlias = KeyCode.R;

		// Token: 0x040010D5 RID: 4309
		[Tooltip("Key used to simulate start menu button.")]
		public KeyCode startMenuAlias = KeyCode.F;

		// Token: 0x040010D6 RID: 4310
		[Tooltip("Key used to switch between button touch and button press mode.")]
		public KeyCode touchModifier = KeyCode.T;

		// Token: 0x040010D7 RID: 4311
		[Tooltip("Key used to switch between hair touch mode.")]
		public KeyCode hairTouchModifier = KeyCode.H;

		// Token: 0x040010D8 RID: 4312
		private bool isHand;

		// Token: 0x040010D9 RID: 4313
		private GameObject hintCanvas;

		// Token: 0x040010DA RID: 4314
		private Text hintText;

		// Token: 0x040010DB RID: 4315
		private Transform rightHand;

		// Token: 0x040010DC RID: 4316
		private Transform leftHand;

		// Token: 0x040010DD RID: 4317
		private Transform currentHand;

		// Token: 0x040010DE RID: 4318
		private Vector3 oldPos;

		// Token: 0x040010DF RID: 4319
		private Transform neck;

		// Token: 0x040010E0 RID: 4320
		private SDK_ControllerSim rightController;

		// Token: 0x040010E1 RID: 4321
		private SDK_ControllerSim leftController;

		// Token: 0x040010E2 RID: 4322
		private static GameObject cachedCameraRig;

		// Token: 0x040010E3 RID: 4323
		private static bool destroyed;

		// Token: 0x040010E4 RID: 4324
		private float sprintMultiplier = 1f;

		// Token: 0x040010E5 RID: 4325
		private GameObject crossHairPanel;

		// Token: 0x020005C4 RID: 1476
		public enum MouseInputMode
		{
			// Token: 0x0400273D RID: 10045
			Always,
			// Token: 0x0400273E RID: 10046
			RequiresButtonPress
		}
	}
}
