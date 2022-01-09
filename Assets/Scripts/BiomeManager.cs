using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public GameObject player;

    public float biomeStartChangeTime;
    public float biomeChangeTime;
    public int biome = -1;
    public ParticleSystem rainParticles;

    [HideInInspector]
    public float timeRemainingTilChange = 0;
    [HideInInspector]
    public float leftOff = 0f;
    public float maxMusicLength = 23f;

    int maxBiomes = 2;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        int seed = System.DateTime.Now.Millisecond;
        Random.InitState(seed);
        biomeChangeTime = biomeStartChangeTime;
    }

    private void Update()
    {
        timeRemainingTilChange -= Time.deltaTime;
        
        if (timeRemainingTilChange <= 0)
        {
            timeRemainingTilChange = biomeChangeTime;

            biome += 1;
            if (biome > maxBiomes)
            {
                biome = 0;
            }

            if (biome == 0)
            {
                audioManager.stopAllAudio();
                Camera.main.backgroundColor = new Color(87f / 255f, 135f / 255f, 212f / 255f, 255f / 255f);
                CharacterController2D characterController2D = player.GetComponent<CharacterController2D>();
                Player playerData = player.GetComponent<Player>();
                characterController2D.queueSlipperyFeet = true;
                playerData.sunburnEnabled = false;
                playerData.waterCollection = false;
                audioManager.PlayAtPosition("music_arctic_tundra_biome", leftOff);
                rainParticles.Stop();
            }

            if (biome == 1)
            {
                audioManager.stopAllAudio();
                Camera.main.backgroundColor = new Color(18f / 255f, 109f / 255f, 255f / 255f, 255f / 255f);
                CharacterController2D characterController2D = player.GetComponent<CharacterController2D>();
                Player playerData = player.GetComponent<Player>();
                characterController2D.queueNormalFeet = true;
                playerData.sunburnEnabled = true;
                playerData.waterCollection = false;
                audioManager.PlayAtPosition("music_beach_biome", leftOff);
                rainParticles.Stop();
            }

            if (biome == 2)
            {
                audioManager.stopAllAudio();
                Camera.main.backgroundColor = new Color(50f / 255f, 168f / 255f, 82f / 255f, 255f / 255f);
                CharacterController2D characterController2D = player.GetComponent<CharacterController2D>();
                Player playerData = player.GetComponent<Player>();
                characterController2D.queueNormalFeet = true;
                playerData.sunburnEnabled = false;
                playerData.waterCollection = true;
                audioManager.PlayAtPosition("music_rainforest_biome", leftOff);
                rainParticles.Play();
            }
        }

        foreach (AudioSource a in audioManager.GetComponents<AudioSource>())
        {
            if (a.isPlaying)
            {
                leftOff = a.time;
            }
        }

    }
}
