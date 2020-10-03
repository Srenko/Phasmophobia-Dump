using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000311 RID: 785
	[AddComponentMenu("VRTK/Scripts/Utilities/VRTK_CustomRaycast")]
	public class VRTK_CustomRaycast : MonoBehaviour
	{
		// Token: 0x06001B96 RID: 7062 RVA: 0x00090A7E File Offset: 0x0008EC7E
		public static bool Raycast(VRTK_CustomRaycast customCast, Ray ray, out RaycastHit hitData, LayerMask ignoreLayers, float length = float.PositiveInfinity, QueryTriggerInteraction affectTriggers = QueryTriggerInteraction.UseGlobal)
		{
			if (customCast != null)
			{
				return customCast.CustomRaycast(ray, out hitData, length);
			}
			return Physics.Raycast(ray, out hitData, length, ~ignoreLayers, affectTriggers);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00090AA6 File Offset: 0x0008ECA6
		public static bool Linecast(VRTK_CustomRaycast customCast, Vector3 startPosition, Vector3 endPosition, out RaycastHit hitData, LayerMask ignoreLayers, QueryTriggerInteraction affectTriggers = QueryTriggerInteraction.UseGlobal)
		{
			if (customCast != null)
			{
				return customCast.CustomLinecast(startPosition, endPosition, out hitData);
			}
			return Physics.Linecast(startPosition, endPosition, out hitData, ~ignoreLayers, affectTriggers);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00090ACD File Offset: 0x0008ECCD
		public static bool CapsuleCast(VRTK_CustomRaycast customCast, Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, out RaycastHit hitData, LayerMask ignoreLayers, QueryTriggerInteraction affectTriggers = QueryTriggerInteraction.UseGlobal)
		{
			if (customCast != null)
			{
				return customCast.CustomCapsuleCast(point1, point2, radius, direction, maxDistance, out hitData);
			}
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitData, maxDistance, ~ignoreLayers, affectTriggers);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00090B00 File Offset: 0x0008ED00
		public virtual bool CustomRaycast(Ray ray, out RaycastHit hitData, float length = float.PositiveInfinity)
		{
			return Physics.Raycast(ray, out hitData, length, ~this.layersToIgnore, this.triggerInteraction);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00090B1C File Offset: 0x0008ED1C
		public virtual bool CustomLinecast(Vector3 startPosition, Vector3 endPosition, out RaycastHit hitData)
		{
			return Physics.Linecast(startPosition, endPosition, out hitData, ~this.layersToIgnore, this.triggerInteraction);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00090B38 File Offset: 0x0008ED38
		public virtual bool CustomCapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, out RaycastHit hitData)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitData, maxDistance, ~this.layersToIgnore, this.triggerInteraction);
		}

		// Token: 0x0400163D RID: 5693
		[Tooltip("The layers to ignore when raycasting.")]
		public LayerMask layersToIgnore = 4;

		// Token: 0x0400163E RID: 5694
		[Tooltip("Determines whether the ray will interact with trigger colliders.")]
		public QueryTriggerInteraction triggerInteraction;
	}
}
