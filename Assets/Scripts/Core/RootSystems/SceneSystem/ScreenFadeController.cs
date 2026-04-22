using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenFadeController : MonoBehaviour
{
    [SerializeField] float fadeDuration;
    [SerializeField] Color fadeScreenColor;
    [SerializeField] AnimationCurve easeCurve;
    
    private VisualElement screen;

    private const float AlphaMin = 0;
    private const float AlphaMax = 1;
    
    private void OnEnable()
    {
        screen = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Screen");
        
        screen.style.backgroundColor = fadeScreenColor;
        SetBackgroundsAlpha(screen, AlphaMin);
    }

    public IEnumerator FadeInRoutine()
    {
        yield return FadeInRoutine(fadeDuration);
    }
    public IEnumerator FadeOutRoutine()
    {
        yield return FadeOutRoutine(fadeDuration);
    }
    
    public IEnumerator FadeInRoutine(float duration)
    {
        yield return FadeAlpha(screen, AlphaMin, AlphaMax,  duration);
    }
    
    public IEnumerator FadeOutRoutine(float duration)
    {
        yield return FadeAlpha(screen, AlphaMax, AlphaMin,  duration);
    }

    public void CutIn()
    {
        SetBackgroundsAlpha(screen, AlphaMax);
    }
    
    public void CutOut()
    {
        SetBackgroundsAlpha(screen, AlphaMin);
    }
    
    private IEnumerator FadeAlpha(VisualElement element, float from, float to, float duration)
    {
        SetBackgroundsAlpha(element, from);
        
        float t = 0f;
        
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            
            float normalized = Mathf.Clamp01(t / duration);
            float eased = easeCurve.Evaluate(normalized);

            SetBackgroundsAlpha(element,Mathf.Lerp(from, to, eased));

            yield return null;
        }

        SetBackgroundsAlpha(element, to);
    }

    private void SetBackgroundsAlpha(VisualElement element, float alpha)
    {
        fadeScreenColor.a = alpha;
        element.style.backgroundColor = fadeScreenColor;
    }
}