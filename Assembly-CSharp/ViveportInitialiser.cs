using System;
using UnityEngine;
using Viveport;
using Viveport.Core;

// Token: 0x02000201 RID: 513
public class ViveportInitialiser : MonoBehaviour
{
	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0005CE95 File Offset: 0x0005B095
	private static ViveportInitialiser Instance
	{
		get
		{
			if (ViveportInitialiser._instance == null)
			{
				return new GameObject("ViveportInitialiser").AddComponent<ViveportInitialiser>();
			}
			return ViveportInitialiser._instance;
		}
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0005CEB9 File Offset: 0x0005B0B9
	private void Awake()
	{
		if (ViveportInitialiser._instance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		ViveportInitialiser._instance = this;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0005CEDA File Offset: 0x0005B0DA
	private void Start()
	{
		Api.Init(new StatusCallback(ViveportInitialiser.InitStatusHandler), ViveportInitialiser.APP_ID);
		this.storeSDKManager.QueryArcadeLicense();
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0005CEFE File Offset: 0x0005B0FE
	private static void InitStatusHandler(int nResult)
	{
		if (nResult == 0)
		{
			ViveportInitialiser.Initialized = true;
			if (Application.isEditor)
			{
				Viveport.Core.Logger.Log("InitStatusHandler is successful");
				return;
			}
		}
		else
		{
			ViveportInitialiser.Initialized = false;
			if (Application.isEditor)
			{
				Viveport.Core.Logger.Log("InitStatusHandler error : " + nResult);
			}
		}
	}

	// Token: 0x04000EC6 RID: 3782
	private static string APP_ID = "044dbdf8-d046-4aa1-a127-7edc2743e438";

	// Token: 0x04000EC7 RID: 3783
	private static string APP_KEY = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDBqmPEEnhK69iGCTiJPQoxlunKA/AN7QfmqWftqqCv67J4d3mC9dzpqnx3hMyBas1z3MxET9TbHBW5DRoM6q1pyAd9E8/MkCMj2ub/Pc7hIRF2P1yKFnrUh2AX0PVpihodU492h33tqHDFDRxz+U6LcMFOZrhhRHQGvsLsXQbS1wIDAQAB";

	// Token: 0x04000EC8 RID: 3784
	public static bool Initialized = false;

	// Token: 0x04000EC9 RID: 3785
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x04000ECA RID: 3786
	private static ViveportInitialiser _instance;
}
