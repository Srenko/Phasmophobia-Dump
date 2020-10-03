using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class Painting : MonoBehaviour
{
	// Token: 0x060008B2 RID: 2226 RVA: 0x00034931 File Offset: 0x00032B31
	private void Awake()
	{
		this.rigid = base.GetComponent<Rigidbody>();
		this.noise = base.GetComponentInChildren<Noise>();
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0003494B File Offset: 0x00032B4B
	private void Start()
	{
		if (this.noise != null)
		{
			this.noise.gameObject.SetActive(false);
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0003496C File Offset: 0x00032B6C
	public void KnockOver()
	{
		this.rigid.isKinematic = false;
		this.rigid.useGravity = true;
		base.enabled = false;
		if (this.noise != null)
		{
			this.PlayNoiseObject();
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x000349A2 File Offset: 0x00032BA2
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x040008CB RID: 2251
	private Rigidbody rigid;

	// Token: 0x040008CC RID: 2252
	private Noise noise;
}
