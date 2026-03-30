using UnityEngine;

public static class SteeringBehaviours
{
    public static Vector3 Seek(Transform self, Vector3 Target)
    {
        Vector3 dir = Target - self.position /*+ Vector3(5,0,5)*/;
        dir.y = 0;
        return dir.normalized;
    }
    public static Vector3 Flee(Transform self, Vector3 Target)
    { 
    Vector3 dir= self.position - Target;
    dir.y = 0;
    return dir.normalized;
    }

    public static Vector3 Arrive(Transform self, Vector3 Target, float slowRadius)
    {
        Vector3 dir = Target - self.position;
        float distance = dir.magnitude;

        if (distance < 0.01f)
        {
            return Vector3.zero;
        }
        float speedFactor = Mathf.Clamp01(distance / slowRadius);
        return dir.normalized * speedFactor;
    }

    public static Vector3 Persue(Transform self,Transform target,Rigidbody targetRB,float time)
    {
        Vector3 futurePos = target.position + targetRB.linearVelocity * time;
        return Seek(self, futurePos);
    }
}
