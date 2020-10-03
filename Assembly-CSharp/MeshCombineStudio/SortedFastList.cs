using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004AC RID: 1196
	[Serializable]
	public class SortedFastList<T> : FastList<T>
	{
		// Token: 0x06002566 RID: 9574 RVA: 0x000B9D98 File Offset: 0x000B7F98
		public new void RemoveAt(int index)
		{
			if (index >= this._count)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Index ",
					index,
					" is out of range ",
					this._count
				}));
			}
			this._count--;
			if (index < this._count)
			{
				Array.Copy(this.items, index + 1, this.items, index, this._count - index);
			}
			this.items[this._count] = default(T);
			this.Count = this._count;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000B9E40 File Offset: 0x000B8040
		public new void RemoveRange(int index, int endIndex)
		{
			int num = endIndex - index + 1;
			if (index < 0)
			{
				Debug.LogError("Index needs to be bigger than 0 -> " + index);
				return;
			}
			if (num < 0)
			{
				Debug.LogError("Length needs to be bigger than 0 -> " + num);
				return;
			}
			if (this._count - index < num)
			{
				return;
			}
			this._count -= num;
			if (index < this._count)
			{
				Array.Copy(this.items, index + num, this.items, index, this._count - index);
			}
			Array.Clear(this.items, this._count, num);
		}
	}
}
