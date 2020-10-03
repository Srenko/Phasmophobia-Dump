using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004AB RID: 1195
	[Serializable]
	public class FastList<T> : FastListBase<T>
	{
		// Token: 0x06002541 RID: 9537 RVA: 0x000B91CC File Offset: 0x000B73CC
		public FastList()
		{
			this.items = new T[4];
			this.arraySize = 4;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000B91E8 File Offset: 0x000B73E8
		public FastList(bool reserve, int reserved)
		{
			int num = Mathf.Max(reserved, 4);
			this.items = new T[num];
			this.arraySize = num;
			this._count = reserved;
			this.Count = reserved;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000B9226 File Offset: 0x000B7426
		public FastList(int capacity)
		{
			if (capacity < 1)
			{
				capacity = 1;
			}
			this.items = new T[capacity];
			this.arraySize = capacity;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000B9248 File Offset: 0x000B7448
		public FastList(FastList<T> list)
		{
			if (list == null)
			{
				this.items = new T[4];
				this.arraySize = 4;
				return;
			}
			this.items = new T[list.Count];
			Array.Copy(list.items, this.items, list.Count);
			this.arraySize = this.items.Length;
			this.Count = (this._count = this.items.Length);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000B92C0 File Offset: 0x000B74C0
		public FastList(T[] items)
		{
			this.items = items;
			this._count = (this.Count = (this.arraySize = items.Length));
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000B92F8 File Offset: 0x000B74F8
		protected void SetCapacity(int capacity)
		{
			this.arraySize = capacity;
			T[] array = new T[this.arraySize];
			if (this._count > 0)
			{
				Array.Copy(this.items, array, this._count);
			}
			this.items = array;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000B933C File Offset: 0x000B753C
		public void SetCount(int count)
		{
			if (count > this.arraySize)
			{
				this.SetCapacity(count);
			}
			this._count = count;
			this.Count = count;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000B936C File Offset: 0x000B756C
		public void EnsureCount(int count)
		{
			if (count <= this._count)
			{
				return;
			}
			if (count > this.arraySize)
			{
				this.SetCapacity(count);
			}
			this._count = count;
			this.Count = count;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000B93A4 File Offset: 0x000B75A4
		public virtual void SetArray(T[] items)
		{
			this.items = items;
			this._count = (this.Count = (this.arraySize = items.Length));
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000B93D3 File Offset: 0x000B75D3
		public int AddUnique(T item)
		{
			if (!this.Contains(item))
			{
				return this.Add(item);
			}
			return -1;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000B93E7 File Offset: 0x000B75E7
		public bool Contains(T item)
		{
			return Array.IndexOf<T>(this.items, item, 0, this._count) != -1;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000B9402 File Offset: 0x000B7602
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this.items, item, 0, this._count);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000B9418 File Offset: 0x000B7618
		public T GetIndex(T item)
		{
			int num = Array.IndexOf<T>(this.items, item, 0, this._count);
			if (num == -1)
			{
				return default(T);
			}
			return this.items[num];
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000B9454 File Offset: 0x000B7654
		public virtual int Add(T item)
		{
			if (this._count == this.arraySize)
			{
				base.DoubleCapacity();
			}
			this.items[this._count] = item;
			int count = this._count + 1;
			this._count = count;
			this.Count = count;
			return this._count - 1;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000B94A8 File Offset: 0x000B76A8
		public virtual int AddThreadSafe(T item)
		{
			int num;
			lock (this)
			{
				if (this._count == this.arraySize)
				{
					base.DoubleCapacity();
				}
				this.items[this._count] = item;
				num = this._count + 1;
				this._count = num;
				this.Count = num;
				num = this._count - 1;
			}
			return num;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000B9524 File Offset: 0x000B7724
		public virtual void Add(T item, T item2)
		{
			if (this._count + 1 >= this.arraySize)
			{
				base.DoubleCapacity();
			}
			T[] items = this.items;
			int count = this._count;
			this._count = count + 1;
			items[count] = item;
			T[] items2 = this.items;
			count = this._count;
			this._count = count + 1;
			items2[count] = item2;
			this.Count = this._count;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000B9590 File Offset: 0x000B7790
		public virtual void Add(T item, T item2, T item3)
		{
			if (this._count + 2 >= this.arraySize)
			{
				base.DoubleCapacity();
			}
			T[] items = this.items;
			int count = this._count;
			this._count = count + 1;
			items[count] = item;
			T[] items2 = this.items;
			count = this._count;
			this._count = count + 1;
			items2[count] = item2;
			T[] items3 = this.items;
			count = this._count;
			this._count = count + 1;
			items3[count] = item3;
			this.Count = this._count;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000B9618 File Offset: 0x000B7818
		public virtual void Add(T item, T item2, T item3, T item4)
		{
			if (this._count + 3 >= this.arraySize)
			{
				base.DoubleCapacity();
			}
			T[] items = this.items;
			int count = this._count;
			this._count = count + 1;
			items[count] = item;
			T[] items2 = this.items;
			count = this._count;
			this._count = count + 1;
			items2[count] = item2;
			T[] items3 = this.items;
			count = this._count;
			this._count = count + 1;
			items3[count] = item3;
			T[] items4 = this.items;
			count = this._count;
			this._count = count + 1;
			items4[count] = item4;
			this.Count = this._count;
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000B96BC File Offset: 0x000B78BC
		public virtual void Add(T item, T item2, T item3, T item4, T item5)
		{
			if (this._count + 4 >= this.arraySize)
			{
				base.DoubleCapacity();
			}
			T[] items = this.items;
			int count = this._count;
			this._count = count + 1;
			items[count] = item;
			T[] items2 = this.items;
			count = this._count;
			this._count = count + 1;
			items2[count] = item2;
			T[] items3 = this.items;
			count = this._count;
			this._count = count + 1;
			items3[count] = item3;
			T[] items4 = this.items;
			count = this._count;
			this._count = count + 1;
			items4[count] = item4;
			T[] items5 = this.items;
			count = this._count;
			this._count = count + 1;
			items5[count] = item5;
			this.Count = this._count;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000B9780 File Offset: 0x000B7980
		public virtual void Insert(int index, T item)
		{
			if (index > this._count)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Index ",
					index,
					" is out of range ",
					this._count
				}));
			}
			if (this._count == this.arraySize)
			{
				base.DoubleCapacity();
			}
			if (index < this._count)
			{
				Array.Copy(this.items, index, this.items, index + 1, this._count - index);
			}
			this.items[index] = item;
			int count = this._count + 1;
			this._count = count;
			this.Count = count;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000B982C File Offset: 0x000B7A2C
		public virtual void AddRange(T[] arrayItems)
		{
			if (arrayItems == null)
			{
				return;
			}
			int num = arrayItems.Length;
			int num2 = this._count + num;
			if (num2 >= this.arraySize)
			{
				this.SetCapacity(num2 * 2);
			}
			Array.Copy(arrayItems, 0, this.items, this._count, num);
			this.Count = (this._count = num2);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000B9880 File Offset: 0x000B7A80
		public virtual void AddRange(T[] arrayItems, int startIndex, int length)
		{
			int num = this._count + length;
			if (num >= this.arraySize)
			{
				this.SetCapacity(num * 2);
			}
			Array.Copy(arrayItems, startIndex, this.items, this._count, length);
			this.Count = (this._count = num);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000B98CC File Offset: 0x000B7ACC
		public virtual void AddRange(FastList<T> list)
		{
			if (list.Count == 0)
			{
				return;
			}
			int num = this._count + list.Count;
			if (num >= this.arraySize)
			{
				this.SetCapacity(num * 2);
			}
			Array.Copy(list.items, 0, this.items, this._count, list.Count);
			this.Count = (this._count = num);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000B9930 File Offset: 0x000B7B30
		public virtual int GrabListThreadSafe(FastList<T> threadList, bool fastClear = false)
		{
			int result;
			lock (threadList)
			{
				int count = this._count;
				this.AddRange(threadList);
				if (fastClear)
				{
					threadList.FastClear();
				}
				else
				{
					threadList.Clear();
				}
				result = count;
			}
			return result;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000B9988 File Offset: 0x000B7B88
		public virtual void ChangeRange(int startIndex, T[] arrayItems)
		{
			for (int i = 0; i < arrayItems.Length; i++)
			{
				this.items[startIndex + i] = arrayItems[i];
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000B99B8 File Offset: 0x000B7BB8
		public virtual bool Remove(T item, bool weakReference = false)
		{
			int num = Array.IndexOf<T>(this.items, item, 0, this._count);
			if (num >= 0)
			{
				T[] items = this.items;
				int num2 = num;
				T[] items2 = this.items;
				int num3 = this._count - 1;
				this._count = num3;
				items[num2] = items2[num3];
				this.items[this._count] = default(T);
				this.Count = this._count;
				return true;
			}
			return false;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000B9A30 File Offset: 0x000B7C30
		public virtual void RemoveAt(int index)
		{
			if (index >= this._count)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Index ",
					index,
					" is out of range. List count is ",
					this._count
				}));
				return;
			}
			T[] items = this.items;
			T[] items2 = this.items;
			int num = this._count - 1;
			this._count = num;
			items[index] = items2[num];
			this.items[this._count] = default(T);
			this.Count = this._count;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000B9ACC File Offset: 0x000B7CCC
		public virtual void RemoveLast()
		{
			if (this._count == 0)
			{
				return;
			}
			this._count--;
			this.items[this._count] = default(T);
			this.Count = this._count;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000B9B18 File Offset: 0x000B7D18
		public virtual void RemoveRange(int index, int length)
		{
			if (this._count - index < length)
			{
				Debug.LogError("Invalid length!");
			}
			if (length > 0)
			{
				this._count -= length;
				if (index < this._count)
				{
					Array.Copy(this.items, index + length, this.items, index, this._count - index);
				}
				Array.Clear(this.items, this._count, length);
				this.Count = this._count;
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000B9B90 File Offset: 0x000B7D90
		public virtual T Dequeue()
		{
			if (this._count == 0)
			{
				Debug.LogError("List is empty!");
				return default(T);
			}
			T[] items = this.items;
			int num = this._count - 1;
			this._count = num;
			T result = items[num];
			this.items[this._count] = default(T);
			this.Count = this._count;
			return result;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000B9BFC File Offset: 0x000B7DFC
		public virtual T Dequeue(int index)
		{
			T result = this.items[index];
			T[] items = this.items;
			T[] items2 = this.items;
			int num = this._count - 1;
			this._count = num;
			items[index] = items2[num];
			this.items[this._count] = default(T);
			this.Count = this._count;
			return result;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000B9C64 File Offset: 0x000B7E64
		public virtual void Clear()
		{
			Array.Clear(this.items, 0, this._count);
			this.Count = (this._count = 0);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000B9C94 File Offset: 0x000B7E94
		public virtual void ClearThreadSafe()
		{
			lock (this)
			{
				Array.Clear(this.items, 0, this._count);
				this.Count = (this._count = 0);
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000B9CEC File Offset: 0x000B7EEC
		public virtual void ClearRange(int startIndex)
		{
			Array.Clear(this.items, startIndex, this._count - startIndex);
			this._count = startIndex;
			this.Count = startIndex;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000B9D20 File Offset: 0x000B7F20
		public virtual void FastClear()
		{
			this.Count = (this._count = 0);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000B9D40 File Offset: 0x000B7F40
		public virtual void FastClear(int newCount)
		{
			if (newCount < this.Count)
			{
				this._count = newCount;
				this.Count = newCount;
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000B9D68 File Offset: 0x000B7F68
		public virtual T[] ToArray()
		{
			T[] array = new T[this._count];
			Array.Copy(this.items, 0, array, 0, this._count);
			return array;
		}
	}
}
