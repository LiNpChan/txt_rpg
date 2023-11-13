using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Character player;
        private static Item[] inventory;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro(); // 게임 인트로를 실행합니다.
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            Item armor = new Item("무쇠갑옷", 0, 5);
            Item sword = new Item("낡은 검", 2, 0);

            inventory = new Item[] { armor, sword }; // 현재 인벤토리 안에 있는 아이템 입니다.
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전선으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 게임종료");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3); // 1번은 캐릭터 정보, 2번은 인벤토리로 이동합니다.
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력: {player.Atk}");
            Console.WriteLine($"방어력: {player.Def}");
            Console.WriteLine($"체력: {player.Hp}");
            Console.WriteLine($"Gold: {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory() // 캐릭터가 보유중인 아이템 및 장착중인 아이템을 [E]로 표현합니다.
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("캐릭터가 보유중인 아이템을 표시합니다.");
            Console.WriteLine();

            for (int i = 0; i < inventory.Length; i++)
            {
                string equipStatus = (i + 1).ToString();
                if (inventory[i].IsEquipped)
                {
                    equipStatus += " [E]";
                }

                Console.WriteLine($"{i + 1} {inventory[i].GetEquippedStatus()}{inventory[i].ItemName} | {GetItemDescription(inventory[i])}"); // 아이템과 아이템의 스텟, 설명을 프린트합니다.
            }
            Console.WriteLine("3. 장착 관리");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");


            int input = CheckValidInput(0, 3); // 만약 아이템이 추가되면 case 3: 부분을 수정해야함
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 3:
                    DisplayEquipManagement();
                    break;

                default:
                    EquipItem(input - 1);
                    break;
            }
        }

        static string GetItemDescription(Item item) // 장비의 설명을 나타내기 위한 메서드 입니다.
        {
            if (item.ItemName == "무쇠갑옷")
            {
                return "방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷입니다.";
            }
            else if (item.ItemName == "낡은 검")
            {
                return "공격력 +2 | 쉽게 볼 수 있는 낡은 검입니다.";
            }

            return string.Empty; // 만약 장비에 대한 설명이 없으면 빈 문자열을 나타냄
        }

        static void DisplayEquipManagement() // 아이템의 장착과 해제를 관리합니다.
        {
            Console.Clear();
            Console.WriteLine("장착 관리");
            Console.WriteLine("아이템의 장착과 해제를 관리합니다.");
            Console.WriteLine();

            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine($"{i + 1} {inventory[i].GetEquippedStatus()}{inventory[i].ItemName} | {GetItemDescription(inventory[i])}");
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, inventory.Length);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;

                default:
                    ToggleEquipStatus(input - 1);
                    DisplayEquipManagement();
                    break;
            }
        }

        static void EquipItem(int index)
        {
            if (inventory[index].IsEquipped)
            {
                Console.WriteLine("이미 장착된 아이템입니다.");
            }
            else
            {
                player.Atk += inventory[index].AtkBonus; // 장비가 만약 장착중이면 캐릭터의 Atk와 Def에 장비의 추가 수치만큼 수치를 증가시킵니다.
                player.Def += inventory[index].DefBonus;
                inventory[index].IsEquipped = true;
            }
            DisplayInventory();
        }

        static void DisplayEquipMenu(int itemIndex)
        {
            Console.Clear();
            Console.WriteLine($"1. {inventory[itemIndex].GetEquippedStatus()} 아이템 {inventory[itemIndex].ItemName}");
            Console.WriteLine("2. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    ToggleEquipStatus(itemIndex);
                    DisplayEquipMenu(itemIndex);
                    break;

                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void ToggleEquipStatus(int itemIndex) // 아이템이 장착될시 스탯을 업데이트 해줍니다.
        {
            if (inventory[itemIndex].IsEquipped)
            {
                player.Atk -= inventory[itemIndex].AtkBonus;
                player.Def -= inventory[itemIndex].DefBonus;
                inventory[itemIndex].IsEquipped = false;
            }
            else
            {
                player.Atk += inventory[itemIndex].AtkBonus;
                player.Def += inventory[itemIndex].DefBonus;
                inventory[itemIndex].IsEquipped = true;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
    public class Item
    {
        public string ItemName { get; set; }
        public int AtkBonus { get; set; }
        public int DefBonus { get; set; }
        public bool IsEquipped { get; set; }


        public Item(string itemname, int atkbonus, int defbonus)
        {
            ItemName = itemname;
            AtkBonus = atkbonus;
            DefBonus = defbonus;
            IsEquipped = false;
        }

        public string GetEquippedStatus()
        {
            return IsEquipped ? "[E]" : "[ ]";
        }
    }

}
