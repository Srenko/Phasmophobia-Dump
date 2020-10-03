using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[Serializable]
public class PhotonTransformViewPositionModel
{
	// Token: 0x040005A0 RID: 1440
	public bool SynchronizeEnabled;

	// Token: 0x040005A1 RID: 1441
	public bool TeleportEnabled = true;

	// Token: 0x040005A2 RID: 1442
	public float TeleportIfDistanceGreaterThan = 3f;

	// Token: 0x040005A3 RID: 1443
	public PhotonTransformViewPositionModel.InterpolateOptions InterpolateOption = PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed;

	// Token: 0x040005A4 RID: 1444
	public float InterpolateMoveTowardsSpeed = 1f;

	// Token: 0x040005A5 RID: 1445
	public float InterpolateLerpSpeed = 1f;

	// Token: 0x040005A6 RID: 1446
	public float InterpolateMoveTowardsAcceleration = 2f;

	// Token: 0x040005A7 RID: 1447
	public float InterpolateMoveTowardsDeceleration = 2f;

	// Token: 0x040005A8 RID: 1448
	public AnimationCurve InterpolateSpeedCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(-1f, 0f, 0f, float.PositiveInfinity),
		new Keyframe(0f, 1f, 0f, 0f),
		new Keyframe(1f, 1f, 0f, 1f),
		new Keyframe(4f, 4f, 1f, 0f)
	});

	// Token: 0x040005A9 RID: 1449
	public PhotonTransformViewPositionModel.ExtrapolateOptions ExtrapolateOption;

	// Token: 0x040005AA RID: 1450
	public float ExtrapolateSpeed = 1f;

	// Token: 0x040005AB RID: 1451
	public bool ExtrapolateIncludingRoundTripTime = true;

	// Token: 0x040005AC RID: 1452
	public int ExtrapolateNumberOfStoredPositions = 1;

	// Token: 0x040005AD RID: 1453
	public bool DrawErrorGizmo = true;

	// Token: 0x020004FC RID: 1276
	public enum InterpolateOptions
	{
		// Token: 0x040023F6 RID: 9206
		Disabled,
		// Token: 0x040023F7 RID: 9207
		FixedSpeed,
		// Token: 0x040023F8 RID: 9208
		EstimatedSpeed,
		// Token: 0x040023F9 RID: 9209
		SynchronizeValues,
		// Token: 0x040023FA RID: 9210
		Lerp
	}

	// Token: 0x020004FD RID: 1277
	public enum ExtrapolateOptions
	{
		// Token: 0x040023FC RID: 9212
		Disabled,
		// Token: 0x040023FD RID: 9213
		SynchronizeValues,
		// Token: 0x040023FE RID: 9214
		EstimateSpeedAndTurn,
		// Token: 0x040023FF RID: 9215
		FixedSpeed
	}
}
