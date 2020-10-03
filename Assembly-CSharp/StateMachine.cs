using System;

// Token: 0x0200010E RID: 270
public class StateMachine
{
	// Token: 0x06000729 RID: 1833 RVA: 0x00029F88 File Offset: 0x00028188
	public void ChangeState(IState newState)
	{
		if (this.currentState != null)
		{
			this.currentState.Exit();
			this.previousState = this.currentState;
		}
		this.currentState = newState;
		this.currentState.Enter();
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00029FBB File Offset: 0x000281BB
	public void ExecuteStateUpdate()
	{
		if (this.currentState != null)
		{
			this.currentState.Execute();
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00029FD0 File Offset: 0x000281D0
	public void ChangeToPreviousState()
	{
		if (this.currentState != null)
		{
			this.currentState.Exit();
		}
		this.currentState = this.previousState;
		this.currentState.Enter();
	}

	// Token: 0x040006D6 RID: 1750
	private IState currentState;

	// Token: 0x040006D7 RID: 1751
	private IState previousState;
}
