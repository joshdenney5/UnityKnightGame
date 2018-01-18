using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class KillBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    print("Killbox collision");
    if (other.gameObject.CompareTag("Player"))
    {
      print("destoyed player");
      new RespawningHelper().ResetPlayer(other.gameObject);
    }
  }
}
