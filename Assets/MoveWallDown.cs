using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveWallDown : MonoBehaviour
{

  public string tagToMove;
  private List<GameObject> objectsToMove;
 
	// Use this for initialization
	void Start ()
	{
    objectsToMove = GameObject.FindGameObjectsWithTag(tagToMove).ToList();
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    objectsToMove.ForEach(wall => wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y - 5, wall.transform.position.z));
  }
}
