using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParameterSetByName : MonoBehaviour
{
    Scene m_scene;
    static FMOD.Studio.EventInstance Song;
    static FMOD.Studio.EventInstance Crystal;
    static FMOD.Studio.EventInstance Desert;
    static FMOD.Studio.EventInstance Tropical;
    static FMOD.Studio.EventInstance End;
    public string SongSelected;
    float timeNow = 0;
    float startTime;
    float timeRemainingD;
    float timeRemainingC;
    float timeRemainingT;
    bool playedC;
    bool playedD;
    bool playedT;
    static bool hold;


    float hillTime;
    Transform hillNow;
    public float speed = 1;
    bool isPlaying = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_scene = SceneManager.GetActiveScene();
        if (m_scene.name == "1Title" && hold == false){
            Crystal.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Desert.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Tropical.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            End.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Song.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        
            Song = FMODUnity.RuntimeManager.CreateInstance(SongSelected);
            Song.start();
        }
        Desert = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Desert");
        Crystal = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Crystal");
        Tropical = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Tropical");
        End = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Win");
        if (m_scene.buildIndex == 1)
        {
            hold = true;
        }
        if (m_scene.buildIndex == 2)
        {
            hold = false;
        }
        playedC = false;
        playedD = false;
        playedT = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time Remaining: " + timeRemaining);
        m_scene = SceneManager.GetActiveScene();
        //Debug.Log("scene: " + hillNow.gameObject.name);
        //Debug.Log("scene index: " + m_scene.buildIndex);
        if (m_scene.buildIndex == 2)
        {
            Song.setParameterByName("Loop", 1f);
        }
        if (m_scene.buildIndex == 2)
        {
            if (hillNow.gameObject.name == "Crystal Hill")
            {
                if (IsPlaying(Tropical) == false && IsPlaying(Desert) == false && IsPlaying(Crystal) == false && playedC == false)
                {
                    Crystal.start();
                    playedC = true;
                }
                //Desert.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                //Tropical.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Crystal.setParameterByName("timeRemainingC", timeRemainingC);
                

            }
            else if (hillNow.gameObject.name == "Desert Hill" )
            {
                if (IsPlaying(Tropical) == false && IsPlaying(Desert) == false && IsPlaying(Crystal) == false && playedD == false)
                {
                    Desert.start();
                    

                    playedD = true;
                }
                //Crystal.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                //Tropical.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Desert.setParameterByName("timeRemainingD", timeRemainingD);
                
            }
            else if (hillNow.gameObject.name == "Tropical Hill")
            {
                if (IsPlaying(Tropical) == false && IsPlaying(Desert) == false && IsPlaying(Crystal) == false && playedT == false)
                {
                    Tropical.start();
                    playedT = true;
                }
                //Crystal.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                //Desert.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Tropical.setParameterByName("timeRemainingT", timeRemainingT);
                
            }
        }
        if (m_scene.buildIndex == 3)
        {
            End.start();
        }

    }
    public void TimeGet(float timeCurrent, Transform hill)
    {
        if (m_scene.buildIndex == 2)
        {
            if (timeNow == 0)
            {
                startTime = timeCurrent;
                hillTime = (timeCurrent) / 3;
                timeRemainingC = hillTime;
                timeRemainingD = hillTime;
                timeRemainingT = hillTime;
            }
            timeNow = timeCurrent;
            hillNow = hill;
            //Debug.Log(Time.deltaTime+ "Time");
            //Debug.Log(speed+"Speed");
            //Debug.Log("Time Remaining: " + timeRemaining);
            //Debug.Log("Time Current: " + timeCurrent);
            if (hillNow.gameObject.name == "Crystal Hill" && timeRemainingC > 0)
            {
                timeRemainingC -= Time.deltaTime * speed;
                //Debug.Log("hit ");
            }
            else if (hillNow.gameObject.name == "Desert Hill" && timeRemainingD > 0)
            {
                timeRemainingD -= Time.deltaTime * speed;
                //Debug.Log("hit ");
            }
            else if (hillNow.gameObject.name == "Tropical Hill" && timeRemainingT > 0)
            {
                timeRemainingT -= Time.deltaTime * speed;
                //Debug.Log("hit");
            }
            //Debug.Log("Tro " + timeRemainingT);
            //Debug.Log("Cris " + timeRemainingC);
            //Debug.Log("Des " + timeRemainingD);
            //Debug.Log(hillNow.gameObject.name);


        }
    }
    //fmod check if song is playing
    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
