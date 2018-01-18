using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
  public class RespawningHelper
  {
    public void ResetPlayer(GameObject player)
    {
      var spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");
      player.gameObject.transform.position = spawnPoint.transform.position;
      var camera = GameObject.FindGameObjectWithTag("MainCamera");
      camera.gameObject.transform.position = new Vector3(spawnPoint.transform.position.x,
        spawnPoint.transform.position.y, -10);
    }
  }
}
