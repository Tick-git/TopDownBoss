using System.Collections;
using UnityEngine;

public class GroundAttackVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leftHandFire;
    [SerializeField] private ParticleSystem _rightHandFire;
    [SerializeField] private ParticleSystem _leftExplosion;
    [SerializeField] private ParticleSystem _rightExplosion;
    [SerializeField] private ParticleSystem _leftDirt;
    [SerializeField] private ParticleSystem _rightDirt;

    [SerializeField] private Transform _leftAnchor;
    [SerializeField] private Transform _rightAnchor;

    private Coroutine _handFollowCoroutine;

    public void PlayHandFireVFX()
    {
        _leftHandFire.Play();
        _rightHandFire.Play();

        if (_handFollowCoroutine != null)
        {
            StopCoroutine(_handFollowCoroutine);
        }

        _handFollowCoroutine = StartCoroutine(FollowAnchor(_leftHandFire, _rightHandFire));
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

    private IEnumerator FollowAnchor(ParticleSystem leftSystem, ParticleSystem rightSystem)
    {
        while (true)
        {
            leftSystem.transform.position = _leftAnchor.position;
            rightSystem.transform.position = _rightAnchor.position;
            yield return null;
        }
    }

    public void PlayGroundImpact()
    {
        _leftExplosion.transform.position = _leftAnchor.position;
        _rightExplosion.transform.position = _rightAnchor.position;

        _leftDirt.transform.position = _leftAnchor.position + Vector3.down * 0.4f;
        _rightDirt.transform.position = _rightAnchor.position + Vector3.down * 0.4f;

        _leftDirt.Play();
        _rightDirt.Play();

        _leftExplosion.Play();
        _rightExplosion.Play();
    }
}