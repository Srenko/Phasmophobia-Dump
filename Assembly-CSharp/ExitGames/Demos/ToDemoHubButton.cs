using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace ExitGames.Demos
{
	// Token: 0x02000483 RID: 1155
	public class ToDemoHubButton : MonoBehaviour
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x000B07FC File Offset: 0x000AE9FC
		public static ToDemoHubButton Instance
		{
			get
			{
				if (ToDemoHubButton.instance == null)
				{
					ToDemoHubButton.instance = (Object.FindObjectOfType(typeof(ToDemoHubButton)) as ToDemoHubButton);
				}
				return ToDemoHubButton.instance;
			}
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000B0829 File Offset: 0x000AEA29
		public void Awake()
		{
			if (ToDemoHubButton.Instance != null && ToDemoHubButton.Instance != this)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000B0850 File Offset: 0x000AEA50
		public void Start()
		{
			Object.DontDestroyOnLoad(base.gameObject);
			this._canvasGroup = base.GetComponent<CanvasGroup>();
			SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode loadingMode)
			{
				this.CalledOnLevelWasLoaded(scene.buildIndex);
			};
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000B087A File Offset: 0x000AEA7A
		private void CalledOnLevelWasLoaded(int level)
		{
			Debug.Log("CalledOnLevelWasLoaded");
			if (EventSystem.current == null)
			{
				Debug.LogError("no eventSystem");
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000B08A0 File Offset: 0x000AEAA0
		public void Update()
		{
			bool flag = SceneManager.GetActiveScene().buildIndex == 0;
			if (flag && this._canvasGroup.alpha != 0f)
			{
				this._canvasGroup.alpha = 0f;
				this._canvasGroup.interactable = false;
			}
			if (!flag && this._canvasGroup.alpha != 1f)
			{
				this._canvasGroup.alpha = 1f;
				this._canvasGroup.interactable = true;
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000B091E File Offset: 0x000AEB1E
		public void BackToHub()
		{
			PhotonNetwork.Disconnect();
			SceneManager.LoadScene(0);
		}

		// Token: 0x0400215A RID: 8538
		private static ToDemoHubButton instance;

		// Token: 0x0400215B RID: 8539
		private CanvasGroup _canvasGroup;
	}
}
