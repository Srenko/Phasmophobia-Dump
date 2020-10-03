using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000431 RID: 1073
	public class Player : MonoBehaviour
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000A21C1 File Offset: 0x000A03C1
		public static Player instance
		{
			get
			{
				if (Player._instance == null)
				{
					Player._instance = Object.FindObjectOfType<Player>();
				}
				return Player._instance;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000A21E0 File Offset: 0x000A03E0
		public int handCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.hands.Length; i++)
				{
					if (this.hands[i].gameObject.activeInHierarchy)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000A221C File Offset: 0x000A041C
		public Hand GetHand(int i)
		{
			for (int j = 0; j < this.hands.Length; j++)
			{
				if (this.hands[j].gameObject.activeInHierarchy)
				{
					if (i <= 0)
					{
						return this.hands[j];
					}
					i--;
				}
			}
			return null;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000A2268 File Offset: 0x000A0468
		public Hand leftHand
		{
			get
			{
				for (int i = 0; i < this.hands.Length; i++)
				{
					if (this.hands[i].gameObject.activeInHierarchy && this.hands[i].GuessCurrentHandType() == Hand.HandType.Left)
					{
						return this.hands[i];
					}
				}
				return null;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000A22B8 File Offset: 0x000A04B8
		public Hand rightHand
		{
			get
			{
				for (int i = 0; i < this.hands.Length; i++)
				{
					if (this.hands[i].gameObject.activeInHierarchy && this.hands[i].GuessCurrentHandType() == Hand.HandType.Right)
					{
						return this.hands[i];
					}
				}
				return null;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x000A2308 File Offset: 0x000A0508
		public SteamVR_Controller.Device leftController
		{
			get
			{
				Hand leftHand = this.leftHand;
				if (leftHand)
				{
					return leftHand.controller;
				}
				return null;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x000A232C File Offset: 0x000A052C
		public SteamVR_Controller.Device rightController
		{
			get
			{
				Hand rightHand = this.rightHand;
				if (rightHand)
				{
					return rightHand.controller;
				}
				return null;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000A2350 File Offset: 0x000A0550
		public Transform hmdTransform
		{
			get
			{
				for (int i = 0; i < this.hmdTransforms.Length; i++)
				{
					if (this.hmdTransforms[i].gameObject.activeInHierarchy)
					{
						return this.hmdTransforms[i];
					}
				}
				return null;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x000A2390 File Offset: 0x000A0590
		public float eyeHeight
		{
			get
			{
				Transform hmdTransform = this.hmdTransform;
				if (hmdTransform)
				{
					return Vector3.Project(hmdTransform.position - this.trackingOriginTransform.position, this.trackingOriginTransform.up).magnitude / this.trackingOriginTransform.lossyScale.x;
				}
				return 0f;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000A23F4 File Offset: 0x000A05F4
		public Vector3 feetPositionGuess
		{
			get
			{
				Transform hmdTransform = this.hmdTransform;
				if (hmdTransform)
				{
					return this.trackingOriginTransform.position + Vector3.ProjectOnPlane(hmdTransform.position - this.trackingOriginTransform.position, this.trackingOriginTransform.up);
				}
				return this.trackingOriginTransform.position;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x000A2454 File Offset: 0x000A0654
		public Vector3 bodyDirectionGuess
		{
			get
			{
				Transform hmdTransform = this.hmdTransform;
				if (hmdTransform)
				{
					Vector3 vector = Vector3.ProjectOnPlane(hmdTransform.forward, this.trackingOriginTransform.up);
					if (Vector3.Dot(hmdTransform.up, this.trackingOriginTransform.up) < 0f)
					{
						vector = -vector;
					}
					return vector;
				}
				return this.trackingOriginTransform.forward;
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000A24B8 File Offset: 0x000A06B8
		private void Awake()
		{
			if (this.trackingOriginTransform == null)
			{
				this.trackingOriginTransform = base.transform;
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000A24D4 File Offset: 0x000A06D4
		private void OnEnable()
		{
			Player._instance = this;
			if (SteamVR.instance != null)
			{
				this.ActivateRig(this.rigSteamVR);
				return;
			}
			this.ActivateRig(this.rig2DFallback);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000A24FC File Offset: 0x000A06FC
		private void OnDrawGizmos()
		{
			if (this != Player.instance)
			{
				return;
			}
			Gizmos.color = Color.white;
			Gizmos.DrawIcon(this.feetPositionGuess, "vr_interaction_system_feet.png");
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(this.feetPositionGuess, this.feetPositionGuess + this.trackingOriginTransform.up * this.eyeHeight);
			Gizmos.color = Color.blue;
			Vector3 bodyDirectionGuess = this.bodyDirectionGuess;
			Vector3 b = Vector3.Cross(this.trackingOriginTransform.up, bodyDirectionGuess);
			Vector3 vector = this.feetPositionGuess + this.trackingOriginTransform.up * this.eyeHeight * 0.75f;
			Vector3 vector2 = vector + bodyDirectionGuess * 0.33f;
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector2 - 0.033f * (bodyDirectionGuess + b));
			Gizmos.DrawLine(vector2, vector2 - 0.033f * (bodyDirectionGuess - b));
			Gizmos.color = Color.red;
			int handCount = this.handCount;
			for (int i = 0; i < handCount; i++)
			{
				Hand hand = this.GetHand(i);
				if (hand.startingHandType == Hand.HandType.Left)
				{
					Gizmos.DrawIcon(hand.transform.position, "vr_interaction_system_left_hand.png");
				}
				else if (hand.startingHandType == Hand.HandType.Right)
				{
					Gizmos.DrawIcon(hand.transform.position, "vr_interaction_system_right_hand.png");
				}
				else
				{
					Hand.HandType handType = hand.GuessCurrentHandType();
					if (handType == Hand.HandType.Left)
					{
						Gizmos.DrawIcon(hand.transform.position, "vr_interaction_system_left_hand_question.png");
					}
					else if (handType == Hand.HandType.Right)
					{
						Gizmos.DrawIcon(hand.transform.position, "vr_interaction_system_right_hand_question.png");
					}
					else
					{
						Gizmos.DrawIcon(hand.transform.position, "vr_interaction_system_unknown_hand.png");
					}
				}
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000A26D4 File Offset: 0x000A08D4
		public void Draw2DDebug()
		{
			if (!this.allowToggleTo2D)
			{
				return;
			}
			if (!SteamVR.active)
			{
				return;
			}
			int num = 100;
			int num2 = 25;
			float num3 = (float)(Screen.width / 2 - num / 2);
			int num4 = Screen.height - num2 - 10;
			string text = this.rigSteamVR.activeSelf ? "2D Debug" : "VR";
			if (GUI.Button(new Rect(num3, (float)num4, (float)num, (float)num2), text))
			{
				if (this.rigSteamVR.activeSelf)
				{
					this.ActivateRig(this.rig2DFallback);
					return;
				}
				this.ActivateRig(this.rigSteamVR);
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000A2764 File Offset: 0x000A0964
		private void ActivateRig(GameObject rig)
		{
			this.rigSteamVR.SetActive(rig == this.rigSteamVR);
			this.rig2DFallback.SetActive(rig == this.rig2DFallback);
			if (this.audioListener)
			{
				this.audioListener.transform.parent = this.hmdTransform;
				this.audioListener.transform.localPosition = Vector3.zero;
				this.audioListener.transform.localRotation = Quaternion.identity;
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00003F60 File Offset: 0x00002160
		public void PlayerShotSelf()
		{
		}

		// Token: 0x04001E72 RID: 7794
		[Tooltip("Virtual transform corresponding to the meatspace tracking origin. Devices are tracked relative to this.")]
		public Transform trackingOriginTransform;

		// Token: 0x04001E73 RID: 7795
		[Tooltip("List of possible transforms for the head/HMD, including the no-SteamVR fallback camera.")]
		public Transform[] hmdTransforms;

		// Token: 0x04001E74 RID: 7796
		[Tooltip("List of possible Hands, including no-SteamVR fallback Hands.")]
		public Hand[] hands;

		// Token: 0x04001E75 RID: 7797
		[Tooltip("Reference to the physics collider that follows the player's HMD position.")]
		public Collider headCollider;

		// Token: 0x04001E76 RID: 7798
		[Tooltip("These objects are enabled when SteamVR is available")]
		public GameObject rigSteamVR;

		// Token: 0x04001E77 RID: 7799
		[Tooltip("These objects are enabled when SteamVR is not available, or when the user toggles out of VR")]
		public GameObject rig2DFallback;

		// Token: 0x04001E78 RID: 7800
		[Tooltip("The audio listener for this player")]
		public Transform audioListener;

		// Token: 0x04001E79 RID: 7801
		public bool allowToggleTo2D = true;

		// Token: 0x04001E7A RID: 7802
		private static Player _instance;
	}
}
