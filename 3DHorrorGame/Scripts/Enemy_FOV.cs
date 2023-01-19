using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_FOV : MonoBehaviour
{
    public float viewDistance;
    [Range(0, 360)] public float viewAngle;
    public LayerMask targetMask;
    public Transform rayPoint;
    public bool findTarget = false;     // 에너미가 플레이러르 찾았는지
    public bool finding = false;        // 에너미가 플레이어를 탐색중인지

    private void Update()
    {
        View();
    }

    Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    void View()
    {
        Vector3 left = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 right = BoundaryAngle(viewAngle * 0.5f);

        Collider[] target = Physics.OverlapSphere(rayPoint.position, viewDistance, targetMask);

        for(int i = 0; i < target.Length; i++)
        {
            Transform targetTransform = target[i].transform;
            if(targetTransform.tag == "Player")
            {
                Vector3 direction = (targetTransform.position - rayPoint.position).normalized;
                float angle = Vector3.Angle(direction, transform.forward);

                if(angle < viewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(rayPoint.position, direction, out hit, viewDistance))
                    {
                        if(hit.transform.tag == "Player")
                        {
                            if (!findTarget)
                            {
                                findTarget = true;
                                GetComponent<Enemy>().FindPlayer();
                            }

                            if (finding)
                            {
                                finding = false;
                            }
                        }
                        else
                        {
                            if (findTarget && !finding)
                            {
                                finding = true;
                                StopCoroutine(LostDelay());
                                StartCoroutine(LostDelay());
                            }
                        }
                    }
                    else
                    {
                        if (findTarget && !finding)
                        {
                            finding = true;
                            StopCoroutine(LostDelay());
                            StartCoroutine(LostDelay());
                        }
                    }
                }
            }
        }
    }

    IEnumerator LostDelay()
    {
        yield return new WaitForSeconds(10f);
        if (finding)
        {
            findTarget = false;
            GetComponent<Enemy>().LostPlayer();
        }
        finding = false;
    }
}
