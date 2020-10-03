using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[RequireComponent(typeof(SphereCollider))]
public class EMF : MonoBehaviour
{
	// Token: 0x0600063F RID: 1599 RVA: 0x00022C21 File Offset: 0x00020E21
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x00022C2F File Offset: 0x00020E2F
	public void SetType(EMF.Type type)
	{
		if (type == EMF.Type.GhostInteraction)
		{
			this.strength = 1;
			return;
		}
		if (type == EMF.Type.GhostThrowing)
		{
			this.strength = 2;
			return;
		}
		if (type == EMF.Type.GhostAppeared)
		{
			this.strength = 3;
			return;
		}
		if (type == EMF.Type.GhostEvidence)
		{
			this.strength = 4;
		}
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00022C60 File Offset: 0x00020E60
	private void Update()
	{
		this.timerUntilDeath -= Time.deltaTime;
		if (this.timerUntilDeath <= 0f)
		{
			this.timerUntilDeath = 20f;
			for (int i = 0; i < this.emfReaders.Count; i++)
			{
				this.emfReaders[i].RemoveEMFZone(this);
			}
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00022CCB File Offset: 0x00020ECB
	private void OnEnable()
	{
		if (EMFData.instance && !EMFData.instance.emfSpots.Contains(this))
		{
			EMFData.instance.emfSpots.Add(this);
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00022CFB File Offset: 0x00020EFB
	private void OnDisable()
	{
		if (EMFData.instance && EMFData.instance.emfSpots.Contains(this))
		{
			EMFData.instance.emfSpots.Remove(this);
		}
	}

	// Token: 0x0400061C RID: 1564
	private PhotonView view;

	// Token: 0x0400061D RID: 1565
	public List<EMFReader> emfReaders;

	// Token: 0x0400061E RID: 1566
	public int strength;

	// Token: 0x0400061F RID: 1567
	public EMF.Type type;

	// Token: 0x04000620 RID: 1568
	private float timerUntilDeath = 20f;

	// Token: 0x02000503 RID: 1283
	public enum Type
	{
		// Token: 0x04002414 RID: 9236
		GhostInteraction,
		// Token: 0x04002415 RID: 9237
		GhostThrowing,
		// Token: 0x04002416 RID: 9238
		GhostAppeared,
		// Token: 0x04002417 RID: 9239
		GhostEvidence
	}
}
