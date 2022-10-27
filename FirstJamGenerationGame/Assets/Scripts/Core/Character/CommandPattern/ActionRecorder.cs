using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Character.Command
{

    public class ActionRecorder : MonoBehaviour
    {
    // private readonly Stack<ActionBase> _actions = new Stack<ActionBase>();
        private readonly Dictionary<int, ActionBase> _actionsDict= new Dictionary<int, ActionBase>();
        
        int _dictLength = 20;
        private int tracker = 0;
        public void Record(ActionBase action)
        {
            if (tracker < 0)
                tracker = 0;

            if (tracker != _dictLength)
            {
                _actionsDict[tracker] = action;
                action.Execute();
            }
            else
            {
                tracker = 0;
                _actionsDict[tracker] = action;
            }
            tracker++;
        }

        public void Rewind()
        {
            ActionBase action;
            if (tracker <= 0)
            {
                tracker = _actionsDict.Count - 1;
            }
            action = _actionsDict[tracker - 1];
            // if (tracker <= 0)
            //     tracker = 1;
            if (action.HasBeenVisited)
            {
                //tracker = 0;
                return;
            }
            tracker--;
            action.Undo();
        }
    }

}