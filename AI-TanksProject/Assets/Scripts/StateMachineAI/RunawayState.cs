using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunawayState : CautionSuperState
{
    private float timer = 0.5f;
    private float timerMax = 0.5f;

    private bool coverNearby;
    public Collider[] Colliders = new Collider[30];

    [Range(-1, 1)]
    public float hideSensitivity = 0; //Lower is better cover point

    public RunawayState(Tank ownerTank, IntruderStateMachine stateMachine) : base(ownerTank, stateMachine)
    {
    }

    public override StateID GetID()
    {
        return StateID.RUNAWAY;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered RunawayState state");
        coverNearby = false;
        ownerTank.closestCover = null;
        ownerTank.navAgent.speed = NeededVariables.RUNAWAYSPEED;

        //Check if near cover
        if(ownerTank.closestEnemy != null)
        {
            //If safePos is not current destination
            if(ownerTank.safePos != ownerTank.navAgent.destination)
            {
                coverNearby = FoundCoverPos(ownerTank.closestEnemy);

            }
            else
            {
                //If destination is SafePos, if the remaining distance is less than arrival range
                if(ownerTank.navAgent.remainingDistance < NeededVariables.ARRIVALRANGE)
                {
                    //Call foundCover function and set coverNearby to its return value
                    coverNearby = FoundCoverPos(ownerTank.closestEnemy);
                }
            }

            if(coverNearby)
            {
                ownerTank.navAgent.SetDestination(ownerTank.safePos);
            }

        }
        else
        {
            stateMachine.ChangeState(StateID.FINDRANDOMPOS);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting RunawayState state");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Updating RunawayState state");


        //If reached destination
        if(ownerTank.navAgent.remainingDistance < NeededVariables.ARRIVALRANGE)
        {
            //If can see enemy still
            if(ownerTank.seenEnemies.Count > 0)
            {
                //If shotTimer is done
                //If aiming at target
                if(ownerTank.aimingAtTarget)
                {
                    //Shoot
                    stateMachine.ChangeState(StateID.CHECKIFINRANGE);
                }

            }
            else
            {
                //If can't see enemy
                //Check if enemy can see me
                stateMachine.ChangeState(StateID.CHECKIFSPOTTED);
            }
            
        }


        //If cover is nearby, 
        if(coverNearby)
        {
            //Set destination to closest cover pos
            ownerTank.navAgent.SetDestination(ownerTank.safePos);
            Debug.Log("Cover Nearby");
        }
        else
        {
            //If no cover nearby, just run away from enemy
        }
        //If distance from tank to enemy is more than safe distance, shoot
        if(ownerTank.seenEnemies.Count > 0)
        {
            stateMachine.ChangeState(StateID.CHECKIFINRANGE);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool SetNearCover()
    {
        // Collect all the colliders of cover within runaway radius
        Collider[] colliders = Physics.OverlapSphere(ownerTank.transform.position, NeededVariables.RUNAWAYRADIUS, ownerTank.coverMask);
        Transform coverObject = null;
        float closestCoverDistance = 1000f;

        //If already have a closestCover
        if(ownerTank.closestCover != null)
        {
            //ClosetCoverDistance is the distance between tank and closetCover
            closestCoverDistance = Vector3.Distance(ownerTank.transform.position, ownerTank.closestCover.position);
        }

        if(colliders != null)
        {
            for(int i = 0; i < colliders.Length; i++)
            {
                //If collider doesn't belong to closestCoverObject or closestCover
                if(colliders[i].GetComponent<Transform>() != coverObject && colliders[i].GetComponent<Transform>() != ownerTank.closestCover)
                {
                    //Check distance from cover
                    float newDistanceToCover = Vector3.Distance(ownerTank.transform.position, colliders[i].GetComponent<Transform>().position);

                    ///////////////////////////////////////Distance from enemy to player is shorter than Distance from enemy to new pos

                    //If distance to cover is shorter than closestCoverDistance
                    if(newDistanceToCover < closestCoverDistance)
                    {
                        //Set Closest distance to be new distance to cover
                        closestCoverDistance = newDistanceToCover;
                        //Set coverObject to be colliders[i] transform
                        coverObject = colliders[i].GetComponent<Transform>();
                    }
                }
            }

            //If coverObject is not null
            if(coverObject != null)
            {
                //Set closetCover to be coverObject
                ownerTank.closestCover = coverObject; 

                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            //No cover nearby
            return false;
        }
    }


    private bool FoundCoverPos(Transform enemy) 
    {
        Vector3 newSafePos = Vector3.zero;
        string coverName = "";
        for(int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i] = null;
        }

        int hits = Physics.OverlapSphereNonAlloc(ownerTank.transform.position, NeededVariables.RUNAWAYRADIUS, Colliders, ownerTank.coverMask);

        for (int i = 0; i < hits; i++)
        {
            Vector3 tempSafePos = Vector3.zero;
            
            //If able to hit a point on the navmesh where the collider is
            if (UnityEngine.AI.NavMesh.SamplePosition(Colliders[i].transform.position, out UnityEngine.AI.NavMeshHit hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
            {
                //If hit position is far enough away from current position
                if (Vector3.Distance(ownerTank.transform.position, hit.position) > 6f)
                {


                    //If can't find an edge
                    if (!UnityEngine.AI.NavMesh.FindClosestEdge(hit.position, out hit, UnityEngine.AI.NavMesh.AllAreas))
                    {
                        Debug.Log("No Edge");
                    }

                    //If cover point is facing away from enemy because the normal is negative
                    if (Vector3.Dot(hit.normal, (enemy.position - hit.position).normalized) < hideSensitivity)
                    {
                        //If safePoint is not closer to enemy than me
                        if (Vector3.Distance(ownerTank.transform.position, hit.position) < Vector3.Distance(enemy.position, hit.position))
                        {
                            //Store position in temp variable
                            tempSafePos = hit.position;

                        }

                    }
                    //If cover point is facing enemy...
                    else
                    {
                        //Try other side of object
                        if (UnityEngine.AI.NavMesh.SamplePosition(Colliders[i].transform.position - (enemy.position - hit.position).normalized * 2, out UnityEngine.AI.NavMeshHit secondHit, 2f, UnityEngine.AI.NavMesh.AllAreas))
                        {
                            //If can't find an edge
                            if (!UnityEngine.AI.NavMesh.FindClosestEdge(secondHit.position, out secondHit, UnityEngine.AI.NavMesh.AllAreas))
                            {
                                Debug.Log("No Edge");
                            }

                            //If cover point is facing away from enemy
                            if (Vector3.Dot(secondHit.normal, (enemy.position - secondHit.position).normalized) < hideSensitivity)
                            {
                                //If safePoint is not closer to enemy than me
                                if (Vector3.Distance(ownerTank.transform.position, secondHit.position) < Vector3.Distance(enemy.position, secondHit.position))
                                {
                                    //Store position in temp variable
                                    tempSafePos = secondHit.position;

                                }

                            }
                        }
                    }

                    //If tempSafePos is not zero, something has been stored in it
                    if (tempSafePos != Vector3.zero)
                    {
                        if(newSafePos != Vector3.zero)
                        {
                            //If distance to tempSafePos is less than newSafePos
                            if (Vector3.Distance(ownerTank.transform.position, tempSafePos) < Vector3.Distance(ownerTank.transform.position, newSafePos))
                            {
                                newSafePos = tempSafePos;
                                coverName = Colliders[i].gameObject.name;
                            }

                        }
                        //If newSafePos is zero
                        else
                        {
                            //Set it to be tempPos
                            newSafePos = tempSafePos;
                        }

                    }
                    //If tempPos is zero
                    else
                    {
                        Debug.Log("Not a valid position found for this obstacle");
                    }
                }
                else
                {
                    Debug.Log("Too close to current pos");
                }
            }
            else
            {
                Debug.Log("Unable to find point near object");
                
            }
        }

        //If safePos is not zero
        if(newSafePos != Vector3.zero)
        {
            //Set safePos to hit position
            ownerTank.safePos = newSafePos;

            Debug.Log(coverName);
            //Return true
            return true;
        }

        return false;
        
    }

    
}
