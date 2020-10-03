using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003D2 RID: 978
	[StructLayout(LayoutKind.Explicit)]
	public struct VROverlayIntersectionMaskPrimitive_Data_t
	{
		// Token: 0x04001BE3 RID: 7139
		[FieldOffset(0)]
		public IntersectionMaskRectangle_t m_Rectangle;

		// Token: 0x04001BE4 RID: 7140
		[FieldOffset(0)]
		public IntersectionMaskCircle_t m_Circle;
	}
}
