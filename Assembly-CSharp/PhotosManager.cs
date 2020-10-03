using System;
using System.IO;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class PhotosManager : MonoBehaviour
{
	// Token: 0x06000AB4 RID: 2740 RVA: 0x00042878 File Offset: 0x00040A78
	private void Start()
	{
		for (int i = 0; i < this.photoRends.Length; i++)
		{
			if (File.Exists(string.Concat(new object[]
			{
				Application.dataPath,
				"/../SavedScreen",
				i,
				".png"
			})))
			{
				Texture2D texture2D = new Texture2D(384, 216, TextureFormat.RGB24, false);
				texture2D.LoadImage(File.ReadAllBytes(string.Concat(new object[]
				{
					Application.dataPath,
					"/../SavedScreen",
					i,
					".png"
				})));
				this.photoRends[i].material.mainTexture = texture2D;
				this.photoRends[i].gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x04000AE9 RID: 2793
	[SerializeField]
	private Renderer[] photoRends;
}
