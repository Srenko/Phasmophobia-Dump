using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
[RequireComponent(typeof(InputToEvent))]
public class PointedAtGameObjectInfo : MonoBehaviour
{
	// Token: 0x060005FC RID: 1532 RVA: 0x00021FF0 File Offset: 0x000201F0
	private void OnGUI()
	{
		if (InputToEvent.goPointedAt != null)
		{
			PhotonView photonView = InputToEvent.goPointedAt.GetPhotonView();
			if (photonView != null)
			{
				GUI.Label(new Rect(Input.mousePosition.x + 5f, (float)Screen.height - Input.mousePosition.y - 15f, 300f, 30f), string.Format("ViewID {0} {1}{2}", photonView.viewID, photonView.isSceneView ? "scene " : "", photonView.isMine ? "mine" : ("owner: " + photonView.ownerId)));
			}
		}
	}
}
