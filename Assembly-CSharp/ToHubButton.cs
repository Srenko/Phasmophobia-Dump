using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000058 RID: 88
public class ToHubButton : MonoBehaviour
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000CA41 File Offset: 0x0000AC41
	public static ToHubButton Instance
	{
		get
		{
			if (ToHubButton.instance == null)
			{
				ToHubButton.instance = (Object.FindObjectOfType(typeof(ToHubButton)) as ToHubButton);
			}
			return ToHubButton.instance;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000CA6E File Offset: 0x0000AC6E
	public void Awake()
	{
		if (ToHubButton.Instance != null && ToHubButton.Instance != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000CA95 File Offset: 0x0000AC95
	public void Start()
	{
		if (this.ButtonTexture == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
	public void OnGUI()
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			int num = this.ButtonTexture.width + 4;
			int num2 = this.ButtonTexture.height + 4;
			this.ButtonRect = new Rect((float)(Screen.width - num), (float)(Screen.height - num2), (float)num, (float)num2);
			if (GUI.Button(this.ButtonRect, this.ButtonTexture, GUIStyle.none))
			{
				PhotonNetwork.Disconnect();
				SceneManager.LoadScene(0);
			}
		}
	}

	// Token: 0x040001F6 RID: 502
	public Texture2D ButtonTexture;

	// Token: 0x040001F7 RID: 503
	private Rect ButtonRect;

	// Token: 0x040001F8 RID: 504
	private static ToHubButton instance;
}
