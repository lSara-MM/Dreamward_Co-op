using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCouroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallLightningCouroutine(GameObject _prefab, BossHealth _boss, float _psDelay, Vector3 _position, float _offset)
    {
        StartCoroutine(SpawnBolt(_prefab, _boss, _psDelay, _position, _offset));
    }

    public IEnumerator SpawnBolt(GameObject _prefab, BossHealth _boss, float _psDelay, Vector3 _position, float _offset)
    {
        int numBolts = 3;
        if (_boss.bossSP) { numBolts = 5; }

        int[] nums = new int[] { -1, 1 };
        int rand = nums[Random.Range(0, nums.Length)];

        GameObject[] _spawnedPrefab = new GameObject[5];
        _spawnedPrefab[0] = Instantiate(_prefab, _position, Quaternion.identity);

        for (int i = 1; i < numBolts; ++i)
        {
            yield return new WaitForSeconds(_psDelay);
            _spawnedPrefab[i] = Instantiate(_prefab, new Vector3(_spawnedPrefab[i - 1].transform.position.x + (_offset * rand), _position.y, _position.z), Quaternion.identity);
        }

        yield return new WaitForSeconds(_psDelay);
        _boss.GetComponent<Animator>().SetTrigger("Exit");
    }

    public void CallBigBoltsCouroutine(GameObject _prefab, BossHealth _boss, float _psDelay, Vector3 _position, float _offset, int _loops)
    {
        StartCoroutine(SpawnBigBolts(_prefab, _boss, _psDelay, _position, _offset, _loops));
    }

    public IEnumerator SpawnBigBolts(GameObject _prefab, BossHealth _boss, float _psDelay, Vector3 _position, float _offset, int _loops)
    {
        float[] nums = new float[] { 0, _offset };
        

        for (int i = 0; i < _loops; ++i)
        {
            Instantiate(_prefab, new Vector3(_position.x + nums[Random.Range(0, nums.Length)], _position.y, _position.z), Quaternion.identity);
            yield return new WaitForSeconds(_psDelay);
        }

        _boss.GetComponent<Animator>().SetTrigger("Exit");
    }
}
