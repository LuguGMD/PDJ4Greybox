using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("X", _player.input.x);
        _animator.SetFloat("Z", _player.input.z);

        if (_player.force.y > 0f && !_animator.GetBool("Jumping"))
        {
            _animator.SetBool("Jumping", true);
        }
    }
}
