using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000193 RID: 403
public class SplashScreen : MonoBehaviour
{
	// Token: 0x06000AE6 RID: 2790 RVA: 0x0004447E File Offset: 0x0004267E
	private void Awake()
	{
		if (XRDevice.isPresent)
		{
			this.loadLevel.Trigger();
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00044494 File Offset: 0x00042694
	private void Update()
	{
		if (XRDevice.isPresent)
		{
			return;
		}
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			this.timer = 5.1f;
			this.index++;
			if (this.index == 1)
			{
				this.mainText.text = LocalisationSystem.GetLocalisedValue("Splash_VoiceRecognition");
				this.headphonesImage.SetActive(false);
				base.StartCoroutine(this.ResetFade());
				return;
			}
			if (this.index == 2)
			{
				this.mainScreen.SetActive(false);
				this.loadingScreen.SetActive(true);
				this.loadingAsyncManager.LoadScene("Menu_New");
			}
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0004454D File Offset: 0x0004274D
	private IEnumerator ResetFade()
	{
		this.fadeObject.SetActive(false);
		yield return new WaitForEndOfFrame();
		this.fadeObject.SetActive(true);
		yield break;
	}

	// Token: 0x04000B32 RID: 2866
	private float timer = 5.1f;

	// Token: 0x04000B33 RID: 2867
	[SerializeField]
	private SteamVR_LoadLevel loadLevel;

	// Token: 0x04000B34 RID: 2868
	[SerializeField]
	private GameObject mainScreen;

	// Token: 0x04000B35 RID: 2869
	[SerializeField]
	private GameObject loadingScreen;

	// Token: 0x04000B36 RID: 2870
	[SerializeField]
	private LoadingAsyncManager loadingAsyncManager;

	// Token: 0x04000B37 RID: 2871
	private int index;

	// Token: 0x04000B38 RID: 2872
	[SerializeField]
	private Text mainText;

	// Token: 0x04000B39 RID: 2873
	[SerializeField]
	private GameObject fadeObject;

	// Token: 0x04000B3A RID: 2874
	[SerializeField]
	private GameObject headphonesImage;
}
