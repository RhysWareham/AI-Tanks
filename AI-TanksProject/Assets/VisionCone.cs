using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField]
    public float viewingRadius;
    [Range(0,360)] //Clamp to maximum of 360
    [SerializeField]
    public float viewingAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public Tank thisTank;



    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        //Clear list so no duplicates get added
        visibleTargets.Clear();
        thisTank.seenEnemies.Clear();

        //Collect all objects within the viewing radius with correct layer
        Collider[] targetsInViewRadius = Physics.OverlapSphere(thisTank.transform.position, viewingRadius, targetMask);

        //For each target in array
        for(int i = 0; i<targetsInViewRadius.Length; i++)
        {
            //If target is not this tank
            if(targetsInViewRadius[i].GetComponent<Tank>().m_PlayerNumber != thisTank.m_PlayerNumber)
            {
                //Get transform of the target
                Transform target = targetsInViewRadius[i].transform;
            
                //Get direction towards target by subtracting the transformPos from targetPos
                Vector3 directionToTarget = (target.position - thisTank.transform.position).normalized;
                //If the angle of the target from Tank's turret forward angle is within the viewing angle
                if(Vector3.Angle(transform.forward, directionToTarget) < viewingAngle / 2)
                {
                    //Get the distance to target
                    float distanceToTarget = Vector3.Distance(thisTank.transform.position, target.position);
                    //If no obstacle in way between tank and target
                    if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        //Can see target
                        visibleTargets.Add(target);
                        //thisTank.seenEnemies.Clear();
                        thisTank.seenEnemies.Add(target);
                        thisTank.InCombatMode = true;
                        //Debug.Log("Tank Seen");
                    }
                }

            }
        }
    }

    public Vector3 DirectionFromAngle(float angle, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angle += transform.eulerAngles.y;
        }
        //Work out the direction 
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    // Start is called before the first frame update
    void Start()
    {
        thisTank = GetComponentInParent<Tank>();
        StartCoroutine("FindTargetsWithDelay", 0.2f);
        NeededVariables.RUNAWAYRADIUS = viewingRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if(thisTank.restarting)
        {
            StartCoroutine("FindTargetsWithDelay", 0.2f);
            thisTank.restarting = false;
        }
    }
}
