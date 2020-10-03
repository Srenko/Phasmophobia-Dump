using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000392 RID: 914
	public class CVRExtendedDisplay
	{
		// Token: 0x06001F03 RID: 7939 RVA: 0x0009CD7F File Offset: 0x0009AF7F
		internal CVRExtendedDisplay(IntPtr pInterface)
		{
			this.FnTable = (IVRExtendedDisplay)Marshal.PtrToStructure(pInterface, typeof(IVRExtendedDisplay));
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0009CDA2 File Offset: 0x0009AFA2
		public void GetWindowBounds(ref int pnX, ref int pnY, ref uint pnWidth, ref uint pnHeight)
		{
			pnX = 0;
			pnY = 0;
			pnWidth = 0U;
			pnHeight = 0U;
			this.FnTable.GetWindowBounds(ref pnX, ref pnY, ref pnWidth, ref pnHeight);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0009CDC6 File Offset: 0x0009AFC6
		public void GetEyeOutputViewport(EVREye eEye, ref uint pnX, ref uint pnY, ref uint pnWidth, ref uint pnHeight)
		{
			pnX = 0U;
			pnY = 0U;
			pnWidth = 0U;
			pnHeight = 0U;
			this.FnTable.GetEyeOutputViewport(eEye, ref pnX, ref pnY, ref pnWidth, ref pnHeight);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0009CDED File Offset: 0x0009AFED
		public void GetDXGIOutputInfo(ref int pnAdapterIndex, ref int pnAdapterOutputIndex)
		{
			pnAdapterIndex = 0;
			pnAdapterOutputIndex = 0;
			this.FnTable.GetDXGIOutputInfo(ref pnAdapterIndex, ref pnAdapterOutputIndex);
		}

		// Token: 0x0400192B RID: 6443
		private IVRExtendedDisplay FnTable;
	}
}
