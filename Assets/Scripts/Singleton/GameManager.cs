using Lugu.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private int _maxPlayerLifes = 3;
    [SerializeField] private float _currentlPlayerLifes;

    protected override void Awake()
    {
        base.Awake();
        _currentlPlayerLifes = _maxPlayerLifes;
    }

    public void PlayerDied()
    {
        _currentlPlayerLifes--;

        if(_currentlPlayerLifes <= 0)
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
