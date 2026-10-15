using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.UI;
using ArenaLegendsRPG.UI.Rendering.EventRenders;
using ArenaLegendsRPG.UI.Rendering.Input;
using Moq;

namespace ArenaLegendsRPG.Tests.UiTests;

public class InventoryEventRenderTests
{
    private readonly Mock<IConsoleIO> _consoleIO = new();
    private readonly InventoryEventRender _sut;

    public InventoryEventRenderTests()
    {
        _sut = new InventoryEventRender(_consoleIO.Object);
    }

    private static WeaponEvent GetWeaponEvent()
    {
        return new WeaponEvent(
            "test",
            "test",
            1,
            1,
            ItemRarity.Common,
            false,
            0);
    }

    [Fact]
    public void Handle_EquipItemInput_ReturnsInputResult()
    {
        var inputHelper = new Mock<IInputHelper>();

        inputHelper
            .Setup(x => x.GetInt(
                "Välj item för att börja/sluta använda det: "))
            .Returns(0);

        var sut = new GameEventInputRouter(inputHelper.Object);
        var gameEvent = new InventoryManagmentOpen();

        var result = sut.Handle(gameEvent);

        Assert.NotNull(result);
        Assert.Equal(InputType.Integer, result.Type);
        Assert.Equal(0, result.Value);

        inputHelper.Verify(
            x => x.GetInt(
                "Välj item för att börja/sluta använda det: "),
            Times.Once);
    }

    [Fact]
    public void CanPresent_Weapons_ReturnsTrue()
    {
        var weaponEvent = GetWeaponEvent();

        var result = _sut.CanPresent(weaponEvent);

        Assert.True(result);
    }

    [Fact]
    public void Present_WeaponEvent_ShowsRarityAndStats()
    {
        var weaponEvent = GetWeaponEvent();

        _sut.Present(weaponEvent);

        _consoleIO.Verify(
            c => c.WriteLine(
                "0: [Vanlig] test: Fysisk skada: 1, Magisk skada: 1"),
            Times.Once);
    }
}