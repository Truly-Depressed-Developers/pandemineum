namespace Statistics {
  public static class GenerateDescription {
    public static string Description(BuffType buff, PlayerStatistics stat) {
      switch ((stat, buff)) {
        case (PlayerStatistics.Armor, BuffType.Buff): return "All this cobalt makes your suit tougher";
        case (PlayerStatistics.Damage, BuffType.Buff): return "Your rounds become sharper and more deadly";
        case (PlayerStatistics.Health, BuffType.Buff): return "The mine taught you how to survive";
        case (PlayerStatistics.Speed, BuffType.Buff): return "Running around made your physique better";
        case (PlayerStatistics.ReloadSpeed, BuffType.Buff): return "You become more familiar with your weapon";
        case (PlayerStatistics.ShotRange, BuffType.Buff): return "Extra gunpowder makes your pellets go further";
        case (PlayerStatistics.SightRange, BuffType.Buff): return "You got used to the dark, sharpening your senses";
        case (PlayerStatistics.Luck, BuffType.Buff): return "You feel the winds of fortune shifting in your favor";
        case (PlayerStatistics.CobaltPickRate, BuffType.Buff): return "You learn to harvest more materials from rocks";
        
        case (PlayerStatistics.Armor, BuffType.Debuff): return "Toxic air makes armor corrode faster";
        case (PlayerStatistics.Damage, BuffType.Debuff): return "Your workers are equipped with older weapons";
        case (PlayerStatistics.Health, BuffType.Debuff): return "Miners become seriously exhausted from their job";
        case (PlayerStatistics.Speed, BuffType.Debuff): return "Sludge makes workers move slower";
        case (PlayerStatistics.ReloadSpeed, BuffType.Debuff): return "Worker weapons occasionally jam";
        case (PlayerStatistics.ShotRange, BuffType.Debuff): return "Air becomes more dense in the mine";
        case (PlayerStatistics.SightRange, BuffType.Debuff): return "Mist makes it easier to lose your way";
        case (PlayerStatistics.CobaltPickRate, BuffType.Debuff): return "Rocks crumble more easily, decreasing gain";
      }

      return "";
    }
    
    public static string Description(BuffType buff, EnemyStatistics stat) {
      switch ((stat, buff)) {
        case (EnemyStatistics.Armor, BuffType.Buff): return "Coat your bullets in substance bypassing enemy armor";
        case (EnemyStatistics.Damage, BuffType.Buff): return "Your enemies claws become more blunt";
        case (EnemyStatistics.Health, BuffType.Buff): return "Years of living in the mine made your enemies vulnerable";
        case (EnemyStatistics.Speed, BuffType.Buff): return "Lack of food makes predators slower";
        case (EnemyStatistics.DropRate, BuffType.Buff): return "Your enemies eat more rocks... resulting in more occasional cobalt drops";
        case (EnemyStatistics.ShotRange, BuffType.Buff): return "It takes your enemies longer to lunge at you";
        
        case (EnemyStatistics.Armor, BuffType.Debuff): return "Evolution makes predators more resistant to damage";
        case (EnemyStatistics.Damage, BuffType.Debuff): return "Smell of blood makes enemies more aggressive";
        case (EnemyStatistics.Health, BuffType.Debuff): return "Enemies are tougher to take down";
        case (EnemyStatistics.Speed, BuffType.Debuff): return "Enemies learn to traverse the mine with ease";
        case (EnemyStatistics.DropRate, BuffType.Debuff): return "Predators no longer enjoy the taste of rocks";
        case (EnemyStatistics.ShotRange, BuffType.Debuff): return "Enemies become more eager to attack you";
      }

      return "";
    }
    
    public static string Badge(PlayerStatistics stat) {
      switch (stat) {
        case PlayerStatistics.Armor: return "Worker armor";
        case PlayerStatistics.Damage: return "Worker damage";
        case PlayerStatistics.Health: return "Worker health";
        case PlayerStatistics.Speed: return "Worker speed";
        case PlayerStatistics.ReloadSpeed: return "Reload speed";
        case PlayerStatistics.ShotRange: return "Worker attack range";
        case PlayerStatistics.SightRange: return "Worker sight range";
        case PlayerStatistics.Luck: return "Worker luck";
        case PlayerStatistics.CobaltPickRate: return "Cobalt pickup rate";
      }

      return "";
    }
    
    public static string Badge(EnemyStatistics stat) {
      switch (stat) {
        case EnemyStatistics.Armor: return "Enemy armor";
        case EnemyStatistics.Damage: return "Enemy damage";
        case EnemyStatistics.Health: return "Enemy health";
        case EnemyStatistics.Speed: return "Enemy speed";
        case EnemyStatistics.DropRate: return "Enemy drop rate";
        case EnemyStatistics.ShotRange: return "Enemy attack range";
        }

      return "";
    }

    public static bool IsAdditive(PlayerStatistics stat) {
      return stat is PlayerStatistics.Armor or PlayerStatistics.Damage;
    }
    
    public static bool IsAdditive(EnemyStatistics stat) {
      return stat is EnemyStatistics.Armor or EnemyStatistics.Damage;
    }

    public static bool IsMultiplicative(PlayerStatistics stat) {
      return !IsAdditive(stat);
    }
    
    public static bool IsMultiplicative(EnemyStatistics stat) {
      return !IsAdditive(stat);
    }
  }
}
