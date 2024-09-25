using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using static System.Collections.Specialized.BitVector32;

namespace RtanRPG
{
    internal class Program
    {
       static Item[] shopItems = new Item[]
            {
                new ("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", 1000, 0, 5),
                new ("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", 1800, 0, 9),
                new ("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 0, 15),
                new ("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", 600, 2, 0),
                new ("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", 1500, 5, 0),
                new ("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2700, 7, 0)
            };

        static void Main()
        {
            int level = 1;
            int attack = 10;
            int defense = 5;
            int health = 100;
            int gold = 1500;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다!");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.WriteLine();
            int action = int.Parse(Console.ReadLine());
            if (action == 1)
            {
                Status(level, ref attack, ref defense, health, ref gold);
            }
            else if (action == 2)
            {
                Inven();
            }
            else if (action == 3)
            {
                ItemShop(ref gold);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Main();
            }
        }

        static void Status(int level, ref int attack, ref int defense, int health, ref int gold)
        {

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine("Chad ( 전사 )");
            for (int i = 0; i < shopItems.Length; i++)
            {
                Item item = shopItems[i];
                if (item.IsEquip)
                    {
                        Console.WriteLine($"공격력 : {attack + item.Attack}(+{item.Attack}");
                        Console.WriteLine($"방어력 : {defense + item.Defense}(+{item.Defense})");
                    }
                else
                {
                    Console.WriteLine($"공격력 : {attack}");
                    Console.WriteLine($"방어력 : {defense}");
                    break;
                }
            }
            Console.WriteLine($"체 력 : {health}");
            Console.WriteLine($"Gold : {gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요: ");
            Console.WriteLine();
            int action = int.Parse(Console.ReadLine());
            if (action == 0)
            {
                Main();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Status(level, ref attack, ref defense, health, ref gold);
            }
        }
        static void Inven()
        {           
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Length; i++)
            {
                Item item = shopItems[i];
                if (item.IsPurchased)
                {
                    if (item.IsEquip)
                    {
                        Console.WriteLine($"-[E]{item.Name} | {item.Attribute} | {item.Description}");
                    }
                    else
                    { 
                        Console.WriteLine($"-{item.Name} | {item.Attribute} | {item.Description}");
                    }
                }

            }
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.WriteLine();
            int action = int.Parse(Console.ReadLine());
            if (action == 0)
            {
                Main();
            }
            else if(action == 1)
            {
                Equipment();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Inven();
            }
        }
        static void Equipment()
        {
            Console.WriteLine("인벤토리-장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Length; i++)
            {
                Item item = shopItems[i];
                if (item.IsPurchased)
                {
                    if (item.IsEquip)
                    {
                        Console.WriteLine($"-{i + 1} [E]{item.Name} | {item.Attribute} | {item.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"-{i + 1} {item.Name} | {item.Attribute} | {item.Description}");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("장착하고 싶은 아이템 번호를 입력하세요.");
            Console.WriteLine();
            int action = int.Parse(Console.ReadLine())-1;
            if (action < 0)
            {
                Inven();
            }
            else if (action >= 0 && action < shopItems.Length)
            {
                Item equipItem = shopItems[action];
                equipItem.Equip();
                Equipment();
            }

            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Equipment();
            }
        }


        static void ItemShop(ref int gold)
        {
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine($"[보유 골드]\n{gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Length; i++)
            {
                Item item = shopItems[i];
                if (item.IsPurchased)
                {
                    Console.WriteLine($"- {item.Name} | {item.Attribute} | {item.Description} | [구매완료]");
                }
                else
                {
                    Console.WriteLine($"- {item.Name} | {item.Attribute} | {item.Description} | {item.Price} G");
                }
            }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요: ");
                Console.WriteLine();
                int action = int.Parse(Console.ReadLine());
                if (action == 0)
                {
                    Main();
                }
                else if (action == 1)
                {
                    BuyItem(ref gold, shopItems);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ItemShop(ref gold);
                }
            }
            static void BuyItem(ref int gold, Item[] shopItems)
            {
                Console.WriteLine("상점-아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine($"[보유 골드]\n{gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Length; i++)
                {
                    Item item = shopItems[i];
                    if (item.IsPurchased)
                    {
                        Console.WriteLine($"{i + 1}. {item.Name} | {item.Attribute} | {item.Description} | [구매완료]");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {item.Name} | {item.Attribute} | {item.Description} | {item.Price} G");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("구매하실 아이템 번호를 입력해주세요: ");
                Console.WriteLine();

                int choice = int.Parse(Console.ReadLine()) - 1;
                if (choice == -1)
                {
                    ItemShop(ref gold);
                }
                else if (choice >= 0 && choice < shopItems.Length)
                {
                    Item selectedItem = shopItems[choice];

                    if (gold >= selectedItem.Price)
                    {
                        gold -= selectedItem.Price;
                        selectedItem.Purchased();
                        Console.WriteLine("구매를 완료했습니다!");
                        Console.WriteLine();
                        BuyItem(ref gold, shopItems);

                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    BuyItem(ref gold, shopItems);
                }

            }
        }
        public class Item
        {
            public string Name { get; }
            public string Attribute { get; }
            public string Description { get; }
            public int Price { get; }
            public int Attack { get; }
            public int Defense { get; }
            public bool IsPurchased { get; private set; } = false;
            public bool IsEquip {  get; private set; } = false;

            public Item(string name, string attribute, string description, int price, int attack, int defense)
            {
                Name = name;
                Attribute = attribute;
                Description = description;
                Price = price;
                Attack = attack;
                Defense = defense;
            }
            public void Purchased()
            {
                IsPurchased = true;
            }
            public void Equip()
            {
                IsEquip = true;
            }
            
        }
}



