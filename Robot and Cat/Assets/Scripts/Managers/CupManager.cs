using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotCat.Objects;

namespace RobotCat
{
    public class CupManager : MonoBehaviour
    {
        public CupManager instance = null;

        private List<GrabbableObject> potentialCups;
        public int cupsAtStartUp = 20;
        public List<SpawnLocation> spawnLocations;
        public GrabbableObject cupPrefab;
        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            instance = this;

            if (cupsAtStartUp > spawnLocations.Count)
            {
                cupsAtStartUp = spawnLocations.Count;
            }

            potentialCups = new List<GrabbableObject>();
            GrabbableObject cupHolder;
            for(int i = 0; i< cupsAtStartUp;i++)
            {
                cupHolder = GameObject.Instantiate<GrabbableObject>(cupPrefab);
                potentialCups.Add(cupHolder);
            }

        }

        public void initiateAGame()
        {
            bool[] possibleSpawnIndexs = new bool[spawnLocations.Count];
            for(int i = 0; i< possibleSpawnIndexs.Length; i++)
            {
                possibleSpawnIndexs[i] = false;
            }
            int indexOfSpawn;
            bool spawnSuccesful;
            for(int i = 0; i<cupsAtStartUp;i++)
            {
                spawnSuccesful = false;
                while(!spawnSuccesful)
                {
                    indexOfSpawn = Mathf.FloorToInt(Random.Range(0.0f, cupsAtStartUp - 1));
                    
                }

            }


        }


    }
}
