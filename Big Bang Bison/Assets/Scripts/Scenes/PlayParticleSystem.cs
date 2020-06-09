using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleSystem : MonoBehaviour
{
    public ParticleSystem psCurrent;
    ParticleSystem.MainModule mainPS;
    public bool isBlue;
    public bool active;
    public bool medium;
    //public int mediumEmissionRate;
    //public int largeEmissionRate;
    public GameObject blueBison;
    public GameObject redBison;
    private Color blue;
    private Color red;
    

    // Start is called before the first frame update
    void Start()
    {
        psCurrent = GetComponent<ParticleSystem>();
        mainPS = psCurrent.main;
        isBlue = true;
        active = false;
        medium = true;
        blue = blueBison.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
        red = redBison.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (isBlue)
            {
                mainPS.startColor = blue;
            }
            else
            {
                mainPS.startColor = red;
            }

            /*var emission = psCurrent.emission;
            if (medium)
            {
                
                emission.rateOverTime = mediumEmissionRate;
            }
            else
            {
                emission.rateOverTime = largeEmissionRate;
            }*/

            psCurrent.Play();
            active = false;
        }
    }
}
