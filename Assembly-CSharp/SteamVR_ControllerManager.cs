using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001E3 RID: 483
public class SteamVR_ControllerManager : MonoBehaviour
{
	// Token: 0x06000D54 RID: 3412 RVA: 0x00053A44 File Offset: 0x00051C44
	private void SetUniqueObject(GameObject o, int index)
	{
		for (int i = 0; i < index; i++)
		{
			if (this.objects[i] == o)
			{
				return;
			}
		}
		this.objects[index] = o;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x00053A78 File Offset: 0x00051C78
	public void UpdateTargets()
	{
		GameObject[] array = this.objects;
		int num = (array != null) ? array.Length : 0;
		this.objects = new GameObject[2 + num];
		this.SetUniqueObject(this.right, 0);
		this.SetUniqueObject(this.left, 1);
		for (int i = 0; i < num; i++)
		{
			this.SetUniqueObject(array[i], 2 + i);
		}
		this.indices = new uint[2 + num];
		for (int j = 0; j < this.indices.Length; j++)
		{
			this.indices[j] = uint.MaxValue;
		}
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00053B00 File Offset: 0x00051D00
	private void Awake()
	{
		this.UpdateTargets();
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00053B08 File Offset: 0x00051D08
	private SteamVR_ControllerManager()
	{
		this.inputFocusAction = SteamVR_Events.InputFocusAction(new UnityAction<bool>(this.OnInputFocus));
		this.deviceConnectedAction = SteamVR_Events.DeviceConnectedAction(new UnityAction<int, bool>(this.OnDeviceConnected));
		this.trackedDeviceRoleChangedAction = SteamVR_Events.SystemAction(EVREventType.VREvent_TrackedDeviceRoleChanged, new UnityAction<VREvent_t>(this.OnTrackedDeviceRoleChanged));
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00053B80 File Offset: 0x00051D80
	private void OnEnable()
	{
		for (int i = 0; i < this.objects.Length; i++)
		{
			GameObject gameObject = this.objects[i];
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			this.indices[i] = uint.MaxValue;
		}
		this.Refresh();
		for (int j = 0; j < SteamVR.connected.Length; j++)
		{
			if (SteamVR.connected[j])
			{
				this.OnDeviceConnected(j, true);
			}
		}
		this.inputFocusAction.enabled = true;
		this.deviceConnectedAction.enabled = true;
		this.trackedDeviceRoleChangedAction.enabled = true;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00053C0F File Offset: 0x00051E0F
	private void OnDisable()
	{
		this.inputFocusAction.enabled = false;
		this.deviceConnectedAction.enabled = false;
		this.trackedDeviceRoleChangedAction.enabled = false;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00053C38 File Offset: 0x00051E38
	private void OnInputFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			for (int i = 0; i < this.objects.Length; i++)
			{
				GameObject gameObject = this.objects[i];
				if (gameObject != null)
				{
					string str = (i < 2) ? SteamVR_ControllerManager.labels[i] : (i - 1).ToString();
					this.ShowObject(gameObject.transform, SteamVR_ControllerManager.hiddenPrefix + str + SteamVR_ControllerManager.hiddenPostfix);
				}
			}
			return;
		}
		for (int j = 0; j < this.objects.Length; j++)
		{
			GameObject gameObject2 = this.objects[j];
			if (gameObject2 != null)
			{
				string str2 = (j < 2) ? SteamVR_ControllerManager.labels[j] : (j - 1).ToString();
				this.HideObject(gameObject2.transform, SteamVR_ControllerManager.hiddenPrefix + str2 + SteamVR_ControllerManager.hiddenPostfix);
			}
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00003F60 File Offset: 0x00002160
	private void HideObject(Transform t, string name)
	{
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00003F60 File Offset: 0x00002160
	private void ShowObject(Transform t, string name)
	{
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00053D0C File Offset: 0x00051F0C
	private void SetTrackedDeviceIndex(int objectIndex, uint trackedDeviceIndex)
	{
		if (trackedDeviceIndex != 4294967295U)
		{
			for (int i = 0; i < this.objects.Length; i++)
			{
				if (i != objectIndex && this.indices[i] == trackedDeviceIndex)
				{
					GameObject gameObject = this.objects[i];
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
					this.indices[i] = uint.MaxValue;
				}
			}
		}
		if (trackedDeviceIndex != this.indices[objectIndex])
		{
			this.indices[objectIndex] = trackedDeviceIndex;
			GameObject gameObject2 = this.objects[objectIndex];
			if (gameObject2 != null)
			{
				if (trackedDeviceIndex == 4294967295U)
				{
					gameObject2.SetActive(false);
					return;
				}
				gameObject2.SetActive(true);
				gameObject2.BroadcastMessage("SetDeviceIndex", (int)trackedDeviceIndex, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00053DAC File Offset: 0x00051FAC
	private void OnTrackedDeviceRoleChanged(VREvent_t vrEvent)
	{
		this.Refresh();
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00053DB4 File Offset: 0x00051FB4
	private void OnDeviceConnected(int index, bool connected)
	{
		bool flag = this.connected[index];
		this.connected[index] = false;
		if (connected)
		{
			CVRSystem system = OpenVR.System;
			if (system != null)
			{
				ETrackedDeviceClass trackedDeviceClass = system.GetTrackedDeviceClass((uint)index);
				if (trackedDeviceClass == ETrackedDeviceClass.Controller || trackedDeviceClass == ETrackedDeviceClass.GenericTracker)
				{
					this.connected[index] = true;
					flag = !flag;
				}
			}
		}
		if (flag)
		{
			this.Refresh();
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00053E08 File Offset: 0x00052008
	public void Refresh()
	{
		int i = 0;
		CVRSystem system = OpenVR.System;
		if (system != null)
		{
			this.leftIndex = system.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.LeftHand);
			this.rightIndex = system.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand);
		}
		if (this.leftIndex == 4294967295U && this.rightIndex == 4294967295U)
		{
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.connected.Length))
			{
				if (i >= this.objects.Length)
				{
					break;
				}
				if (this.connected[(int)num])
				{
					this.SetTrackedDeviceIndex(i++, num);
					if (!this.assignAllBeforeIdentified)
					{
						break;
					}
				}
				num += 1U;
			}
		}
		else
		{
			this.SetTrackedDeviceIndex(i++, ((ulong)this.rightIndex < (ulong)((long)this.connected.Length) && this.connected[(int)this.rightIndex]) ? this.rightIndex : uint.MaxValue);
			this.SetTrackedDeviceIndex(i++, ((ulong)this.leftIndex < (ulong)((long)this.connected.Length) && this.connected[(int)this.leftIndex]) ? this.leftIndex : uint.MaxValue);
			if (this.leftIndex != 4294967295U && this.rightIndex != 4294967295U)
			{
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)this.connected.Length))
				{
					if (i >= this.objects.Length)
					{
						break;
					}
					if (this.connected[(int)num2] && num2 != this.leftIndex && num2 != this.rightIndex)
					{
						this.SetTrackedDeviceIndex(i++, num2);
					}
					num2 += 1U;
				}
			}
		}
		while (i < this.objects.Length)
		{
			this.SetTrackedDeviceIndex(i++, uint.MaxValue);
		}
	}

	// Token: 0x04000DB8 RID: 3512
	public GameObject left;

	// Token: 0x04000DB9 RID: 3513
	public GameObject right;

	// Token: 0x04000DBA RID: 3514
	[Tooltip("Populate with objects you want to assign to additional controllers")]
	public GameObject[] objects;

	// Token: 0x04000DBB RID: 3515
	[Tooltip("Set to true if you want objects arbitrarily assigned to controllers before their role (left vs right) is identified")]
	public bool assignAllBeforeIdentified;

	// Token: 0x04000DBC RID: 3516
	private uint[] indices;

	// Token: 0x04000DBD RID: 3517
	private bool[] connected = new bool[16];

	// Token: 0x04000DBE RID: 3518
	private uint leftIndex = uint.MaxValue;

	// Token: 0x04000DBF RID: 3519
	private uint rightIndex = uint.MaxValue;

	// Token: 0x04000DC0 RID: 3520
	private SteamVR_Events.Action inputFocusAction;

	// Token: 0x04000DC1 RID: 3521
	private SteamVR_Events.Action deviceConnectedAction;

	// Token: 0x04000DC2 RID: 3522
	private SteamVR_Events.Action trackedDeviceRoleChangedAction;

	// Token: 0x04000DC3 RID: 3523
	private static string hiddenPrefix = "hidden (";

	// Token: 0x04000DC4 RID: 3524
	private static string hiddenPostfix = ")";

	// Token: 0x04000DC5 RID: 3525
	private static string[] labels = new string[]
	{
		"left",
		"right"
	};
}
