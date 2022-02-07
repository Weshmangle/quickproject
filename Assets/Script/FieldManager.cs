using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private TerrainLayer _fieldSynth;
    [SerializeField] private float _offsetSpeed;

    [SerializeField] private float _maxOffsetSpeed = 40;
    [SerializeField] private float _offsetSpeedIncreaseValue = 5;

    private float _actualOffsetSpeed;

    private void Start()
    {
        _actualOffsetSpeed = _offsetSpeed;

        GameManager.Instance.OnGameSpeedChanged += IncreaseOffsetSpeed;
        GameManager.Instance.OnGameSpeedReset += ResetOffsetSpeed;
    }

    private void IncreaseOffsetSpeed()
    {
        if (_actualOffsetSpeed >= _maxOffsetSpeed) return;
        _actualOffsetSpeed += _offsetSpeedIncreaseValue;
    }

    private void ResetOffsetSpeed()
    {
        _actualOffsetSpeed = _offsetSpeed;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            float offset = _actualOffsetSpeed * Time.deltaTime;
            _fieldSynth.tileOffset = new Vector2(_fieldSynth.tileOffset.x + offset, 0);
        }
    }
    public void creatStarsInSky()
    {
        //GameObject stars = Instantiate()
    }
}
