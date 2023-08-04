using UnityEngine;

public class RangeActive : MonoBehaviour
{
    [SerializeField] private Transform _activePos;
    [SerializeField] private float _activeRange;
    [SerializeField] private LayerMask _entityLayer;

    private void Start()
    {
        ChangeEnableScripts(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_activePos.position, _activeRange);
    }

    void Update()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(_activePos.position, _activeRange, _entityLayer);
        
        if (monsters.Length < 0.8)
        {
            ChangeEnableScripts(false);
            return;
        }

        ChangeEnableScripts(true);
    }

    private void ChangeEnableScripts(bool isEnable)
    {
        Units[] scripts = gameObject.GetComponentsInChildren<Units>();

        foreach (Units script in scripts)
        {
            script.enabled = isEnable;
        }
    }
}
