using Unity.CharacterController;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Character.GameObjects
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct UpdatePlayerView : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            if(PlayerView.Instance == null)
                return;
            
            foreach (var (characterControl, localToWorld) in SystemAPI.Query<RefRO<KinematicCharacterBody>, RefRO<LocalToWorld>>())
            {
                PlayerView.Instance.UpdateMovement(math.lengthsq(characterControl.ValueRO.RelativeVelocity) > 0.1f);
                PlayerView.Instance.UpdateGrounded(characterControl.ValueRO.IsGrounded);
                PlayerView.Instance.SetPositionRotation(localToWorld.ValueRO.Position, localToWorld.ValueRO.Rotation);
            }

        }
    }
}