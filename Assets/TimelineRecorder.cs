using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineRecorder : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    List<Quaternion> rotations = new List<Quaternion>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < positions.Count; i++)
            {
                lines.Add($"{positions[i]}_{rotations[i]}");
            }
            File.WriteAllLines("recording.txt", lines.ToArray());
        }
    }

    void FixedUpdate()
    {
        positions.Add(transform.position);
        rotations.Add(transform.rotation);
    }
}
