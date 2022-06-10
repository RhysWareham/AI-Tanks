using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimTurret : Node
{
    public AimTurret(Tank ownerAgent) : base(ownerAgent)
    {
    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.targetPos != Vector3.zero && ownerTank.seenEnemies.Count > 0)
        {
            //Vector3 directionToTarget = (ownerTank.targetPos - ownerTank.tankTurret.transform.position).normalized;

            //if (Vector3.Angle(ownerTank.tankTurret.transform.forward, directionToTarget)


            //lookRotation.eulerAngles = new Vector3(0f, lookRotation.eulerAngles.y, 0f);

                if(ownerTank.tankTurret.GetComponent<VisionCone>())
                {
                    Debug.Log("");
                }
            //Vector3 targetDir = ownerTank.targetPos - ownerTank.transform.position;
            Vector3 targetDirNorm = (ownerTank.targetPos - ownerTank.transform.position).normalized;
            //float newAngle = Vector3.Angle(ownerTank.tankTurret.transform.forward, targetDirNorm);
            //Debug.Log(newAngle);
            //float newY = NeededVariables.UnwrapAngle(ownerTank.tankTurret.transform.localEulerAngles.y - newAngle);
            //float offset =  ownerTank.transform.localEulerAngles.y - ownerTank.tankTurret.transform.localEulerAngles.y;
            //Got the angle difference between tank forward and targetPos
            //turretrotation.y = 0. and then plus angle
            //Get difference between turret y rotation and tank y rotation
            //Add the angle difference from 

            //newAngle =- ownerTank.tankTurret.transform.localEulerAngles.y; //This gives the angle that 

            //float newAngle = Vector3.Angle(ownerTank.tankTurret.transform.forward, targetDirNorm) + 90;
            //newAngle = ownerTank.transform.localEulerAngles.y - newAngle;
            //Time.timeScale = 0;
            ///////////////////////Still not correct angle^^^

            //float distanceToTarget = Vector3.Distance(ownerTank.tankTurret.transform.position, ownerTank.targetPos);
            Quaternion lookRotation = Quaternion.LookRotation(targetDirNorm, ownerTank.transform.up);
            lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y, 0);

            //ownerTank.firePoint.transform.rotation = ownerTank.tankTurret.transform.localRotation;

            //lookRotation.x = ownerTank.transform.rotation.x;
            //lookRotation.z = ownerTank.transform.rotation.z;

            if(Vector3.Angle(ownerTank.tankTurret.transform.forward, targetDirNorm) > NeededVariables.AIMLEEWAY)
            {
                ownerTank.tankTurret.transform.rotation = Quaternion.RotateTowards(ownerTank.tankTurret.transform.rotation, lookRotation, Time.deltaTime * NeededVariables.TURRETAIMSPEED);
                ownerTank.aimingAtTarget = false;
                return NODE_STATUS.RUNNING;
            }
            else
            {
                ownerTank.aimingAtTarget = true;
                return NODE_STATUS.SUCCESS;
            }

            ///////////////////////////This is getting the wrong angle?
            ////Debug.Log(Vector3.Angle(ownerTank.tankTurret.transform.position, ownerTank.targetPos));
            ////If angle is more than x degrees out
            //if (/*Vector3.Angle(ownerTank.tankTurret.transform.position, ownerTank.targetPos)*/ NeededVariables.UnwrapAngle(newAngle) > NeededVariables.AIMLEEWAY)
            //{

            //    ownerTank.tankTurret.transform.rotation = Quaternion.Slerp(ownerTank.tankTurret.transform.rotation, lookRotation, Time.deltaTime * NeededVariables.TURRETAIMSPEED);
            //    //ownerTank.tankTurret.transform.localEulerAngles = Vector3.Lerp(ownerTank.tankTurret.transform.localEulerAngles, new Vector3(0f, NeededVariables.UnwrapAngle(newY), 0f), Time.deltaTime * NeededVariables.TURRETAIMSPEED);
            //    //ownerTank.tankTurret.transform.rotation.eulerAngles = new Vector3(0, ownerTank.tankTurret.transform.rotation.y)
            //    ownerTank.aimingAtTarget = false;
            //    return NODE_STATUS.RUNNING;
            //}
            //else
            //{
            //    Debug.Log("AIMING!!!!!!!!!!!!!!!!!!");
            //    ownerTank.aimingAtTarget = true;
            //    return NODE_STATUS.SUCCESS;
            //}


        }

        return NODE_STATUS.FAILURE;
    }


    public Vector3 DirectionFromAngle(float angle, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angle += ownerTank.tankTurret.transform.localEulerAngles.y;
        }
        //Work out the direction 
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
