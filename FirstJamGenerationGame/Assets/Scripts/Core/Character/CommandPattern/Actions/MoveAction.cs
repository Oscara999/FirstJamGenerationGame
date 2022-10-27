using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character;

namespace Core.Character.Command
{

    public class MoveAction : ActionBase
    {
        Vector3 _position;
        float _speed;
        public MoveAction(Player player, Vector3 position, float speed) : base(player)
        {
            _position = position;
            _speed = speed;
        }
        public override void Execute()
        {}
        public override void Undo()
        {
            _hasBeenVisited = true;
            _player.SetRewind(_position, _speed);
        }
    }


}