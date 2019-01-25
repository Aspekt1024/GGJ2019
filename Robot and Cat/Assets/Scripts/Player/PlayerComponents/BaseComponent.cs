using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RobotCat.Objects
{
    public enum PlaceableObject
    {
        CupBase,
    }
    public class BaseComponent : MonoBehaviour
    {
        public PlaceableObject placeable;
        public GameObject itemLocation;
    }
}
