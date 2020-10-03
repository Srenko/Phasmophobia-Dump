using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000184 RID: 388
public class LoadingAsyncManager : MonoBehaviour
{
	// Token: 0x06000A58 RID: 2648 RVA: 0x000403F6 File Offset: 0x0003E5F6
	public void LoadScene(string levelToLoad)
	{
		base.StartCoroutine(this.PCLoadLevel(levelToLoad));
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00040406 File Offset: 0x0003E606
	private IEnumerator PCLoadLevel(string levelToLoad)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);
		while (!async.isDone)
		{
			if (this.progressText)
			{
				this.progressText.text = (async.progress * 100f).ToString("0") + "%";
			}
			if (async.progress == 0.9f && MainManager.instance && MainManager.instance.localPlayer && XRDevice.isPresent)
			{
				MainManager.instance.localPlayer.pcCanvas.LoadingGame();
			}
			yield return null;
		}
		if (MainManager.instance && MainManager.instance.localPlayer)
		{
			MainManager.instance.localPlayer.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x04000A7F RID: 2687
	[SerializeField]
	private Text progressText;
}
