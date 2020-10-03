using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002BB RID: 699
	[ExecuteInEditMode]
	public class VRTK_RoomExtender_PlayAreaGizmo : MonoBehaviour
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0007C4B4 File Offset: 0x0007A6B4
		protected virtual void OnEnable()
		{
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.roomExtender = Object.FindObjectOfType<VRTK_RoomExtender>();
			if (this.playArea == null || this.roomExtender == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_RoomExtender_PlayAreaGizmo",
					"PlayArea or VRTK_RoomExtender",
					"an active"
				}));
				return;
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0007C51D File Offset: 0x0007A71D
		protected virtual void OnDrawGizmos()
		{
			if (!this.drawWireframeWhenSelectedOnly)
			{
				this.DrawWireframe();
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0007C52D File Offset: 0x0007A72D
		protected virtual void OnDrawGizmosSelected()
		{
			if (this.drawWireframeWhenSelectedOnly)
			{
				this.DrawWireframe();
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0007C540 File Offset: 0x0007A740
		protected virtual void DrawWireframe()
		{
			if (this.playArea == null || this.roomExtender == null)
			{
				return;
			}
			Vector3[] playAreaVertices = VRTK_SDK_Bridge.GetPlayAreaVertices();
			if (playAreaVertices == null || playAreaVertices.Length == 0)
			{
				return;
			}
			int num = 4;
			int num2 = 5;
			int num3 = 6;
			int num4 = 7;
			Vector3 a = playAreaVertices[num] * this.roomExtender.additionalMovementMultiplier;
			Vector3 a2 = playAreaVertices[num2] * this.roomExtender.additionalMovementMultiplier;
			Vector3 a3 = playAreaVertices[num3] * this.roomExtender.additionalMovementMultiplier;
			Vector3 a4 = playAreaVertices[num4] * this.roomExtender.additionalMovementMultiplier;
			Vector3 vector = new Vector3(0f, this.roomExtender.transform.localPosition.y, 0f);
			Vector3 b = vector + this.playArea.TransformVector(Vector3.up * this.wireframeHeight);
			Gizmos.color = this.color;
			Gizmos.DrawLine(a + vector, a2 + vector);
			Gizmos.DrawLine(a3 + vector, a4 + vector);
			Gizmos.DrawLine(a + vector, a4 + vector);
			Gizmos.DrawLine(a2 + vector, a3 + vector);
			Gizmos.DrawLine(a + b, a2 + b);
			Gizmos.DrawLine(a3 + b, a4 + b);
			Gizmos.DrawLine(a + b, a4 + b);
			Gizmos.DrawLine(a2 + b, a3 + b);
			Gizmos.DrawLine(a + vector, a + b);
			Gizmos.DrawLine(a2 + vector, a2 + b);
			Gizmos.DrawLine(a4 + vector, a4 + b);
			Gizmos.DrawLine(a3 + vector, a3 + b);
		}

		// Token: 0x040012FF RID: 4863
		public Color color = Color.red;

		// Token: 0x04001300 RID: 4864
		public float wireframeHeight = 2f;

		// Token: 0x04001301 RID: 4865
		public bool drawWireframeWhenSelectedOnly;

		// Token: 0x04001302 RID: 4866
		protected Transform playArea;

		// Token: 0x04001303 RID: 4867
		protected VRTK_RoomExtender roomExtender;
	}
}
