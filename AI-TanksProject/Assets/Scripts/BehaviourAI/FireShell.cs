using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireShell : Node
{
    private float h = 2;
    private float gravity = -18;


    public FireShell(Tank ownerTank) : base(ownerTank)
    {

    }

    public override NODE_STATUS Update()
    {
        if(ownerTank.targetPos != null /*&& ownerTank.shootTimer <= 0*/)
        {
            if(ownerTank.canShoot == true)
            {
                if (ownerTank.tankTurret.GetComponent<VisionCone>())
                {
                    Debug.Log("");
                }
                Vector3 velocity = CalculateShellVelocity();
                Fire(velocity);
                
                Debug.Log("ShellFired");
                ownerTank.canShoot = false;
                ownerTank.shootTimer = NeededVariables.SHOOTTIMER;

                return NODE_STATUS.SUCCESS;

            }
            else
            {
                return NODE_STATUS.FAILURE;
            }

            
            
            //ownerTank.shootTimer -= Time.deltaTime;
            //return NODE_STATUS.FAILURE;
            
        }
        else
        {
            return NODE_STATUS.FAILURE;
        }
    }

    private void Fire(Vector3 vel)
    {
        Physics.gravity = Vector3.up * gravity;

        GameObject shellInstance;
        shellInstance = GameObject.Instantiate(ownerTank.shellPrefab, ownerTank.firePoint.position, ownerTank.firePoint.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = vel;
        Debug.Log("fired shell");
    }

    private Vector3 CalculateShellVelocity()
    {
        float displacementY = ownerTank.targetPos.y - ownerTank.firePoint.position.y;
        Vector3 dispacementXZ = new Vector3(ownerTank.targetPos.x - ownerTank.firePoint.position.x, 0, ownerTank.targetPos.z - ownerTank.firePoint.position.z);

        Vector3 velY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velXZ = dispacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity));
         
        return velXZ + velY;
    }
}

