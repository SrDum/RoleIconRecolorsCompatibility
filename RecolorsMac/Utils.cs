using Server.Shared.Extensions;
using Server.Shared.State;
using Home.Shared;
using Server.Shared.Info;
using Services;

namespace RecolorsMac;

public static class Utils
{
    public static T Random<T>(this IEnumerable<T> input, T defaultVal = default)
    {
        var list = input.ToList();
        return list.Count == 0 ? defaultVal : list[URandom.Range(0, list.Count)];
    }

    public static T Random<T>(this IEnumerable<T> list, Func<T, bool> predicate, T defaultVal = default) => list.Where(predicate).Random(defaultVal);

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) => source.ToList().ForEach(action);

    public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dict, Action<TKey, TValue> action) => dict.ToList().ForEach(pair => action(pair.Key, pair.Value));

    public static string RoleName(Role role) => role switch
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
        Role.WILDLING => "Wildling",
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
        /*Role.RANDOM_TOWN => "Town",
        Role.RANDOM_COVEN => "Coven",
        Role.RANDOM_NEUTRAL => "Neutral",*/
        _ => "Blank"
    };

    public static string FactionName(FactionType faction, Role role = Role.NONE)
    {
        if (role == Role.NONE)
            role = Pepper.GetMyRole();

        return faction switch
        {
            FactionType.TOWN => "Town",
            FactionType.COVEN => "Coven",
            FactionType.SERIALKILLER => "SerialKiller",
            FactionType.ARSONIST => "Arsonist",
            FactionType.WEREWOLF => "Werewolf",
            FactionType.SHROUD => "Shroud",
            FactionType.APOCALYPSE => role is Role.SOULCOLLECTOR or Role.BAKER or Role.PLAGUEBEARER or Role.BERSERKER ? "Apocalypse" : "Horsemen",
            FactionType.EXECUTIONER => "Executioner",
            FactionType.JESTER => "Jester",
            FactionType.PIRATE => "Pirate",
            FactionType.DOOMSAYER => "Doomsayer",
            FactionType.VAMPIRE => "Vampire",
            FactionType.CURSED_SOUL => "CursedSoul",
            _ => "Blank"
        };
    }

    public static string DisplayString(this Role role, FactionType factionType)
    {
        if (role.IsBucket())
        {
            var bucketDisplayString = ClientRoleExtensions.GetBucketDisplayString(role);

            if (!string.IsNullOrEmpty(bucketDisplayString))
                return bucketDisplayString;
        }

        var text = role.ToDisplayString();
        var text2 = "";

        if (role.IsTraitor(factionType))
            text2 = "\n<color=#B545FFFF>(Traitor)</color>";
        else if (Service.Game.Sim.info.roleCardObservation.Data.modifier == ROLE_MODIFIER.VIP)
            text2 = "\n<color=#06E00CFF>(VIP)</color>";

        if (text2.Length > 0)
            text2 = $"<size=85%>{text2}</size>";

        return $"<color={role.GetFaction().GetFactionColor()}>{text}{text2}</color>";
    }

    public static void Log(object message, bool logIt = false)
    {
        logIt = logIt || Constants.Debug;

        if (logIt)
            Console.WriteLine($"[Recolors] {message}");
    }
}