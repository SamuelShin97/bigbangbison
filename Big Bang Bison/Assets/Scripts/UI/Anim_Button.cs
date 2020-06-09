using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Anim_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { DOTween.Init(); }

    public void select() { transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutSine); }
    public void deslect() { transform.DOScale(0.8f, 0.2f).SetEase(Ease.InSine); }

    public void zoom_in() { transform.DOScale(new Vector3(1f, 1f, 1f), .3f); }

    public void FadeIn()
    {
        DOTween.Sequence()
        .Append(transform.GetComponent<Image>().DOFade(1f, 0.05f))
        .Join(transform.DOScale(1f, .1f));
    }
    public void FadeOut()
    {
        DOTween.Sequence()
        .Append(transform.GetComponent<Image>().DOFade(0f, 0.05f))
        .Join(transform.DOScale(.75f, .1f));
    }
}
