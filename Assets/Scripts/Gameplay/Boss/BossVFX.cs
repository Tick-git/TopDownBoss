using System.Collections;
using UnityEngine;

public class BossVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leftHandFire;
    [SerializeField] private ParticleSystem _rightHandFire;
    [SerializeField] private ParticleSystem _leftExplosion;
    [SerializeField] private ParticleSystem _rightExplosion;
    [SerializeField] private ParticleSystem _leftDirt;
    [SerializeField] private ParticleSystem _rightDirt;
    [SerializeField] private CameraShake _cameraShake;

    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    private Coroutine _handFollowCoroutine;

    public void PlayHandFireVFX()
    {
        _leftHandFire.Play();
        _rightHandFire.Play();

        if (_handFollowCoroutine != null)
        {
            StopCoroutine(_handFollowCoroutine);
        }

        _handFollowCoroutine = StartCoroutine(FollowHands());
    }

    private IEnumerator FollowHands()
    {
        while (true)
        {
            _leftHandFire.transform.position = _leftHand.position;
            _rightHandFire.transform.position = _rightHand.position;
            yield return null;
        }
    }

    public void StopHandFireVFX()
    {
        _leftHandFire.Stop();
        _rightHandFire.Stop();

        _leftHandFire.Clear(true);
        _rightHandFire.Clear(true);

        if (_handFollowCoroutine != null)
        {
            StopCoroutine(_handFollowCoroutine);
        }
    }

    public void PlayExplosion()
    {
        _leftExplosion.transform.position = _leftHand.position;
        _rightExplosion.transform.position = _rightHand.position;

        _leftDirt.transform.position = _leftHand.position + Vector3.down * 0.4f;
        _rightDirt.transform.position = _rightHand.position + Vector3.down * 0.4f;

        _leftDirt.Play();
        _rightDirt.Play();

        _leftExplosion.Play();
        _rightExplosion.Play();

        _cameraShake.Shake(4, 0.1f, 0.3f);
    }
}