using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class HandAnimatorManager : MonoBehaviour
{
	// Token: 0x06000100 RID: 256 RVA: 0x0000865E File Offset: 0x0000685E
	private void Start()
	{
		this.handAnimator = base.GetComponent<Animator>();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000866C File Offset: 0x0000686C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			this.currentState = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.currentState = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.currentState = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.currentState = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			this.currentState = 4;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			this.currentState = 5;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			this.currentState = 6;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			this.currentState = 7;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			this.currentState = 8;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			this.currentState = 9;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			this.currentState = 10;
		}
		else if (Input.GetKeyDown(KeyCode.I))
		{
			this.currentState = 100;
		}
		if (this.lastState != this.currentState)
		{
			this.lastState = this.currentState;
			this.handAnimator.SetInteger("State", this.currentState);
			this.TurnOnState(this.currentState);
		}
		this.handAnimator.SetBool("Action", Input.GetMouseButton(0));
		this.handAnimator.SetBool("Hold", Input.GetMouseButton(1));
	}

	// Token: 0x06000102 RID: 258 RVA: 0x000087C8 File Offset: 0x000069C8
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

	// Token: 0x04000128 RID: 296
	public StateModel[] stateModels;

	// Token: 0x04000129 RID: 297
	private Animator handAnimator;

	// Token: 0x0400012A RID: 298
	public int currentState = 100;

	// Token: 0x0400012B RID: 299
	private int lastState = -1;
}
