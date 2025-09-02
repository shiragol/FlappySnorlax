using UnityEngine;

public class PokefluteSpawnerScript : MonoBehaviour
{
    public GameObject pokeflute;
    public float spawnRate = 2f;
    private float timer = 0f;
    public float heightOffset = 2.5f; 

    void Start()
    {
        SpawnPokeflute();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPokeflute();
            timer = 0;
        }
    }

    void SpawnPokeflute()
    {
        float camRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;

        float spawnX = camRight + 2f;

        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float spawnY = Random.Range(lowestPoint, highestPoint);

        Instantiate(pokeflute, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
    }
}
