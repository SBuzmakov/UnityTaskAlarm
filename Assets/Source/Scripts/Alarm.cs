using System.Collections;
using UnityEngine;

namespace Source.Scripts
{
    public class Alarm : MonoBehaviour
    {
        private const float VolumeSoundStep = 0.1f;

        [SerializeField] private AudioSource _alarmSound;
        [SerializeField] private float _soundStepTime;

        private Coroutine _coroutine;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new WaitForSeconds(_soundStepTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = StartCoroutine(IncreaseVolumeJob());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = StartCoroutine(DecreaseVolumeJob());
            }
        }

        private IEnumerator IncreaseVolumeJob()
        {
            for (float i = 0; i < 1; i += VolumeSoundStep)
            {
                _alarmSound.volume += VolumeSoundStep;

                yield return _wait;
            }
        }

        private IEnumerator DecreaseVolumeJob()
        {
            for (float i = _alarmSound.volume; i >= 0; i -= VolumeSoundStep)
            {
                _alarmSound.volume -= VolumeSoundStep;

                yield return _wait;
            }
        }
    }
}