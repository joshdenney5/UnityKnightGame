using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{

  public Vector3 nexLevelPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    var spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");
    spawnPoint.transform.position = nexLevelPosition;
    print("End of Level");
    new RespawningHelper().ResetPlayer(other.gameObject);
    
  }
}
