using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;

    [Space] [SerializeField] private DotsGenerator dotsGenerator;
    [SerializeField] private InputController inputController;
    [SerializeField] private LineController lineController;
    [SerializeField] private UiManager uiManager;

    private readonly List<Dot> _selectedDots = new List<Dot>();
    private Dot _lastDot;

    private HashSet<Vector2Int> _destroyedDotsHashSet = new HashSet<Vector2Int>();

    private void OnEnable()
    {
        inputController.onDotTouched += OnDotTouched;
        inputController.onGetMouseButtonUp += CheckSelectedDots;
    }

    private void OnDisable()
    {
        inputController.onDotTouched -= OnDotTouched;
        inputController.onGetMouseButtonUp -= CheckSelectedDots;
    }

    private void Start()
    {
        dotsGenerator.GenerateDotsGrid(gameConfig.dotsGridRowsCount, gameConfig.dotsGridColumnsCount,
            gameConfig.dotsColorsCount);

        GameSessionData.Score = PlayerPrefs.GetInt(gameConfig.PlayerPrefsScoreKey, 0);
        uiManager.UpdateScoreText(GameSessionData.Score);
    }

    private void OnDotTouched(Dot dot)
    {
        if (dot.isTouching) return;

        if (!_lastDot)
        {
            SelectDot();
        }

        if (_lastDot != dot && _lastDot.ColorId == dot.ColorId &&
            Mathf.Abs(dot.Coordinates.x - _lastDot.Coordinates.x) <= 1 &&
            Mathf.Abs(dot.Coordinates.y - _lastDot.Coordinates.y) <= 1 &&
            (dot.Coordinates.x == _lastDot.Coordinates.x || dot.Coordinates.y == _lastDot.Coordinates.y))
        {
            SelectDot();
        }

        void SelectDot()
        {
            dot.isTouching = true;
            _selectedDots.Add(dot);
            _lastDot = dot;
        }

        lineController.DrawLine(_selectedDots);
    }

    private void ClearPrevData()
    {
        lineController.SetActiveLineRenderer(false);

        foreach (var selectedDot in _selectedDots)
        {
            selectedDot.isTouching = false;
        }

        _lastDot = null;
        _selectedDots.Clear();
    }

    private void CheckSelectedDots()
    {
        if (_selectedDots.Count > 1)
        {
            GameSessionData.Score += _selectedDots.Count;
            PlayerPrefs.SetInt(gameConfig.PlayerPrefsScoreKey, GameSessionData.Score);
            uiManager.UpdateScoreText(GameSessionData.Score);

            _destroyedDotsHashSet = new HashSet<Vector2Int>();

            foreach (var selectedDot in _selectedDots)
            {
                _destroyedDotsHashSet.Add(selectedDot.Coordinates);
                Destroy(selectedDot.gameObject);
            }

            StartCoroutine(CreateAndShowNewDots(_destroyedDotsHashSet));
        }

        ClearPrevData();
    }

    private IEnumerator CreateAndShowNewDots(HashSet<Vector2Int> newDots)
    {
        yield return new WaitForSeconds(0.25f);

        foreach (var dot in newDots)
        {
            dotsGenerator.CreateDot(dot.x, dot.y);
            yield return new WaitForSeconds(0.1f);
        }
    }
}