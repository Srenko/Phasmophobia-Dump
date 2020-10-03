using System;
using TLGFPowerBooks;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class ExampleEventHandler : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00005A54 File Offset: 0x00003C54
	private void OnEnable()
	{
		PBook.OnBookOpened += this.OpenBookChangeColor;
		PBook.OnBookWillOpen += this.BookWillOpenChangeColor;
		PBook.OnBookClosed += this.CloseBookChangeColor;
		PBook.OnBookWillClose += this.BookWillCloseChangeColor;
		PBook.OnBookLastPage += this.LastPageChangeColor;
		PBook.OnBookFirstPage += this.FirstPageChangeColor;
		PBook.OnBookTurnToLastPage += this.TurnToLastPageChangeColor;
		PBook.OnBookTurnToFirstPage += this.TurnToFirstPageChangeColor;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00005AEC File Offset: 0x00003CEC
	private void OnDisable()
	{
		PBook.OnBookOpened -= this.OpenBookChangeColor;
		PBook.OnBookWillOpen -= this.BookWillOpenChangeColor;
		PBook.OnBookClosed -= this.CloseBookChangeColor;
		PBook.OnBookWillClose -= this.BookWillCloseChangeColor;
		PBook.OnBookLastPage -= this.LastPageChangeColor;
		PBook.OnBookFirstPage -= this.FirstPageChangeColor;
		PBook.OnBookTurnToLastPage -= this.TurnToLastPageChangeColor;
		PBook.OnBookTurnToFirstPage -= this.TurnToFirstPageChangeColor;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00005B84 File Offset: 0x00003D84
	private void OpenBookChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.openBookColor);
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00005BD4 File Offset: 0x00003DD4
	private void BookWillOpenChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.willOpenBookColor);
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005C24 File Offset: 0x00003E24
	public void CloseBookChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.closeBookColor);
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005C74 File Offset: 0x00003E74
	private void BookWillCloseChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.willCloseBookColor);
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00005CC4 File Offset: 0x00003EC4
	private void LastPageChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.lastPageColor);
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005D14 File Offset: 0x00003F14
	private void FirstPageChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.firstPageColor);
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00005D64 File Offset: 0x00003F64
	private void TurnToLastPageChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.enterLastPageColor);
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00005DB4 File Offset: 0x00003FB4
	private void TurnToFirstPageChangeColor(GameObject sender)
	{
		if (this.pbook != null && sender == this.pbook.gameObject)
		{
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", this.enterFirstPageColor);
		}
	}

	// Token: 0x0400008A RID: 138
	public PBook pbook;

	// Token: 0x0400008B RID: 139
	public Color openBookColor;

	// Token: 0x0400008C RID: 140
	public Color willOpenBookColor;

	// Token: 0x0400008D RID: 141
	public Color closeBookColor;

	// Token: 0x0400008E RID: 142
	public Color willCloseBookColor;

	// Token: 0x0400008F RID: 143
	public Color lastPageColor;

	// Token: 0x04000090 RID: 144
	public Color firstPageColor;

	// Token: 0x04000091 RID: 145
	public Color enterLastPageColor;

	// Token: 0x04000092 RID: 146
	public Color enterFirstPageColor;
}
