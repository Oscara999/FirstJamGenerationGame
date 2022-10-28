using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public class Offset : MonoBehaviour
    {
        public GameObject player;
        Vector3 offset = new Vector3(0, 0.8f, -0.7f);
        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position + offset;
        }
    }

}