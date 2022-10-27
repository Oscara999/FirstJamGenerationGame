using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Platforms
{

    public abstract class Platform : MonoBehaviour
    {
        
        protected void FixedUpdate()
        {
            Behavior();
        }

        protected abstract void Behavior();
   
    }


}