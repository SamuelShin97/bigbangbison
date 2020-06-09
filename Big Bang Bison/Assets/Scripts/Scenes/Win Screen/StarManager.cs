

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject star;
    public float score = 5;
    public float countUp = 0;
    public int team = 0;
    private Transform[] stars;

    // Start is called before the first frame update
    void Start()
    {
        // Get and set my score
        if (GameObject.Find("GameManager")) score = GameObject.Find("GameManager").GetComponent<GameManager>().scores[team] / 3.0f;
        else score = 25;

        // Get the transforms of all the stars
        // NOTE: stars[0] is my own transform
        stars = GetComponentsInChildren<Transform>();

        for (int i = 1; i < stars.Length; i++)
        {
            stars[i].Rotate(Random.rotation.eulerAngles);
            stars[i].localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Count up until score
        if (countUp < score + 1 && countUp < stars.Length + 1)
        {
            countUp += Time.deltaTime;

            if (countUp > score + 1) countUp = score;

            if (countUp >= 1)
            {
                // Get the current star and make it grow, and set the previous star to full, should work above 1fps
                if (countUp == score)
                {
                    stars[(int)Mathf.Floor(countUp)].localScale = new Vector3(countUp % 1, countUp % 1, countUp % 1);
                } else
                {
                    stars[(int)Mathf.Floor(countUp)].localScale = new Vector3(countUp % 1, countUp % 1, countUp % 1);
                    stars[(int)Mathf.Floor(countUp) - 1].localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
