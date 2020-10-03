using System;
using System.Collections;
using TLGFPowerBooks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000016 RID: 22
public class SimpleBookCreatorUI : MonoBehaviour
{
	// Token: 0x060000A6 RID: 166 RVA: 0x0000519F File Offset: 0x0000339F
	private void Start()
	{
		if (this.sbc.convertMultipleFiles)
		{
			this.CreateMultipleBookPrefabs();
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000051B4 File Offset: 0x000033B4
	private void Update()
	{
		this.loadingBarPercent.fillAmount = (float)this.sbc.GetPercentComplete() / 100f;
		if (this.sbc.GetBookState() == SimpleBookCreator.BookState.OPEN && !this.loadingComplete)
		{
			this.loadingComplete = true;
			this.ShowControlUI();
		}
		if (Input.GetAxis("Horizontal") > 0f)
		{
			this.sbc.NextPage();
		}
		if (Input.GetAxis("Horizontal") < 0f)
		{
			this.sbc.PrevPage();
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00005239 File Offset: 0x00003439
	private void ShowControlUI()
	{
		this.loadingBar.SetActive(false);
		this.controlPanel.SetActive(true);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00003F60 File Offset: 0x00002160
	public void CreateBookPrefab()
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00005253 File Offset: 0x00003453
	public void CreateMultipleBookPrefabs()
	{
		base.StartCoroutine(this.CreateMultipleBooks());
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00005262 File Offset: 0x00003462
	private IEnumerator CreateMultipleBooks()
	{
		yield return null;
		yield break;
	}

	// Token: 0x04000079 RID: 121
	public SimpleBookCreator sbc;

	// Token: 0x0400007A RID: 122
	public GameObject loadingBar;

	// Token: 0x0400007B RID: 123
	public Image loadingBarPercent;

	// Token: 0x0400007C RID: 124
	public GameObject controlPanel;

	// Token: 0x0400007D RID: 125
	private bool loadingComplete;
}
