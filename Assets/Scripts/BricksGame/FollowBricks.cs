using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowBricks : MonoBehaviour
{
    public List<GameObject> brickList = new();
    private List<float> brickHeightList = new();
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform maxHeight;
    [SerializeField] private Transform minHeight;

    void Start()
    {
        
    }

    void Update()
    {
        brickHeightList = GetHeight(brickList);
        Move(cameraSpeed, GetTargetHeight(brickHeightList));
    }
    private float GetTargetHeight(List<float> _brickHeightList)
    {
        _brickHeightList.Sort();
        if (_brickHeightList.Count > 0)
            return _brickHeightList[^1];
        else
            return transform.position.y;
    }
    private void Move(float _speed, float _targetHeight)
    {
        if(_targetHeight  > maxHeight.position.y || _targetHeight < minHeight.position.y)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _targetHeight, transform.position.z  ), _speed*Time.deltaTime);
    }
    private List<float> GetHeight(List<GameObject> _brickList)
    {
        List<float> _heightList = new();
        foreach (GameObject _brick in brickList)
        {
            if (_brick != null)
                _heightList.Add(_brick.transform.position.y);
        }
        return(_heightList);
    }
}
