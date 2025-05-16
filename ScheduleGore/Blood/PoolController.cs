using MelonLoader;
using ScheduleGore.Blood.Generics;
using ScheduleOne.Combat;
using ScheduleOne.NPCs;
using ScheduleOne.UI.Shop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScheduleGore.Blood
{
    internal class PoolController : GenericImpactReciever
    {
        internal class SplatSpout : MonoBehaviour
        {
            public void Spout(Rigidbody part)
            {
                rb = part;
                MelonCoroutines.Start(RunCountdown(part));
                MelonCoroutines.Start(DripDrop());
            }

            Rigidbody rb;
            void Update()
            {
                if (rb != null && rb.velocity.magnitude >= 0.1f)
                    timeSinceMove = Time.time;
            }

            float timeSinceMove = Time.time;
            IEnumerator RunCountdown(Rigidbody part)
            {
                for (float t = 1f; t >= 0f; t -= (1f / 150f))
                {
                    if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 3, 1 << LayerMask.NameToLayer("Default")) && Time.time - timeSinceMove <= 2f)
                    {
                        float scale = Mathf.Lerp(0.75f, 0.1f, 1 - t);
                        if (UnityEngine.Random.Range(1, 10) == 10)
                            scale *= 3f;
                        SplatController.Splat(hitInfo.point, Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), -hitInfo.normal) * Quaternion.LookRotation(-hitInfo.normal), scale: scale, opacity: Mathf.Lerp(0.5f, 0.1f, 1 - t));
                        yield return new WaitForSeconds(0.05f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.25f);
                    }
                }
            }
            IEnumerator DripDrop()
            {
                for (int i=0; i < UnityEngine.Random.Range(5, 10); i++)
                {
                    if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 3, 1 << LayerMask.NameToLayer("Default")))
                    {
                        SplatController.Splat(hitInfo.point, Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), -hitInfo.normal) * Quaternion.LookRotation(-hitInfo.normal), scale: 0.1f, opacity: .85f, staticOpacity: true);
                        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
                    }
                }
            }
        }
        float timeSinceSplatted = Time.time;
        public override void ImpactRecieved(NPC npc, Impact impact)
        {
            if (Time.time - timeSinceSplatted > 1f)
                timeSinceSplatted = Time.time;
            else
                return;

            GameObject spout = new GameObject("SplatSpout");
            spout.transform.SetParent(impact.Hit.collider.transform);
            spout.transform.position = impact.Hit.point;
            spout.AddComponent<SplatSpout>().Spout(impact.Hit.rigidbody);
        }
    }
}
