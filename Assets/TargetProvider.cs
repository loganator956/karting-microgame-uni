using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProvider : MonoBehaviour
{
    public Target RootTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void DrawTargetsGizmosRecursively(Target t)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(t.GetPosition(), 1f);
        foreach (Target childT in t.NextTargets)
        {
            Gizmos.DrawLine(t.GetPosition(), childT.GetPosition());
            DrawTargetsGizmosRecursively(childT);
        }
    }

    public List<Vector3> RequestRandomPath()
    {
        List<Vector3> list = new List<Vector3>();
        RecursePathRandom(RootTarget, list);
        return list;
    }

    private void RecursePathRandom(Target t, List<Vector3> list)
    {
        list.Add(t.GetPosition() + new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 1f), Random.Range(-2f, 2f)));
        if (t.NextTargets.Length == 0) { return; };
        int index = Random.Range(0, t.NextTargets.Length);
        RecursePathRandom(t.NextTargets[index], list);
    }

    void OnDrawGizmos()
    {
        DrawTargetsGizmosRecursively(RootTarget);
    }
}