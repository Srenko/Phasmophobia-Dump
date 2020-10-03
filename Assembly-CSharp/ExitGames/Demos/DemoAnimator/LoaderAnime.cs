using System;
using UnityEngine;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x02000487 RID: 1159
	public class LoaderAnime : MonoBehaviour
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x000B0F3A File Offset: 0x000AF13A
		private void Awake()
		{
			this._particleTransform = this.particles.GetComponent<Transform>();
			this._transform = base.GetComponent<Transform>();
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000B0F5C File Offset: 0x000AF15C
		private void Update()
		{
			if (this._isAnimating)
			{
				this._transform.Rotate(0f, 0f, this.speed * Time.deltaTime);
				this._particleTransform.localPosition = Vector3.MoveTowards(this._particleTransform.localPosition, this._offset, 0.5f * Time.deltaTime);
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000B0FBE File Offset: 0x000AF1BE
		public void StartLoaderAnimation()
		{
			this._isAnimating = true;
			this._offset = new Vector3(this.radius, 0f, 0f);
			this.particles.SetActive(true);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000B0FEE File Offset: 0x000AF1EE
		public void StopLoaderAnimation()
		{
			this.particles.SetActive(false);
		}

		// Token: 0x0400216E RID: 8558
		[Tooltip("Angular Speed in degrees per seconds")]
		public float speed = 180f;

		// Token: 0x0400216F RID: 8559
		[Tooltip("Radius os the loader")]
		public float radius = 1f;

		// Token: 0x04002170 RID: 8560
		public GameObject particles;

		// Token: 0x04002171 RID: 8561
		private Vector3 _offset;

		// Token: 0x04002172 RID: 8562
		private Transform _transform;

		// Token: 0x04002173 RID: 8563
		private Transform _particleTransform;

		// Token: 0x04002174 RID: 8564
		private bool _isAnimating;
	}
}
