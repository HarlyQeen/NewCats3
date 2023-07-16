using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "World", menuName = "World")]
    public class World : ScriptableObject
    {
        public Level[] _levels;

    }
}
