using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000352 RID: 850
	public class ExampleSceneSimulatorCameraRigMover : MonoBehaviour
	{
		// Token: 0x06001D96 RID: 7574 RVA: 0x00096F3D File Offset: 0x0009513D
		protected virtual void Awake()
		{
			base.transform.Find("VRSimulatorCameraRig").position -= base.transform.position;
		}
	}
}
