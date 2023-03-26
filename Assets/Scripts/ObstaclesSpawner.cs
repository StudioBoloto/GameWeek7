using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] GameObject _prefabToSpawn;
    [SerializeField] int _maxNumObjectsToSpawn;
    [SerializeField] float _heightAboveGroundToSpawn;

    [SerializeField] Material[] _materials;
    [SerializeField] Texture[] _textures;
    [SerializeField] float _initialSpeed;

    private GameObject _ground;
    private Vector3 _groundSize;
    private Vector3 _groundPosition;
    private Vector3 _spawnPosition;
    private float _groundHeight;
    private float _lastSpawnPositionZ;
    private float _spawnX;
    private float _spawnY;
    private float _spawnZ;
    private int _numObjectsToSpawn;

    private void Start()
    {
        _ground = GameObject.FindGameObjectWithTag("Ground");
        _groundSize = _ground.GetComponent<Renderer>().bounds.size;
        _groundPosition = _ground.transform.position;
        _groundHeight = _groundPosition.y;
        _lastSpawnPositionZ = _player.position.z;
    }

    private void GenRandomPrefab()
    {
        int materialIndex = Random.Range(0, _materials.Length);
        int textureIndex = Random.Range(0, _textures.Length);
        Material newMaterial = new Material(_materials[materialIndex]);
        newMaterial.mainTexture = _textures[textureIndex];
        GameObject newObstacle = Instantiate(_prefabToSpawn, _spawnPosition, Quaternion.identity);
        newObstacle.GetComponent<Renderer>().material = newMaterial;
        Rigidbody rb = newObstacle.GetComponent<Rigidbody>();
        Vector3 direction = (_player.position - newObstacle.transform.position).normalized;
        rb.AddForce(direction * _initialSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        float distanceTraveled = _player.position.z - _lastSpawnPositionZ;
        if (distanceTraveled >= 0.1f)
        {
            _numObjectsToSpawn = Random.Range(3, _maxNumObjectsToSpawn);
            for (int i = 0; i < _numObjectsToSpawn; i++)
            {
                _spawnX = Random.Range(_groundPosition.x - _groundSize.x / 2, _groundPosition.x + _groundSize.x / 2);
                _spawnY = _groundHeight + _heightAboveGroundToSpawn;
                _spawnZ = _player.position.z + Random.Range(5f, 15f);
                _spawnPosition = new Vector3(_spawnX, _spawnY, _spawnZ);
                GenRandomPrefab();
            }
            _lastSpawnPositionZ = _spawnZ;
        }
    }
}
