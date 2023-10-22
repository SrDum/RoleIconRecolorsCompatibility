using Game.Interface;
using Server.Shared.Extensions;
using Server.Shared.State;
using Server.Shared.Info;
using Services;

namespace Recolors;

public static class Patches
{
    private static int Index;
    private static string RoleName(Role role) => role switch
    {
        Role.ADMIRER => "Admirer",
        Role.AMNESIAC => "Amnesiac",
        Role.BODYGUARD => "Bodyguard",
        Role.CLERIC => "Cleric",
        Role.CORONER => "Coroner",
        Role.CRUSADER => "Crusader",
        Role.DEPUTY => "Deputy",
        Role.INVESTIGATOR => "Investigator",
        Role.JAILOR => "Jailor",
        Role.LOOKOUT => "Lookout",
        Role.MAYOR => "Mayor",
        Role.MONARCH => "Monarch",
        Role.PROSECUTOR => "Prosecutor",
        Role.PSYCHIC => "Psychic",
        Role.RETRIBUTIONIST => "Retributionist",
        Role.SEER => "Seer",
        Role.SHERIFF => "Sheriff",
        Role.SPY => "Spy",
        Role.TAVERNKEEPER => "TavernKeeper",
        Role.TRACKER => "Tracker",
        Role.TRAPPER => "Trapper",
        Role.TRICKSTER => "Trickster",
        Role.VETERAN => "Veteran",
        Role.VIGILANTE => "Vigilante",
        Role.CONJURER => "Conjurer",
        Role.COVENLEADER => "CovenLeader",
        Role.DREAMWEAVER => "Dreamweaver",
        Role.ENCHANTER => "Enchanter",
        Role.HEXMASTER => "HexMaster",
        Role.ILLUSIONIST => "Illusionist",
        Role.JINX => "Jinx",
        Role.MEDUSA => "Medusa",
        Role.NECROMANCER => "Necromancer",
        Role.POISONER => "Poisoner",
        Role.POTIONMASTER => "PotionMaster",
        Role.RITUALIST => "Ritualist",
        Role.VOODOOMASTER => "VoodooMaster",
        Role.WILDLING => "Wilding",
        Role.WITCH => "Witch",
        Role.ARSONIST => "Arsonist",
        Role.BAKER => "Baker",
        Role.BERSERKER => "Berserker",
        Role.DOOMSAYER => "Doomsayer",
        Role.EXECUTIONER => "Executioner",
        Role.JESTER => "Jester",
        Role.PIRATE => "Pirate",
        Role.PLAGUEBEARER => "Plaguebearer",
        Role.SERIALKILLER => "SerialKiller",
        Role.SHROUD => "Shroud",
        Role.SOULCOLLECTOR => "SoulCollector",
        Role.WEREWOLF => "Werewolf",
        Role.FAMINE => "Famine",
        Role.WAR => "War",
        Role.PESTILENCE => "Pestilence",
        Role.DEATH => "Death",
        Role.CURSED_SOUL => "CursedSoul",
        Role.VAMPIRE => "Vampire",
        Role.STONED => "Stoned",
        _ => "Blank"
    };
    private static string FactionName(FactionType role) => role switch
    {
        FactionType.TOWN => "Town",
        FactionType.COVEN => "Coven",
        FactionType.SERIALKILLER => "SerialKiller",
        FactionType.ARSONIST => "Arsonist",
        FactionType.WEREWOLF => "Werewolf",
        FactionType.SHROUD => "Shroud",
        FactionType.APOCALYPSE => "Horsemen",
        FactionType.EXECUTIONER => "Executioner",
        FactionType.JESTER => "Jester",
        FactionType.PIRATE => "Pirate",
        FactionType.DOOMSAYER => "Doomsayer",
        FactionType.VAMPIRE => "Vampire",
        FactionType.CURSED_SOUL => "CursedSoul",
        _ => "Blank"
    };

