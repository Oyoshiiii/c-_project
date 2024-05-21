[Serializable]
public class WrongNumException : Exception
{
    public WrongNumException() : base("Ошибка ввода. Такого действия или выбора нет, попробуйте снова\n") { }
    public static void WrongNum(int num, int maxNum)
    {
        if(num < 1 || num > maxNum)
        {
            throw new WrongNumException();
        }
    }
}

class Program
{
    interface IGame
    {
        public bool Menu(bool continue_the_game, string thingToUse);
        public void Plot();
    }
    //------------------
    class Oyoshi
    {
        static string? Name { get; set; }
        static Dictionary<string, int> Inventory = new Dictionary<string, int>()
        {
            { "венок", 0 },
            { "кошачий корм", 0 }
        };
        public  Oyoshi() { Name = "Ойоши"; }
        
        void inventory()
        {
            int i = 1;
            foreach(var item in Inventory)
            {
                if(item.Value != 0) { Console.WriteLine($"[{i}.] {item.Key} -- {item.Value}"); }
                i++;
            }
        }
        static void addInventory(string thing, int count)
        {
            if (Inventory.ContainsKey(thing))
            {
                Inventory[thing] += count;
            }
            else { Inventory.Add(thing, count); }
        }
        static string deleteInventory(string thing)
        {
            int value = Inventory[thing];
            if (value - 1 == 0)
            {
                Inventory.Remove(thing);
            }
            else if(value - 1 > 0)
            {
                Inventory[thing] = value - 1;
            }
            else
            {
                Console.WriteLine("У вас нет этой вещи");
                thing = "";
            }
            return thing;
        }
        public static Add Add = addInventory;
        public static Delete Delete = deleteInventory;

        public bool menu(bool menu, string useThing, string thingToUse, bool thing)
        {
            while (menu)
            {
                Console.Write("\t\t\tМеню\n[1.] Инвентарь\n[2.] Выйти из меню\nВвод: ");
                try
                {
                    int choice = 0;
                    choice = UseFullMethods.Check(2, choice);
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            int n = 1;
                            foreach(var pair in Inventory)
                            {
                                Console.WriteLine($"[{n}.] {pair.Key} -- {pair.Value}");
                                n++;
                            }
                            Console.Write("\n\n[1.] Использовать вещь из инвентаря\n[2.] Выйти\nВвод: ");
                            choice = UseFullMethods.Check(2, choice);
                            Console.WriteLine();
                            if (choice == 1)
                            {
                                Console.Write("Введите номер вещи, которую хотите использовать: ");
                                UseFullMethods.Check(Inventory.Count, choice);
                                Console.Clear();
                                useThing = Inventory.ElementAt(choice - 1).Key;
                                if (useThing == thingToUse) 
                                { 
                                    thing = true; 
                                    Delete(useThing); 
                                    if(useThing != "")
                                    {
                                        Console.WriteLine("Выбранный элемент используется\n");
                                    }
                                }
                                else
                                {
                                    thing = false;
                                    Console.WriteLine("Это мы возьмем в следующий раз\n");
                                }
                            }
                            break;
                        case 2:
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
            return thing;
        }
    }

    delegate void Add(string thing, int count);
    delegate string Delete(string thing);
    delegate string Info();

    //--------------------
    class UseFullMethods
    {
        public static void ClearConsole() { if (Console.ReadKey().Key != null) { Console.Clear(); } }
        public static int Check(int max, int choice)
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
            return choice;
        }
    } 

