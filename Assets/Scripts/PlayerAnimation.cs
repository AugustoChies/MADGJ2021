using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IdleAnimationName = "PlayerIdle";
    private const string RunAnimationName = "PlayerRun";
    private const string JumpAnimationName = "PlayerJump";
    private const string CrouchAnimationName = "PlayerCrouch";
    private const string GettingUpAnimationName = "GettingUp";

    [SerializeField] private Vector2 _normalOffset;
    [SerializeField] private Vector2 _normalSize;
    [SerializeField] private Vector2 _crouchOffset;
    [SerializeField] private Vector2 _crouchSize;

    [SerializeField] private static PlayerState _playerState;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _collider;

    private bool isCrouchingHitboxActive = false;

    public static PlayerState CurrentPlayerState => _playerState;

    private void Update()
    {
        if(GameController.instance._gameState == GameState.Gameplay)
        {
            UpdateAnimations();
            UpdateHitBox();
        }
    }

    public void UpdateAnimations()
    {
        switch (_playerState)
        {
            case PlayerState.Idle:
                if(_playerMovement.Movement != 0)
                {
                    _animator.Play(RunAnimationName);
                    _playerState = PlayerState.Running;
                }
                if (!_playerMovement.GroundCheck())
                {
                    _animator.Play(JumpAnimationName);
                    _playerState = PlayerState.Jumping;
                }
                if (_playerMovement.IsCrouching)
                {
                    _animator.Play(CrouchAnimationName);
                    _playerState = PlayerState.Crouching;
                }
                break;

            case PlayerState.Running:
                if (_playerMovement.Movement == 0 && _playerMovement.GroundCheck())
                {
                    _animator.Play(IdleAnimationName);
                    _playerState = PlayerState.Idle;
                }
                if (!_playerMovement.GroundCheck())
                {
                    _animator.Play(JumpAnimationName);
                    _playerState = PlayerState.Jumping;
                }
                if (_playerMovement.IsCrouching)
                {
                    _animator.Play(CrouchAnimationName);
                    _playerState = PlayerState.Crouching;
                }
                break;

            case PlayerState.Jumping:
                if(_playerMovement.GroundCheck() && _playerMovement.Movement == 0)
                {
                    _animator.Play(IdleAnimationName);
                    _playerState = PlayerState.Idle;
                }
                if (_playerMovement.GroundCheck() && _playerMovement.Movement != 0)
                {
                    _animator.Play(RunAnimationName);
                    _playerState = PlayerState.Running;
                }
                break;

            case PlayerState.Crouching:
                if (!_playerMovement.IsCrouching)
                {
                    _animator.Play(GettingUpAnimationName);
                    _playerState = PlayerState.GettingUp;
                }
                break;

            case PlayerState.GettingUp:
                if(!AnimatorIsPlaying())
                {
                    if (_playerMovement.Movement == 0)
                    {
                        _animator.Play(IdleAnimationName);
                        _playerState = PlayerState.Idle;
                    }
                    if (_playerMovement.Movement != 0)
                    {
                        _animator.Play(RunAnimationName);
                        _playerState = PlayerState.Running;
                    }
                }
                break;
        }
    }

    public void UpdateHitBox()
    {
        if(_playerState == PlayerState.Crouching && !isCrouchingHitboxActive)
        {
            isCrouchingHitboxActive = true;
            _collider.size = _crouchSize;
            _collider.offset = _crouchOffset;
        }
        else if ((_playerState != PlayerState.Crouching && _playerState != PlayerState.GettingUp) && isCrouchingHitboxActive)
        {
            isCrouchingHitboxActive = false;
            _collider.size = _normalSize;
            _collider.offset = _normalOffset;
        }
    }

    public bool AnimatorIsPlaying()
    {
        print(_animator.GetCurrentAnimatorStateInfo(0).length > _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        return _animator.GetCurrentAnimatorStateInfo(0).length >
               _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
