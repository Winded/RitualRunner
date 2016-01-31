using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	public int scoreReward;
    public GameObject particle;


	void Start() {
   
	}



	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Game.Score.AddScore (scoreReward);
            if(particle)
            {
                Instantiate(particle, transform.position + new Vector3(0f, 0f, -5f), Quaternion.identity);
            }
            Destroy (gameObject);
		}
	}
}
