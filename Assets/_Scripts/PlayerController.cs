using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //private instance variable
    private AudioSource[] _audioSources;
    private AudioSource _CoinSound;

	// Use this for initialization
	void Start () {
        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._CoinSound =this._audioSources[0];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Coin"))
        {
            this._CoinSound.Play();
        }

    }
}
