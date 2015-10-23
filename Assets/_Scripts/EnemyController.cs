using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    //public instance variables
    public float speed = 100f;

    //private instance variable
    private Rigidbody2D _rigidBody2D;
    private Transform _transform;
    private Animator _animator;

    private bool _isGrounded = false;

	// Use this for initialization
	void Start () {
        this._rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
	
	}
	
	
	void FixedUpdate () {
        if (this._isGrounded)
        {
            this._animator.SetInteger("AnimState", 1);
            this._rigidBody2D.velocity = new Vector2(this.speed, 0f);
        }
        else
        {
            this._animator.SetInteger("AnimState", 0);
        }
	
	}

    // on collision with platform
    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = false;
        }
    }
}
