using UnityEngine;

public class ShotCollision : MonoBehaviour
{
    [SerializeField]
    GameObject _hitEffect;

    public string TagToIgnore = "Bullet";
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 11, true);

        GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("BULETTTTTTTTTTTTTTTTT");
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (collision.gameObject.tag == "TagToIgnore")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.layer != 3)
        {
            GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.1f);
            Destroy(gameObject);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("BULETTTTTTTTTTTTTTTTT");
            collision.gameObject.GetComponent<Collider2D>().enabled = enabled;
        }
    }
}

