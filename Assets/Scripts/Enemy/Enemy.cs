using System.Collections;
using Cobalt;
using UnityEngine;

public class Enemy : MonoBehaviour {
  private Transform player; 
  [SerializeField] private float detectionRange = 10f;
  [SerializeField] public float movementSpeed = 5f;
  [SerializeField] private float jumpForce = 50f;
  [SerializeField] private float jumpCooldown = 2f;
  [SerializeField] private float jumpFreezeTime = 1f;
  [SerializeField] public float minJumpDistance = 2f;

  [SerializeField] private float cobaltDropChance;
  [SerializeField] private float cobaltDropAmount;
  [SerializeField] private GameObject chunkPrefab;

  private bool canJump = true;

  private Rigidbody2D rb;

  public bool isRunning {
    get; private set;
  }
  public bool isJumping {
    get; private set;
  }

  public Vector2 MovementDirection {
    get {
      if(!player) return Vector2.zero;
      else return (player.position - transform.position).normalized;
    }
  }

  private void Start() {
    rb = GetComponent<Rigidbody2D>();

    isJumping = false;

    if (player == null) {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }
  }

  private void FixedUpdate() {
    isRunning = false;

    if (!player) return;
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

    if (distanceToPlayer <= detectionRange && !isJumping) {

      isRunning = true;
      Move(player);

      if (distanceToPlayer <= minJumpDistance && canJump) {

        Jump();

        StartCoroutine(JumpCooldown());
      }
    }
  }

  private void Move(Transform target) {
    Vector2 movementDirection = (target.position - transform.position).normalized;

    rb.velocity = movementDirection* movementSpeed;
  }

  private void Jump() {
    Vector3 jumpDirection = (player.position - transform.position).normalized;
    rb.AddForce(jumpDirection * jumpForce);
  }

  private IEnumerator JumpCooldown() {
    canJump = false;
    isJumping = true;
    yield return new WaitForSeconds(jumpFreezeTime);
    isJumping = false;
    yield return new WaitForSeconds(jumpCooldown);
    canJump = true;
  }

  public void OnDeath() {
    if (Random.value > cobaltDropChance) return;

    var chunkGO = Instantiate(chunkPrefab, transform.position, Quaternion.identity);
    if(!chunkGO.TryGetComponent(out Chunk chunk)) return;
    
    chunk.SetRichness(Mathf.FloorToInt(Random.Range(cobaltDropAmount * 0.8f, cobaltDropAmount * 1.2f)));
  }
}
