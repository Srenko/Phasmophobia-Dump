using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR;

// Token: 0x020001E2 RID: 482
public class SteamVR_Controller
{
	// Token: 0x06000D50 RID: 3408 RVA: 0x0005388C File Offset: 0x00051A8C
	public static SteamVR_Controller.Device Input(int deviceIndex)
	{
		if (SteamVR_Controller.devices == null)
		{
			SteamVR_Controller.devices = new SteamVR_Controller.Device[16];
			uint num = 0U;
			while ((ulong)num < (ulong)((long)SteamVR_Controller.devices.Length))
			{
				SteamVR_Controller.devices[(int)num] = new SteamVR_Controller.Device(num);
				num += 1U;
			}
		}
		return SteamVR_Controller.devices[deviceIndex];
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x000538D4 File Offset: 0x00051AD4
	public static void Update()
	{
		int num = 0;
		while ((long)num < 16L)
		{
			SteamVR_Controller.Input(num).Update();
			num++;
		}
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x000538FC File Offset: 0x00051AFC
	public static int GetDeviceIndex(SteamVR_Controller.DeviceRelation relation, ETrackedDeviceClass deviceClass = ETrackedDeviceClass.Controller, int relativeTo = 0)
	{
		int result = -1;
		SteamVR_Utils.RigidTransform t = (relativeTo < 16) ? SteamVR_Controller.Input(relativeTo).transform.GetInverse() : SteamVR_Utils.RigidTransform.identity;
		CVRSystem system = OpenVR.System;
		if (system == null)
		{
			return result;
		}
		float num = float.MinValue;
		int num2 = 0;
		while ((long)num2 < 16L)
		{
			if (num2 != relativeTo && system.GetTrackedDeviceClass((uint)num2) == deviceClass)
			{
				SteamVR_Controller.Device device = SteamVR_Controller.Input(num2);
				if (device.connected)
				{
					if (relation == SteamVR_Controller.DeviceRelation.First)
					{
						return num2;
					}
					Vector3 vector = t * device.transform.pos;
					float num3;
					if (relation == SteamVR_Controller.DeviceRelation.FarthestRight)
					{
						num3 = vector.x;
					}
					else if (relation == SteamVR_Controller.DeviceRelation.FarthestLeft)
					{
						num3 = -vector.x;
					}
					else
					{
						Vector3 normalized = new Vector3(vector.x, 0f, vector.z).normalized;
						float num4 = Vector3.Dot(normalized, Vector3.forward);
						Vector3 vector2 = Vector3.Cross(normalized, Vector3.forward);
						if (relation == SteamVR_Controller.DeviceRelation.Leftmost)
						{
							num3 = ((vector2.y > 0f) ? (2f - num4) : num4);
						}
						else
						{
							num3 = ((vector2.y < 0f) ? (2f - num4) : num4);
						}
					}
					if (num3 > num)
					{
						result = num2;
						num = num3;
					}
				}
			}
			num2++;
		}
		return result;
	}

	// Token: 0x04000DB7 RID: 3511
	private static SteamVR_Controller.Device[] devices;

	// Token: 0x02000568 RID: 1384
	public class ButtonMask
	{
		// Token: 0x040025B7 RID: 9655
		public const ulong System = 1UL;

		// Token: 0x040025B8 RID: 9656
		public const ulong ApplicationMenu = 2UL;

		// Token: 0x040025B9 RID: 9657
		public const ulong Grip = 4UL;

		// Token: 0x040025BA RID: 9658
		public const ulong Axis0 = 4294967296UL;

		// Token: 0x040025BB RID: 9659
		public const ulong Axis1 = 8589934592UL;

		// Token: 0x040025BC RID: 9660
		public const ulong Axis2 = 17179869184UL;

		// Token: 0x040025BD RID: 9661
		public const ulong Axis3 = 34359738368UL;

		// Token: 0x040025BE RID: 9662
		public const ulong Axis4 = 68719476736UL;

		// Token: 0x040025BF RID: 9663
		public const ulong Touchpad = 4294967296UL;

		// Token: 0x040025C0 RID: 9664
		public const ulong Trigger = 8589934592UL;
	}

	// Token: 0x02000569 RID: 1385
	public class Device
	{
		// Token: 0x06002840 RID: 10304 RVA: 0x000C32A8 File Offset: 0x000C14A8
		public Device(uint i)
		{
			this.index = i;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000C32C9 File Offset: 0x000C14C9
		// (set) Token: 0x06002842 RID: 10306 RVA: 0x000C32D1 File Offset: 0x000C14D1
		public uint index { get; private set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000C32DA File Offset: 0x000C14DA
		// (set) Token: 0x06002844 RID: 10308 RVA: 0x000C32E2 File Offset: 0x000C14E2
		public bool valid { get; private set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x000C32EB File Offset: 0x000C14EB
		public bool connected
		{
			get
			{
				this.Update();
				return this.pose.bDeviceIsConnected;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x000C32FE File Offset: 0x000C14FE
		public bool hasTracking
		{
			get
			{
				this.Update();
				return this.pose.bPoseIsValid;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000C3311 File Offset: 0x000C1511
		public bool outOfRange
		{
			get
			{
				this.Update();
				return this.pose.eTrackingResult == ETrackingResult.Running_OutOfRange || this.pose.eTrackingResult == ETrackingResult.Calibrating_OutOfRange;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x000C333C File Offset: 0x000C153C
		public bool calibrating
		{
			get
			{
				this.Update();
				return this.pose.eTrackingResult == ETrackingResult.Calibrating_InProgress || this.pose.eTrackingResult == ETrackingResult.Calibrating_OutOfRange;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x000C3364 File Offset: 0x000C1564
		public bool uninitialized
		{
			get
			{
				this.Update();
				return this.pose.eTrackingResult == ETrackingResult.Uninitialized;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000C337A File Offset: 0x000C157A
		public SteamVR_Utils.RigidTransform transform
		{
			get
			{
				this.Update();
				return new SteamVR_Utils.RigidTransform(this.pose.mDeviceToAbsoluteTracking);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000C3392 File Offset: 0x000C1592
		public Vector3 velocity
		{
			get
			{
				this.Update();
				return new Vector3(this.pose.vVelocity.v0, this.pose.vVelocity.v1, -this.pose.vVelocity.v2);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000C33D0 File Offset: 0x000C15D0
		public Vector3 angularVelocity
		{
			get
			{
				this.Update();
				return new Vector3(-this.pose.vAngularVelocity.v0, -this.pose.vAngularVelocity.v1, this.pose.vAngularVelocity.v2);
			}
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000C340F File Offset: 0x000C160F
		public VRControllerState_t GetState()
		{
			this.Update();
			return this.state;
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000C341D File Offset: 0x000C161D
		public VRControllerState_t GetPrevState()
		{
			this.Update();
			return this.prevState;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000C342B File Offset: 0x000C162B
		public TrackedDevicePose_t GetPose()
		{
			this.Update();
			return this.pose;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000C343C File Offset: 0x000C163C
		public void Update()
		{
			if (Time.frameCount != this.prevFrameCount)
			{
				this.prevFrameCount = Time.frameCount;
				this.prevState = this.state;
				CVRSystem system = OpenVR.System;
				if (system != null)
				{
					this.valid = system.GetControllerStateWithPose(SteamVR_Render.instance.trackingSpace, this.index, ref this.state, (uint)Marshal.SizeOf(typeof(VRControllerState_t)), ref this.pose);
					this.UpdateHairTrigger();
				}
			}
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000C34B3 File Offset: 0x000C16B3
		public bool GetPress(ulong buttonMask)
		{
			this.Update();
			return (this.state.ulButtonPressed & buttonMask) > 0UL;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000C34CC File Offset: 0x000C16CC
		public bool GetPressDown(ulong buttonMask)
		{
			this.Update();
			return (this.state.ulButtonPressed & buttonMask) != 0UL && (this.prevState.ulButtonPressed & buttonMask) == 0UL;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000C34F6 File Offset: 0x000C16F6
		public bool GetPressUp(ulong buttonMask)
		{
			this.Update();
			return (this.state.ulButtonPressed & buttonMask) == 0UL && (this.prevState.ulButtonPressed & buttonMask) > 0UL;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000C3520 File Offset: 0x000C1720
		public bool GetPress(EVRButtonId buttonId)
		{
			return this.GetPress(1UL << (int)buttonId);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000C352F File Offset: 0x000C172F
		public bool GetPressDown(EVRButtonId buttonId)
		{
			return this.GetPressDown(1UL << (int)buttonId);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000C353E File Offset: 0x000C173E
		public bool GetPressUp(EVRButtonId buttonId)
		{
			return this.GetPressUp(1UL << (int)buttonId);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000C354D File Offset: 0x000C174D
		public bool GetTouch(ulong buttonMask)
		{
			this.Update();
			return (this.state.ulButtonTouched & buttonMask) > 0UL;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000C3566 File Offset: 0x000C1766
		public bool GetTouchDown(ulong buttonMask)
		{
			this.Update();
			return (this.state.ulButtonTouched & buttonMask) != 0UL && (this.prevState.ulButtonTouched & buttonMask) == 0UL;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000C3590 File Offset: 0x000C1790
		public bool GetTouchUp(ulong buttonMask)
		{
			this.Update();
			return (this.state.ulButtonTouched & buttonMask) == 0UL && (this.prevState.ulButtonTouched & buttonMask) > 0UL;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000C35BA File Offset: 0x000C17BA
		public bool GetTouch(EVRButtonId buttonId)
		{
			return this.GetTouch(1UL << (int)buttonId);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000C35C9 File Offset: 0x000C17C9
		public bool GetTouchDown(EVRButtonId buttonId)
		{
			return this.GetTouchDown(1UL << (int)buttonId);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000C35D8 File Offset: 0x000C17D8
		public bool GetTouchUp(EVRButtonId buttonId)
		{
			return this.GetTouchUp(1UL << (int)buttonId);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000C35E8 File Offset: 0x000C17E8
		public Vector2 GetAxis(EVRButtonId buttonId = EVRButtonId.k_EButton_Axis0)
		{
			this.Update();
			switch (buttonId)
			{
			case EVRButtonId.k_EButton_Axis0:
				return new Vector2(this.state.rAxis0.x, this.state.rAxis0.y);
			case EVRButtonId.k_EButton_Axis1:
				return new Vector2(this.state.rAxis1.x, this.state.rAxis1.y);
			case EVRButtonId.k_EButton_Axis2:
				return new Vector2(this.state.rAxis2.x, this.state.rAxis2.y);
			case EVRButtonId.k_EButton_Axis3:
				return new Vector2(this.state.rAxis3.x, this.state.rAxis3.y);
			case EVRButtonId.k_EButton_Axis4:
				return new Vector2(this.state.rAxis4.x, this.state.rAxis4.y);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000C36E4 File Offset: 0x000C18E4
		public void TriggerHapticPulse(ushort durationMicroSec = 500, EVRButtonId buttonId = EVRButtonId.k_EButton_Axis0)
		{
			CVRSystem system = OpenVR.System;
			if (system != null)
			{
				uint unAxisId = (uint)(buttonId - EVRButtonId.k_EButton_Axis0);
				system.TriggerHapticPulse(this.index, unAxisId, (char)durationMicroSec);
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000C3710 File Offset: 0x000C1910
		private void UpdateHairTrigger()
		{
			this.hairTriggerPrevState = this.hairTriggerState;
			float x = this.state.rAxis1.x;
			if (this.hairTriggerState)
			{
				if (x < this.hairTriggerLimit - this.hairTriggerDelta || x <= 0f)
				{
					this.hairTriggerState = false;
				}
			}
			else if (x > this.hairTriggerLimit + this.hairTriggerDelta || x >= 1f)
			{
				this.hairTriggerState = true;
			}
			this.hairTriggerLimit = (this.hairTriggerState ? Mathf.Max(this.hairTriggerLimit, x) : Mathf.Min(this.hairTriggerLimit, x));
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000C37AA File Offset: 0x000C19AA
		public bool GetHairTrigger()
		{
			this.Update();
			return this.hairTriggerState;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000C37B8 File Offset: 0x000C19B8
		public bool GetHairTriggerDown()
		{
			this.Update();
			return this.hairTriggerState && !this.hairTriggerPrevState;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000C37D3 File Offset: 0x000C19D3
		public bool GetHairTriggerUp()
		{
			this.Update();
			return !this.hairTriggerState && this.hairTriggerPrevState;
		}

		// Token: 0x040025C3 RID: 9667
		private VRControllerState_t state;

		// Token: 0x040025C4 RID: 9668
		private VRControllerState_t prevState;

		// Token: 0x040025C5 RID: 9669
		private TrackedDevicePose_t pose;

		// Token: 0x040025C6 RID: 9670
		private int prevFrameCount = -1;

		// Token: 0x040025C7 RID: 9671
		public float hairTriggerDelta = 0.1f;

		// Token: 0x040025C8 RID: 9672
		private float hairTriggerLimit;

		// Token: 0x040025C9 RID: 9673
		private bool hairTriggerState;

		// Token: 0x040025CA RID: 9674
		private bool hairTriggerPrevState;
	}

	// Token: 0x0200056A RID: 1386
	public enum DeviceRelation
	{
		// Token: 0x040025CC RID: 9676
		First,
		// Token: 0x040025CD RID: 9677
		Leftmost,
		// Token: 0x040025CE RID: 9678
		Rightmost,
		// Token: 0x040025CF RID: 9679
		FarthestLeft,
		// Token: 0x040025D0 RID: 9680
		FarthestRight
	}
}
