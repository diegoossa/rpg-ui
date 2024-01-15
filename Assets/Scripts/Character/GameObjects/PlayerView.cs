using Unity.Mathematics;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public static PlayerView Instance;
    
    private Animator _animator;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }
    
    public void SetPositionRotation(float3 position, quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    public void UpdateMovement(bool isMoving)
    {
        _animator.SetBool(IsMoving, isMoving);
    }
    
    public void UpdateGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGrounded, isGrounded);
    }
}
