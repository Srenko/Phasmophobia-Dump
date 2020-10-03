using System;

// Token: 0x020000CA RID: 202
public class CellTree
{
	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00020761 File Offset: 0x0001E961
	// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00020769 File Offset: 0x0001E969
	public CellTreeNode RootNode { get; private set; }

	// Token: 0x060005B3 RID: 1459 RVA: 0x00008842 File Offset: 0x00006A42
	public CellTree()
	{
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00020772 File Offset: 0x0001E972
	public CellTree(CellTreeNode root)
	{
		this.RootNode = root;
	}
}
