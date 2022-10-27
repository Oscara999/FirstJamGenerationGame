using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character;
namespace  Core.Character.Command
{
    
    public abstract class ActionBase
    {
        protected readonly Player _player;
        protected bool _hasBeenVisited = false;
        public bool HasBeenVisited
        {
            get{return _hasBeenVisited;}
            private set{}
        }
        protected ActionBase(Player player)
        {
            _player = player;
            
        }

        public abstract void Execute();
        public abstract void Undo();
    }

}