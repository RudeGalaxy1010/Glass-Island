using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private const string SaveKey = "CurrentLevel";

    [SerializeField] private Levels _levels;
    [SerializeField] private Transform _backgroundTransform;
    [SerializeField] private Button _nextLevelButton;

    private GameObject _map;
    private int _currentLevelIndex = 0;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevel);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevel);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveKey) == false)
        {
            SaveProgress(_currentLevelIndex);
        }

        _currentLevelIndex = LoadProgress();
        LoadLevel(_currentLevelIndex);
    }

    public Level CurrentLevel => _levels.List[_currentLevelIndex];
    private float BackgroundHeight => CurrentLevel.MinDieHeight - 2f;

    private void OnNextLevel()
    {
        _currentLevelIndex++;

        // TODO: end screen
        if (_currentLevelIndex == _levels.List.Count)
        {
            Debug.Log("All levels passed");
            return;
        }

        SaveProgress(_currentLevelIndex);
        LoadLevel(_currentLevelIndex);
    }

    private void SaveProgress(int index)
    {
        PlayerPrefs.SetInt(SaveKey, index);
    }

    private int LoadProgress()
    {
        return PlayerPrefs.GetInt(SaveKey);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }

    private void LoadLevel(int index)
    {
        if (_map != null)
        {
            Destroy(_map);
        }

        _map = Instantiate(_levels.List[index].MapPrefab);
        _backgroundTransform.position = new Vector3(_backgroundTransform.position.x, BackgroundHeight, _backgroundTransform.position.z);
    }
}
