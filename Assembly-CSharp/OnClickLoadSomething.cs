using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000078 RID: 120
public class OnClickLoadSomething : MonoBehaviour
{
	// Token: 0x060002AA RID: 682 RVA: 0x00011AD8 File Offset: 0x0000FCD8
	public void OnClick()
	{
		OnClickLoadSomething.ResourceTypeOption resourceTypeToLoad = this.ResourceTypeToLoad;
		if (resourceTypeToLoad == OnClickLoadSomething.ResourceTypeOption.Scene)
		{
			SceneManager.LoadScene(this.ResourceToLoad);
			return;
		}
		if (resourceTypeToLoad != OnClickLoadSomething.ResourceTypeOption.Web)
		{
			return;
		}
		Application.OpenURL(this.ResourceToLoad);
	}

	// Token: 0x040002EC RID: 748
	public OnClickLoadSomething.ResourceTypeOption ResourceTypeToLoad;

	// Token: 0x040002ED RID: 749
	public string ResourceToLoad;

	// Token: 0x020004E9 RID: 1257
	public enum ResourceTypeOption : byte
	{
		// Token: 0x040023BF RID: 9151
		Scene,
		// Token: 0x040023C0 RID: 9152
		Web
	}
}
