using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FusionManager fusionManager;

    public void CreateSession()
    {
        fusionManager.CreateSession();
    }

    public void JoinSession()
    {
        fusionManager.JoinSession();
    }
}
