using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _swingSpeed = 1f;
    [SerializeField] float _swingAmplitude = 5f;
    [SerializeField] float _liftSpeed = 1f;
    [SerializeField] float _liftHeight = 1f;
    [SerializeField] TMP_Text _wastedText;
    [SerializeField] Image _screenOverlay;
    [SerializeField] float _opacityMaxValue = 0.8f;
   

    private float _timeOffset;

    void Start()
    {
        _timeOffset = Random.Range(0f, 2f * Mathf.PI);
        StartCoroutine(ShowWastedText());
    }

    IEnumerator ShowWastedText()
    {
        yield return new WaitForSeconds(2f);
        _wastedText.gameObject.SetActive(true);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, _opacityMaxValue, t);
            Color overlayColor = _screenOverlay.color;
            overlayColor.a = alpha;
            _screenOverlay.color = overlayColor;

            yield return null;
        }
    }

    void LateUpdate()
    {
        float swing = Mathf.Sin((Time.time + _timeOffset) * _swingSpeed) * _swingAmplitude;
        Vector3 targetPosition = _target.position + new Vector3(swing, 0f, 0f);

        float lift = Mathf.Clamp01((Time.time + _timeOffset) * _liftSpeed);
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition + new Vector3(0f, _liftHeight, 0f), lift);

        transform.position = newPosition;
    }
}
