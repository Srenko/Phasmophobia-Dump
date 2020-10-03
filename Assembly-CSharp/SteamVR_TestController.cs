using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001F4 RID: 500
public class SteamVR_TestController : MonoBehaviour
{
	// Token: 0x06000DFB RID: 3579 RVA: 0x00058FAC File Offset: 0x000571AC
	private void OnDeviceConnected(int index, bool connected)
	{
		CVRSystem system = OpenVR.System;
		if (system == null || system.GetTrackedDeviceClass((uint)index) != ETrackedDeviceClass.Controller)
		{
			return;
		}
		if (connected)
		{
			Debug.Log(string.Format("Controller {0} connected.", index));
			this.PrintControllerStatus(index);
			this.controllerIndices.Add(index);
			return;
		}
		Debug.Log(string.Format("Controller {0} disconnected.", index));
		this.PrintControllerStatus(index);
		this.controllerIndices.Remove(index);
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00059022 File Offset: 0x00057222
	private void OnEnable()
	{
		SteamVR_Events.DeviceConnected.Listen(new UnityAction<int, bool>(this.OnDeviceConnected));
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0005903A File Offset: 0x0005723A
	private void OnDisable()
	{
		SteamVR_Events.DeviceConnected.Remove(new UnityAction<int, bool>(this.OnDeviceConnected));
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00059054 File Offset: 0x00057254
	private void PrintControllerStatus(int index)
	{
		SteamVR_Controller.Device device = SteamVR_Controller.Input(index);
		Debug.Log("index: " + device.index);
		Debug.Log("connected: " + device.connected.ToString());
		Debug.Log("hasTracking: " + device.hasTracking.ToString());
		Debug.Log("outOfRange: " + device.outOfRange.ToString());
		Debug.Log("calibrating: " + device.calibrating.ToString());
		Debug.Log("uninitialized: " + device.uninitialized.ToString());
		Debug.Log("pos: " + device.transform.pos);
		Debug.Log("rot: " + device.transform.rot.eulerAngles);
		Debug.Log("velocity: " + device.velocity);
		Debug.Log("angularVelocity: " + device.angularVelocity);
		int deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost, ETrackedDeviceClass.Controller, 0);
		int deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost, ETrackedDeviceClass.Controller, 0);
		Debug.Log((deviceIndex == deviceIndex2) ? "first" : ((deviceIndex == index) ? "left" : "right"));
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x000591C0 File Offset: 0x000573C0
	private void Update()
	{
		foreach (int num in this.controllerIndices)
		{
			SteamVR_Overlay instance = SteamVR_Overlay.instance;
			if (instance && this.point && this.pointer)
			{
				SteamVR_Utils.RigidTransform transform = SteamVR_Controller.Input(num).transform;
				this.pointer.transform.localPosition = transform.pos;
				this.pointer.transform.localRotation = transform.rot;
				SteamVR_Overlay.IntersectionResults intersectionResults = default(SteamVR_Overlay.IntersectionResults);
				if (instance.ComputeIntersection(transform.pos, transform.rot * Vector3.forward, ref intersectionResults))
				{
					this.point.transform.localPosition = intersectionResults.point;
					this.point.transform.localRotation = Quaternion.LookRotation(intersectionResults.normal);
				}
			}
			else
			{
				foreach (EVRButtonId evrbuttonId in this.buttonIds)
				{
					if (SteamVR_Controller.Input(num).GetPressDown(evrbuttonId))
					{
						Debug.Log(evrbuttonId + " press down");
					}
					if (SteamVR_Controller.Input(num).GetPressUp(evrbuttonId))
					{
						Debug.Log(evrbuttonId + " press up");
						if (evrbuttonId == EVRButtonId.k_EButton_Axis1)
						{
							SteamVR_Controller.Input(num).TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
							this.PrintControllerStatus(num);
						}
					}
					if (SteamVR_Controller.Input(num).GetPress(evrbuttonId))
					{
						Debug.Log(evrbuttonId);
					}
				}
				foreach (EVRButtonId evrbuttonId2 in this.axisIds)
				{
					if (SteamVR_Controller.Input(num).GetTouchDown(evrbuttonId2))
					{
						Debug.Log(evrbuttonId2 + " touch down");
					}
					if (SteamVR_Controller.Input(num).GetTouchUp(evrbuttonId2))
					{
						Debug.Log(evrbuttonId2 + " touch up");
					}
					if (SteamVR_Controller.Input(num).GetTouch(evrbuttonId2))
					{
						Vector2 axis = SteamVR_Controller.Input(num).GetAxis(evrbuttonId2);
						Debug.Log("axis: " + axis);
					}
				}
			}
		}
	}

	// Token: 0x04000E6E RID: 3694
	private List<int> controllerIndices = new List<int>();

	// Token: 0x04000E6F RID: 3695
	private EVRButtonId[] buttonIds = new EVRButtonId[]
	{
		EVRButtonId.k_EButton_ApplicationMenu,
		EVRButtonId.k_EButton_Grip,
		EVRButtonId.k_EButton_Axis0,
		EVRButtonId.k_EButton_Axis1
	};

	// Token: 0x04000E70 RID: 3696
	private EVRButtonId[] axisIds = new EVRButtonId[]
	{
		EVRButtonId.k_EButton_Axis0,
		EVRButtonId.k_EButton_Axis1
	};

	// Token: 0x04000E71 RID: 3697
	public Transform point;

	// Token: 0x04000E72 RID: 3698
	public Transform pointer;
}
