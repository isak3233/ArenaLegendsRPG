namespace ArenaLegendsRPG.Infrastructure.Entities;

public class BattleLogEntity
{
    public int Id { get; set; }

    public int CharacterId { get; set; }
    public CharacterEntity Character { get; set; } = null!;

    public string MonsterName { get; set; }
    public int MonsterHealth { get; set; }
    public int MonsterAttackResist { get; set; }
    public int MonsterMagicResist { get; set; }
    public int MonsterAttackDamage { get; set; }
    public int MonsterMagicDamage { get; set; }
    public int DamageDoneByPlayer { get; set; }
    public int DamageDoneByMonster { get; set; }
    public bool IsPlayerWinner { get; set; }
    public DateTime BattleFinished { get; set; }
}