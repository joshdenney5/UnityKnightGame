using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
  public float initialDelay;
  public float patternTime;
  private bool isActive;
	// Use this for initialization
	void Start () {
    InvokeRepeating("DissapearingPlatform", initialDelay, patternTime);
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  private void DissapearingPlatform()
  {
    isActive = !isActive;
    gameObject.SetActive(isActive);
  }
}
