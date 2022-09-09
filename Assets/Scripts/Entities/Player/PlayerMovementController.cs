using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private float _groundedRadius = 0.2f;

	private bool _isGrounded;

	private void FixedUpdate()
	{
		_isGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _groundLayer);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_isGrounded = true;
			}
		}
	}

	public void Move(float speed, Vector2 direction)
    {
        if (_isGrounded)
        {
			//_rigidBody.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
			transform.parent.Translate(speed * Time.fixedDeltaTime * direction);
		}
    }
}
