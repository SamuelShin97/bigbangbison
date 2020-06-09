using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmod_Bison : MonoBehaviour
{
    /*private void AnimationSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    } */

    private void begFoot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
