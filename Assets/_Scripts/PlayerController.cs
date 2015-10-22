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

    private float _moving = 0;

	// Use this for initialization
	void Start () {
        this._rigidBody2D=gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        //this._animator = gameObject.GetComponent<Animator>();


        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._CoinSound =this._audioSources[0];
	
	}
	
	
	void FixedUpdate () {

        float forceX = 0f;
        float forceY = 0f;

        float absVelX = Mathf.Abs(this._rigidBody2D.velocity.x);
        float absVelY = Mathf.Abs(this._rigidBody2D.velocity.y);

        this._moving = Input.GetAxis("Horizontal");

        if(this._moving != 0)
        {
            //we are moving
            if(this._moving>0)
            {
            //we are moving right
                if(absVelX < this.velocityRange.vMax)
                {
                    forceX = this.speed;
                }
            }
            else if (this._moving<0)
            {
            //we are moving left
                if (absVelX < this.velocityRange.vMax)
                {
                    forceX = - this.speed;
                }
            }
        }

        else if (this._moving == 0) 
        {
        //we are not moving
        }

        this._rigidBody2D.AddForce(new Vector2(forceX, forceY));
	
	}

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Coin"))
        {
            this._CoinSound.Play();
        }

    }
}
