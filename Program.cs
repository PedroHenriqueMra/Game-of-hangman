string[] listWords = new string[]
{
    "programa", "teclado", "binario", "chuva", "audiencia", "objeto"
};

int sort = new Random().Next(4);
string wordSort = listWords[sort].ToLower();
List<char> hidden = new List<char>();
for (int l = 0; l < wordSort.Length; l++)
{
    hidden.Add('_');
}

Forca forca = new Forca();
forca.Start(hidden, wordSort);

public class Forca
{
    private List<char> Chutes { get; set; } = new List<Char>();
    private List<int> Index { get; set; } = new List<int>();
    private int Chances = 5;

    public void Start(List<char> hidden, string wordSort)
    {
        Decoration("bottom");
        WriteColor("JOGO DA FORCA", ConsoleColor.DarkCyan);
        while (true)
        {
            if (Draw(hidden, wordSort)) break;

            Console.Write("Digite sua dedução: ");
            string input = Console.ReadLine();
            Thread.Sleep(1000);
            if (Filter(Char.Parse(input)))
            {
                Check(hidden, wordSort, Char.Parse(input));
            }
        }
    }

    private bool Filter(char input)
    {
        input = Char.ToLower(input);
        if (Char.IsNumber(input) || Char.IsWhiteSpace(input) || Char.IsSymbol(input))
        {
            WriteColorLine("Digite um caractere válido!!", ConsoleColor.DarkYellow);
            return false;
        }
        if (Chutes.Contains(input))
        {
            WriteColorLine($"Voce já tentou a letra {input}!", ConsoleColor.DarkYellow);
            return false;
        }

        WriteColorLine($"Seu chute foi: {Char.ToUpper(input)}", ConsoleColor.White);
        Chutes.Add(input);

        return true;
    }

    private bool Check(List<char> hidden, string wordSort, char input)
    {
        if (!wordSort.Contains(input))
        {
            Chances -= 1;
            WriteColorLine("VOCE ERROU!. -1 CHANCE", ConsoleColor.Red);
            return true;
        }

        for (int l = 0; l < wordSort.Length; l++)
        {
            if (input == wordSort[l] && !(Index.Contains(l)))
            {
                hidden[l] = input;
                Index.Add(l);
            }
        }

        WriteColorLine("Voce acertou!", ConsoleColor.Green);
        return true;
    }

    private bool Draw(List<char> hidden, string wordSort)
    {
        if (Chances == 0)
        {
            Decoration();
            WriteColorLine("\nBURROOO!!", ConsoleColor.Red);
            WriteColorLine("\nVoce perdeu!!", ConsoleColor.Red);
            WriteColorLine("\nA palavra correta era:", ConsoleColor.Red);
            foreach (char w in wordSort)
            {
                Thread.Sleep(80);
                WriteColor($" {Char.ToUpper(w)} ", ConsoleColor.Yellow);
            }
            Decoration();

            return true;
        }
        if (!hidden.Contains('_'))
        {
            Decoration();
            WriteColorLine("\nVOCÊ GANHOU!!", ConsoleColor.Green);
            WriteColorLine($"\nVOCÊ ACERTOU A PALAVRA FALTANDO {Chances} CHANCES. PARABENS!!", ConsoleColor.Green);
            WriteColorLine($"\nVOCÊ ACERTOU A PALAVRA:", ConsoleColor.Green);
            foreach (char w in wordSort)
            {
                Thread.Sleep(80);
                WriteColor($" {Char.ToUpper(w)} ", ConsoleColor.Yellow);
            }
            Decoration();

            return true;
        }
        Decoration();
        Thread.Sleep(1000);
        WriteColorLine("\nACERTE A FRASE:", ConsoleColor.Yellow);
        foreach (char w in hidden)
        {
            Thread.Sleep(200);
            WriteColor($" {Char.ToUpper(w)} ", ConsoleColor.Yellow);
        }
        Console.WriteLine($"\nSuas chances: {Chances}");
        Console.WriteLine($"Seus chutes:");
        if (Chutes.Count != 0)
        {
            foreach (var w in Chutes)
            {
                Console.Write($" {Char.ToUpper(w)} ");
            }
        }
        else
        {
            WriteColor("Voce nao realizou nenhum chute ainda!", ConsoleColor.DarkRed);
        }
        Console.WriteLine("");
        Decoration("bottom");


        return false;
    }

    private void Decoration(string side = "top")
    {
        if (side == "top")
        {
            Console.WriteLine("");
            for (var l = 0; l < 20; l++)
            { Console.Write("=+"); }
        }
        else
        {
            for (var l = 0; l < 20; l++)
            { Console.Write("=+"); }
            Console.WriteLine("");
        }
    }

    private void WriteColorLine(string text, ConsoleColor color, bool backgrund = false)
    {
        if (backgrund)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private void WriteColor(string text, ConsoleColor color, bool backgrund = false)
    {
        if (backgrund)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ResetColor();
        }

        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}
