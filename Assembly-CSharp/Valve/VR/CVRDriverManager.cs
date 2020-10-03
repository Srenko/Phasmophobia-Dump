using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200039E RID: 926
	public class CVRDriverManager
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x0009E4CC File Offset: 0x0009C6CC
		internal CVRDriverManager(IntPtr pInterface)
		{
			this.FnTable = (IVRDriverManager)Marshal.PtrToStructure(pInterface, typeof(IVRDriverManager));
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0009E4EF File Offset: 0x0009C6EF
		public uint GetDriverCount()
		{
			return this.FnTable.GetDriverCount();
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0009E501 File Offset: 0x0009C701
		public uint GetDriverName(uint nDriver, StringBuilder pchValue, uint unBufferSize)
		{
			return this.FnTable.GetDriverName(nDriver, pchValue, unBufferSize);
		}

		// Token: 0x04001937 RID: 6455
		private IVRDriverManager FnTable;
	}
}
