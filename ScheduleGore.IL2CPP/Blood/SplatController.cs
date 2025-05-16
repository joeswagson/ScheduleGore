using MelonLoader;
using ScheduleGore.Blood.Generics;
using ScheduleGore.Embedded;
using ScheduleGore.Utils;
using ScheduleGore;
using Il2CppScheduleOne.Combat;
using Il2CppScheduleOne.NPCs;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Collections;

namespace ScheduleGore.Blood
{
    [RegisterTypeInIl2Cpp]
    internal class SplatController : MonoBehaviour
    {
        public NPC NPC { get; private set; } = null!;
        void Start()
        {
            NPC? inParent = GetComponentInParent<NPC>();
            if (inParent)
                NPC = inParent;
            else
                Destroy(this);
        }
        public SplatController(IntPtr ptr) : base(ptr) { }

        [RegisterTypeInIl2Cpp]
        public class Cleaner : MonoBehaviour
        {
            public Cleaner(IntPtr ptr) : base(ptr) { }
            public DecalProjector? projector;
            void Start()
            {
                MelonCoroutines.Start(Dispose());
                projector = projector ?? GetComponent<DecalProjector>();
            }

            IEnumerator Dispose()
            {
                yield return new WaitForSeconds(30f);

                for (float t = 1f; t >= 0f; t -= 0.025f)
                {
                    if (projector != null)
                        projector.fadeFactor = Mathf.Lerp(projector.fadeFactor, 0f, 1 - t);
                    yield return new WaitForSeconds(0.05f);
                }

                Destroy(projector);
                Destroy(this);
            }
        }

        DecalProjector? decalProjector;

        public static DecalProjector Splat(Vector3 position, Vector3 eulerRotation)
        {
            return Splat(position, Quaternion.Euler(eulerRotation));
        }
        public static DecalProjector Splat(Vector3 position, Quaternion rotation, float scale = 1f, Color? color = null, float opacity = 1f, bool staticOpacity = false)
        {
            DecalProjector? decalProjector = null;
            GameObject? splat = Assets.Splat.Instantiate();

            if (!Logging.NullSanity(splat, "Splat Decal"))
                return null!; // shouldnt happen unless assets arent loaded so im fine with passing null here

            //init
            splat.layer = LayerMask.NameToLayer("Default");
            decalProjector = splat.GetComponent<DecalProjector>();

            //pass material data into existing decal projector material cause if we dont itll flicker and disappear randomly (depending on viewing angle)
            Texture original = decalProjector.material.GetTexture("Base_Map");
            Texture originalNormal = decalProjector.material.GetTexture("Normal_Map");
            decalProjector.material = Material.Instantiate(ShaderFix.bodySearchDecal) ?? decalProjector.material; //new Material(ShaderFix.bodySearchDecal);
            decalProjector.material.SetTexture("_Base_Map", original);
            decalProjector.material.SetTexture("_Normal_Map", originalNormal);
            decalProjector.material.color = color ?? new Color(0.5573585f, 0f, 0f);

            //style the damn thing
            float scaleFactor = UnityEngine.Random.Range(1f, 3f);
            decalProjector.size = new Vector3(decalProjector.size.x * scaleFactor * scale, decalProjector.size.y * scaleFactor * scale, decalProjector.size.z);
            decalProjector.fadeFactor = staticOpacity ? opacity : (0.6f + UnityEngine.Random.Range(0f, .4f)) * opacity;
            decalProjector.startAngleFade = 0f;
            decalProjector.enabled = false;
            decalProjector.enabled = true;

            //move it move it
            decalProjector.transform.position = position;
            decalProjector.transform.rotation = rotation;

            decalProjector.gameObject.AddComponent<Cleaner>().projector = decalProjector;

            return decalProjector;
        }

        public void ImpactRecieved(NPC npc, Impact impact)
        {
            if (Physics.Raycast(impact.Hit.point, impact.ImpactForceDirection.normalized, out RaycastHit hitInfo, 3, 1 << LayerMask.NameToLayer("Default")))
            {
                Splat(hitInfo.point, Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), -hitInfo.normal) * Quaternion.LookRotation(-hitInfo.normal));
            }
            else if (Physics.Raycast(impact.Hit.point, Vector3.Lerp(Vector3.down, impact.ImpactForceDirection.normalized, 0.25f), out RaycastHit hitInfoFallback, 3, 1 << LayerMask.NameToLayer("Default")))
            {
                Splat(hitInfoFallback.point, Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), -hitInfoFallback.normal) * Quaternion.LookRotation(-hitInfoFallback.normal));
            }
        }
    }
}