using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class PlayerController : MonoBehaviour
    {
        private Attacker _attacker;
        public Attacker attacker => _attacker;
        
        private void Awake()
        {
            _attacker = new Attacker();
        }
    }
}