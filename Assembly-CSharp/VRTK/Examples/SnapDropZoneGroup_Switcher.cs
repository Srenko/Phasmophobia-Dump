using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000367 RID: 871
	public class SnapDropZoneGroup_Switcher : MonoBehaviour
	{
		// Token: 0x06001DFD RID: 7677 RVA: 0x00098704 File Offset: 0x00096904
		private void Start()
		{
			this.cubeZone = base.transform.Find("Cube_SnapDropZone").gameObject;
			this.sphereZone = base.transform.Find("Sphere_SnapDropZone").gameObject;
			this.cubeZone.GetComponent<VRTK_SnapDropZone>().ObjectEnteredSnapDropZone += this.DoCubeZoneSnapped;
			this.cubeZone.GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += this.DoCubeZoneSnapped;
			this.cubeZone.GetComponent<VRTK_SnapDropZone>().ObjectExitedSnapDropZone += this.DoCubeZoneUnsnapped;
			this.cubeZone.GetComponent<VRTK_SnapDropZone>().ObjectUnsnappedFromDropZone += this.DoCubeZoneUnsnapped;
			this.sphereZone.GetComponent<VRTK_SnapDropZone>().ObjectEnteredSnapDropZone += this.DoSphereZoneSnapped;
			this.sphereZone.GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += this.DoSphereZoneSnapped;
			this.sphereZone.GetComponent<VRTK_SnapDropZone>().ObjectExitedSnapDropZone += this.DoSphereZoneUnsnapped;
			this.sphereZone.GetComponent<VRTK_SnapDropZone>().ObjectUnsnappedFromDropZone += this.DoSphereZoneUnsnapped;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00098827 File Offset: 0x00096A27
		private void DoCubeZoneSnapped(object sender, SnapDropZoneEventArgs e)
		{
			this.sphereZone.SetActive(false);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00098835 File Offset: 0x00096A35
		private void DoCubeZoneUnsnapped(object sender, SnapDropZoneEventArgs e)
		{
			this.sphereZone.SetActive(true);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00098843 File Offset: 0x00096A43
		private void DoSphereZoneSnapped(object sender, SnapDropZoneEventArgs e)
		{
			this.cubeZone.SetActive(false);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00098851 File Offset: 0x00096A51
		private void DoSphereZoneUnsnapped(object sender, SnapDropZoneEventArgs e)
		{
			this.cubeZone.SetActive(true);
		}

		// Token: 0x040017A8 RID: 6056
		private GameObject cubeZone;

		// Token: 0x040017A9 RID: 6057
		private GameObject sphereZone;
	}
}
