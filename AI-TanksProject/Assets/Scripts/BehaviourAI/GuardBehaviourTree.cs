using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Selector tries the child, if node fais, move to next child; if one node is sucesful, go back up the tree
//Sequence executes all children left to right until a node fails.
//parallels are single tasks/actions: that return only success or failure

public class GuardBehaviourTree : BehaviourTree
{
    //Compounds
    Selector rootSelector;
    Sequence waitSequence;
    //Sequence infectedSequence;
    //Selector humanSelector;
    Sequence combatSequence;
    Selector canISeeEnemySelector;
    Sequence tankSeeEnemyCombatSequence;
    Selector canShootSelector;
    Selector canIShootNowSelector;
    Sequence mustIWaitSequence;
    Sequence moveToSafePlaceSequence;
    Selector haveIGotSafeTargetSelector;
    Sequence dontHaveSafeTargetSequence;
    Sequence inShotRangeSequence;
    Selector amISafeDistanceSelector;
    Sequence amISafeDistanceSequence;
    Sequence scoutLastKnownPosSequence;
    Selector haveIArrivedSelector;
    Sequence scoutAreaSequence;
    Sequence tankMoveSequence;
    Sequence tankSpotEnemySequence;
    Sequence takenDamageSequence;
    Sequence amIDeadSequence;
    //Sequence turnSequence;

    ////Conditions
    //AmIInfected amIInfected;
    //ShouldIRest shouldIRest;
    AmIInCombatMode amIInCombatMode;
    HaveIArrived haveIArrived;
    HaveINOTArrived haveINOTArrived;
    CanISeeEnemy canISeeEnemy;
    HasNotChecked3Points hasNotChecked3Points;
    IsEnemyInRange isEnemyInRange;
    AmISafeDistance amISafeDistance;
    AmIAimingAtTarget amIAimingAtTarget;
    HaveIBeenHit haveIBeenHit;
    AmIDead amIDead;
    IsShootTimerOn isShootTimerOn;
    IGotSafeTarget iGotSafeTarget;
    //WasIBitten wasIBitten;


    ////Actions
    FindClosestEnemy findClosestEnemy;
    //MoveToTarget zombieMoveToTarget;
    FindRandomPosition findRandomPosition;
    MoveToTarget tankMoveToTarget;
    MoveToSafeTarget tankMoveToSafeTarget;
    GetLastKnownPos getLastKnownPos;
    SelectPosInSearchArea selectPosInSearchArea;
    RotateTurret rotateTurret;
    //RotateTurretTowardEnemy rotateTurretTowardEnemy;
    AimTurret aimTurret;
    FireShell fireShell;
    UpdateEnemyTargetPos updateEnemyTargetPos;
    ReduceMyHealth reduceMyHealth;
    Die die;
    FindNearSafePosition findNearSafePosition;

    TestSequenceNode testSequenceNode;
    //Mutate mutate;
    Wait wait;


