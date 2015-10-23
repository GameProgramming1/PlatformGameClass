using UnityEngine;
using System.Collections;

[System.Serializable]
public class VelocityRange 
{
    public float vMin, vMax;

    public VelocityRange(float vMin, float vMax)
    {
        this.vMin = vMin;
        this.vMax = vMax;
    }
}

public class PlayerController : MonoBehaviour {

    //public instance variables
    public float speed = 50f;
    public float jump = 500f;

    public VelocityRange velocityRange = new VelocityRange(300f, 1000f);


    //private instance variable
    private Rigidbody2D _rigidBody2D;
    private Transform _transform;
    private Animator _animator; //will use it later
    private AudioSource[] _audioSources;
    private AudioSource _CoinSound;
    private AudioSource _JumpSound;

    private float _movingValue = 0;
    private bool _isFlippingRight = true;
    private bool _isGrounded = true;

	// Use this for initialization
	void Start () {
        this._rigidBody2D=gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();


        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._CoinSound =this._audioSources[0];
        this._JumpSound = this._audioSources[1];
	
	}
	
	
	void FixedUpdate () {

        float forceX = 0f;
        float forceY = 0f;

        float absVelX = Mathf.Abs(this._rigidBody2D.velocity.x);
        float absVelY = Mathf.Abs(this._rigidBody2D.velocity.y);

        this._movingValue = Input.GetAxis("Horizontal");

        if(this._movingValue != 0)
        {
            //we are moving

            this._animator.SetInteger("AnimState", 1);//walk
            if(this._movingValue>0)
            {
            //we are moving right
                if(absVelX < this.velocityRange.vMax)
                {
                    forceX = this.speed;
                    this._isFlippingRight = true;
                    this.flip();
                }
            }
            else if (this._movingValue<0)
            {
            //we are moving left
                if (absVelX < this.velocityRange.vMax)
                {
                    forceX = - this.speed;
                    this._isFlippingRight = false;
                    this.flip();
                }
            }
        }

        else if (this._movingValue == 0) 
        {
        //we are not moving
            this._animator.SetInteger("AnimState", 0);
        }

        //to check if player is jumping
        if((Input.GetKey("up") || Input.GetKey(KeyCode.W)))
        { //player is jumping
            
            if (this._isGrounded)
            {
               
                if (absVelY < this.velocityRange.vMax)
                {
                    forceY = this.jump;
                    this._animator.SetInteger("Animstate", 2);
                    this._JumpSound.Play();
                    this._isGrounded = false;
                }
            }
        
        }
      

        this._rigidBody2D.AddForce(new Vector2(forceX, forceY));
	
	}
    // on collision with platform
    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }

    //on collision with coin
    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Coin"))
        {
            this._CoinSound.Play();
        }

    }

    private void flip()
    { 
    if(this._isFlippingRight)
    {
        this._transform.localScale = new Vector2(1f,1f);
    }
    else 
    {
        this._transform.localScale = new Vector2(-1f, 1f);
    }
    }
}
