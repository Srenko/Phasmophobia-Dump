using System;
using System.Collections;
using Photon;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class OnClickFlashRpc : PunBehaviour
{
	// Token: 0x0600016F RID: 367 RVA: 0x0000A631 File Offset: 0x00008831
	private void OnClick()
	{
		base.photonView.RPC("Flash", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000A649 File Offset: 0x00008849
	[PunRPC]
	private IEnumerator Flash()
	{
		if (this.isFlashing)
		{
			yield break;
		}
		this.isFlashing = true;
		this.originalMaterial = base.GetComponent<Renderer>().material;
		if (!this.originalMaterial.HasProperty("_Emission"))
		{
			Debug.LogWarning("Doesnt have emission, can't flash " + base.gameObject);
			yield break;
		}
		this.originalColor = this.originalMaterial.GetColor("_Emission");
		this.originalMaterial.SetColor("_Emission", Color.white);
		for (float f = 0f; f <= 1f; f += 0.08f)
		{
			Color value = Color.Lerp(Color.white, this.originalColor, f);
			this.originalMaterial.SetColor("_Emission", value);
			yield return null;
		}
		this.originalMaterial.SetColor("_Emission", this.originalColor);
		this.isFlashing = false;
		yield break;
	}

	// Token: 0x040001AA RID: 426
	private Material originalMaterial;

	// Token: 0x040001AB RID: 427
	private Color originalColor;

	// Token: 0x040001AC RID: 428
	private bool isFlashing;
}
