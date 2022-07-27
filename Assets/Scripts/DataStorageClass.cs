using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStorage
{
    public class DataStorageClass : MonoBehaviour
    {
        //Manager Data Input
        public int exp = 0;
        //Manager Data Output
        public int manpower = 1000;
        public int expMultiplikator = 1;

        //Digging Scene Vector (Ausgrabungsschnitt)
        public Vector2 startingPoint;
        public Vector2 size;

        public int artifactsEnabled;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
