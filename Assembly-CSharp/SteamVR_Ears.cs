using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001E4 RID: 484
[RequireComponent(typeof(AudioListener))]
public class SteamVR_Ears : MonoBehaviour
{
	// Token: 0x06000D62 RID: 3426 RVA: 0x00053FA4 File Offset: 0x000521A4
	private void OnNewPosesApplied()
	{
		Transform origin = this.vrcam.origin;
		Quaternion lhs = (origin != null) ? origin.rotation : Quaternion.identity;
		base.transform.rotation = lhs * this.offset;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x00053FEC File Offset: 0x000521EC
	private void OnEnable()
	{
		this.usingSpeakers = false;
		CVRSettings settings = OpenVR.Settings;
		if (settings != null)
		{
			EVRSettingsError evrsettingsError = EVRSettingsError.None;
			if (settings.GetBool("steamvr", "usingSpeakers", ref evrsettingsError))
			{
				this.usingSpeakers = true;
				float @float = settings.GetFloat("steamvr", "speakersForwardYawOffsetDegrees", ref evrsettingsError);
				this.offset = Quaternion.Euler(0f, @float, 0f);
			}
		}
		if (this.usingSpeakers)
		{
			SteamVR_Events.NewPosesApplied.Listen(new UnityAction(this.OnNewPosesApplied));
		}
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0005406D File Offset: 0x0005226D
	private void OnDisable()
	{
		if (this.usingSpeakers)
		{
			SteamVR_Events.NewPosesApplied.Remove(new UnityAction(this.OnNewPosesApplied));
		}
	}

	// Token: 0x04000DC6 RID: 3526
	public SteamVR_Camera vrcam;

	// Token: 0x04000DC7 RID: 3527
	private bool usingSpeakers;

	// Token: 0x04000DC8 RID: 3528
	private Quaternion offset;
}
