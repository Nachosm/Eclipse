using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour
{

    public GameObject NPC;
    public Transform opponent;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    protected float accuracy = 3.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        NPC = animator.gameObject;
        //accuracy = NPC.GetComponent<Pathfinding.AIPath>().endReachedDistance;
        //Debug.Log(accuracy);

        if (NPC.GetComponent<NPC>())
            opponent = NPC.GetComponent<NPC>().target;
    }
}
