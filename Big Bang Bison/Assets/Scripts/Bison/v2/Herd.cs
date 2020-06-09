/*
    Herd.cs
    Caetano 
    3/23/19
    Caetano
    Class for bison behavior
    Functions in file:
        GetNearbyObjects: In, agent - Out, list of nearby objects not ignored by bison
        .SquareAvoidanceRadius: Out, the square of the avoidance radius
        .SpawnBison: In: number of bison to spawn, where is the origin of the spawn - Out, spawns 'em
*/

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Herd is an object that will contain and manage each HerdAgent (Bison).   Herd = Flock
public class Herd : MonoBehaviour
{
    // These are set in the editor
    public HerdAgent agentPrefab; // The prefab used when instantiating new agents
    //public List<HerdShepherd> player = new List<HerdShepherd>; // the shepherds of the flock
    public HerdBehavior idleBehavior;
    public HerdBehavior runningBehavior;
    public HerdBehavior panicBehavior;  //  CUT
    public HerdBehavior outOfPlayBehavior;
    public HerdBehavior inHerdBoxBehavior;

    private List<HerdBehavior> behaviors; // The behaviors run on each agent, sorted by state

    private float bisonHeight;

    // A list of all the agents in this herd
    List<HerdAgent> agents = new List<HerdAgent>(); // it starts empty

    // The number of agents to spawn in this herd on startup
    [Range(0, 200)]
    public int startingCount = 0; // defaults to 0

    // These variables affect the behaviors in the herd
    [Range(0f, 20f)]
    public float driveFactor = 4f; // increases the intensity of behaviors
    [Range(0.1f, 3f)]
    public float boxDrive = 0.8f; // increases the intensity of box
    [Range(0f, 10f)]
    public float drag = 4f; // drag
    [Range(1f, 100f)]
    public float maxAcc = 10f; // the max acceleration
    [Range(1f, 20f)]
    public float neighborRadius = 15f; // how close something has to be to be detected as a neighbor
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.33f; // the radius for agents to avoid things, relative to neighborRadius
    [Range(0f, 1f)]
    public float idleAvoidanceRadiusMultiplier = 0.2f; // the radius for bison to avoid things when idle, relative to neighborRadius
    [Range(0, 10)]
    public float idleTimer = 1;  // the amount of time, in seconds, bison have to spend outside of a box to go idle
    [Range(0, 10)]
    public float lostTimer = 0.1f;  // the amount of time in seconds bison have to be out of play for to be lost
    [Range(0, 10)]
    public int joinDroveThresh = 3;  // the amount of nearby bison that will trigger droving
    [Range(0, 15)]
    public float spawnCircle = 7.5f; // Size or spawner circle

    // Math is faster when done with squares, save them here
    float squareMaxAcc;
    float squareNeighBorRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius {
        get { return squareAvoidanceRadius; }
    }
    float squareIdleAvoidanceRadius;
    public float SquareIdleAvoidanceRadius
    {
        get { return squareIdleAvoidanceRadius; }
    }

    public ParticleSystem deathPfx;

    //ListOfNames m_listOfNames = new ListOfNames();
    //List<int> usedIndexes = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        // Math is faster when done with squares, calculate them here
        squareMaxAcc = maxAcc * maxAcc;
        squareNeighBorRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighBorRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareIdleAvoidanceRadius = squareNeighBorRadius * idleAvoidanceRadiusMultiplier * idleAvoidanceRadiusMultiplier;

        // Get the distance bison should be from the ground
        bisonHeight = agentPrefab.GetComponent<Collider>().bounds.extents.y;

        behaviors = new List<HerdBehavior>() {
            idleBehavior,
            runningBehavior,
            panicBehavior,
            outOfPlayBehavior };// The behaviors run on each agent, sorted by state

        // Make all the Bison
        //SpawnBison(startingCount, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        // Keep track of bison to remove
        List<HerdAgent> toDestroy = new List<HerdAgent>();

