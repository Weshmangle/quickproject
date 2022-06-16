using UnityEngine;

public class AnimateModel : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshes;
    [SerializeField] private float _delayForNextMesh;

    private float _remainingTime;
    private int _index = 0;

    private void Start()
    {
        _remainingTime = _delayForNextMesh;
    }

    private void Update()
    {
        _remainingTime -= Time.deltaTime;

        if(_remainingTime <= 0)
        {
            _remainingTime = _delayForNextMesh;
            _index++;

            _index %= _meshes.Length;

            foreach (GameObject mesh in _meshes)
            {
                mesh.SetActive(false);
            }

            _meshes[_index].SetActive(true);
        }
    }
}
