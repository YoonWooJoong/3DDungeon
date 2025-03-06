using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorManager : MonoBehaviour
{
    private static CharactorManager _instance;
    public static CharactorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharactorManager").AddComponent<CharactorManager>();
            }
            return _instance;
        }
    }

    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