        // Iterate through all agents and  move them
        foreach (HerdAgent agent in agents)
        {
            if (agent.state == 4 || (!agent.hasMoved && agent.readyToRemove) || agent.scored)
            {
                toDestroy.Add(agent);
                continue;
            }

            

            RaycastHit ground;
            // Determine state and keep them above the ground
            if (!Physics.Raycast(agent.transform.position, -Vector3.up, out ground, bisonHeight + 5f))
            {
                if (agent.state != 3)
                {
                    agent.floating = true; // Off the ground
                    agent.Float();
                }

                agent.agentBody.drag = 1f;
                agent.agentBody.angularDrag = 0f;
                //agent.agentBody.AddTorque(); twist?
            } else
            {
                agent.floating = false; // Over the ground

                Vector3 adjustment = Vector3.zero;
                adjustment.y = (bisonHeight-0.8f) - ground.distance;
                agent.transform.position += adjustment;
            }

            List<Transform> context = GetNearbyObjects(agent); // get the context, also flag if a player or hill is near

            // Calculate move
            Vector3 move = behaviors[agent.state].CalculateMove(agent, context, this); // figure out how much the agent should move, and which behavior it's using

            // Run herdBox behavior
            if (agent.inHerdBox > 0)
            {
                move += inHerdBoxBehavior.CalculateMove(agent, context, this) * (agent.boxPressure * boxDrive);
            }

            move *= driveFactor; // make it bigger

            //if (move.sqrMagnitude > squareMaxAcc) move = move.normalized * maxAcc; // cap it at max

            agent.Move(move); // tell it to move itself

            // colors crowded bison
            // agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 8f);
        }

        foreach (HerdAgent agent in toDestroy)
        {
            // if (this.agents.Remove(agent)) Debug.Log("removed");
            this.agents.Remove(agent);
            if (!agent.scored)
            {
                ParticleSystem ps = Instantiate(deathPfx, agent.transform.position, agent.transform.localRotation);
                ps.Play();
            }
            
            Destroy(agent.gameObject);
        }
    }

    // Gets nearby objects for bison, and sets their state
    List<Transform> GetNearbyObjects(HerdAgent agent)
    {
        int layerMask = ~(1 << 10); // used to ignore objects in layer 10

        List<Transform> context = new List<Transform>(); // the list to return

        // gets all colliders near the agent
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius, layerMask, QueryTriggerInteraction.Collide);

        agent.nearPlayer = false;
        agent.inHill = false;
        int drovingNeighbors = 0;
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider) // if the collider isn't its own
            {
                context.Add(c.transform); // add it to the context
                if (c.gameObject.layer == 12 && c.gameObject.GetComponent<HerdAgent>().inHerdBox > 0)
                {
                    drovingNeighbors += 1;
                }
            }

            if (c.gameObject.layer == 11) // check if the player is near
            {
                //agent.state = 1;
                agent.nearPlayer = true;
            }
            else if (c.gameObject.tag == "Hill")
            {
                agent.inHill = true;
            }
        }
        
        // If the bison is in a player's herd box or near enough bison that are, it will drove
        if (agent.inHerdBox > 0 || (drovingNeighbors >= joinDroveThresh)) agent.Drove();

        return context;
    }

    // Make some new boys
    public void SpawnBison(int count, Vector3 origin, string name)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawn = (Vector3)Random.insideUnitCircle * spawnCircle;
            spawn.z = spawn.y; // we want z
            spawn.y = 2; // not y

            spawn += origin;

            // Yo, it's a new bison
            HerdAgent newAgent = Instantiate(
                agentPrefab,
                spawn,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)), // random start rotation
                transform // herd position
                );

            //int index = RandomSelectionExcept();
            newAgent.name = name;
            newAgent.Initialize(this); // agent has startup function
            agents.Add(newAgent);
        }
        /*for (int j = 0; j < usedIndexes.Count; j++)
        {
            Debug.Log("index " + usedIndexes[j]);
        }*/
    }

    /*int RandomSelectionExcept()
    {
        int rng = -1;
        int length = m_listOfNames.GetLength();

        do
        {
            rng = Random.Range(0, length);
        }
        while (usedIndexes.Contains(rng));

        usedIndexes.Add(rng);


        if (usedIndexes.Count >= length)
        {
            ClearUsedIndexes();
        }

        Debug.Log("index2: " + rng);
        return rng;
    }

    void ClearUsedIndexes()
    {
        usedIndexes.Clear();
    }*/

}
