using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {

    public Transform relativeRotation;
    public Transform thisRotation;

    void Start()
    {
        thisRotation = GetComponent<Transform>();
    }

	void Update () {
        thisRotation.rotation = (Quaternion.identity);
	}
}
