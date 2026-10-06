using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using Xunit;

namespace ArenaLegendsRPG.Tests.CoreTests;

public class CharacterCreationScreenTests : IClassFixture<CharacterCreationScreenFixture>
{
    private readonly CharacterCreationScreenFixture _fixture;
    public CharacterCreationScreenTests(CharacterCreationScreenFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Submit_SetsPlayerOnSession()
    {
        _fixture.Screen.Submit("Hero");

        Assert.Equal("Hero", _fixture.Session.RequirePlayer().Name);

    }

    [Fact]
    public void Submit_ReturnsCharacterCreatedEvent()
    {
        var result = _fixture.Screen.Submit("Hero");
        Assert.Contains(result.Events, e => e is CharacterCreated created && created.PlayerName == "Hero");
    }

    [Fact]
    public void Submit_ReturnsGameMenuAsNextScreen()
    {
        var result = _fixture.Screen.Submit("Hero");

        Assert.Equal(_fixture.GameMenu, result.Next);
    }
}
//VIKTIGT!!
//Eftersom Submit inte ändrar något värde på Character så funkar det med en IClassFixture här.
//Om vi i framtiden ska ha en inventory test så måste vi skapa en ny karaktär för att det ska funka, alltså inte använda denna fixture
//Just för att då kommer den ändras i andra tester och det kommer misslyckas. Viktigt att tänka på!