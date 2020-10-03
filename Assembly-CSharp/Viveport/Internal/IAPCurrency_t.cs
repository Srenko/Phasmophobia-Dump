using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000221 RID: 545
	internal struct IAPCurrency_t
	{
		// Token: 0x04000F37 RID: 3895
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string m_pName;

		// Token: 0x04000F38 RID: 3896
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string m_pSymbol;
	}
}
