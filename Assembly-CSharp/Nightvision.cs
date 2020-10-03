using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class Nightvision : MonoBehaviour
{
	// Token: 0x06000126 RID: 294 RVA: 0x000090FC File Offset: 0x000072FC
	private void Awake()
	{
		this.nvMaterial = new Material(Shader.Find("Hidden/Nightvision"));
		this.UpdateMaterial();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00009119 File Offset: 0x00007319
	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		this.UpdateMaterial();
		Graphics.Blit(src, dst, this.nvMaterial);
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00009130 File Offset: 0x00007330
	public void UpdateMaterial()
	{
		this.nvMaterial.SetFloat("_Power", this.Power);
		this.nvMaterial.SetColor("_Color", this.EffectColor);
		if (this.Vignette.Mode == Nightvision.VignetteSettings.VignetteMode.Off)
		{
			this.nvMaterial.EnableKeyword("Vignette_Off");
		}
		else if (this.Vignette.Mode == Nightvision.VignetteSettings.VignetteMode.Texture)
		{
			this.nvMaterial.SetColor("_VignetteColor", this.Vignette.color);
			this.nvMaterial.SetTexture("_VignetteTex", this.Vignette.Texture.Texture);
			this.nvMaterial.EnableKeyword("Vignette_Texture");
		}
		else if (this.Vignette.Mode == Nightvision.VignetteSettings.VignetteMode.Procedural)
		{
			this.nvMaterial.SetColor("_VignetteColor", this.Vignette.color);
			this.nvMaterial.SetFloat("_VignetteRadius", this.Vignette.Procedural.Radius);
			this.nvMaterial.SetFloat("_VignetteSharpness", this.Vignette.Procedural.Sharpness);
			this.nvMaterial.EnableKeyword("Vignette_Procedural");
		}
		if (this.lastTime < Time.time - this.Noise.OffsetTime)
		{
			this.noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
			this.lastTime = Time.time;
		}
		if (this.Noise.Mode == Nightvision.NoiseSettings.NoiseMode.Off)
		{
			this.nvMaterial.EnableKeyword("Noise_Off");
			return;
		}
		if (this.Noise.Mode == Nightvision.NoiseSettings.NoiseMode.Texture)
		{
			this.nvMaterial.SetTexture("_NoiseTex", this.Noise.Texture.Texture);
			if (this.Noise.Texture.Texture != null)
			{
				Vector2 vector = new Vector2((float)Screen.width / (float)this.Noise.Texture.Texture.width, (float)Screen.height / (float)this.Noise.Texture.Texture.width);
				Vector2 vector2 = Vector2.one;
				if (vector.x > vector.y)
				{
					vector2.x = vector.x / vector.y;
				}
				else
				{
					vector2.y = vector.y / vector.x;
				}
				vector2 /= this.Noise.Texture.Scale;
				this.nvMaterial.SetFloat("_NoiseTileX", vector2.x);
				this.nvMaterial.SetFloat("_NoiseTileY", vector2.y);
			}
			this.nvMaterial.SetFloat("_NoiseOffsetX", this.noiseOffset.x);
			this.nvMaterial.SetFloat("_NoiseOffsetY", this.noiseOffset.y);
			this.nvMaterial.SetFloat("_NoisePower", this.Noise.Power);
			this.nvMaterial.EnableKeyword("Noise_Texture");
			return;
		}
		if (this.Noise.Mode == Nightvision.NoiseSettings.NoiseMode.Procedural)
		{
			this.nvMaterial.SetFloat("_NoiseOffsetX", this.noiseOffset.x);
			this.nvMaterial.SetFloat("_NoiseOffsetY", this.noiseOffset.y);
			this.nvMaterial.SetFloat("_NoisePower", this.Noise.Power);
			this.nvMaterial.EnableKeyword("Noise_Procedural");
		}
	}

	// Token: 0x0400016C RID: 364
	public const float DefaultPower = 20f;

	// Token: 0x0400016D RID: 365
	public float Power = 20f;

	// Token: 0x0400016E RID: 366
	public Nightvision.VignetteSettings Vignette = new Nightvision.VignetteSettings();

	// Token: 0x0400016F RID: 367
	public Nightvision.NoiseSettings Noise = new Nightvision.NoiseSettings();

	// Token: 0x04000170 RID: 368
	public Color EffectColor = Color.green;

	// Token: 0x04000171 RID: 369
	private Material nvMaterial;

	// Token: 0x04000172 RID: 370
	private Vector2 noiseOffset = Vector2.zero;

	// Token: 0x04000173 RID: 371
	private float lastTime;

	// Token: 0x020004DD RID: 1245
	[Serializable]
	public class VignetteSettings
	{
		// Token: 0x0400238F RID: 9103
		public Nightvision.VignetteSettings.VignetteMode Mode;

		// Token: 0x04002390 RID: 9104
		public Color color = Color.black;

		// Token: 0x04002391 RID: 9105
		public Nightvision.VignetteSettings.TextureSettings Texture = new Nightvision.VignetteSettings.TextureSettings();

		// Token: 0x04002392 RID: 9106
		public Nightvision.VignetteSettings.ProceduralSettings Procedural = new Nightvision.VignetteSettings.ProceduralSettings();

		// Token: 0x020007C6 RID: 1990
		public enum VignetteMode
		{
			// Token: 0x04002A43 RID: 10819
			Off,
			// Token: 0x04002A44 RID: 10820
			Texture,
			// Token: 0x04002A45 RID: 10821
			Procedural
		}

		// Token: 0x020007C7 RID: 1991
		[Serializable]
		public class TextureSettings
		{
			// Token: 0x04002A46 RID: 10822
			public Texture Texture;
		}

		// Token: 0x020007C8 RID: 1992
		[Serializable]
		public class ProceduralSettings
		{
			// Token: 0x04002A47 RID: 10823
			public const float DefaultRadius = 1f;

			// Token: 0x04002A48 RID: 10824
			public const float DefaultSharpness = 50f;

			// Token: 0x04002A49 RID: 10825
			public float Radius = 1f;

			// Token: 0x04002A4A RID: 10826
			public float Sharpness = 50f;
		}
	}

	// Token: 0x020004DE RID: 1246
	[Serializable]
	public class NoiseSettings
	{
		// Token: 0x04002393 RID: 9107
		public const float DefaultOffsetTime = 0f;

		// Token: 0x04002394 RID: 9108
		public const float DefaultPower = 0.5f;

		// Token: 0x04002395 RID: 9109
		public Nightvision.NoiseSettings.NoiseMode Mode;

		// Token: 0x04002396 RID: 9110
		public Nightvision.NoiseSettings.TextureSettings Texture = new Nightvision.NoiseSettings.TextureSettings();

		// Token: 0x04002397 RID: 9111
		public float OffsetTime;

		// Token: 0x04002398 RID: 9112
		public float Power = 0.5f;

		// Token: 0x020007C9 RID: 1993
		public enum NoiseMode
		{
			// Token: 0x04002A4C RID: 10828
			Off,
			// Token: 0x04002A4D RID: 10829
			Texture,
			// Token: 0x04002A4E RID: 10830
			Procedural
		}

		// Token: 0x020007CA RID: 1994
		[Serializable]
		public class TextureSettings
		{
			// Token: 0x04002A4F RID: 10831
			public const float DefaultScale = 1f;

			// Token: 0x04002A50 RID: 10832
			public Texture Texture;

			// Token: 0x04002A51 RID: 10833
			public float Scale = 1f;
		}
	}
}
