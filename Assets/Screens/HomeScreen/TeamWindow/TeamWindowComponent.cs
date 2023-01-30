using ScreenSystem.Components;

public class TeamWindowComponent : WindowComponent
{
    public ListComponent charactersList;

    public TextComponent selectedCharacterName;
    public TextComponent selectedCharacterInfo;
    public TextComponent selectedCharacterStats;

    public EquipmentButton weaponSelectionButton;
    public EquipmentButton armorSelectionButton;

    private CharacterData[] _characters;

    protected override void OnShow()
    {
        base.OnShow();

        _characters = PlayerDataManager.Data.characterDatas;
        charactersList.SetItems<CharactersListItem>(4, (item, par) =>
        {
            if (_characters.Length > par.index)
            {
                item.SetInfo(_characters[par.index]);
                item.AddCallback(() => SelectCharacter(par.index));
            }
            else
            {
                item.SetInfo(null);
            }
        });

        SelectCharacter(0);
    }

    private void SelectCharacter(int index)
    {
        var character = _characters[index];

        for (int i = 0; i < charactersList.items.Count; i++)
        {
            var item = charactersList.items[i] as CharactersListItem;
            item.Select(i == index);
        }

        selectedCharacterName.SetText(character.prototype.metaData.Name);
        selectedCharacterInfo.SetText(character.prototype.metaData.Info);

        var statsText = $"Health: {character.currentHealth}/{character.prototype.health}\n" +
            $"Experience: {character.currentExp}/{100}\n" +
            $"Damage: {(character.weapon ? character.weapon.Damage : character.prototype.damage)}\n" +
            $"Defence:  {(character.armor ? character.armor.defence : 0)}";
        selectedCharacterStats.SetText(statsText);

        weaponSelectionButton.SetItem(character.weapon);
        armorSelectionButton.SetItem(character.armor);
    }
}
