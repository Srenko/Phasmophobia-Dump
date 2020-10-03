using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000318 RID: 792
	[AddComponentMenu("VRTK/Scripts/Utilities/VRTK_Simulator")]
	public class VRTK_Simulator : MonoBehaviour
	{
		// Token: 0x06001BE9 RID: 7145 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00091AD4 File Offset: 0x0008FCD4
		protected virtual void OnEnable()
		{
			if (this.onlyInEditor && !Application.isEditor)
			{
				base.enabled = false;
				return;
			}
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			if (!this.headset)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_Simulator",
					"Headset Camera",
					". Simulator deactivated."
				}));
				base.enabled = false;
				return;
			}
			if (this.camStart && this.camStart.gameObject.activeInHierarchy)
			{
				this.playArea.position = this.camStart.position;
				this.playArea.rotation = this.camStart.rotation;
			}
			this.initialPosition = this.playArea.position;
			this.initialRotation = this.playArea.rotation;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00091BBC File Offset: 0x0008FDBC
		protected virtual void Update()
		{
			Vector3 a = Vector3.zero;
			Vector3 zero = Vector3.zero;
			if (Input.GetKey(this.keys.forward))
			{
				a = this.overwriteY(this.headset.forward, 0f);
			}
			else if (Input.GetKey(this.keys.backward))
			{
				a = this.overwriteY(-this.headset.forward, 0f);
			}
			else if (Input.GetKey(this.keys.strafeLeft))
			{
				a = this.overwriteY(-this.headset.right, 0f);
			}
			else if (Input.GetKey(this.keys.strafeRight))
			{
				a = this.overwriteY(this.headset.right, 0f);
			}
			else if (Input.GetKey(this.keys.up))
			{
				a = new Vector3(0f, 1f, 0f);
			}
			else if (Input.GetKey(this.keys.down))
			{
				a = new Vector3(0f, -1f, 0f);
			}
			else if (Input.GetKey(this.keys.left))
			{
				zero = new Vector3(0f, -1f, 0f);
			}
			else if (Input.GetKey(this.keys.right))
			{
				zero = new Vector3(0f, 1f, 0f);
			}
			else if (Input.GetKey(this.keys.reset))
			{
				this.playArea.position = this.initialPosition;
				this.playArea.rotation = this.initialRotation;
			}
			this.playArea.Translate(a * this.stepSize, Space.World);
			this.playArea.Rotate(zero);
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00091D9D File Offset: 0x0008FF9D
		protected virtual Vector3 overwriteY(Vector3 vector, float value)
		{
			return new Vector3(vector.x, value, vector.z);
		}

		// Token: 0x0400164D RID: 5709
		[Tooltip("Per default the keys on the left-hand side of the keyboard are used (WASD). They can be individually set as needed. The reset key brings the camera to its initial location.")]
		public VRTK_Simulator.Keys keys;

		// Token: 0x0400164E RID: 5710
		[Tooltip("Typically the simulator should be turned off when not testing anymore. This option will do this automatically when outside the editor.")]
		public bool onlyInEditor = true;

		// Token: 0x0400164F RID: 5711
		[Tooltip("Depending on the scale of the world the step size can be defined to increase or decrease movement speed.")]
		public float stepSize = 0.05f;

		// Token: 0x04001650 RID: 5712
		[Tooltip("An optional game object marking the position and rotation at which the camera should be initially placed.")]
		public Transform camStart;

		// Token: 0x04001651 RID: 5713
		protected Transform headset;

		// Token: 0x04001652 RID: 5714
		protected Transform playArea;

		// Token: 0x04001653 RID: 5715
		protected Vector3 initialPosition;

		// Token: 0x04001654 RID: 5716
		protected Quaternion initialRotation;

		// Token: 0x0200061C RID: 1564
		[Serializable]
		public class Keys
		{
			// Token: 0x040028B6 RID: 10422
			public KeyCode forward = KeyCode.W;

			// Token: 0x040028B7 RID: 10423
			public KeyCode backward = KeyCode.S;

			// Token: 0x040028B8 RID: 10424
			public KeyCode strafeLeft = KeyCode.A;

			// Token: 0x040028B9 RID: 10425
			public KeyCode strafeRight = KeyCode.D;

			// Token: 0x040028BA RID: 10426
			public KeyCode left = KeyCode.Q;

			// Token: 0x040028BB RID: 10427
			public KeyCode right = KeyCode.E;

			// Token: 0x040028BC RID: 10428
			public KeyCode up = KeyCode.Y;

			// Token: 0x040028BD RID: 10429
			public KeyCode down = KeyCode.C;

			// Token: 0x040028BE RID: 10430
			public KeyCode reset = KeyCode.X;
		}
	}
}