    public GuardBehaviourTree(Tank owner) : base(owner)
    {
    //    //Instanstiate
    //    //Compounds
        rootSelector = new Selector(ownerTank);
        waitSequence = new Sequence(ownerTank);

        combatSequence = new Sequence(ownerTank);
        canISeeEnemySelector = new Selector(ownerTank);
        tankSeeEnemyCombatSequence = new Sequence(ownerTank);
        canShootSelector = new Selector(ownerTank);
        inShotRangeSequence = new Sequence(ownerTank);
        canIShootNowSelector = new Selector(ownerTank);
        moveToSafePlaceSequence = new Sequence(ownerTank);
        dontHaveSafeTargetSequence = new Sequence(ownerTank);
        mustIWaitSequence = new Sequence(ownerTank);
        amISafeDistanceSelector = new Selector(ownerTank);
        amISafeDistanceSequence = new Sequence(ownerTank);
        haveIGotSafeTargetSelector = new Selector(ownerTank);
        scoutLastKnownPosSequence = new Sequence(ownerTank);
        haveIArrivedSelector = new Selector(ownerTank);
        scoutAreaSequence = new Sequence(ownerTank);
        tankSpotEnemySequence = new Sequence(ownerTank);
        tankMoveSequence = new Sequence(ownerTank);
        takenDamageSequence = new Sequence(ownerTank);
        amIDeadSequence = new Sequence(ownerTank);




        //    //Conditions

        amIInCombatMode = new AmIInCombatMode(ownerTank);
        haveIArrived = new HaveIArrived(ownerTank);
        haveINOTArrived = new HaveINOTArrived(ownerTank);
        canISeeEnemy = new CanISeeEnemy(ownerTank);
        isEnemyInRange = new IsEnemyInRange(ownerTank);
        hasNotChecked3Points = new HasNotChecked3Points(ownerTank);
        amISafeDistance = new AmISafeDistance(ownerTank);
        amIAimingAtTarget = new AmIAimingAtTarget(ownerTank);
        haveIBeenHit = new HaveIBeenHit(ownerTank);
        amIDead = new AmIDead(ownerTank);
        isShootTimerOn = new IsShootTimerOn(ownerTank);
        iGotSafeTarget = new IGotSafeTarget(ownerTank);

        //    //Actions
        findClosestEnemy = new FindClosestEnemy(ownerTank);
        findRandomPosition = new FindRandomPosition(ownerTank);
        tankMoveToTarget = new MoveToTarget(ownerTank);
        tankMoveToSafeTarget = new MoveToSafeTarget(ownerTank);
        getLastKnownPos = new GetLastKnownPos(ownerTank);
        selectPosInSearchArea = new SelectPosInSearchArea(ownerTank);
        rotateTurret = new RotateTurret(ownerTank);
        aimTurret = new AimTurret(ownerTank);
        testSequenceNode = new TestSequenceNode(ownerTank);
            wait = new Wait(ownerTank);
        fireShell = new FireShell(ownerTank);
        updateEnemyTargetPos = new UpdateEnemyTargetPos(ownerTank);
        reduceMyHealth = new ReduceMyHealth(ownerTank);
        die = new Die(ownerTank);
        findNearSafePosition = new FindNearSafePosition(ownerTank);

        //Link nodes
        //Set the root node
        rootNode = rootSelector;

        //Top level root selector
        rootSelector.AddChild(takenDamageSequence);
        rootSelector.AddChild(combatSequence);
        rootSelector.AddChild(tankMoveSequence);


        waitSequence.AddChild(wait);

        takenDamageSequence.AddChild(haveIBeenHit);
        takenDamageSequence.AddChild(reduceMyHealth);
        takenDamageSequence.AddChild(amIDeadSequence);
        amIDeadSequence.AddChild(amIDead);
        amIDeadSequence.AddChild(die);


        
        combatSequence.AddChild(amIInCombatMode);
        combatSequence.AddChild(canISeeEnemySelector);

        
        canISeeEnemySelector.AddChild(tankSeeEnemyCombatSequence);
        canISeeEnemySelector.AddChild(scoutLastKnownPosSequence);

        //Can i see an enemy?
        tankSeeEnemyCombatSequence.AddChild(canISeeEnemy);
        tankSeeEnemyCombatSequence.AddChild(findClosestEnemy);
        tankSeeEnemyCombatSequence.AddChild(updateEnemyTargetPos);
        //If i can, can i shoot?
        tankSeeEnemyCombatSequence.AddChild(canShootSelector);

        //Check if in shot range
        canShootSelector.AddChild(inShotRangeSequence);
        canShootSelector.AddChild(tankSpotEnemySequence);

        //If in range
        inShotRangeSequence.AddChild(isEnemyInRange);
        //Aim turret at enemy
        inShotRangeSequence.AddChild(aimTurret);
        //Check if I'm at safe distance away
        inShotRangeSequence.AddChild(amISafeDistanceSelector);
            amISafeDistanceSelector.AddChild(amISafeDistanceSequence);
            //If im not safe distance...
            //...Move away
            amISafeDistanceSelector.AddChild(moveToSafePlaceSequence);
        //If i am safe distance
        amISafeDistanceSequence.AddChild(amISafeDistance);
        //Am i aiming at the target?
        amISafeDistanceSequence.AddChild(amIAimingAtTarget);
        //If so, shoot
        amISafeDistanceSequence.AddChild(fireShell);


        //If not a safe distance away
        moveToSafePlaceSequence.AddChild(haveIGotSafeTargetSelector);
        //If got safe target, skip to rest of moveToSafePlaceSequence
            //Get a nearby safe position
            haveIGotSafeTargetSelector.AddChild(iGotSafeTarget);
            haveIGotSafeTargetSelector.AddChild(dontHaveSafeTargetSequence);
            dontHaveSafeTargetSequence.AddChild(findNearSafePosition);
        moveToSafePlaceSequence.AddChild(aimTurret);
        moveToSafePlaceSequence.AddChild(tankMoveToSafeTarget);
        moveToSafePlaceSequence.AddChild(haveIArrived);
        moveToSafePlaceSequence.AddChild(findNearSafePosition);


        scoutLastKnownPosSequence.AddChild(getLastKnownPos);
        scoutLastKnownPosSequence.AddChild(aimTurret);
        scoutLastKnownPosSequence.AddChild(haveIArrivedSelector);
        haveIArrivedSelector.AddChild(scoutAreaSequence);
        haveIArrivedSelector.AddChild(tankMoveToTarget);

        scoutAreaSequence.AddChild(hasNotChecked3Points);
        scoutAreaSequence.AddChild(haveIArrived);
        //If have arrived at scout point, select new point to scout
        scoutAreaSequence.AddChild(selectPosInSearchArea);
        scoutAreaSequence.AddChild(tankMoveToTarget);

        //Check if can see enemy
        tankSpotEnemySequence.AddChild(canISeeEnemy);
        tankSpotEnemySequence.AddChild(findClosestEnemy);
        tankSpotEnemySequence.AddChild(amISafeDistanceSelector);
        tankSpotEnemySequence.AddChild(tankMoveToTarget);       

        //    //if not...
        tankMoveSequence.AddChild(rotateTurret);
        tankMoveSequence.AddChild(haveIArrived);
        tankMoveSequence.AddChild(findRandomPosition);
        tankMoveSequence.AddChild(tankMoveToTarget);
    }
}

