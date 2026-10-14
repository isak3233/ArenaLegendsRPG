namespace ArenaLegendsRPG.API.Features.BattleLog.DTOs;

public class BattleLogDto
{
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