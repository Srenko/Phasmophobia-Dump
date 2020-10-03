using System;
using Photon;
using UnityEngine.SceneManagement;
using Valve.VR;

// Token: 0x020001BA RID: 442
public class TrailerVRController : MonoBehaviour
{
	// Token: 0x06000C25 RID: 3109 RVA: 0x0004C618 File Offset: 0x0004A818
	private void Awake()
	{
		if (SceneManager.GetActiveScene().name == "Menu_New")
		{
			base.enabled = false;
		}
		this.trackedObject = base.GetComponent<SteamVR_TrackedObject>();
	}

	// Token: 0x04000CA7 RID: 3239
	private EVRButtonId A_Button = EVRButtonId.k_EButton_A;

	// Token: 0x04000CA8 RID: 3240
	private EVRButtonId B_Button = EVRButtonId.k_EButton_ApplicationMenu;

	// Token: 0x04000CA9 RID: 3241
	private SteamVR_TrackedObject trackedObject;

	// Token: 0x04000CAA RID: 3242
	private SteamVR_Controller.Device device;
}
