using UnityEngine;

namespace Source.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string WalkClipName = "Walk";
        private const string IdleClipName = "Idle";
        
        private readonly int _walkClipHash = Animator.StringToHash(WalkClipName);
        private readonly int _idleClipHash = Animator.StringToHash(IdleClipName);
        
        [SerializeField] private Animator _animator;
        [SerializeField] private InputService _inputService;


        private void OnEnable()
        {
            _inputService.PressedMoveKey += PlayWalkClip;
            _inputService.StoppedMove += PlayIdleClip;
        }
        
        private void OnDisable()
        {
            _inputService.PressedMoveKey -= PlayWalkClip;
            _inputService.StoppedMove -= PlayIdleClip;
        }

        private void PlayWalkClip()
        {
            _animator.Play(_walkClipHash);
        }

        private void PlayIdleClip()
        {
            _animator.Play(_idleClipHash);
        }
    }
}