using System;

// Token: 0x02000097 RID: 151
public class RaiseEventOptions
{
	// Token: 0x0600031F RID: 799 RVA: 0x0001389C File Offset: 0x00011A9C
	public void Reset()
	{
		this.CachingOption = RaiseEventOptions.Default.CachingOption;
		this.InterestGroup = RaiseEventOptions.Default.InterestGroup;
		this.TargetActors = RaiseEventOptions.Default.TargetActors;
		this.Receivers = RaiseEventOptions.Default.Receivers;
		this.SequenceChannel = RaiseEventOptions.Default.SequenceChannel;
		this.ForwardToWebhook = RaiseEventOptions.Default.ForwardToWebhook;
		this.Encrypt = RaiseEventOptions.Default.Encrypt;
	}

	// Token: 0x04000425 RID: 1061
	public static readonly RaiseEventOptions Default = new RaiseEventOptions();

	// Token: 0x04000426 RID: 1062
	public EventCaching CachingOption;

	// Token: 0x04000427 RID: 1063
	public byte InterestGroup;

	// Token: 0x04000428 RID: 1064
	public int[] TargetActors;

	// Token: 0x04000429 RID: 1065
	public ReceiverGroup Receivers;

	// Token: 0x0400042A RID: 1066
	public byte SequenceChannel;

	// Token: 0x0400042B RID: 1067
	public bool ForwardToWebhook;

	// Token: 0x0400042C RID: 1068
	public bool Encrypt;
}
