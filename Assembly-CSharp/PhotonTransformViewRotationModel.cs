using System;

// Token: 0x020000C5 RID: 197
[Serializable]
public class PhotonTransformViewRotationModel
{
	// Token: 0x040005B0 RID: 1456
	public bool SynchronizeEnabled;

	// Token: 0x040005B1 RID: 1457
	public PhotonTransformViewRotationModel.InterpolateOptions InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards;

	// Token: 0x040005B2 RID: 1458
	public float InterpolateRotateTowardsSpeed = 180f;

	// Token: 0x040005B3 RID: 1459
	public float InterpolateLerpSpeed = 5f;

	// Token: 0x020004FE RID: 1278
	public enum InterpolateOptions
	{
		// Token: 0x04002401 RID: 9217
		Disabled,
		// Token: 0x04002402 RID: 9218
		RotateTowards,
		// Token: 0x04002403 RID: 9219
		Lerp
	}
}
