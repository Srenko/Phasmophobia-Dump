using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class OnClickInstantiate : MonoBehaviour
{
	// Token: 0x060005DC RID: 1500 RVA: 0x00021634 File Offset: 0x0001F834
	private void OnClick()
	{
		if (!PhotonNetwork.inRoom)
		{
			return;
		}
		int instantiateType = this.InstantiateType;
		if (instantiateType == 0)
		{
			PhotonNetwork.Instantiate(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
			return;
		}
		if (instantiateType != 1)
		{
			return;
		}
		PhotonNetwork.InstantiateSceneObject(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity, 0, null);
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x000216C4 File Offset: 0x0001F8C4
	private void OnGUI()
	{
		if (this.showGui)
		{
			GUILayout.BeginArea(new Rect((float)(Screen.width - 180), 0f, 180f, 50f));
			this.InstantiateType = GUILayout.Toolbar(this.InstantiateType, this.InstantiateTypeNames, Array.Empty<GUILayoutOption>());
			GUILayout.EndArea();
		}
	}

	// Token: 0x040005F6 RID: 1526
	public GameObject Prefab;

	// Token: 0x040005F7 RID: 1527
	public int InstantiateType;

	// Token: 0x040005F8 RID: 1528
	private string[] InstantiateTypeNames = new string[]
	{
		"Mine",
		"Scene"
	};

	// Token: 0x040005F9 RID: 1529
	public bool showGui;
}
