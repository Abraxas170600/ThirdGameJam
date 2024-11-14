using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DOTweenAnimation
{
    public AnimationType animation;

    private Sequence secuence;
    private GameObject target;


    [SerializeField] public bool appendTween = true;
    [SerializeField] public bool unscaleTime = true;
    [SerializeField] public Ease easeAnimation;

    [SerializeField] public float timeToInsert;

    [SerializeField] public Transform tfEndValue;
    [SerializeField] public Vector3 vEndValue;
    [SerializeField] public float fEndValue;
    [SerializeField] public bool useLocalCoords;
    [SerializeField] public float duration;




    public void Animate(Sequence mSecuence, GameObject mTarget)
    {
        secuence = mSecuence;
        target = mTarget;

        if (appendTween)
            timeToInsert = secuence.Duration();


        switch (animation)
        {
            case AnimationType.MoveNormal:
            case AnimationType.MoveShake:
            case AnimationType.MovePunch:
                Move();
                break;
            case AnimationType.RotateNormal:
            case AnimationType.RotateShake:
            case AnimationType.RotatePunch:
                Rotate();
                break;
            case AnimationType.ScaleNormal:
            case AnimationType.ScaleShake:
            case AnimationType.ScalePunch:
                Scale();
                break;
            case AnimationType.Jump:
                Jump();
                break;
            case AnimationType.Fade:
                Fade();
                break;
        }
    }


    #region basic animations
    public float randomness = 90, elasticity = 1;
    public int vibrato = 10;
    public bool fadeOut;
    public RotateMode mode;

    public void Move()
    {
        switch (animation)
        {
            case AnimationType.MoveNormal:
                if (useLocalCoords)
                    secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DOLocalMove(
                                (tfEndValue == null ? vEndValue : tfEndValue.localPosition),
                                duration).SetUpdate(unscaleTime).SetEase(easeAnimation));
                else
                    secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DOMove(
                                (tfEndValue == null ? vEndValue : tfEndValue.position),
                                duration).SetUpdate(unscaleTime).SetEase(easeAnimation));
                break;
            case AnimationType.MoveShake:
                secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DOShakePosition(duration,
                    (tfEndValue == null ? vEndValue : tfEndValue.position),
                    vibrato, randomness, fadeOut));
                break;
            case AnimationType.MovePunch:
                secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DOPunchPosition(
                    (tfEndValue == null ? vEndValue : tfEndValue.position),
                    duration, vibrato, elasticity));
                break;
        }

    }

    public void Rotate()
    {
        switch (animation)
        {
            case AnimationType.RotateNormal:
                if (useLocalCoords)
                    secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DOLocalRotate(
                    (tfEndValue == null ? vEndValue : tfEndValue.localRotation.eulerAngles),
                    duration, mode).SetUpdate(unscaleTime).SetEase(easeAnimation));
                else
                    secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DORotate(
                    (tfEndValue == null ? vEndValue : tfEndValue.rotation.eulerAngles),
                    duration, mode).SetUpdate(unscaleTime).SetEase(easeAnimation));
                break;
            case AnimationType.RotateShake:
                secuence.Insert(timeToInsert, target.transform.DOShakeRotation(duration,
                    (tfEndValue == null ? vEndValue : tfEndValue.rotation.eulerAngles),
                    vibrato, randomness, fadeOut));
                break;
            case AnimationType.RotatePunch:
                secuence.Insert(timeToInsert, target.transform.DOPunchRotation(
                    (tfEndValue == null ? vEndValue : tfEndValue.rotation.eulerAngles),
                    duration, vibrato, elasticity));
                break;
        }
    }

    public void Scale()
    {
        switch (animation)
        {
            case AnimationType.ScaleNormal:
                secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.transform.DOScale(
                    (tfEndValue == null ? vEndValue : tfEndValue.localScale),
                    duration).SetUpdate(unscaleTime).SetEase(easeAnimation));
                break;
            case AnimationType.ScaleShake:
                secuence.Insert(timeToInsert, target.transform.DOShakeScale(duration,
                (tfEndValue == null ? vEndValue : tfEndValue.localScale),
                vibrato, randomness, fadeOut));
                break;
            case AnimationType.ScalePunch:
                secuence.Insert(timeToInsert, target.transform.DOPunchScale(
                    (tfEndValue == null ? vEndValue : tfEndValue.localScale),
                    duration, vibrato, elasticity));
                break;
        }
    }
    #endregion

    #region others
    [SerializeField] private float jumpPower;
    [SerializeField] private int numJumps;

    public void Jump()
    {
        if (useLocalCoords)
            secuence.Insert(timeToInsert, target.transform.DOJump((tfEndValue == null ? vEndValue : tfEndValue.position), jumpPower, numJumps, duration));
        else
            secuence.Insert(timeToInsert, target.transform.DOLocalJump((tfEndValue == null ? vEndValue : tfEndValue.localPosition), jumpPower, numJumps, duration));
    }

    [SerializeField] private ImageComponentType componentType;
    [SerializeField] private FadeTransitionType transitionType;
    [SerializeField] private bool deactiveWithFadeOut;

    public void Fade()
    {
        try
        {
            switch (componentType)
            {
                case ImageComponentType.CanvasGroup:
                    if (transitionType == FadeTransitionType.linear)
                    {
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.GetComponent<CanvasGroup>().DOFade(fEndValue, duration).SetUpdate(unscaleTime));
                    }
                    else
                    {
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.GetComponent<CanvasGroup>().DOFade(1, duration / 2).SetUpdate(unscaleTime));
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert + (duration / 2), target.GetComponent<CanvasGroup>().DOFade(0, duration / 2).SetUpdate(unscaleTime));
                    }
                    break;
                case ImageComponentType.Image:
                    if (transitionType == FadeTransitionType.linear)
                    {
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.GetComponent<Image>().DOFade(fEndValue, duration).SetUpdate(unscaleTime));
                    }
                    else
                    {
                        float currentValue = target.GetComponent<Image>().color.a;

                        secuence.SetUpdate(unscaleTime).Append(target.GetComponent<Image>().DOFade(fEndValue, duration / 2).SetUpdate(unscaleTime));
                        secuence.SetUpdate(unscaleTime).Append(target.GetComponent<Image>().DOFade(currentValue, duration / 2).SetUpdate(unscaleTime));
                    }
                    break;
                case ImageComponentType.SpriteRenderer:
                    if (transitionType == FadeTransitionType.linear)
                    {
                        secuence.Insert(timeToInsert, target.GetComponent<SpriteRenderer>().DOFade(fEndValue, duration));
                    }
                    else
                    {
                        secuence.Insert(timeToInsert, target.GetComponent<SpriteRenderer>().DOFade(1, duration / 2));
                        secuence.Insert(timeToInsert + (duration / 2), target.GetComponent<SpriteRenderer>().DOFade(0, duration / 2));
                    }
                    break;
                case ImageComponentType.TextMesh:
                    if (transitionType == FadeTransitionType.linear)
                    {
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.GetComponent<TextMeshProUGUI>().DOFade(fEndValue, duration).SetUpdate(unscaleTime));
                    }
                    else
                    {
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert, target.GetComponent<TextMeshProUGUI>().DOFade(1, duration / 2).SetUpdate(unscaleTime));
                        secuence.SetUpdate(unscaleTime).Insert(timeToInsert + (duration / 2), target.GetComponent<TextMeshProUGUI>().DOFade(0, duration / 2).SetUpdate(unscaleTime));
                    }
                    break;
            }

            if (deactiveWithFadeOut && fEndValue == 0)
            {
                secuence.OnComplete(() => {
                    target.SetActive(false);
                });
            }
        }
        catch (System.NullReferenceException e)
        {
            throw new System.Exception("No se encontro el componente" + e);
        }
    }
    #endregion
}
