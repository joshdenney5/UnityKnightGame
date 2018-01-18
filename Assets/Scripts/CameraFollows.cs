using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{

  private Transform target;
  public float smoothing = 5f;

  private Vector3 offset;
	// Use this for initialization
	void Start ()
	{
    target = GameObject.FindGameObjectWithTag("Player").transform;
    offset = transform.position - target.position;
	}
	
	
	void FixedUpdate ()
	{
	  var targetCamPos = target.position + offset;
	  transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime);
	}
}
