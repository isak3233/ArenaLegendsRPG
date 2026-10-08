using ArenaLegendsRPG.Core.Encounter;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface IEncounterService
{
    IEncounter GenerateEncounter();
}