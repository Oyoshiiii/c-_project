[Serializable]
public class WrongNumException : Exception
{
    public WrongNumException() : base("Ошибка ввода. Такого действия или выбора нет, попробуйте снова\n") { }
    public static void WrongNum(int num, int maxNum)
    {
        if(num < 1 || num > maxNum) { throw new ArgumentException(); }
    }
}

class Program
{
    interface IGame
    {
        public void Menu();
        public void Plot();
    }
    //------------------
    class Oyoshi
    {
        static string? Name { get; set; }
        static string? Mind { get; set; }
        int MindLvl { get; set; }
        List<string> mindLvls = new List<string>() { "Рассудок в норме", "Помутнение сознания", "Потеря рассудка, дереализация" };
        static Dictionary<string, int> Inventory = new Dictionary<string, int>()
        {
            { "аптечка", 0 },
            { "венок", 0 },
            { "незабудки", 0 }
        };
        Oyoshi() { Name = "Ойоши"; MindLvl = 1; Mind = mindLvls[MindLvl - 1]; }
        
        void inventory()
        {
            int i = 1;
            foreach(var item in Inventory)
            {
                if(item.Value != 0) { Console.WriteLine($"[{i}.] {item.Key} -- {item.Value}"); }
                i++;
            }
        }
        static string info() { return $"{Name}\nРассудок: {Mind}"; }
        static void addInventory(string thing, int count) { Inventory.Add(thing, count); }
        static void deleteInventory(string thing) { Inventory.Remove(thing); }

        public static Info Info = info;
        public static Add Add = addInventory;
        public static Delete Delete = deleteInventory;

        public void menu(bool menu)
        {
            while (menu)
            {
                Console.Write("\t\t\tМеню\n[1.] Инвентарь\n[2.] Информация о персонаже\n[3.] Выйти из меню\nВвод: ");
                try
                {
                    int choice = 0;
                    UseFullMethods.Check(3, choice);
                    Console.WriteLine("\n->");
                    UseFullMethods.ClearConsole();
                    switch (choice)
                    {
                        case 1:
                            int n = 1;
                            foreach(var pair in Inventory)
                            {
                                Console.WriteLine($"[{n}.] {pair.Key} -- {pair.Value}");
                                n++;
                            }
                            Console.Write("[1.] Использовать вещь из инвентаря\n[2.] Выйти\nВвод: ");
                            if(choice == 1)
                            {
                                Console.Write("Введите номер вещи, которую хотите использовать: ");
                                UseFullMethods.Check(Inventory.Count, choice);
                                Console.WriteLine("\n->");
                                UseFullMethods.ClearConsole();
                                Console.WriteLine("Выбранный элемент используется\n->");
                                UseFullMethods.ClearConsole();
                            }
                            break;
                        case 2:
                            Info();
                            Console.Write("\n[1.] Выйти\nВвод: ");
                            UseFullMethods.Check(1, choice);
                            UseFullMethods.ClearConsole();
                            break;
                        case 3:
                            menu = false;
                            break;
                    }
                }
                catch (FormatException except)
                {
                    Console.WriteLine("Неккоректный ввод, повторите снова\n");
                }
                catch(WrongNumException except)
                {
                    Console.WriteLine(except.Message);
                }
            }
        }
    }

    delegate void Add(string thing, int count);
    delegate void Delete(string thing);
    delegate string Info();

