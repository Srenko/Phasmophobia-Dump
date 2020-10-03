using System;
using UnityEngine;
using Valve.VR;

// Token: 0x020001F1 RID: 497
public class SteamVR_Skybox : MonoBehaviour
{
	// Token: 0x06000DEE RID: 3566 RVA: 0x00058AF0 File Offset: 0x00056CF0
	public void SetTextureByIndex(int i, Texture t)
	{
		switch (i)
		{
		case 0:
			this.front = t;
			return;
		case 1:
			this.back = t;
			return;
		case 2:
			this.left = t;
			return;
		case 3:
			this.right = t;
			return;
		case 4:
			this.top = t;
			return;
		case 5:
			this.bottom = t;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00058B4C File Offset: 0x00056D4C
	public Texture GetTextureByIndex(int i)
	{
		switch (i)
		{
		case 0:
			return this.front;
		case 1:
			return this.back;
		case 2:
			return this.left;
		case 3:
			return this.right;
		case 4:
			return this.top;
		case 5:
			return this.bottom;
		default:
			return null;
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00058BA4 File Offset: 0x00056DA4
	public static void SetOverride(Texture front = null, Texture back = null, Texture left = null, Texture right = null, Texture top = null, Texture bottom = null)
	{
		CVRCompositor compositor = OpenVR.Compositor;
		if (compositor != null)
		{
			Texture[] array = new Texture[]
			{
				front,
				back,
				left,
				right,
				top,
				bottom
			};
			Texture_t[] array2 = new Texture_t[6];
			for (int i = 0; i < 6; i++)
			{
				array2[i].handle = ((array[i] != null) ? array[i].GetNativeTexturePtr() : IntPtr.Zero);
				array2[i].eType = SteamVR.instance.textureType;
				array2[i].eColorSpace = EColorSpace.Auto;
			}
			EVRCompositorError evrcompositorError = compositor.SetSkyboxOverride(array2);
			if (evrcompositorError != EVRCompositorError.None)
			{
				Debug.LogError("Failed to set skybox override with error: " + evrcompositorError);
				if (evrcompositorError == EVRCompositorError.TextureIsOnWrongDevice)
				{
					Debug.Log("Set your graphics driver to use the same video card as the headset is plugged into for Unity.");
					return;
				}
				if (evrcompositorError == EVRCompositorError.TextureUsesUnsupportedFormat)
				{
					Debug.Log("Ensure skybox textures are not compressed and have no mipmaps.");
				}
			}
		}
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x00058C84 File Offset: 0x00056E84
	public static void ClearOverride()
	{
		CVRCompositor compositor = OpenVR.Compositor;
		if (compositor != null)
		{
			compositor.ClearSkyboxOverride();
		}
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x00058CA0 File Offset: 0x00056EA0
	private void OnEnable()
	{
		SteamVR_Skybox.SetOverride(this.front, this.back, this.left, this.right, this.top, this.bottom);
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00058CCB File Offset: 0x00056ECB
	private void OnDisable()
	{
		SteamVR_Skybox.ClearOverride();
	}

	// Token: 0x04000E61 RID: 3681
	public Texture front;

	// Token: 0x04000E62 RID: 3682
	public Texture back;

	// Token: 0x04000E63 RID: 3683
	public Texture left;

	// Token: 0x04000E64 RID: 3684
	public Texture right;

	// Token: 0x04000E65 RID: 3685
	public Texture top;

	// Token: 0x04000E66 RID: 3686
	public Texture bottom;

	// Token: 0x04000E67 RID: 3687
	public SteamVR_Skybox.CellSize StereoCellSize = SteamVR_Skybox.CellSize.x32;

	// Token: 0x04000E68 RID: 3688
	public float StereoIpdMm = 64f;

	// Token: 0x0200057E RID: 1406
	public enum CellSize
	{
		// Token: 0x04002613 RID: 9747
		x1024,
		// Token: 0x04002614 RID: 9748
		x64,
		// Token: 0x04002615 RID: 9749
		x32,
		// Token: 0x04002616 RID: 9750
		x16,
		// Token: 0x04002617 RID: 9751
		x8
	}
}
