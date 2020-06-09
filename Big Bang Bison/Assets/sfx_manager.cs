using UnityEngine;
using UnityEngine.EventSystems;

public class sfx_manager : MonoBehaviour
{
    private float arrowTimer;

    //UI SFX
    [FMODUnity.EventRef]
    public string startA;

    [FMODUnity.EventRef]
    public string arrow;

    [FMODUnity.EventRef]
    public string confirm;

    [FMODUnity.EventRef]
    public string backB;

    FMOD.Studio.EventInstance startAInst;
    FMOD.Studio.EventInstance arrowInst;
    FMOD.Studio.EventInstance confirmInst;
    FMOD.Studio.EventInstance backBInst;

    // Start is called before the first frame update
    void Start()
    {
        //UI Sound
        startAInst = FMODUnity.RuntimeManager.CreateInstance(startA);
        arrowInst = FMODUnity.RuntimeManager.CreateInstance(arrow);
        confirmInst = FMODUnity.RuntimeManager.CreateInstance(confirm);
        backBInst = FMODUnity.RuntimeManager.CreateInstance(backB);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A1")) { startAInst.start(); }
        if (Input.GetButtonDown("B1")) { backBInst.start(); }
        if (((Input.GetAxis("Vertical1") >= 0.2 && EventSystem.current.currentSelectedGameObject.name != "Button Play")
            || (Input.GetAxis("Vertical1") <= -0.2 && EventSystem.current.currentSelectedGameObject.name != "Button Quit")) && Time.time > arrowTimer + 0.2f)
        {
            arrowTimer = Time.time;
            arrowInst.start();
        }
        if ((Input.GetAxis("Horizontal1") >= 0.2 || Input.GetAxis("Horizontal1") <= -0.2) && Time.time > arrowTimer + 0.2f )
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            arrowTimer = Time.time;
            arrowInst.start();
        }
    }
}
