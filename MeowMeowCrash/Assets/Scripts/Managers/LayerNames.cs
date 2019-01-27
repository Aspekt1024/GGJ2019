using UnityEngine;

namespace RobotCat
{
    public enum Layers
    {
        Surface, ObjectPlacemat, GrabbableObject
    }

    public class LayerUtil
    {
        public static int GetLayerMask(Layers layer)
        {
            return 1 << LayerMask.NameToLayer(layer.ToString());
        }

        public static int GetLayerMask(Layers[] layers)
        {
            int layerMask = 0;
            foreach (var layer in layers)
            {
                layerMask |= 1 << LayerMask.NameToLayer(layer.ToString());
            }
            return layerMask;
        }
    }
}
