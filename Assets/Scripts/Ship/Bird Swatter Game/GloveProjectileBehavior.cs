using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GloveProjectileBehavior : MonoBehaviour
{
    private BirdSwattingMinigameBehavior parent;
    private float speed = 5f;
    private SpriteRenderer birdSpriteRenderer;
    private bool hitBird = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        birdSpriteRenderer = parent.Bird.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void SetMinigameParent(BirdSwattingMinigameBehavior parent)
    {
        this.parent = parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bird" || hitBird) { return; }
        hitBird = true;
        // make it go silly
        FindObjectOfType<AudioManager>().Play("Sad Bird Call");
        birdSpriteRenderer.sprite = parent.SadBirdSprite;
        var rigidbody = GetComponent<Rigidbody>();
        var torque = Random.Range(-4, 4);
        rigidbody.AddTorque(new Vector3(0, 0, torque), ForceMode.Impulse);
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;

        parent.EndTheHarpoons();
    }
}
