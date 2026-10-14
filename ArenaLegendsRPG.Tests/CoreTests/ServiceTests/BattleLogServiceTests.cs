
using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.APIHandlers.DTOs;

using Moq;
using Xunit;

namespace ArenaLegendsRPG.Tests.GameServices;

public class BattleLogServiceTests
{
    private readonly Mock<IBattleLogRepository> _battleLogRepositoryMock;
    private readonly BattleLogService _battleLogService;

    public BattleLogServiceTests()
    {
        _battleLogRepositoryMock = new Mock<IBattleLogRepository>();

        _battleLogService = new BattleLogService(
            _battleLogRepositoryMock.Object);
    }

    [Fact]
    public async Task GetBattleLogs_ShouldReturnBattleLogs_WhenLogsExist()
    {
        int characterId = 1;

        var battleLogDtos = new List<BattleLogDto>
        {
            new BattleLogDto
            {
                MonsterName = "Dragon",
                MonsterHealth = 100,
                MonsterAttackResist = 20,
                MonsterMagicResist = 15,
                MonsterAttackDamage = 30,
                MonsterMagicDamage = 25,
                DamageDoneByPlayer = 80,
                DamageDoneByMonster = 40,
                IsPlayerWinner = true,
                BattleFinished = new DateTime(2026, 10, 14, 12, 0, 0)
            },
            new BattleLogDto
            {
                MonsterName = "Goblin",
                MonsterHealth = 50,
                MonsterAttackResist = 10,
                MonsterMagicResist = 5,
                MonsterAttackDamage = 15,
                MonsterMagicDamage = 10,
                DamageDoneByPlayer = 50,
                DamageDoneByMonster = 20,
                IsPlayerWinner = false,
                BattleFinished = new DateTime(2026, 10, 13, 15, 30, 0)
            }
        };

        _battleLogRepositoryMock
            .Setup(repo => repo.GetBattleLogsAsync(characterId))
            .ReturnsAsync(battleLogDtos);

        var result = await _battleLogService.GetBattleLogs(characterId);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        Assert.Equal("Dragon", result[0].MonsterName);
        Assert.Equal(100, result[0].MonsterHealth);
        Assert.Equal(20, result[0].MonsterAttackResist);
        Assert.Equal(15, result[0].MonsterMagicResist);
        Assert.Equal(30, result[0].MonsterAttackDamage);
        Assert.Equal(25, result[0].MonsterMagicDamage);
        Assert.Equal(80, result[0].DamageDoneByPlayer);
        Assert.Equal(40, result[0].DamageDoneByMonster);
        Assert.True(result[0].IsPlayerWinner);
        Assert.Equal(
            new DateTime(2026, 10, 14, 12, 0, 0),
            result[0].BattleFinished);

        Assert.Equal("Goblin", result[1].MonsterName);
        Assert.False(result[1].IsPlayerWinner);

        _battleLogRepositoryMock.Verify(
            repo => repo.GetBattleLogsAsync(characterId),
            Times.Once);
    }

    [Fact]
    public async Task GetBattleLogs_ShouldReturnEmptyList_WhenNoLogsExist()
    {
        int characterId = 1;

        _battleLogRepositoryMock
            .Setup(repo => repo.GetBattleLogsAsync(characterId))
            .ReturnsAsync(new List<BattleLogDto>());

        var result = await _battleLogService.GetBattleLogs(characterId);

        Assert.NotNull(result);
        Assert.Empty(result);

        _battleLogRepositoryMock.Verify(
            repo => repo.GetBattleLogsAsync(characterId),
            Times.Once);
    }

    [Fact]
    public async Task SaveBattleLog_ShouldSaveBattleLog_WhenCalled()
    {
        int characterId = 1;

        var battleLog = new BattleLog
        {
            MonsterName = "Dragon",
            MonsterHealth = 100,
            MonsterAttackResist = 20,
            MonsterMagicResist = 15,
            MonsterAttackDamage = 30,
            MonsterMagicDamage = 25,
            DamageDoneByPlayer = 80,
            DamageDoneByMonster = 40,
            IsPlayerWinner = true,
            BattleFinished = new DateTime(2026, 10, 14, 12, 0, 0)
        };

        _battleLogRepositoryMock
            .Setup(repo => repo.SaveBattleLogAsync(
                characterId,
                It.IsAny<BattleLogDto>()))
            .Returns(Task.CompletedTask);

        await _battleLogService.SaveBattleLog(characterId, battleLog);

        _battleLogRepositoryMock.Verify(
            repo => repo.SaveBattleLogAsync(
                characterId,
                It.Is<BattleLogDto>(dto =>
                    dto.MonsterName == battleLog.MonsterName &&
                    dto.MonsterHealth == battleLog.MonsterHealth &&
                    dto.MonsterAttackResist == battleLog.MonsterAttackResist &&
                    dto.MonsterMagicResist == battleLog.MonsterMagicResist &&
                    dto.MonsterAttackDamage == battleLog.MonsterAttackDamage &&
                    dto.MonsterMagicDamage == battleLog.MonsterMagicDamage &&
                    dto.DamageDoneByPlayer == battleLog.DamageDoneByPlayer &&
                    dto.DamageDoneByMonster == battleLog.DamageDoneByMonster &&
                    dto.IsPlayerWinner == battleLog.IsPlayerWinner &&
                    dto.BattleFinished == battleLog.BattleFinished)),
            Times.Once);
    }

    [Fact]
    public async Task SaveBattleLog_ShouldSaveCorrectCharacterId()
    {
        int characterId = 42;

        var battleLog = new BattleLog
        {
            MonsterName = "Goblin",
            MonsterHealth = 50,
            MonsterAttackResist = 10,
            MonsterMagicResist = 5,
            MonsterAttackDamage = 15,
            MonsterMagicDamage = 10,
            DamageDoneByPlayer = 50,
            DamageDoneByMonster = 20,
            IsPlayerWinner = false,
            BattleFinished = new DateTime(2026, 10, 14, 14, 0, 0)
        };

        _battleLogRepositoryMock
            .Setup(repo => repo.SaveBattleLogAsync(
                It.IsAny<int>(),
                It.IsAny<BattleLogDto>()))
            .Returns(Task.CompletedTask);

        await _battleLogService.SaveBattleLog(characterId, battleLog);

        _battleLogRepositoryMock.Verify(
            repo => repo.SaveBattleLogAsync(
                characterId,
                It.IsAny<BattleLogDto>()),
            Times.Once);
    }
}