using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dataStorage
{
    public class dataStorage : MonoBehaviour
    {
        //Manager Data Input
        public int exp = 0;
        //Manager Data Output
        public int manpower = 1000;
        public int expMultiplikator = 1;

        //Digging Scene Vector (Ausgrabungsschnitt)
        public Vector2 startingPoint;
        public Vector2 size;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
