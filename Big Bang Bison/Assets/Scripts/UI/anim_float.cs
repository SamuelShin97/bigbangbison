using UnityEngine;
using DG.Tweening;

public class anim_float : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { this.anim_gentle_float(); }

    void anim_gentle_float()
    {
        DOTween.Sequence()
        .Append(this.GetComponent<Transform>().DOLocalRotate((new Vector3(0, 180, 0)), 60, RotateMode.Fast).SetEase(Ease.Linear))
        .Append(this.GetComponent<Transform>().DOLocalRotate((new Vector3(0, 360, 0)), 60, RotateMode.Fast).SetEase(Ease.Linear))
        .SetLoops(-1, LoopType.Restart);
    }   
}
