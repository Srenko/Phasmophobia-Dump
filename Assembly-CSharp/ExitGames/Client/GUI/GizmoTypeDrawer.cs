using System;
using UnityEngine;

namespace ExitGames.Client.GUI
{
	// Token: 0x02000481 RID: 1153
	public class GizmoTypeDrawer
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x000B0274 File Offset: 0x000AE474
		public static void Draw(Vector3 center, GizmoType type, Color color, float size)
		{
			Gizmos.color = color;
			switch (type)
			{
			case GizmoType.WireSphere:
				Gizmos.DrawWireSphere(center, size * 0.5f);
				return;
			case GizmoType.Sphere:
				Gizmos.DrawSphere(center, size * 0.5f);
				return;
			case GizmoType.WireCube:
				Gizmos.DrawWireCube(center, Vector3.one * size);
				return;
			case GizmoType.Cube:
				Gizmos.DrawCube(center, Vector3.one * size);
				return;
			default:
				return;
			}
		}
	}
}
