using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[HelpURL("https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/package-demos/culling-demo")]
public class CullArea : MonoBehaviour
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0002002E File Offset: 0x0001E22E
	// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00020036 File Offset: 0x0001E236
	public int CellCount { get; private set; }

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0002003F File Offset: 0x0001E23F
	// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00020047 File Offset: 0x0001E247
	public CellTree CellTree { get; private set; }

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00020050 File Offset: 0x0001E250
	// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00020058 File Offset: 0x0001E258
	public Dictionary<int, GameObject> Map { get; private set; }

	// Token: 0x060005A9 RID: 1449 RVA: 0x00020061 File Offset: 0x0001E261
	private void Awake()
	{
		this.idCounter = this.FIRST_GROUP_ID;
		this.CreateCellHierarchy();
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00020075 File Offset: 0x0001E275
	public void OnDrawGizmos()
	{
		this.idCounter = this.FIRST_GROUP_ID;
		if (this.RecreateCellHierarchy)
		{
			this.CreateCellHierarchy();
		}
		this.DrawCells();
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00020098 File Offset: 0x0001E298
	private void CreateCellHierarchy()
	{
		if (!this.IsCellCountAllowed())
		{
			if (Debug.isDebugBuild)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"There are too many cells created by your subdivision options. Maximum allowed number of cells is ",
					(int)(250 - this.FIRST_GROUP_ID),
					". Current number of cells is ",
					this.CellCount,
					"."
				}));
				return;
			}
			Application.Quit();
		}
		byte b = this.idCounter;
		this.idCounter = b + 1;
		CellTreeNode cellTreeNode = new CellTreeNode(b, CellTreeNode.ENodeType.Root, null);
		if (this.YIsUpAxis)
		{
			this.Center = new Vector2(base.transform.position.x, base.transform.position.y);
			this.Size = new Vector2(base.transform.localScale.x, base.transform.localScale.y);
			cellTreeNode.Center = new Vector3(this.Center.x, this.Center.y, 0f);
			cellTreeNode.Size = new Vector3(this.Size.x, this.Size.y, 0f);
			cellTreeNode.TopLeft = new Vector3(this.Center.x - this.Size.x / 2f, this.Center.y - this.Size.y / 2f, 0f);
			cellTreeNode.BottomRight = new Vector3(this.Center.x + this.Size.x / 2f, this.Center.y + this.Size.y / 2f, 0f);
		}
		else
		{
			this.Center = new Vector2(base.transform.position.x, base.transform.position.z);
			this.Size = new Vector2(base.transform.localScale.x, base.transform.localScale.z);
			cellTreeNode.Center = new Vector3(this.Center.x, 0f, this.Center.y);
			cellTreeNode.Size = new Vector3(this.Size.x, 0f, this.Size.y);
			cellTreeNode.TopLeft = new Vector3(this.Center.x - this.Size.x / 2f, 0f, this.Center.y - this.Size.y / 2f);
			cellTreeNode.BottomRight = new Vector3(this.Center.x + this.Size.x / 2f, 0f, this.Center.y + this.Size.y / 2f);
		}
		this.CreateChildCells(cellTreeNode, 1);
		this.CellTree = new CellTree(cellTreeNode);
		this.RecreateCellHierarchy = false;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000203B8 File Offset: 0x0001E5B8
	private void CreateChildCells(CellTreeNode parent, int cellLevelInHierarchy)
	{
		if (cellLevelInHierarchy > this.NumberOfSubdivisions)
		{
			return;
		}
		int num = (int)this.Subdivisions[cellLevelInHierarchy - 1].x;
		int num2 = (int)this.Subdivisions[cellLevelInHierarchy - 1].y;
		float num3 = parent.Center.x - parent.Size.x / 2f;
		float num4 = parent.Size.x / (float)num;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				float num5 = num3 + (float)i * num4 + num4 / 2f;
				byte b = this.idCounter;
				this.idCounter = b + 1;
				CellTreeNode cellTreeNode = new CellTreeNode(b, (this.NumberOfSubdivisions == cellLevelInHierarchy) ? CellTreeNode.ENodeType.Leaf : CellTreeNode.ENodeType.Node, parent);
				if (this.YIsUpAxis)
				{
					float num6 = parent.Center.y - parent.Size.y / 2f;
					float num7 = parent.Size.y / (float)num2;
					float num8 = num6 + (float)j * num7 + num7 / 2f;
					cellTreeNode.Center = new Vector3(num5, num8, 0f);
					cellTreeNode.Size = new Vector3(num4, num7, 0f);
					cellTreeNode.TopLeft = new Vector3(num5 - num4 / 2f, num8 - num7 / 2f, 0f);
					cellTreeNode.BottomRight = new Vector3(num5 + num4 / 2f, num8 + num7 / 2f, 0f);
				}
				else
				{
					float num9 = parent.Center.z - parent.Size.z / 2f;
					float num10 = parent.Size.z / (float)num2;
					float num11 = num9 + (float)j * num10 + num10 / 2f;
					cellTreeNode.Center = new Vector3(num5, 0f, num11);
					cellTreeNode.Size = new Vector3(num4, 0f, num10);
					cellTreeNode.TopLeft = new Vector3(num5 - num4 / 2f, 0f, num11 - num10 / 2f);
					cellTreeNode.BottomRight = new Vector3(num5 + num4 / 2f, 0f, num11 + num10 / 2f);
				}
				parent.AddChild(cellTreeNode);
				this.CreateChildCells(cellTreeNode, cellLevelInHierarchy + 1);
			}
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00020618 File Offset: 0x0001E818
	private void DrawCells()
	{
		if (this.CellTree != null && this.CellTree.RootNode != null)
		{
			this.CellTree.RootNode.Draw();
			return;
		}
		this.RecreateCellHierarchy = true;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00020648 File Offset: 0x0001E848
	private bool IsCellCountAllowed()
	{
		int num = 1;
		int num2 = 1;
		foreach (Vector2 vector in this.Subdivisions)
		{
			num *= (int)vector.x;
			num2 *= (int)vector.y;
		}
		this.CellCount = num * num2;
		return this.CellCount <= (int)(250 - this.FIRST_GROUP_ID);
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000206B0 File Offset: 0x0001E8B0
	public List<byte> GetActiveCells(Vector3 position)
	{
		List<byte> list = new List<byte>(0);
		this.CellTree.RootNode.GetActiveCells(list, this.YIsUpAxis, position);
		return list;
	}

	// Token: 0x040005BD RID: 1469
	private const int MAX_NUMBER_OF_ALLOWED_CELLS = 250;

	// Token: 0x040005BE RID: 1470
	public const int MAX_NUMBER_OF_SUBDIVISIONS = 3;

	// Token: 0x040005BF RID: 1471
	public readonly byte FIRST_GROUP_ID = 1;

	// Token: 0x040005C0 RID: 1472
	public readonly int[] SUBDIVISION_FIRST_LEVEL_ORDER = new int[]
	{
		0,
		1,
		1,
		1
	};

	// Token: 0x040005C1 RID: 1473
	public readonly int[] SUBDIVISION_SECOND_LEVEL_ORDER = new int[]
	{
		0,
		2,
		1,
		2,
		0,
		2,
		1,
		2
	};

	// Token: 0x040005C2 RID: 1474
	public readonly int[] SUBDIVISION_THIRD_LEVEL_ORDER = new int[]
	{
		0,
		3,
		2,
		3,
		1,
		3,
		2,
		3,
		1,
		3,
		2,
		3
	};

	// Token: 0x040005C3 RID: 1475
	public Vector2 Center;

	// Token: 0x040005C4 RID: 1476
	public Vector2 Size = new Vector2(25f, 25f);

	// Token: 0x040005C5 RID: 1477
	public Vector2[] Subdivisions = new Vector2[3];

	// Token: 0x040005C6 RID: 1478
	public int NumberOfSubdivisions;

	// Token: 0x040005CA RID: 1482
	public bool YIsUpAxis;

	// Token: 0x040005CB RID: 1483
	public bool RecreateCellHierarchy;

	// Token: 0x040005CC RID: 1484
	private byte idCounter;
}
