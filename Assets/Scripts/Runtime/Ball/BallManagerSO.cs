using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Manager", menuName = "Ball/Manager")]
public class BallManagerSO : ScriptableObject
{
    [SerializeField] List<BallInfo> _balls = new List<BallInfo>();
    [SerializeField] int[] _ballSlots = new int[5];
    [SerializeField] List<Ball> _catchedBalls = new List<Ball>();
    [SerializeField] int _maxCatchedShown = 0;
    public int[] BallSlots => _ballSlots;

    public event Action OnSlotsChange;
    public event Action OnSpawn;
    public event Action OnCatched;

    public Material GetSkin(int index)
    {
        index = Mathf.Clamp(index, 0, _balls.Count - 1);
        return _balls[index].Material;
    }

    public void ChangeSlotValue(int index, int value)
    {
        index = Mathf.Clamp(index, 0, _ballSlots.Length - 1);
        value = Mathf.Clamp(value, -1, _balls.Count - 1);

        _ballSlots[index] = value;
        OnSlotsChange?.Invoke();
    }

    public List<Ball> GetCatchedBalls => _catchedBalls; 

    public void AddCatchecdBall(Ball newBall)
    {
        foreach (var ball in _catchedBalls)
        {
            if (ball == newBall)
                return;
        }

        _catchedBalls.Add(newBall);

        if (_catchedBalls.Count >= _maxCatchedShown)
        {
            newBall.Despawn();
            foreach (var ball in _catchedBalls)
                ball.Despawn();

            _catchedBalls.Clear();

            return;
        }

        OnCatched?.Invoke();
    }

    public void RemoveCatchedBall(Ball removedBal)
    {
        foreach (var ball in _catchedBalls)
        {
            if (ball != removedBal)
                continue;

            _catchedBalls.Remove(ball);
            return;
        }
    }

    public void InvokeMeshRefresh() => OnSlotsChange?.Invoke();

    public void InvokeSpawn() => OnSpawn?.Invoke();

    [System.Serializable]
    struct BallInfo
    {
        public string Name;
        public Material Material;
    }
}
