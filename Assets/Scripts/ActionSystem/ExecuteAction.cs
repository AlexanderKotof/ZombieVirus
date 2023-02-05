using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public abstract class ExecuteAction : ScriptableObject
    {
        public abstract void Execute();
    }
}
