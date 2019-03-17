using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotCat.Objects;

namespace RobotCat
{
    public class CupManager : MonoBehaviour
    {
        public static CupManager instance = null;

        private List<GrabbableObject> potentialCups = new List<GrabbableObject>();
        public int cupsAtStartUp = 20;
        public SpawnLocation[] spawnLocations;
        public GrabbableObject cupPrefab;
        void Awake()
        {
           /* if (instance != null)
            {
                Destroy(this);
            }
            instance = this;

            if (cupsAtStartUp > spawnLocations.Length)
            {
                cupsAtStartUp = spawnLocations.Length;
            }

            potentialCups = new List<GrabbableObject>();
            GrabbableObject cupHolder;
            for(int i = 0; i< cupsAtStartUp;i++)
            {
                cupHolder = GameObject.Instantiate<GrabbableObject>(cupPrefab);
                potentialCups.Add(cupHolder);
            }*/

        }

        public void initiateAGame()
        {
            int[] myList = new int[cupsAtStartUp];
            for (int i = 0; i < cupsAtStartUp; i++)
            {
                myList[i] = i;
            }
            
            for (int i = 0; i < cupsAtStartUp; i++)
            {
                int randIndex = Random.Range(0, cupsAtStartUp);
                int temp = myList[i];
                myList[i] = myList[randIndex];
                myList[randIndex] = temp;
            }

            List<int> randomIndexs = new List<int>();
            int spawnIndex;
            for (int i = 0; i < cupsAtStartUp; i++)
            {
                spawnIndex = myList[i];
                potentialCups.ToArray()[i].SpawnCup(spawnLocations[spawnIndex]);
            }


        }

        public void endGame()
        {
            foreach(GrabbableObject a in potentialCups)
            {
                a.DeactivateCup();
            }
        }

    }
}
