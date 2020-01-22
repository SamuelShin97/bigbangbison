using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleSystem : MonoBehaviour
{
    private ParticleSystem ps;
    ParticleSystem.MainModule main;
    public bool isBlue;
    public bool active;
    public bool medium;
    public int mediumEmissionRate;
    public int largeEmissionRate;
    public GameObject blueBison;
    public GameObject redBison;
    private Color blue;
    private Color red;
    

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
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
                main.startColor = blue;
            }
            else
            {
                main.startColor = red;
            }

            var emission = ps.emission;
            if (medium)
            {
                
                emission.rateOverTime = mediumEmissionRate;
            }
            else
            {
                emission.rateOverTime = largeEmissionRate;
            }

            ps.Play();
            active = false;
        }
    }
}
