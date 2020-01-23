using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    class Fighter : MonoBehaviour
    {
        public int hitpoint = 10;
        public int maxHitpoint = 10;
        public float pushRecoverySpeed = 0.2f;

        //Immunity
        protected float immuneTime = 0.2f;
        protected float lastImmune;

        //Push
        protected Vector3 pushDirection;

        //Waypoints
        protected bool usedwaypoint = false;

        protected bool colliding;
        //All fighters can receive damage / Die
        /*
        protected virtual void ReceiveDamage(Damage dmg)
        {
            if (Time.time - lastImmune > immuneTime)
            {
                lastImmune = Time.time;
                hitpoint -= dmg.damageAmount;
                pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
                if (transform.name == "Player")
                    GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 35, Color.red, transform.position, Vector3.zero, 0.5f);
                else
                {
                    //Debug.Log(transform.name);
                    GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 35, Color.cyan, transform.position, Vector3.zero, 0.5f);
                }

                if (hitpoint <= 0)
                {
                    hitpoint = 0;
                    Death();
                }
            }
        }*/
        /*protected virtual void usedWaypoints(Waypoint wp)
        {
            GameManager.instance.ShowText(wp.isUsed.ToString(), 45, Color.blue, transform.position, Vector3.zero, 0.5f);

        }*/

        protected void isColliding(bool colliding)
        {
            this.colliding = colliding;
        }

        protected virtual void Death()
        {

        }
    }
}
