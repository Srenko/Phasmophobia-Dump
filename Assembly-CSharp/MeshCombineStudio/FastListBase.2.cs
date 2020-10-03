using System;

namespace MeshCombineStudio
{
	// Token: 0x020004AA RID: 1194
	public class FastListBase<T> : FastListBase
	{
		// Token: 0x0600253F RID: 9535 RVA: 0x000B9184 File Offset: 0x000B7384
		protected void DoubleCapacity()
		{
			this.arraySize *= 2;
			T[] destinationArray = new T[this.arraySize];
			Array.Copy(this.items, destinationArray, this._count);
			this.items = destinationArray;
		}

		// Token: 0x0400229C RID: 8860
		public T[] items;
	}
}
