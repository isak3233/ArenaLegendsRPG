
using ArenaLegendsRPG.Core.Encounters;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface IEncounterService
{
    IEncounter GenerateEncounter();
}