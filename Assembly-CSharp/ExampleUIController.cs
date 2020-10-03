using System;
using TLGFPowerBooks;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class ExampleUIController : MonoBehaviour
{
	// Token: 0x060000CA RID: 202 RVA: 0x00006701 File Offset: 0x00004901
	public void OpenBook()
	{
		this.pBook.OpenBook();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000670E File Offset: 0x0000490E
	public void CloseBook()
	{
		this.pBook.CloseBook();
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000671B File Offset: 0x0000491B
	public void NextPage()
	{
		this.pBook.NextPage();
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00006728 File Offset: 0x00004928
	public void PrevPage()
	{
		this.pBook.PrevPage();
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00006735 File Offset: 0x00004935
	public void GoToLastPage()
	{
		this.pBook.GoToLastPage(50f);
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006747 File Offset: 0x00004947
	public void GoToFirstPage()
	{
		this.pBook.GoToFirstPage(50f);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00006759 File Offset: 0x00004959
	public void JumpToLastPage()
	{
		this.pBook.JumpToLastPage(true);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00006767 File Offset: 0x00004967
	public void JumpToFirstPage()
	{
		this.pBook.JumpToFirstPage(true);
	}

	// Token: 0x040000B6 RID: 182
	public PBook pBook;
}
