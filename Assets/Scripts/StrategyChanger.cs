using Player.Strategy;
using TMPro;
using UnityEngine;

public class StrategyChanger : MonoBehaviour
{
    [SerializeField] private PlayerStrategyHandler.Strategy _strategy;
    [SerializeField] private TextMeshProUGUI _strategyText;

    #region Properties

    public PlayerStrategyHandler.Strategy strategy { get { return _strategy; } private set { _strategy = value; } }

    #endregion

    private void Awake()
    {
        if (_strategyText != null)
        {
            _strategyText.text = _strategy.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerStrategyHandler = other.GetComponent<PlayerMovement>();
            if (playerStrategyHandler != null)
            {
                playerStrategyHandler.ChangeStrategy(_strategy);
            }
        }
    }
}
