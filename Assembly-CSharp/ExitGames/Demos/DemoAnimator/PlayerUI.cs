using System;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x0200048B RID: 1163
	public class PlayerUI : MonoBehaviour
	{
		// Token: 0x0600243A RID: 9274 RVA: 0x000B146C File Offset: 0x000AF66C
		private void Awake()
		{
			base.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000B1488 File Offset: 0x000AF688
		private void Update()
		{
			if (this._target == null)
			{
				Object.Destroy(base.gameObject);
				return;
			}
			if (this.PlayerHealthSlider != null)
			{
				this.PlayerHealthSlider.value = this._target.Health;
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000B14C8 File Offset: 0x000AF6C8
		private void LateUpdate()
		{
			if (this._targetRenderer != null)
			{
				base.gameObject.SetActive(this._targetRenderer.isVisible);
			}
			if (this._targetTransform != null)
			{
				this._targetPosition = this._targetTransform.position;
				this._targetPosition.y = this._targetPosition.y + this._characterControllerHeight;
				base.transform.position = Camera.main.WorldToScreenPoint(this._targetPosition) + this.ScreenOffset;
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000B1554 File Offset: 0x000AF754
		public void SetTarget(PlayerManager target)
		{
			if (target == null)
			{
				Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
				return;
			}
			this._target = target;
			this._targetTransform = this._target.GetComponent<Transform>();
			this._targetRenderer = this._target.GetComponent<Renderer>();
			CharacterController component = this._target.GetComponent<CharacterController>();
			if (component != null)
			{
				this._characterControllerHeight = component.height;
			}
			if (this.PlayerNameText != null)
			{
				this.PlayerNameText.text = this._target.photonView.owner.NickName;
			}
		}

		// Token: 0x0400217D RID: 8573
		[Tooltip("Pixel offset from the player target")]
		public Vector3 ScreenOffset = new Vector3(0f, 30f, 0f);

		// Token: 0x0400217E RID: 8574
		[Tooltip("UI Text to display Player's Name")]
		public Text PlayerNameText;

		// Token: 0x0400217F RID: 8575
		[Tooltip("UI Slider to display Player's Health")]
		public Slider PlayerHealthSlider;

		// Token: 0x04002180 RID: 8576
		private PlayerManager _target;

		// Token: 0x04002181 RID: 8577
		private float _characterControllerHeight;

		// Token: 0x04002182 RID: 8578
		private Transform _targetTransform;

		// Token: 0x04002183 RID: 8579
		private Renderer _targetRenderer;

		// Token: 0x04002184 RID: 8580
		private Vector3 _targetPosition;
	}
}
