using Features.CharactersFeature.Utils;
using PlayerDataSystem;
using PlayerDataSystem.DataStructures;
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
        _selectedCharacter.currentHealth = _selectedCharacter.maxHealth;
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
        if (_selectedCharacter.weaponId != -1)
            PlayerInventoryManager.Instance.PlayerInventory.AddItem(_selectedCharacter.weaponId);

        PlayerInventoryManager.Instance.PlayerInventory.RemoveItem(weapon);

        PlayerInventoryManager.Instance.Debug();

        _selectedCharacter.weaponId = weapon.Id;

        weaponSelectionButton.SetItem(weapon);
    }

    private void SelectArmor(Item armor)
    {
        if (_selectedCharacter.armorId != -1)
            PlayerInventoryManager.Instance.PlayerInventory.AddItem(_selectedCharacter.armorId);

        PlayerInventoryManager.Instance.PlayerInventory.RemoveItem(armor);

        PlayerInventoryManager.Instance.Debug();

        _selectedCharacter.armorId = armor.Id;

        armorSelectionButton.SetItem(armor);
    }

    private void SelectCharacter(int index)
    {
        var characterData = _characters[index];
        _selectedCharacter = characterData;

        for (int i = 0; i < charactersList.items.Count; i++)
        {
            var item = charactersList.items[i] as CharactersListItem;
            item.Select(i == index);
        }

        var prototype = CharactersUtils.GetPrototype(characterData.prototypeId);
        selectedCharacterName.SetText(prototype.metaData.Name);
        selectedCharacterInfo.SetText(prototype.metaData.Info);

        var statsText = $"Health: {characterData.currentHealth}/{characterData.maxHealth}\n" +
            $"Experience: {characterData.currentExp}/{100}\n" +
            $"Damage: {CharactersUtils.GetCharacterDamage(characterData)}\n" +
            $"Defence:  {CharactersUtils.GetCharacterDamage(characterData)}";
        selectedCharacterStats.SetText(statsText);

        var weapon = characterData.weaponId != -1 ? InventoryUtils.GetItem(characterData.weaponId) : null;
        var armor = characterData.armorId != -1 ? InventoryUtils.GetItem(characterData.armorId) : null;

        weaponSelectionButton.SetItem(weapon);
        armorSelectionButton.SetItem(armor);
    }
}
