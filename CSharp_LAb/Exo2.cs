namespace CSharp_LAb;

public class Exo2
{
  private static IEnumerable<string> mots = new List<string> { "chat", "chien", "oiseau", "ane" };

  public static void run()
  {
    var r1 = mots.Where(mot => mot.Length > 3);
    var r2 = mots.Select(mot => mot.ToUpper());
    var r3 = mots.OrderBy(mot => mot.Length);
    var r4 = mots.GroupBy(mot => mot[0]);

    foreach (var mot in r1)
    {
      Console.WriteLine(mot);
    }

    foreach (var mot in r2)
    {
      Console.WriteLine(mot);
    }

    foreach (var mot in r3)
    {
      Console.WriteLine(mot);
    }

    foreach (var groupe in r4)
    {
      Console.WriteLine($"{groupe.Key} ({groupe.Count()}) : {string.Join(", ", groupe)}");
    }

    Console.WriteLine("---------------------------------------------------------------------------");

    var r5 = mots.Where(mot => mot.Length > 3)
      .Select(mot => mot.ToUpper())
      .OrderBy(mot => mot.Length);

    foreach (var mot in r5)
    {
      Console.WriteLine(mot);
    }

    Console.WriteLine("---------------------------------------------------------------------------");

    var r6 = mots.Where(mot => mot.Length > 3)
      .Select(mot => mot.ToUpper())
      .OrderByDescending(mot => mot.Length)
      .First();

    Console.WriteLine(r6);
  }
}