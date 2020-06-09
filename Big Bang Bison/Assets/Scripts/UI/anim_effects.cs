using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class anim_effects : MonoBehaviour
{
    
    // Immediately make "invisible"
    void Awake() { transform.localScale = new Vector3(0, 0, 0); }

    public void zoom_in() { transform.DOScale(0.8f, 0.2f); }

    public void ready()
    {
        DOTween.Sequence()
        .Append(transform.DOScale(1.4f, 0.1f))
        .Append(transform.DOScale(1f, 0.2f))
        .Append(transform.DOScale(1f, 0.5f))
        .OnComplete(advance_scene);
    }

    public void disappear() { transform.DOScale(0f, 0f); }

    public void advance_scene() { SceneManager.LoadScene(2); }
}
