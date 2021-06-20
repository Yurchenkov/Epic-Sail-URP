using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 1f;

    private GameManager _gameManager;
    private Pointer _pointer;
    private bool _isToched = false;
    private Rigidbody _rigidbody;
    private Vector3 _startTrailPosition; //Для проверки изменения позиции Pointer'a

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag(GameManager.TAG_GAME_MANAGER).GetComponent<GameManager>();
        _pointer = GameObject.FindGameObjectWithTag(GameManager.TAG_POINTER).GetComponent<Pointer>();
        _rigidbody = GetComponent<Rigidbody>();
        

        //Позволит использовать любой меш и коллайдер из дочернего объекта для препядствия
        GetComponent<MeshFilter>().mesh = GetComponentInChildren<MeshFilter>().mesh;
        GetComponent<MeshCollider>().sharedMesh = GetComponentInChildren<MeshFilter>().mesh;

    }

    private void Update()
    {
        Vector3 target = GetMotionTarget();
        if (_isToched && target != _startTrailPosition)
        {
            _rigidbody.AddForce(GetForceVector(target), ForceMode.Impulse);
            _rigidbody.AddTorque(GetDirection(target) * Random.Range(1, 4));
            _isToched = false;
        }
    }

    private Vector3 GetForceVector(Vector3 target)
    {
        Vector3 forceVector = GetDirection(target).normalized;
        forceVector.y = 1f;
        return forceVector * GetDistance(target);
    }
    private Vector3 GetMotionTarget()
    {
        Vector3 target = _pointer.lastTrailPointerPosition;
        return target;
    }
    
    private float GetDistance(Vector3 target)
    {
        return GetDirection(target).magnitude;
    }

    private Vector3 GetDirection(Vector3 target)
    {
        return target - transform.position;
    }
     
    private void OnTriggerEnter(Collider other)
    {
        _startTrailPosition = GetMotionTarget();
        _isToched = true;
       
    }

    //Сравнение по тэгу - не очень хорошая практика. Возможно стоит вынести коллайдеры на отдельный уровень
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _gameManager.Restart();
        }
    }
}