    //--------------------
    class Game : IGame
    {
        int end = 0;
        Oyoshi Oyo;
        public Game(Oyoshi Oyo) { this.Oyo = Oyo; }
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
            choice = UseFullMethods.Check(2, choice);
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Вы предпочли остаться и вскоре вас мгновенно унесло в глубокий сон, хотя со стороны скорее выглядело, " +
                        "что вы потеряли сознание\n->");
                    break;
                case 2:
                    Console.Write("Поле казалось бесконечным, вокруг вас ничего не было, кроме огромной, лазурного цвета, сакуры вдали\n" +
                        "[1.] Подойти\n[2.] Остаться на месте\nВвод: ");
                    choice = UseFullMethods.Check(2, choice);
                    Console.Clear();
                    if (choice == 2) Console.WriteLine("Вы предпочли остаться и вскоре вас мгновенно унесло в глубокий сон, хотя со стороны " +
                        "скорее выглядело, что вы потеряли сознание\n" +
                        "Вы упалим в мягкую подушку цветов, крепко уснув\n->");
                    else
                    {
                        Console.WriteLine("Вы довольно быстро дошли до сакуры, хотя та, казалось, была очень далеко\nКак только вы дошли," +
                            " вы поняли, что вас смщуло в этом месте еще\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Здесь напрочь отсутсвовало будто понятие времени, усталости.. возможно и других билогически " +
                            "важных факторов, как желание спать или голод\n" +
                            "С сакуры медленно опадали листья голубого, бирюзового и лазурного цветов\n->");
                        UseFullMethods.ClearConsole();
                        Console.Write("У подножья сакуры находился маленький венок, сплетенный из незабудок. Он был весь усеян лепестками сакуры и был настолько" +
                            " маленьким, что больше походил на цветочный браслет\n[1.] Взять венок\n[2.] Не трогать\nВвод: ");
                        choice = UseFullMethods.Check(2, choice);
                        Console.Clear();

                        if (choice == 1)
                        {
                            Oyoshi.Add("венок", 1);
                            Console.WriteLine("Вы подняли венок. Он был невообразимо мягок и легок, казалось, что он и вовсе нереален\n->");
                            UseFullMethods.ClearConsole();
                            Console.WriteLine("Как и все здесь\n->");
                            UseFullMethods.ClearConsole();
                        }
                        
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
        protected void MainPlotPart()
        {
            int choice = 0;
            Console.WriteLine("\t\t\t\tОйоши\n\t\t\t\t->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("??? - Ойоши, хватит спать, или расскажешь может тему лучше меня?\n->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Ойоши - девушка 14 лет, главная героиня, за которую вы, собственно, и играете\n->");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Вам пришлось проснуться, иначе придется и вправду рассказывать новую тему.. а с литературой вы, увы, не особо дружите\n");
            UseFullMethods.ClearConsole();
            Console.WriteLine("Только вот... урок такой скучный..\n[1.] Ну и что, потерпеть еще полчаса можно\n" +
                "[2.] Вот именно, полчаса, никто ничего не потеряет, идем отсюда\nВвод: ");
            choice = UseFullMethods.Check(2, choice);
            Console.Clear();
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
                    choice = UseFullMethods.Check(2, choice);
                    Console.Clear();
                    break;
                case 2:
                    end--;
                    Console.WriteLine("Ну и правильно, на улице такая прекрасная погода, какой смысл тут сидеть и помирать от скуки дальше?\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Дабы не попасться никому на глаза, вы тихонько собрали вещи и, когда преподаватель вышел, попросту испарились из кабинета" +
                        ", обходя все коридоры окольными путями, чтобы дойти до выхода из школы\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Домой идти смысла нет, почему бы не прогуляться до парка?\n[1.] Хорошая идея\n[2.] Нет, усталость выше моего желания гулять\n" +
                        "Ввод: ");
                    choice = UseFullMethods.Check(2, choice);
                    Console.Clear();
                    break;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("В парке на удивление никого не было. Тишина, спокойствие\nУсевшись на скамейке, которая скрывалась в тени густой кроны деревьев" +
                        ", вы достали наушники и только уже хотели включить музыку");
                    UseFullMethods.ClearConsole();
                    //Ойо встречает котенка, над которым поиздевались люди. 
                    //Ойоши идет в магазин за кормом, чтобы накормить кота, если она его не кормит, то end--, и она идет домой, где ее уже и похищают
                    //Ойо кормит котика -> end++, девушка сидит рядом с котиком и следит за ним, затем засыпает и после ее похищают
                    break;
                case 2:
                    Console.WriteLine("");
                    //по пути домой, она встретит на земле потертое лезвие
                    //если она его возьмет, то end++ и при попытке сбежать от тех, кто ее будет похищать, она сможет выиграть время => шанс на другую концовку
                    //если не берет, то уend-- ее быстро похищают и при попытке сбежать, ей ничего не остается сделать, кроме как сдаться
                    UseFullMethods.ClearConsole();
                    break;
            }
            //описание ее похищения, как ее до туда везли, описание окружения
        }
        protected void End()
        {
            int end_type = 0, choice = 0;
            if (end < 0) { end_type = 1; }
            else if (end >= 0 && end < 2) {  end_type = 2; }
            else { end_type = 3; }

            switch (end_type)
            {
                case 1:
                    Console.WriteLine("Шум, темнота, разговоры, касания\nВас кто-то уложил на холодную койку, привязав к ней и завязав глаза\nЕще пару мгновений, " +
                        "вы перестали двигаться и чувствовать свое тело\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Но вы все слышали\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Слышали лязг метала, слышали капание крови и мерзкий хруст из-под скальпеля, разрезающий плоть\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Три, два, один, вы все же теряете сознание\n->");
                    UseFullMethods.ClearConsole();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\tВ мае пропала одна из падших, являвшаяся ученицей 8-9 класса\nТело найдено не было, девушка объявлена все " +
                        "еще пропавшей без вести\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("\t\tЖители района, в котором пропала девушка предполагают, что возможно она стала очередной жертвой детской продажи органов\n->");
                    UseFullMethods.ClearConsole();
                    Console.ResetColor();
                    Console.WriteLine("\t\tИ они были абсолютно правы\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Ойо действительно стала жертовй организации по продажи детских органов\nВ ходе операции ей успели вырезать пару не жизненноважных органов" +
                        " и начать ампутацию ноги\n");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Но в ходе операции что-то произошло и спустя время она очнулась в полном одиночестве\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Именно так и начался ее путь становления пятой падшей\nПытаясь сбежать, она успела найти в одном из шкафчиков какой-то потрепанный" +
                        " протез для ноги\nА следующие 5 лет она, сбежав ото всех в заброшенную часть поселения падших, заменяла свои выжившие и не очень части тела на протезы" +
                        " и биомеханические протезы, стараясь попросту выжить и стать снова 'нормальной'\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("В итоге, почти 70% ее тело теперь роботизированно и она стала пятой падшей, совсем того не желая\nЕй просто хотелось избавиться от боли и страха" +
                        " за свою жизнь\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Конец");
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
                    choice = UseFullMethods.Check(2, choice);
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
                    else
                    {
                        Console.WriteLine("Вы осмотрелись вокруг, казалось, что этому месту нет ни конца, ни края. Что весь этот мир - этот водоем\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Но все же, вдали вы смогли увидеть будто бы небольшую возвышенность, островок?\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Вы добежали до этого островка\nУдивительно, но за все время бега вы не испытали ни капли усталости.. хотя бежали достаточно долго\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Или вам только показалось то, что вы бежали долго?..\n->");
                        UseFullMethods.ClearConsole();
                        Console.Write("Существует ли вообще понятие времени и зависимости физических потребностей в месте, которое является ____???????" +
                            ": ");
                        string answ = Console.ReadLine();
                        if (answ == "сознанием" || answ == "Сознанием") { UseFullMethods.ClearConsole(); Console.WriteLine("\nУдивительно, вы правы\n->"); }
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Весь островок был покрыт полем незабудок, над ним, в воздухе, парили некие существа, схожие с медузами, но в тоже" +
                            " время они были настолько невесомыми, легкими и прозрачными, что казалось, что это пары дым\nЛишь ултрамариновый отблеск в тени их выделял" +
                            " и позволял увидеть\n");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Что-то в этом мирке манило, затуманивало разум, успокаивало..\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("В центре плоя стояла распуствшаяся лазурная сакура, листья которых медленно опадали\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Наблюдая за красотой его места, вы не заметили, как потеряли сознание, упав в мягкую подушку цветов и трав\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Прохладное раннее утро, цветочное поле, которое еще не заполнилось солнечным светом и находится в тени\n" +
                "Среди незабудок лежит девочка, лет 12-14 наверное\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Чей-то взгляд разбудил ее\nОглядевшись, никого не оказалось рядом, но та продолжала осматриваться," +
                            " будто ощущение чего-то присутсвия было не единственным, что пугало..\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Конец\n");
                    }
                    break;
                case 3:
                    Console.WriteLine("Сакура, бескрайнее цветочное поле\nВы были тут когда-то. Да, точно были\nВсе было до жути знакомым, теплым, легким\n->");
                    UseFullMethods.ClearConsole();
                    Console.WriteLine("Будто совсем недавно вы стояли здесь же, что-то здесь было важное..\n");
                    bool contTheGame = false;
                    bool charecter_menu = Menu(contTheGame, "венок");
                    if (charecter_menu)
                    {
                        Console.WriteLine("Точно, вы же подняли венок в одном из своем возвращении в это место\n->");
                        UseFullMethods.ClearConsole();
                        Console.Write("Кстати, вы ведь знаете, что вы в вашем ____?????\n ");
                        string answ = Console.ReadLine();
                        if(answ == "сознании" || answ == "Сознании")
                        {
                            Console.WriteLine("\nХорошо, значит еще не все потеряно\n->");
                            UseFullMethods.ClearConsole();
                        }
                        else
                        {
                            Console.WriteLine("Ничего, быть может еще вспомните\n->");
                            UseFullMethods.ClearConsole();
                        }
                        Console.WriteLine("Надень венок на руку\n[1.] Хорошо\n[2.] Нет\nВвод:");
                        choice = UseFullMethods.Check(2, choice);
                        if(choice == 1)
                        {
                            Console.WriteLine("Стоило вам надеть венок на руку, как в глазах мгновенно потемнело\nВас будто с силой вытолкнули в темноту, " +
                                "ударив в грудь\n->");
                            UseFullMethods.ClearConsole();
                            Console.WriteLine("Вы с криком просыпаетесь, оглядываетесь вокруг и тотчас же падаете на пол, пытаясь встать\n->");
                            UseFullMethods.ClearConsole();
                            Console.WriteLine("Правая нога не двигалась и вы с трудом, опираясь на руки и задыхаясь, поползли к ближайшей двери\n->");
                            UseFullMethods.ClearConsole();
                            Console.WriteLine("Конец");
                        }
                        else
                        {
                            Console.WriteLine("Тебя можно понять, мало кто захотел бы возвращаться, зная, что с ним так обошлись..\n->");
                            UseFullMethods.ClearConsole();
                            Console.WriteLine("В таком случае, добро пожаловать в твой мир спокойствия и бескрайней свободы\n->");
                            UseFullMethods.ClearConsole();
                            Console.WriteLine("Конец");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нет, видимо тебе показалось\n->");
                        Console.WriteLine("В таком случае, добро пожаловать в твой мир спокойствия и бескрайней свободы\n->");
                        UseFullMethods.ClearConsole();
                        Console.WriteLine("Конец");
                    }
                    break;
            }
        }
        public bool Menu(bool continue_the_game, string thingToUse)
        {
            string? usingThing = "";
            bool Menu = true;
            while (Menu)
            {
                Console.Clear();
                Console.Write($"[1.] Меню персонажа\t\t[2.] Выйти из меню\nВвод: ");
                int choice = 0;
                choice = UseFullMethods.Check(2, choice);
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        bool menu = true;
                        bool thing = false;
                        thing = Oyo.menu(menu, usingThing, thingToUse, thing);
                        if (thing)
                        {
                            continue_the_game = true;
                            Menu = false;
                        }
                        break;
                    case 2:
                        if(!continue_the_game)
                        {
                            Console.Write("\n\nБоюсь, вам нужно еще подумать над своим выбором\nУверены, что хотите выйти из меню?\n[1.] Нет\n[2.] Да\nВвод: ");
                            choice = UseFullMethods.Check(2, choice);
                            Console.Clear();
                            if (choice == 2)
                            {
                                continue_the_game = false;
                                Menu = false;
                            }
                        }
                        break;
                }
            }
            return continue_the_game;
        }
        public void Plot()
        {
            Preface();
            MainPlotPart();
            End();
        }
    }

    static void Main(string[] args)
    {
        Oyoshi Oyoshi = new Oyoshi();
        Game game = new Game(Oyoshi);
        game.Plot();
    }
}