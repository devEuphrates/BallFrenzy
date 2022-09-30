using GameAnalyticsSDK;
using UnityEngine;

public class GAInit : MonoBehaviour
{
    private void Awake() => GameAnalytics.Initialize();
}
