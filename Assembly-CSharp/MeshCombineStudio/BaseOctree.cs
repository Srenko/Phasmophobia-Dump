using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004BA RID: 1210
	public class BaseOctree
	{
		// Token: 0x020007B5 RID: 1973
		public class Cell
		{
			// Token: 0x0600306B RID: 12395 RVA: 0x00008842 File Offset: 0x00006A42
			public Cell()
			{
			}

			// Token: 0x0600306C RID: 12396 RVA: 0x000CD7F0 File Offset: 0x000CB9F0
			public Cell(Vector3 position, Vector3 size, int maxLevels)
			{
				this.bounds = new Bounds(position, size);
				this.maxLevels = maxLevels;
			}

			// Token: 0x0600306D RID: 12397 RVA: 0x000CD80C File Offset: 0x000CBA0C
			public Cell(BaseOctree.Cell parent, int cellIndex, Bounds bounds)
			{
				if (parent != null)
				{
					this.maxLevels = parent.maxLevels;
					this.mainParent = parent.mainParent;
					this.level = parent.level + 1;
				}
				this.parent = parent;
				this.cellIndex = cellIndex;
				this.bounds = bounds;
			}

			// Token: 0x0600306E RID: 12398 RVA: 0x000CD85D File Offset: 0x000CBA5D
			public void SetCell(BaseOctree.Cell parent, int cellIndex, Bounds bounds)
			{
				if (parent != null)
				{
					this.maxLevels = parent.maxLevels;
					this.mainParent = parent.mainParent;
					this.level = parent.level + 1;
				}
				this.parent = parent;
				this.cellIndex = cellIndex;
				this.bounds = bounds;
			}

			// Token: 0x0600306F RID: 12399 RVA: 0x000CD8A0 File Offset: 0x000CBAA0
			protected int AddCell<T, U>(ref T[] cells, Vector3 position, out bool maxCellCreated) where T : BaseOctree.Cell, new() where U : BaseOctree.Cell, new()
			{
				Vector3 vector = position - this.bounds.min;
				int num = (int)(vector.x / this.bounds.extents.x);
				int num2 = (int)(vector.y / this.bounds.extents.y);
				int num3 = (int)(vector.z / this.bounds.extents.z);
				int num4 = num + num2 * 4 + num3 * 2;
				this.AddCell<T, U>(ref cells, num4, num, num2, num3, out maxCellCreated);
				return num4;
			}

			// Token: 0x06003070 RID: 12400 RVA: 0x000CD920 File Offset: 0x000CBB20
			protected void AddCell<T, U>(ref T[] cells, int index, int x, int y, int z, out bool maxCellCreated) where T : BaseOctree.Cell, new() where U : BaseOctree.Cell, new()
			{
				if (cells == null)
				{
					cells = new T[8];
				}
				if (this.cellsUsed == null)
				{
					this.cellsUsed = new bool[8];
				}
				if (!this.cellsUsed[index])
				{
					Bounds bounds = new Bounds(new Vector3(this.bounds.min.x + this.bounds.extents.x * ((float)x + 0.5f), this.bounds.min.y + this.bounds.extents.y * ((float)y + 0.5f), this.bounds.min.z + this.bounds.extents.z * ((float)z + 0.5f)), this.bounds.extents);
					if (this.level == this.maxLevels - 1)
					{
						cells[index] = (Activator.CreateInstance<U>() as T);
						cells[index].SetCell(this, index, bounds);
						maxCellCreated = true;
					}
					else
					{
						maxCellCreated = false;
						cells[index] = Activator.CreateInstance<T>();
						cells[index].SetCell(this, index, bounds);
					}
					this.cellsUsed[index] = true;
					this.cellCount++;
					return;
				}
				maxCellCreated = false;
			}

			// Token: 0x06003071 RID: 12401 RVA: 0x000CDA7C File Offset: 0x000CBC7C
			public bool InsideBounds(Vector3 position)
			{
				position -= this.bounds.min;
				return position.x < this.bounds.size.x && position.y < this.bounds.size.y && position.z < this.bounds.size.z && position.x > 0f && position.y > 0f && position.z > 0f;
			}

			// Token: 0x06003072 RID: 12402 RVA: 0x000CDB0E File Offset: 0x000CBD0E
			public void Reset(ref BaseOctree.Cell[] cells)
			{
				cells = null;
				this.cellsUsed = null;
			}

			// Token: 0x04002A29 RID: 10793
			public BaseOctree.Cell mainParent;

			// Token: 0x04002A2A RID: 10794
			public BaseOctree.Cell parent;

			// Token: 0x04002A2B RID: 10795
			public bool[] cellsUsed;

			// Token: 0x04002A2C RID: 10796
			public Bounds bounds;

			// Token: 0x04002A2D RID: 10797
			public int cellIndex;

			// Token: 0x04002A2E RID: 10798
			public int cellCount;

			// Token: 0x04002A2F RID: 10799
			public int level;

			// Token: 0x04002A30 RID: 10800
			public int maxLevels;
		}
	}
}
