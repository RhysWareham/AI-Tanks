using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//This will be like the zombie agent script
public class Tank : MonoBehaviour
{
    public int m_PlayerNumber; //Or AI type
    public GameObject tankTurret { get; private set; }
    public Transform firePoint;
    
    public GameObject shellPrefab;

    public static List<Tank> tankList = new List<Tank>();
    //[SerializeField]
    public Vector3 targetPos { get; set; }
    public Vector3 safeTargetPos { get; set; }
    public NavMeshAgent navAgent { get; private set; }
    private BehaviourTree behaviourTree;
    
    public List<Transform> seenEnemies = new List<Transform>();
    public Tank interactingEnemy { get; set; }

    public bool InCombatMode { get; set; }
    [SerializeField]
    public Transform lastKnownEnemyPos { get; set; }
    public int areaSearchNum = 0;
    public float scoutRadius = 20f;

    public float currentHealth { get; set; }

    public bool aimingAtTarget = false;

    public float shootTimer { get; set; }
    public bool canShoot { get; set; }

    public float takingDamageAmount { get; set; }



    //Code from old version of project
    public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
    public Image m_FillImage;                           // The image component of the slider.
    public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.

    private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
    private ParticleSystem m_ExplosionParticles;

    public bool isDead { get; set; }
    public bool restarting { get; set; }

    public StateMachine stateMachine { get; private set; }
    public StateID initialState;

    public Transform closestEnemy { get; set; }
    public bool canSeeEnemy { get; set; }
    public Vector3 safePos { get; set; }
    public LayerMask coverMask;
    public Transform closestCover { get; set; }
    //public Transform currentCover

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = 0;
        canShoot = true;
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        // Get a reference to the audio source on the instantiated prefab.
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        // Disable the prefab so it can be activated when it's required.
        m_ExplosionParticles.gameObject.SetActive(false);
        isDead = false;
        tankList.Add(this);
        tankTurret = GetComponentInChildren<Turret>().gameObject;
        interactingEnemy = null;
        takingDamageAmount = 0;
        currentHealth = NeededVariables.MAXHEALTH;
        navAgent = GetComponent<NavMeshAgent>();
        initialState = StateID.WANDER;

        canSeeEnemy = false;
        safePos = Vector3.zero;


        if (m_PlayerNumber == 1)
        {
            behaviourTree = new GuardBehaviourTree(this);

        }
        else
        {
            stateMachine = new IntruderStateMachine(this);
            stateMachine.Initialise(initialState);
            if(stateMachine == null)
            {
                stateMachine = new IntruderStateMachine(this);
            }
        }
        SetHealthUI();
        restarting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PlayerNumber == 1)
        {
            behaviourTree.Update();
        }
        else
        {
            stateMachine.Update();
        }
    }

    public void Reset()
    {
        canShoot = true;
        shootTimer = 0;
        gameObject.SetActive(true);
        isDead = false;
        currentHealth = NeededVariables.MAXHEALTH;
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        // Get a reference to the audio source on the instantiated prefab.
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
        // Disable the prefab so it can be activated when it's required.
        m_ExplosionParticles.gameObject.SetActive(false);
        interactingEnemy = null;
        InCombatMode = false;
        lastKnownEnemyPos = null;
        areaSearchNum = 0;
        seenEnemies.Clear();
        targetPos = Vector3.zero;
        SetHealthUI();
        //behaviourTree = new GuardBehaviourTree(this);
        aimingAtTarget = false;
        restarting = true;

        safeTargetPos = Vector3.zero;
        canSeeEnemy = false;
        safePos = Vector3.zero;
        closestCover = null;
        //if(m_PlayerNumber != 1)
        //{
        //    stateMachine.Initialise(initialState);
        //}
    }

    public void TakeDamage(float dmg)
    {
        takingDamageAmount = dmg;
    }

    public void SetHealthUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, currentHealth / NeededVariables.MAXHEALTH);
    }

    /// <summary>
    /// Code from old version of project
    /// </summary>
    public void OnDeath()
    {
        isDead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        m_ExplosionParticles.Play();

        // Play the tank explosion sound effect.
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles);
        // Turn the tank off.
        gameObject.SetActive(false);
    }
}
