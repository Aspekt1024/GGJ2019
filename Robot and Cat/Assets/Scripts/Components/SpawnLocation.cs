using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RobotCat
{ 

public class SpawnLocation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GetComponent<MeshRenderer>() != null)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
}