    /*[HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.Update))]
    public static class PatchRoleCardIcons1
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons || __instance.testMe != 0)
                return;

            var role = RoleName(Pepper.GetMyCurrentIdentity().role);
            var faction = FactionName(Pepper.GetMyCurrentIdentity().faction);
            var sprite = AssetManager.GetSprite(role);

            if (sprite != Recolors.Instance.Blank)
                __instance.roleIcon.sprite = sprite;

            var index = 0;
            var spriteName = $"{role}_Ability";
            var sprite1 = AssetManager.GetSprite(spriteName);

            if (sprite1 == Recolors.Instance.Blank)
                spriteName += "_1";

            sprite1 = AssetManager.GetSprite(spriteName);

            if (sprite1 != Recolors.Instance.Blank)
            {
                __instance.roleInfoButtons[index].abilityIcon.sprite = sprite1;
                index++;
            }

            var sprite2 = AssetManager.GetSprite($"{role}_Ability_2");

            if (sprite2 == Recolors.Instance.Blank)
                sprite2 = AssetManager.GetSprite($"Attributes_{faction}");

            if (sprite2 != Recolors.Instance.Blank)
            {
                __instance.roleInfoButtons[index].abilityIcon.sprite = sprite2;
                index++;
            }

            var sprite3 = AssetManager.GetSprite($"{role}_Special");

            if (sprite3 != Recolors.Instance.Blank)
                __instance.specialAbilityPanel.useButton.abilityIcon.sprite = sprite3;
        }
    }*/

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.SetRole))]
    public static class PatchRoleCardIcons2
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons)
                return;

            var role = Pepper.GetMyCurrentIdentity().role;
            var name = RoleName(role);
            var sprite = role.IsTraitor(Pepper.GetMyCurrentIdentity().faction) ? AssetManager.GetSprite(name) : AssetManager.GetTTSprite(name);

            if (sprite != Recolors.Instance.Blank)
                __instance.roleIcon.sprite = sprite;
        }
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.DetermineFrameAndSlots))]
    public static class ResetIndexPatch1
    {
        public static void Prefix() => Index = 0;
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.DetermineFrameAndSlots_DestroyRoleInfoSlots))]
    public static class ResetIndexPatch2
    {
        public static void Prefix() => Index = 0;
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.DetermineFrameAndSlots_AbilityIcon))]
    public static class PatchRoleCardAbilityIcons1
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons || __instance.myData.abilityIcon == null)
                return;

            var spriteName = $"{RoleName(Pepper.GetMyCurrentIdentity().role)}_Ability";
            var sprite = AssetManager.GetSprite(spriteName);

            if (sprite == Recolors.Instance.Blank)
                spriteName += "_1";

            sprite = AssetManager.GetSprite(spriteName);

            if (sprite != Recolors.Instance.Blank)
            {
                __instance.roleInfoButtons[Index].abilityIcon.sprite = sprite;
                Index++;
            }
        }
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.DetermineFrameAndSlots_AbilityIcon2))]
    public static class PatchRoleCardAbilityIcons2
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons || __instance.myData.abilityIcon2 == null)
                return;

            var sprite = AssetManager.GetSprite($"{RoleName(Pepper.GetMyCurrentIdentity().role)}_Ability_2");

            if (sprite != Recolors.Instance.Blank)
            {
                __instance.roleInfoButtons[Index].abilityIcon.sprite = sprite;
                Index++;
            }
        }
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.DetermineFrameAndSlots_AttributeIcon))]
    public static class PatchRoleCardAttributeIcon
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons || __instance.myData.attributeIcon == null)
                return;

            var sprite = AssetManager.GetSprite($"Attribute_{FactionName(Pepper.GetMyCurrentIdentity().faction)}");

            if (sprite != Recolors.Instance.Blank)
            {
                __instance.roleInfoButtons[Index].abilityIcon.sprite = sprite;
                Index++;
            }
        }
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.DetermineFrameAndSlots_Necro))]
    public static class PatchRoleCardNecronomicon
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons || Service.Game.Sim.info.roleCardObservation.Data.powerUp != POWER_UP_TYPE.NECRONOMICON)
                return;

            var sprite = AssetManager.GetSprite("Necronomicon");

            if (sprite != Recolors.Instance.Blank)
            {
                __instance.roleInfoButtons[Index].abilityIcon.sprite = sprite;
                Index++;
            }
        }
    }

    [HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.ValidateSpecialAbilityPanel))]
    public static class PatchRoleCardSpecialAbilityIcon
    {
        public static void Postfix(RoleCardPanel __instance)
        {
            if (!Settings.EnableIcons || __instance.myData.specialAbilityIcon == null)
                return;

            var sprite = AssetManager.GetSprite($"{RoleName(Pepper.GetMyCurrentIdentity().role)}_Special");

            if (sprite != Recolors.Instance.Blank)
                __instance.specialAbilityPanel.useButton.abilityIcon.sprite = sprite;
        }
    }
}