    //--------------------
    class UseFullMethods
    {
        public static void ClearConsole() { if (Console.ReadKey().Key != null) { Console.Clear(); } }
        public static void Check(int max, int choice)
        {
            bool check = true;
            while (check)
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    WrongNumException.WrongNum(choice, max);
                    check = false;
                }
                catch (FormatException except)
                {
                    Console.WriteLine("\nНеккоректный ввод, попробуйте снова");
                }
                catch(WrongNumException except)
                {
                    Console.WriteLine(except.Message);
                }
            }
        }
    }

    //--------------------
    class Game : IGame
    {
        int end = 0;
        void Preface()
        {
            int choice = 0;
            Console.WriteLine("\t\t\t\t\tПеред началом!\n\tНажимайте любую клавишу, если видите значок -> для перехода к следующей части текста!\n->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Приятной игры\n->");
            UseFullMethods.ClearConsole();

            Console.WriteLine("Прохладное раннее утро, цветочное поле, которое еще не заполнилось солнечным светом и находится в тени\n" +
                "Среди незабудок лежит девочка, лет 12-14 наверное\n->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Чей-то взгляд разбудил ее\nОглядевшись, никого не оказалось рядом, но та продолжала осматриваться," +
                " будто ощущение чего-то присутсвия было не единственным, что пугало\n->");
            UseFullMethods.ClearConsole();
            Console.Write("[1.] Остаться на месте\n[2.] Пойти осмотреться\nВвод: ");
            UseFullMethods.Check(2, choice);
            UseFullMethods.ClearConsole();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Вы предпочли остаться и вскоре вас мгновенно унесло в глубокий сон, хотя со стороны скорее выглядело, " +
                        "что вы потеряли сознание\n->");
                    break;
                case 2:
                    Console.Write("Поле казалось бесконечным, вокруг вас ничего не было, кроме огромной, лазурного цвета, сакуры вдали\n" +
                        "[1.] Подойти\n[2.] Остаться на месте\nВвод: ");
                    UseFullMethods.Check(2, choice);
                    if (choice == 2) Console.WriteLine("Вы предпочли остаться и вскоре вас мгновенно унесло в глубокий сон, хотя со стороны " +
                        "скорее выглядело, что вы потеряли сознание\n" +
                        "Вы упалим в мягкую подушку цветов, крепко уснув\n->");
                    else
                    {
                        Console.WriteLine("Вы довольно быстро дошли до сакуры, хотя та казалась очень далеко\nКак только вы дошли," +
                            " вы поняли, что вас смщуло в этом месте еще\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Здесь напрочь отсутсвовало будто понятие времени, усталости.. возможно и других билогически " +
                            "важных факторов, как желание спать или голод\n" +
                            "С сакуры медленно опадали листья голубого, бирюзового и лазурного цветов\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Вся эта картина завораживала и вы даже не заметили, как потеряли сознание, " +
                            "упав в мягкую подушку цветов и крепко уснув\n->");
                        end++;
                    }
                    break;
            }
            UseFullMethods.ClearConsole();
            Console.WriteLine("\t\t\t\tКонец пролога\n\t\t\t\t\t->");
            UseFullMethods.ClearConsole();
        }

        protected void Part1()
        {
            int choice = 0;
            Console.WriteLine("\t\t\t\tЧасть 1\n\t\t\t\t\t->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("??? - Ойоши, хватит спать, или расскажешь может тему лучше меня?\n->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Ойоши - девушка 14 лет, главная героиня, за которую вы, собственно, и играете\n->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Вам пришлось проснуться, иначе придется и вправду рассказывать новую тему.. а с литературой вы, увы, не особо дружите\n");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Только вот... урок такой скучный..\n[1.] Ну и что, потерпеть еще полчаса можно\n" +
                "[2.] Вот именно, полчаса, никто ничего не потеряет, идем отсюда\nВвод: ");
            UseFullMethods.Check(2, choice);
            UseFullMethods.ClearConsole();
            switch (choice)
            {
                case 1:
                    end++;
                    Console.WriteLine("Вы решили остаться, получать н-ку лишний раз не особо хочется\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Пережив свои мучения, вы наконец выбираетесь из школы\nНа улице стоял приятный майский " +
                        "день, дул чуть прохладный ветер, все вокруг уже стало зеленым и школьный сад был усыпан цветами\nРомашки, одуванчики, пару роз даже было..\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("..и незабудки.. в тени они особенно красивы\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Школа была совсем небольшой, как и все ваше поселение.. по сути это надо было бы назвать городом," +
                        " но у падших, к сожалению, не всегда есть возможность организовать городскую стркутуру\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Почему бы не прогуляться с приятной музыкой ы наушниках по парку?\n[1.] Прогуляться по парку\n" +
                        "[2.] Боюсь, для прогулок у меня сильная усталость..\nВвод: ");
                    UseFullMethods.Check(2, choice);
                    break;
                case 2:
                    break;
            }
        }
        protected void Part2()
        {

        }
        protected void End()
        {
            int end_type = 0, choice = 0;
            if (end < 0) { end_type = 1; }
            else if (end >= 0 && end <= 5) {  end_type = 2; }
            else { end_type = 3; }

            switch (end_type)
            {
                case 1:

                    break;
                case 2:
                    Console.WriteLine("Темно. Холодно. Сыро\nДвижения что-то сковывало или, быть может, у вас было лишьощущение того, что вы пытались двигаться" +
                        "\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Открыть глаза вам удалось лишь спустя время и уйму усилий\nМедленно восстанавливая фокус зрения, вы смогли понять" +
                        ", что находитесь в каком-то бескрайнем водоеме.. бескрайнем исключительно относительно горизонта\n" +
                        "Вглубь этот водоем был примерно по щиколотки\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("\n[1.] Подняться и осмотреться\n[2.] Остаться лежать в воде\nВвод: ");
                    UseFullMethods.Check(2, choice);
                    UseFullMethods.ClearConsole();
                    if(choice == 2)
                    {
                        Console.WriteLine("Вы перевернулись на спину и остались наблюдать за тем, как плывут облака\nВаше сознание будто постепенно отделялось от вас," +
                            "мысли таяли, теперь вы уже действительно не могли больше пошевелиться или хотя бы подумать об этом\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Ваше сознание медленно растворялось в чем-то легком и бескрайнем, как и все это место\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Хотя, быть может это ваше тело наоборот растворялось внутри вашего же сознания, которое вас добродушно заперло внутри себя" +
                            ", дабы защитить от внешнего мира?\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("?..\n\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Конец");
                    }
                    break;
                case 3:
                    break;
            }
        }
        public void Menu() { }
        public void Plot()
        {
            Preface();
            Part1();
            Part2();
        }
    }

    static void Main(string[] args)
    {

    }
}