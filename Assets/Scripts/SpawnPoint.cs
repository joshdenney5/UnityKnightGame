using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	  Instantiate(Resources.Load<GameObject>("preFabs/Knight"));
	  Instantiate(Resources.Load<GameObject>("preFabs/Main Camera"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
