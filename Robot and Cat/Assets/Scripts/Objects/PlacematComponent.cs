using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RobotCat.Objects
{
    public enum PlaceableObject
    {
        CupBase,
    }
    public class PlacematComponent : MonoBehaviour
    {
        public PlaceableObject placeable;
        public GameObject itemLocation;

        public void positionObject(GrabbableObject obj)
        {
            obj.transform.position = itemLocation.transform.position;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
}
