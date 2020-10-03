using System;

// Token: 0x020000C7 RID: 199
[Serializable]
public class PhotonTransformViewScaleModel
{
	// Token: 0x040005B6 RID: 1462
	public bool SynchronizeEnabled;

	// Token: 0x040005B7 RID: 1463
	public PhotonTransformViewScaleModel.InterpolateOptions InterpolateOption;

	// Token: 0x040005B8 RID: 1464
	public float InterpolateMoveTowardsSpeed = 1f;

	// Token: 0x040005B9 RID: 1465
	public float InterpolateLerpSpeed;

	// Token: 0x020004FF RID: 1279
	public enum InterpolateOptions
	{
		// Token: 0x04002405 RID: 9221
		Disabled,
		// Token: 0x04002406 RID: 9222
		MoveTowards,
		// Token: 0x04002407 RID: 9223
		Lerp
	}
}
