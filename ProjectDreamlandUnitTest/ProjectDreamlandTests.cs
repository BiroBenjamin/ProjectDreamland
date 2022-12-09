using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Items;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Handlers;
using ProjectDreamland.Managers;

namespace ProjectDreamlandUnitTest
{
  [TestClass]
  public class ProjectDreamlandTests
  {
    [TestMethod]
    public void TestDamageDone()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();
      testCharacter.MaxHealthPoints = 100;
      testCharacter.CurrentHealthPoints = 100;

      Player testPlayer = new Player();
      testPlayer.AttackDamage = (10, 15);
      Player.MeleeAttack = new MeleeAttack("", "", ResourceTypesEnum.None, 0, 15, DamageTypesEnum.Physical, AbilityTypesEnum.Damage, 0, 0);

      //Act
      testPlayer.Attack(testCharacter, Player.MeleeAttack);
      Console.WriteLine(testCharacter.CurrentHealthPoints);

      //Assert
      Assert.IsTrue(testCharacter.CurrentHealthPoints <= 100 - (int)(15 * .8 + 10 * .8));
      Assert.IsTrue(testCharacter.CurrentHealthPoints >= 100 - (int)(15 * 1.2 + 15 * 1.2));
    }

    [TestMethod]
    public void TestHealingDone()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();

      Player testPlayer = new Player();
      testPlayer.AttackDamage = (10, 15);
      testPlayer.MaxHealthPoints = 125;
      testPlayer.CurrentHealthPoints = 50;
      Player.NurturingWinds = new HealAbility("", "", ResourceTypesEnum.None, 0, 24, DamageTypesEnum.Nature, AbilityTypesEnum.Heal, 0, 0);

      //Act
      Player.NurturingWinds.Cast(new List<BaseCharacter> { testCharacter }, testPlayer);
      Console.WriteLine(testPlayer.CurrentHealthPoints);

