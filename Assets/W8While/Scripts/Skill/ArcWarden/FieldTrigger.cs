using UnityEngine;

public class FieldTrigger : MonoBehaviour
{
    private MagneticField4ScriptibleObj _magneticField4ScriptibleObj;

    public void OnStart(MagneticField4ScriptibleObj magneticField4ScriptibleObj)
    {
        _magneticField4ScriptibleObj = magneticField4ScriptibleObj;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<EnemyMove>(out EnemyMove enemy))
        {
            enemy.ChangeSpeed((100 - _magneticField4ScriptibleObj.EnemySlowProcently) * 0.01f * enemy.BaseSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_magneticField4ScriptibleObj == null)
            return;
        if (other.TryGetComponent<EnemyMove>(out EnemyMove enemy))
        {
            enemy.ChangeSpeed(enemy.BaseSpeed);
        }
    }
}
