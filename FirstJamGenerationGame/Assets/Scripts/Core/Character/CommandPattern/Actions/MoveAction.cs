using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character;

namespace Core.Character.Command
{

    public class MoveAction : ActionBase
    {
        Vector3 position;
        float speed;
        ActionRecorder actionRecorder;
        public MoveAction(Player player, Vector3 position, float speed, ActionRecorder actionRecorder) : base(player)
        {
            this.position = position;
            this.speed = speed;
            this.actionRecorder = actionRecorder;
        }
        public override void Execute()
        {
            _player.PopulatePositionsList(position);
        }
        public override void Undo() =>   _player.SetRewind(position, speed, actionRecorder);
        public override void Clear() => _player.positions.Clear();
    }


}