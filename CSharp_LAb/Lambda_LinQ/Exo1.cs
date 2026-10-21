namespace CSharp_LAb;

public class Exo1
{
  public static string Decrire(int n)
  {
    if (n < 0) return "négatif";
    if (n == 0) return "zéro";
    return "positif";
  }

  public static void run()
  {
    Console.WriteLine(Decrire(10));
    Console.WriteLine(Decrire(0));
    Console.WriteLine(Decrire(-10));
  }
}

