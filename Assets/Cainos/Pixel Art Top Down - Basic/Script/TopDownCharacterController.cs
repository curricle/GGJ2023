using UnityEngine;
using UnityEngine.InputSystem;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;
        private Vector2 movement;

        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }

        private void OnMove(InputValue value) {
            movement = value.Get<Vector2>();
        }
    }
}
