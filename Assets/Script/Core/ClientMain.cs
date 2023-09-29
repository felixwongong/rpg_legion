using System.Collections.Generic;
using CofyEngine.Editor;
using Engine.Util;
using UnityEngine;

namespace CofyEngine
{
    public class ClientMain : MonoInstance<ClientMain>
    {
        [CofyScene]
        [SerializeField]
        public List<string> persistentScenes;

        private void Start()
        {
            BootstrapStateMachine.instance.Init();
            LevelManager.instance.SetPersistent(persistentScenes);
        }
    }
}