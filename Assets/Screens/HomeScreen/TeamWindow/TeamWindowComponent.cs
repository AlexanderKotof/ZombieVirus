using ScreenSystem.Components;

public class TeamWindowComponent : WindowComponent
{
    public ListComponent charactersList;

    public TextComponent selectedCharacterName;
    public TextComponent selectedCharacterInfo;
    public TextComponent selectedCharacterStats;

    public ButtonComponent healButton;

    public EquipmentButton weaponSelectionButton;
    public EquipmentButton armorSelectionButton;

    private CharacterData[] _characters;

    private CharacterData _selectedCharacter;

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

        healButton.SetCallback(HealCharacter);

        weaponSelectionButton.SetCallback(ShowWeaponPopup);
        armorSelectionButton.SetCallback(ShowArmorPopup);

        SelectCharacter(0);
    }

    private void HealCharacter()
    {
        _selectedCharacter.currentHealth = _selectedCharacter.prototype.health;
    }

    private void ShowWeaponPopup()
    {
        var weapons = PlayerInventoryManager.Instance.GetWepons();

        if (weapons.Count > 0)
            WeaponSelectionPopup.ShowWeaponsPopup(weaponSelectionButton.RectTransform.position, weapons, SelectWeapon);
    }

    private void ShowArmorPopup()
    {
        var armors = PlayerInventoryManager.Instance.GetArmors();

        if (armors.Count > 0)
            WeaponSelectionPopup.ShowWeaponsPopup(armorSelectionButton.RectTransform.position, armors, SelectArmor);
    }

    private void SelectWeapon(Item weapon)
    {
        PlayerInventoryManager.Instance.PlayerInventory.AddItem(_selectedCharacter.weapon);
        PlayerInventoryManager.Instance.PlayerInventory.RemoveItem(weapon);

        PlayerInventoryManager.Instance.Debug();

        _selectedCharacter.weapon = weapon as Weapon;

        weaponSelectionButton.SetItem(_selectedCharacter.weapon);
    }

    private void SelectArmor(Item weapon)
    {
        PlayerInventoryManager.Instance.PlayerInventory.AddItem(_selectedCharacter.armor);
        PlayerInventoryManager.Instance.PlayerInventory.RemoveItem(weapon);

        PlayerInventoryManager.Instance.Debug();

        _selectedCharacter.armor = weapon as Armor;

        armorSelectionButton.SetItem(_selectedCharacter.armor);
    }

    private void SelectCharacter(int index)
    {
        var character = _characters[index];
        _selectedCharacter = character;

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
