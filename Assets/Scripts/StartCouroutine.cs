using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCouroutine : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject[] _spawnedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _spawnedPrefab = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: to make generic all
    public void CallCouroutine(BossHealth _boss, float _psDelay, Vector3 _position, float _offset)
    {
        StartCoroutine(SpawnBolt(_boss, _psDelay, _position, _offset));
    }

    public IEnumerator SpawnBolt(BossHealth _boss, float _psDelay, Vector3 _position, float _offset)
    {
        int numBolts = 3;
        if (_boss.bossSP) { numBolts = 5; }

        int[] nums = new int[] { -1, 1 };
        int rand = nums[Random.Range(0, nums.Length)];

        _spawnedPrefab[0] = Instantiate(_prefab, _position, Quaternion.identity);

        for (int i = 1; i < numBolts; ++i)
        {
            yield return new WaitForSeconds(_psDelay);
            Debug.Log(_spawnedPrefab[i - 1].transform.position.x + (_offset * rand));
            _spawnedPrefab[i] = Instantiate(_prefab, new Vector3(_spawnedPrefab[i - 1].transform.position.x + (_offset * rand), _position.y, _position.z), Quaternion.identity);
        }
    }
}