      //Assert
      Assert.IsTrue(testPlayer.CurrentHealthPoints <= 50 + (int)(24 * 1.3 + 15 * 1.3));
      Assert.IsTrue(testPlayer.CurrentHealthPoints > 50);
    }

    [TestMethod]
    public void TestOverHeal()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();

      Player testPlayer = new Player();
      testPlayer.AttackDamage = (10, 15);
      testPlayer.MaxHealthPoints = 125;
      testPlayer.CurrentHealthPoints = 125;
      Player.NurturingWinds = new HealAbility("", "", ResourceTypesEnum.None, 0, 24, DamageTypesEnum.Nature, AbilityTypesEnum.Heal, 0, 0);

      //Act
      Player.NurturingWinds.Cast(new List<BaseCharacter> { testCharacter }, testPlayer);
      Console.WriteLine(testPlayer.CurrentHealthPoints);

      //Assert
      Assert.IsTrue(testPlayer.CurrentHealthPoints <= testPlayer.MaxHealthPoints);
    }

    [TestMethod]
    public void TestResourceDecrease()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();

      Player testPlayer = new Player();
      testPlayer.AttackDamage = (10, 15);
      testPlayer.MaxHealthPoints = 125;
      testPlayer.CurrentHealthPoints = 125;
      testPlayer.MaxResourcePoints = 100;
      testPlayer.CurrentResourcePoints = 100;
      Player.NurturingWinds = new HealAbility("", "", ResourceTypesEnum.Mana, 35, 24, DamageTypesEnum.Nature, AbilityTypesEnum.Heal, 0, 0);

      //Act
      Player.NurturingWinds.Cast(new List<BaseCharacter> { testCharacter }, testPlayer);
      Console.WriteLine(testPlayer.CurrentResourcePoints);

      //Assert
      Assert.AreEqual(testPlayer.MaxResourcePoints - Player.NurturingWinds.Cost, 65);
    }

    [TestMethod]
    public void TestHasEnoughResources()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();

      Player testPlayer = new Player();
      testPlayer.AttackDamage = (10, 15);
      testPlayer.MaxHealthPoints = 125;
      testPlayer.CurrentHealthPoints = 50;
      testPlayer.MaxResourcePoints = 100;
      testPlayer.CurrentResourcePoints = 20;
      Player.NurturingWinds = new HealAbility("", "", ResourceTypesEnum.Mana, 35, 24, DamageTypesEnum.Nature, AbilityTypesEnum.Heal, 0, 0);

      //Act
      Player.NurturingWinds.Cast(new List<BaseCharacter> { testCharacter }, testPlayer);
      Console.WriteLine(testPlayer.CurrentResourcePoints);

      //Assert
      Assert.AreEqual(testPlayer.CurrentResourcePoints, 20);
      Assert.AreEqual(testPlayer.CurrentHealthPoints, 50);
    }

    [TestMethod]
    public void TestExperienceIncrease()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();
      testCharacter.Level = 1;
      testCharacter.CharacterState = CharacterStatesEnum.Dying;

      Player testPlayer = new Player();
      testPlayer.ExperienceNeeded = 100;
      Player.CurrentExperience = 13;

      //Act
      CharacterManager.HandleDeadCharacters(new List<BaseCharacter> { testCharacter });
      Console.WriteLine(Player.CurrentExperience);

      //Assert
      Assert.AreEqual(Player.CurrentExperience, 13 + (int)Math.Pow(testCharacter.Level * 5, 1.1));
    }

    [TestMethod]
    public void TestLevelIncrease()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();
      testCharacter.Level = 10;
      testCharacter.CharacterState = CharacterStatesEnum.Dying;

      Player testPlayer = new Player();
      testPlayer.BaseStats = new Stats();
      testPlayer.Level = 1;
      testPlayer.ExperienceNeeded = 100;
      Player.CurrentExperience = 50;

      //Act
      CharacterManager.HandleDeadCharacters(new List<BaseCharacter> { testCharacter });
      testPlayer.HandleLevel();
      Console.WriteLine(Player.CurrentExperience);

      //Assert
      Assert.AreEqual(testPlayer.Level, 2);
      Assert.AreEqual(Player.CurrentExperience, 50 + (int)Math.Pow(testCharacter.Level * 5, 1.1) - 100);
    }

    [TestMethod]
    public void TestSetToDead()
    {
      //Arrange
      BaseCharacter testCharacter = new BaseCharacter();
      testCharacter.Level = 10;
      testCharacter.CharacterState = CharacterStatesEnum.Dying;

      //Act
      CharacterManager.HandleDeadCharacters(new List<BaseCharacter> { testCharacter });
      Console.WriteLine(testCharacter.CharacterState);

      //Assert
      Assert.AreEqual(testCharacter.CharacterState, CharacterStatesEnum.Dead);
    }

    [TestMethod]
    public void TestAddStats()
    {
      //Arrange
      Player testPlayer = new Player();
      testPlayer.BaseStats = new Stats(healtPoints: 100, manaPoints: 64);
      testPlayer.Level = 1;
      testPlayer.ExperienceNeeded = 100;
      Player.CurrentExperience = 125;

      //Act
      testPlayer.HandleLevel();
      Console.WriteLine(testPlayer.BaseStats);

      //Assert
      Assert.AreEqual(testPlayer.BaseStats.HealthPoints, 100 + (int)Math.Pow(100, .3));
      Assert.AreEqual(testPlayer.BaseStats.ManaPoints, 64 + (int)Math.Pow(64, .3));
    }

    [TestMethod]
    public void TestSetPosition()
    {
      //Arrange
      Player testPlayer = new Player();
      testPlayer.Position = new System.Drawing.Point(0, 0);

      System.Drawing.Point setTo = new System.Drawing.Point(123, 35);

      //Act
      testPlayer.SetPosition(setTo);
      Console.WriteLine(testPlayer.Position);

      //Assert
      Assert.AreEqual(testPlayer.Position, setTo);
    }

    [TestMethod]
    public void TestEnemyAI()
    {
      //Arrange
      Player testPlayer = new Player();
      testPlayer.Position = new System.Drawing.Point(0, 0);

      BaseCharacter testCharacter1 = new BaseCharacter();
      testCharacter1.Position = new System.Drawing.Point(10, 10);
      testCharacter1.CharacterAffiliation = CharacterAffiliationsEnum.Hostile;
      testCharacter1.BehaviourState = BehaviourStatesEnum.Idle;

      BaseCharacter testCharacter2 = new BaseCharacter();
      testCharacter2.Position = new System.Drawing.Point(200, 200);
      testCharacter2.CharacterAffiliation = CharacterAffiliationsEnum.Hostile;
      testCharacter2.BehaviourState = BehaviourStatesEnum.Idle;

      AIHandler aiHandler = new AIHandler(testCharacter1);

      //Act
      aiHandler.HandleEnemyAI(new List<BaseObject> { testPlayer });
      Console.WriteLine(testCharacter1.BehaviourState);
      Console.WriteLine(testCharacter2.BehaviourState);

      //Assert
      Assert.AreEqual(testCharacter1.BehaviourState, BehaviourStatesEnum.Chase);
      Assert.AreEqual(testCharacter2.BehaviourState, BehaviourStatesEnum.Idle);
    }

    [TestMethod]
    public void TestCommandManager()
    {
      //Arrange
      WorldObject testObject = new WorldObject();
      testObject.ZIndex = 10;
      testObject.Instructions = "OnLoad;alwaysOnTop(true);OnLoadEnd;";

      //Act
      CommandManager.LoadCommand(testObject.Instructions, testObject, CommandLoadStateEnum.OnLoad);
      Console.WriteLine(testObject.ZIndex);

      //Assert
      Assert.AreEqual(testObject.ZIndex, int.MaxValue);
    }

    [TestMethod]
    public void TestEquipmentTaken()
    {
      //Arrange
      EquipmentManager.HeadSlot = new Armor("", "", ItemTypesEnum.Head, null, null);

      //Act
      EquipmentManager.TakeEquipment(ItemTypesEnum.Head);
      Console.WriteLine(EquipmentManager.HeadSlot);

      //Assert
      Assert.IsNull(EquipmentManager.HeadSlot);
    }

    [TestMethod]
    public void TestSwapEquipment()
    {
      //Arrange
      Armor testArmorTaken = new Armor("testArmorTaken", "", ItemTypesEnum.Head, null, null);
      Armor testArmorPlaced = new Armor("testArmorPlaced", "", ItemTypesEnum.Head, null, null);
      EquipmentManager.HeadSlot = testArmorTaken;

      //Act
      EquipmentManager.SwapEquipment(testArmorPlaced);
      Console.WriteLine(EquipmentManager.HeadSlot);

      //Assert
      Assert.AreEqual(testArmorPlaced, EquipmentManager.HeadSlot);
    }

    [TestMethod]
    public void TestAddToInventory()
    {
      //Arrange
      Item testItem = new Item("", "", ItemTypesEnum.Quest, null, false);

      //Act
      InventoryManager.AddItem(testItem);
      Console.WriteLine(InventoryManager.Items[0]);

      //Assert
      Assert.AreEqual(testItem, InventoryManager.Items[0]);

      //Clean up
      InventoryManager.RemoveItem(testItem);
    }

    [TestMethod]
    public void TestRemoveFromInventory()
    {
      //Arrange
      Item testItem = new Item("", "", ItemTypesEnum.Quest, null, false);
      InventoryManager.AddItem(testItem);

      //Act
      InventoryManager.RemoveItem(testItem);
      Console.WriteLine(InventoryManager.Items[0]);

      //Assert
      Assert.IsNull(InventoryManager.Items[0]);
    }
  }
}