using HarmonyLib;
using MelonLoader;
using ScheduleGore.Blood;
using ScheduleGore.Blood.Generics;
using ScheduleGore.Helpers;
using Il2CppScheduleOne.Combat;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.NPCs.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGore.Patches
{
    [HarmonyPatch(typeof(NPC))]
    internal class NPCPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(NPC.ReceiveImpact))]
        public static void ReceiveImpact(NPC __instance, Impact impact)
        {
            switch (impact.ImpactType)
            {
                case EImpactType.Bullet:
                    GunshotController.Hit(__instance, impact);
                    break;
                case EImpactType.BluntMetal:
                    BluntController.Hit(__instance, impact); 
                    break;
                default:
                    break;
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Start(NPC __instance)
        {
            __instance.gameObject.AddComponent<VisualDamageController>();
            __instance.gameObject.AddComponent<SplatController>();
            __instance.gameObject.AddComponent<SoundController>();
            __instance.gameObject.AddComponent<PoolController>();
        }
    }
}
