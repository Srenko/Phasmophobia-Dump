using System;
using Photon;
using UnityEngine;

// Token: 0x02000055 RID: 85
[RequireComponent(typeof(Collider))]
public class OnClickCallMethod : Photon.MonoBehaviour
{
	// Token: 0x060001CC RID: 460 RVA: 0x0000C33D File Offset: 0x0000A53D
	public void OnClick()
	{
		if (this.TargetGameObject == null || string.IsNullOrEmpty(this.TargetMethod))
		{
			Debug.LogWarning(this + " can't call, cause GO or Method are empty.");
			return;
		}
		this.TargetGameObject.SendMessage(this.TargetMethod);
	}

	// Token: 0x040001EA RID: 490
	public GameObject TargetGameObject;

	// Token: 0x040001EB RID: 491
	public string TargetMethod;
}
