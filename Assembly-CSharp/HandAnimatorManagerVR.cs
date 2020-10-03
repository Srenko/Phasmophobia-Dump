using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class HandAnimatorManagerVR : MonoBehaviour
{
	// Token: 0x06000105 RID: 261 RVA: 0x0000884C File Offset: 0x00006A4C
	private void Start()
	{
		string[] joystickNames = Input.GetJoystickNames();
		for (int i = 0; i < joystickNames.Length; i++)
		{
			Debug.Log(joystickNames[i]);
		}
		this.handAnimator = base.GetComponent<Animator>();
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00008884 File Offset: 0x00006A84
	private void Update()
	{
		if (Input.GetKeyUp(this.changeKey))
		{
			this.currentState = (this.currentState + 1) % (this.numberOfAnimations + 1);
		}
		if (Input.GetAxis(this.holdKey) > 0f)
		{
			this.hold = true;
		}
		else
		{
			this.hold = false;
		}
		if (Input.GetKey(this.actionKey))
		{
			this.action = true;
		}
		else
		{
			this.action = false;
		}
		if (this.lastState != this.currentState)
		{
			this.lastState = this.currentState;
			this.handAnimator.SetInteger("State", this.currentState);
			this.TurnOnState(this.currentState);
		}
		this.handAnimator.SetBool("Action", this.action);
		this.handAnimator.SetBool("Hold", this.hold);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000895C File Offset: 0x00006B5C
	private void TurnOnState(int stateNumber)
	{
		foreach (StateModel stateModel in this.stateModels)
		{
			if (stateModel.stateNumber == stateNumber && !stateModel.go.activeSelf)
			{
				stateModel.go.SetActive(true);
			}
			else if (stateModel.go.activeSelf)
			{
				stateModel.go.SetActive(false);
			}
		}
	}

	// Token: 0x0400012E RID: 302
	public StateModel[] stateModels;

	// Token: 0x0400012F RID: 303
	private Animator handAnimator;

	// Token: 0x04000130 RID: 304
	public int currentState = 100;

	// Token: 0x04000131 RID: 305
	private int lastState = -1;

	// Token: 0x04000132 RID: 306
	public bool action;

	// Token: 0x04000133 RID: 307
	public bool hold;

	// Token: 0x04000134 RID: 308
	public string changeKey = "joystick button 9";

	// Token: 0x04000135 RID: 309
	public string actionKey = "joystick button 15";

	// Token: 0x04000136 RID: 310
	public string holdKey = "Axis 12";

	// Token: 0x04000137 RID: 311
	public int numberOfAnimations = 8;
}
