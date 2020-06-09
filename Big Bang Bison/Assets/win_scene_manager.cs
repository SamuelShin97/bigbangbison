using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class win_scene_manager : MonoBehaviour
{
    public GameObject camera;
    public Vector3[] cameraPoints = new Vector3[0];
    public ParticleSystem[] VFX = new ParticleSystem[0];
    public GameObject[] panels = new GameObject[0];
    public EventSystem es;
    public GameObject end;
    //private DOTween.Sequence()

    void Start()
    {
        DOTween.Sequence()
        .AppendInterval(15f)
        .Append(panels[0].GetComponent<Transform>().DOScale(new Vector3(1f, 1f, 1f), 0.5f))
        .OnStepComplete(start_VFX)
        .AppendInterval(1.5f)
        .Append(panels[1].GetComponent<Transform>().DOScale(new Vector3(1f, 1f, 1f), 0.5f))
        .OnStepComplete(enable_button);
        move_camera();
    }

    // Update is called once per frame

    void start_VFX()
    {
        VFX[0].Play();
        VFX[1].Play();
        //x.Play();
    }

    void enable_button()
    {
        es.SetSelectedGameObject(end);
    }

    void move_camera()
    {
        DOTween.Sequence()
        .Append(camera.GetComponent<Transform>().DOMove(cameraPoints[0], 4f).SetEase(Ease.OutCubic))
        .Append(camera.GetComponent<Transform>().DOMove(cameraPoints[1], 5f).SetEase(Ease.InCubic))
        .Join(camera.GetComponent<Transform>().DORotate(new Vector3(-20f, 274f, 0f), 5f).SetEase(Ease.OutCubic));
        //.Append(camera.GetComponent<Transform>().DOMove(cameraPoints[4], 1f).SetEase(Ease.InQuad));
    }
}
