using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class CellTreeNode
{
	// Token: 0x060005B5 RID: 1461 RVA: 0x00008842 File Offset: 0x00006A42
	public CellTreeNode()
	{
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00020781 File Offset: 0x0001E981
	public CellTreeNode(byte id, CellTreeNode.ENodeType nodeType, CellTreeNode parent)
	{
		this.Id = id;
		this.NodeType = nodeType;
		this.Parent = parent;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0002079E File Offset: 0x0001E99E
	public void AddChild(CellTreeNode child)
	{
		if (this.Childs == null)
		{
			this.Childs = new List<CellTreeNode>(1);
		}
		this.Childs.Add(child);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00003F60 File Offset: 0x00002160
	public void Draw()
	{
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x000207C0 File Offset: 0x0001E9C0
	public void GetActiveCells(List<byte> activeCells, bool yIsUpAxis, Vector3 position)
	{
		if (this.NodeType != CellTreeNode.ENodeType.Leaf)
		{
			using (List<CellTreeNode>.Enumerator enumerator = this.Childs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CellTreeNode cellTreeNode = enumerator.Current;
					cellTreeNode.GetActiveCells(activeCells, yIsUpAxis, position);
				}
				return;
			}
		}
		if (this.IsPointNearCell(yIsUpAxis, position))
		{
			if (this.IsPointInsideCell(yIsUpAxis, position))
			{
				activeCells.Insert(0, this.Id);
				for (CellTreeNode parent = this.Parent; parent != null; parent = parent.Parent)
				{
					activeCells.Insert(0, parent.Id);
				}
				return;
			}
			activeCells.Add(this.Id);
		}
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0002086C File Offset: 0x0001EA6C
	public bool IsPointInsideCell(bool yIsUpAxis, Vector3 point)
	{
		if (point.x < this.TopLeft.x || point.x > this.BottomRight.x)
		{
			return false;
		}
		if (yIsUpAxis)
		{
			if (point.y >= this.TopLeft.y && point.y <= this.BottomRight.y)
			{
				return true;
			}
		}
		else if (point.z >= this.TopLeft.z && point.z <= this.BottomRight.z)
		{
			return true;
		}
		return false;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x000208F8 File Offset: 0x0001EAF8
	public bool IsPointNearCell(bool yIsUpAxis, Vector3 point)
	{
		if (this.maxDistance == 0f)
		{
			this.maxDistance = (this.Size.x + this.Size.y + this.Size.z) / 2f;
		}
		return (point - this.Center).sqrMagnitude <= this.maxDistance * this.maxDistance;
	}

	// Token: 0x040005CE RID: 1486
	public byte Id;

	// Token: 0x040005CF RID: 1487
	public Vector3 Center;

	// Token: 0x040005D0 RID: 1488
	public Vector3 Size;

	// Token: 0x040005D1 RID: 1489
	public Vector3 TopLeft;

	// Token: 0x040005D2 RID: 1490
	public Vector3 BottomRight;

	// Token: 0x040005D3 RID: 1491
	public CellTreeNode.ENodeType NodeType;

	// Token: 0x040005D4 RID: 1492
	public CellTreeNode Parent;

	// Token: 0x040005D5 RID: 1493
	public List<CellTreeNode> Childs;

	// Token: 0x040005D6 RID: 1494
	private float maxDistance;

	// Token: 0x02000500 RID: 1280
	public enum ENodeType
	{
		// Token: 0x04002409 RID: 9225
		Root,
		// Token: 0x0400240A RID: 9226
		Node,
		// Token: 0x0400240B RID: 9227
		Leaf
	}
}
