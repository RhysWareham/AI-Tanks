using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateTurret : Node
{
    public RotateTurret(Tank ownerTank) : base(ownerTank)
    {
    }

    public override NODE_STATUS Update()
    {
        ownerTank.tankTurret.transform.Rotate(new Vector3(0, NeededVariables.TURRETROTATION * Time.deltaTime, 0 ));
        //ownerTank.tankTurret.transform.localRotation.rot
        return NODE_STATUS.SUCCESS; 

    }
}