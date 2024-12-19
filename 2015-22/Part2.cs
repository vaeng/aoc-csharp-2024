using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.Json;



public static class Part2
{
  public enum Action
  {
    MagicMissile,
    Drain,
    Shield,
    Poison,
    Recharge
  }

  private static readonly Dictionary<Action, long> ManaCost = new() {
      {Action.MagicMissile, 53},
      {Action.Drain, 73},
      {Action.Shield, 113},
      {Action.Poison, 173},
      {Action.Recharge, 229}
    };

  private static readonly Dictionary<Action, long> EffectDuration = new() {
      {Action.MagicMissile, 1},
      {Action.Drain, 1},
      {Action.Shield, 6},
      {Action.Poison, 6},
      {Action.Recharge, 5}
    };
  public struct GameState
  {
    public long ManaSpend = 0;
    public long round = 1;
    public Player player;
    public Boss boss;
    public List<string> log = new();

    public GameState(long playerHP, long playerMana, long bossHP, long bossDamage)
    {
      player = new(playerHP, playerMana);
      boss = new(bossHP, bossDamage);
    }


    public Dictionary<Action, long> CurrentEffects = new() {
      {Action.MagicMissile, 0},
      {Action.Drain, 0},
      {Action.Shield, 0},
      {Action.Poison, 0},
      {Action.Recharge, 0}
    };

    public List<Action> AvailableActions()
    {
      List<Action> actions = new();
      foreach (var (action, cost) in ManaCost)
      {
        if (player.Mana >= cost && CurrentEffects[action] <= 1)
        {
          actions.Add(action);
        }
      }
      return actions;
    }

    private void QueueCast(Action spell)
    {

      CurrentEffects[spell] = EffectDuration[spell];
      player.Mana -= ManaCost[spell];
      ManaSpend += ManaCost[spell];
      if (spell == Action.MagicMissile)
      {
        Log("Player casts Magic Missile, dealing 4 damage.");
        boss.HP -= 4;
      }
      else if (spell == Action.Drain)
      {
        Log("Player casts Drain, dealing 2 damage, and healing 2 hit points.");
        boss.HP -= 2;
        player.HP += 2;
      }
      else if (spell == Action.Shield)
      {
        Log("Player casts Shield, increasing armor by 7.");
        player.Armor = 7;
      }
      else
      {
        Log($"Player casts {spell}.");
      }
      Log("");
    }

    private void ResolveEffects()
    {
      if (CurrentEffects[Action.Shield] <= 0)
      {
        player.Armor = 0;
      }

      foreach (var (effect, duration) in CurrentEffects)
      {
        if (duration > 0)
        {
          switch (effect)
          {
            case Action.MagicMissile:
              // changed to instant
              break;
            case Action.Drain:
              // changed to instant
              break;
            case Action.Shield:
              Log($"Shield's timer is now {CurrentEffects[Action.Shield] - 1}.");
              break;
            case Action.Poison:
              Log($"Poison deals 3 damage; its timer is now {CurrentEffects[Action.Poison] - 1}.");
              boss.HP -= 3;
              break;
            case Action.Recharge:
              Log($"Recharge provides 101 mana; its timer is now {CurrentEffects[Action.Recharge] - 1}.");
              player.Mana += 101;
              break;
          }
        }
        CurrentEffects[effect] = Math.Max(0, duration - 1);
      }
    }

    private void PlayerTurn(Action spell)
    {
      Log($"{round}-- Player turn --");
      Log($"- Hard mode: lose 1 hp.");
      player.HP -= 1;
      Log($"- Player has {player.HP} hit points, {player.Armor} armor, {player.Mana} mana");
      Log($"- Boss has {boss.HP} hit points");

      ResolveEffects();
      QueueCast(spell);
    }

    private void BossTurn()
    {
      Log($"{round}-- Boss turn --");
      Log($"- Player has {player.HP} hit points, {player.Armor} armor, {player.Mana} mana");
      Log($"- Boss has {boss.HP} hit points");
      ResolveEffects();
      long dmg = Math.Max(1, boss.Damage - player.Armor);
      if(boss.HP > 0) {
        player.HP -= dmg;
        Log($"Boss attacks for {dmg} damage!");
      } else {
        Log($"This kills the boss");
      }
    }

    public void ProcessRound(Action spell)
    {
      PlayerTurn(spell);
      BossTurn();
      round++;
      Log("");
    }



    private void Log(string message)
    {
      log.Add(message);
    }

    public void PrintLog()
    {
      foreach (var entry in log)
      {
        Console.WriteLine(entry);
      }
      Console.WriteLine();
    }

  }

  public struct Player
  {
    public Player(long hp, long mana)
    {
      HP = hp;
      Mana = mana;
      Armor = 0;
    }

    public long HP;
    public long Mana;
    public long Armor;
  }

  public struct Boss
  {
    public Boss(long hp, long dmg)
    {
      HP = hp;
      Damage = dmg;
    }
    public long HP;
    public long Damage;
  }

  public static long bossHP = 0;
  public static long bossDamage = 0;

  public static void Parse(List<String> input)
  {
    bossHP = Convert.ToInt64(input[0][11..]);
    bossDamage = Convert.ToInt64(input[1][7..]);
  }


  public static string Solve(List<String> input)
  {
    Parse(input);
    long playerHP = 50;
    long playerMana = 500;
    GameState initialGameState = new(playerHP, playerMana, bossHP, bossDamage);

    PriorityQueue<GameState, long> queue = new();
    queue.Enqueue(initialGameState, initialGameState.ManaSpend);

    long result = 0;

    JsonSerializerOptions options = new()
    {
      WriteIndented = true,
      IncludeFields = true
    };
    JsonSerializerOptions optionsCopy = new(options);

    while (queue.Count > 0)
    {
      GameState state = queue.Dequeue();
      if (state.player.HP <= 0)
      {
        continue;
      }

      List<Action> possibleActions = state.AvailableActions();
      if (possibleActions.Count == 0)
      {
        // losing scenario
        continue;
      }
      foreach (Action spell in possibleActions)
      {
        GameState nextState = JsonSerializer.Deserialize<GameState>(JsonSerializer.Serialize(state, optionsCopy), optionsCopy);

        nextState.ProcessRound(spell);
        if (nextState.boss.HP <= 0 && nextState.player.HP > 0)
        {
          result = nextState.ManaSpend;
          // nextState.PrintLog();
          return result.ToString();
        }
        queue.Enqueue(nextState, nextState.ManaSpend);
      }
    }

    return result.ToString();
  }
}
