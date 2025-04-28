using System.Collections;
using UnityEngine;

namespace Source.Scripts
{
    public class Alarm : MonoBehaviour
    {
        private const float MinVolume = 0f;
        private const float MaxVolume = 1.0f;

        [SerializeField] private float _volumeSoundStep;
        [SerializeField] private AudioSource _alarmSound;
        [SerializeField] private AlarmTrigger _trigger;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            _trigger.Entered += StartIncreaseVolumeCoroutine;
            _trigger.Exited += StartDecreaseVolumeCoroutine;
        }

        private void OnDisable()
        {
            _trigger.Entered -= StartIncreaseVolumeCoroutine;
            _trigger.Exited -= StartDecreaseVolumeCoroutine;
        }

        private void StartIncreaseVolumeCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeVolume(MaxVolume));
        }

        private void StartDecreaseVolumeCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeVolume(MinVolume));
        }

        private IEnumerator ChangeVolume(float targetVolume)
        {
            if (_alarmSound.volume == 0)
                _alarmSound.Play();

            while (!Mathf.Approximately(_alarmSound.volume, targetVolume))
            {
                _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeSoundStep);

                yield return null;
            }

            if (_alarmSound.volume == 0)
                _alarmSound.Stop();
        }
    }
}