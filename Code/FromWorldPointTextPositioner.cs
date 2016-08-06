using UnityEngine;

public class FromWorldPointTextPositioner:IFloatingTextPositioner
 {
    private readonly Camera _camera;
    private readonly Vector3 _worldPosition;
    private readonly float _speed;
    private float _timeTolive;
    private float _yOffset;


    public FromWorldPointTextPositioner(Camera camera, Vector3 worldPosition , float timetoLive , float speed)
    {
        _camera = camera;
        _worldPosition = worldPosition;
        _timeTolive = timetoLive;
        _speed = speed;

    }
    public bool GetPosition(ref Vector2 position, GUIContent content, Vector2 size)
    {
        if ((_timeTolive -=Time.deltaTime) <= 0)
             return false;
        var screenPosition = _camera.WorldToScreenPoint(_worldPosition);
        position.x = screenPosition.x - (size.x / 2);
        position.y = Screen.height - screenPosition.y - _yOffset;

        _yOffset += Time.deltaTime * _speed;
        return true;

    }
 }
