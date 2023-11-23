class SekSekMemento
{
    public int Level { get; set; }
    public string ChapterName { get; set; }
}

class SekSekCareTaker
{
    public SekSekMemento Memento { get; set; }
}

class Seksek
{
    public int Level { get; set; }
    public string ChapterName { get; set; }

    public override string ToString()
    {
        return $"You are on level {Level} and chapter {ChapterName}.";
    }

    public SekSekMemento Save()
    {
        return new SekSekMemento
        {
            ChapterName = this.ChapterName,
            Level = this.Level
        };
    }

    public void LoadPrevious(SekSekMemento Memento)
    {
        this.ChapterName = Memento.ChapterName;
        this.Level = Memento.Level;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Seksek game = new Seksek();
        game.Level = 1;
        game.ChapterName = "Cave";
        Console.WriteLine(game.ToString());

        SekSekCareTaker Taker = new SekSekCareTaker();
        Taker.Memento = game.Save();

        game.Level = 2;
        game.ChapterName = "Carribbean Islands";
        Console.WriteLine(game.ToString());

        game.LoadPrevious(Taker.Memento);

        Console.WriteLine(game.ToString());

        Console.Read();
    }
}