/*
    BisonSpawn.cs
    Caetano 
    4/5/20
    Caetano
    Class for bison spawner
    Functions in file:
        SpawnAll:
            In, count - number of bison to spawn
            Out, spawns "count" bison for each herd at this spawner's location
        SpawnOne:
            In, count - number of bison to spawn, herd - the herd the bison will belong to
            Out, spawns "count" bison for "herd" at this spawner's location
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisonSpawn : MonoBehaviour
{
    private Herd[] herds;
    private OnlineHerd[] onlineHerds;
    private ParticleSystem[] spawnerParticles;

    // Start is called before the first frame update
    void Awake()
    {
        herds = GameObject.FindObjectsOfType<Herd>(); // for some reason this is [Herd 2, Herd 1]
        onlineHerds = GameObject.FindObjectsOfType<OnlineHerd>();
    }

    // Update is called once per frame
    void Start()
    {
        if (herds.Length == 0) herds = GameObject.FindObjectsOfType<Herd>();
        if (onlineHerds.Length == 0) onlineHerds = GameObject.FindObjectsOfType<OnlineHerd>();

        spawnerParticles = GetComponentsInChildren<ParticleSystem>();
        Debug.Log(spawnerParticles);
        Debug.Log(spawnerParticles.Length);

        StartCoroutine("CheckBison");
    }

    public void Spawn(int count, string name, string color)
    {
        //int index = 0;
        
        if (herds != null)
        {

            /*string[] names = new string[count];
            for (int i = 0; i < count; i++)
            {
                names[i] = nameSet[index];
                index++;
            }*/
            //Debug.Log("herds 0" + herds[0]);
            //Debug.Log("herds 1" + herds[1]);
            if (color == "cyan")
            {
                herds[1].SpawnBison(count, transform.position, name);
            }
            else if (color == "pink")
            {
                herds[0].SpawnBison(count, transform.position, name);
            }
            
            
        } /*else if (onlineHerds != null)
        {
            foreach (Herd herd in herds)
            {
                herd.SpawnBison(count, transform.position);
            }
        }*/
        else
        {
            Debug.Log("Trying to spawn before herds are set.");
        }

    }

    IEnumerator CheckBison()
    {

        while (spawnerParticles.Length > 0)
        {
            int[] bisonInSpawn = CountBison();

            // Debug.Log("There are " + bisonInSpawn[0] + " blue bison and "+ bisonInSpawn[1] + " pink bison near this spawner");
            
            // Set each spawner rock to emit the number of bison of their color, particles will emit even on both sides, or have the remainder in front
            foreach (ParticleSystem p in spawnerParticles)
            {
                if (p.gameObject.name.Contains("Blue"))
                {
                    if (p.gameObject.name.Contains("Front"))
                    {
                        Debug.Log("Front, Blue");
                        p.emission.SetBurst(0, new ParticleSystem.Burst(0, (short)Mathf.Ceil(bisonInSpawn[0] / 2.0f), (short)Mathf.Ceil(bisonInSpawn[0] / 2.0f), 0, 4));
                    } else if (p.gameObject.name.Contains("Rear"))
                    {
                        Debug.Log("Rear, Blue");
                        p.emission.SetBurst(0, new ParticleSystem.Burst(0, (short)Mathf.Floor(bisonInSpawn[0] / 2.0f), (short)Mathf.Floor(bisonInSpawn[0] / 2.0f), 0, 4));
                    }
                } else if (p.gameObject.name.Contains("Pink"))
                {
                    if (p.gameObject.name.Contains("Front"))
                    {
                        Debug.Log("Front, Pink");
                        p.emission.SetBurst(0, new ParticleSystem.Burst(0, (short)Mathf.Ceil(bisonInSpawn[1] / 2.0f), (short)Mathf.Ceil(bisonInSpawn[1] / 2.0f), 0, 4));
                    }
                    else if (p.gameObject.name.Contains("Rear"))
                    {
                        Debug.Log("Rear, Pink");
                        p.emission.SetBurst(0, new ParticleSystem.Burst(0, (short)Mathf.Floor(bisonInSpawn[1] / 2.0f), (short)Mathf.Floor(bisonInSpawn[1] / 2.0f), 0, 4));
                    }
                }
            }

            yield return new WaitForSeconds(4);
        }
    }
    

    public void SpawnOne(int count, Herd herd)
    {
        //herd.SpawnBison(count, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (GameObject.FindObjectOfType<Herd>())
        Gizmos.DrawWireSphere(transform.position, GameObject.FindObjectsOfType<Herd>()[0].spawnCircle);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 15); // <- number should match line 143
    }


    // Counts all pink and blue bison within 15 units the spawner, returns [blueCount, pinkCount]
    private int[] CountBison()
    {
        int blueCount = 0, pinkCount = 0;

        foreach(Collider c in Physics.OverlapSphere(transform.position, 15)) // <- number should match line 134
        {
            if (c.GetComponent<HerdAgent>())
            {
                HerdAgent herdAgent = c.GetComponent<HerdAgent>();

                if (herdAgent.AgentHerd == herds[1]) blueCount++;
                else if (herdAgent.AgentHerd == herds[0]) pinkCount++;
            }
        }

        return new int[] { blueCount, pinkCount };
    }
}
