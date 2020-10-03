using System;
using Photon;
using UnityEngine;

// Token: 0x0200004B RID: 75
[RequireComponent(typeof(PhotonView))]
public class ShowInfoOfPlayer : Photon.MonoBehaviour
{
	// Token: 0x06000189 RID: 393 RVA: 0x0000AD28 File Offset: 0x00008F28
	private void Start()
	{
		if (this.font == null)
		{
			this.font = (Font)Resources.FindObjectsOfTypeAll(typeof(Font))[0];
			Debug.LogWarning("No font defined. Found font: " + this.font);
		}
		if (this.tm == null)
		{
			this.textGo = new GameObject("3d text");
			this.textGo.transform.parent = base.gameObject.transform;
			this.textGo.transform.localPosition = Vector3.zero;
			this.textGo.AddComponent<MeshRenderer>().material = this.font.material;
			this.tm = this.textGo.AddComponent<TextMesh>();
			this.tm.font = this.font;
			this.tm.anchor = TextAnchor.MiddleCenter;
			if (this.CharacterSize > 0f)
			{
				this.tm.characterSize = this.CharacterSize;
			}
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000AE2C File Offset: 0x0000902C
	private void Update()
	{
		bool flag = !this.DisableOnOwnObjects || base.photonView.isMine;
		if (this.textGo != null)
		{
			this.textGo.SetActive(flag);
		}
		if (!flag)
		{
			return;
		}
		PhotonPlayer owner = base.photonView.owner;
		if (owner != null)
		{
			this.tm.text = (string.IsNullOrEmpty(owner.NickName) ? ("player" + owner.ID) : owner.NickName);
			return;
		}
		if (base.photonView.isSceneView)
		{
			this.tm.text = "scn";
			return;
		}
		this.tm.text = "n/a";
	}

	// Token: 0x040001BC RID: 444
	private GameObject textGo;

	// Token: 0x040001BD RID: 445
	private TextMesh tm;

	// Token: 0x040001BE RID: 446
	public float CharacterSize;

	// Token: 0x040001BF RID: 447
	public Font font;

	// Token: 0x040001C0 RID: 448
	public bool DisableOnOwnObjects;
}
