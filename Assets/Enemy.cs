using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private float _damage;
    public UnityEvent OnKillEvent;
    public void AddDamage()
    {
        _damage++;
        if (_damage > 5)
        {
            _damage = 0;
            OnKillEvent.Invoke();
            Debug.Log("kil");
        }
    }
}