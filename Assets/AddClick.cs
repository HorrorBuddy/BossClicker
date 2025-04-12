using UnityEngine;

public class AddClick : MonoBehaviour
{
    public Variables stats;

    public void AddOneToDamage()
    {
        stats.damage = 1;

        stats.damage += 1;
        Debug.Log("Clicks incremented! Current clicks: " + stats.damage);
    }

    // Read-only access to the score
    public int GetDamage()
    {
        return stats.damage;
    }
}
