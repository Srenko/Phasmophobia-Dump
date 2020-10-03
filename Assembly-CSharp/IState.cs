using System;

// Token: 0x0200010D RID: 269
public interface IState
{
	// Token: 0x06000726 RID: 1830
	void Enter();

	// Token: 0x06000727 RID: 1831
	void Execute();

	// Token: 0x06000728 RID: 1832
	void Exit();
}
