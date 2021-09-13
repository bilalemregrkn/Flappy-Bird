using System;
using System.Collections;
using Script;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D myRigidbody2D;
	[SerializeField] private float jumpPower;
	[SerializeField] private float speed;

	private bool IsPressJump => Input.GetMouseButtonDown(0);

	private float _initialGravityScale;
	private bool _isFirstJump = true;

	private void Start()
	{
		_initialGravityScale = myRigidbody2D.gravityScale;
		myRigidbody2D.gravityScale = 0;
	}
	
	public void Update()
	{
		if(GameManager.Instance.IsGameOver) return;
		
		if (IsPressJump)
		{
			if (_isFirstJump)
			{
				_isFirstJump = false;
				myRigidbody2D.gravityScale = _initialGravityScale;
				GameManager.Instance.StartGame();
			}

			Jump();
		}

		Move();
	}

	private void Move()
	{
		var position = transform.position;
		position.x += speed * Time.deltaTime;
		transform.position = position;
	}

	private void Jump()
	{
		myRigidbody2D.velocity = Vector2.zero;
		myRigidbody2D.AddForce(Vector2.up * jumpPower);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag($"Success"))
		{
			Debug.Log("Success");
			GameManager.Instance.OnPlayerPass();
		}
		
		if (other.transform.CompareTag($"Block"))
		{
			GameManager.Instance.GameOver();
		}
	}
}