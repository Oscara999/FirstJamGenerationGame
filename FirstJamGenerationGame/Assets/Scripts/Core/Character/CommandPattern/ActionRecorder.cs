using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Character.Command
{

    public class ActionRecorder : MonoBehaviour
    {
        public readonly Stack<ActionBase> actions = new Stack<ActionBase>();
        
        public void Record(ActionBase action)
        {

            actions.Push(action);
            action.Execute();
        }

        public void Rewind()
        {
            if (actions.Count == 0) return;
            var action = actions.Pop();
            action.Undo();
        }
    }

}