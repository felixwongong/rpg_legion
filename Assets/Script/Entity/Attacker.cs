using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class Attacker
    {
        private int attackDmg = 10;
        
        public void Attack()
        {
            Debug.Log($"attacking with dmg {attackDmg}");
        }
    }
}