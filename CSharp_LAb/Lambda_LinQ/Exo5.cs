namespace CSharp_LAb;

public record Client(int Id, string Nom, string Ville);

public record Commande(int Id, int ClientId, string Produit, decimal Montant, DateOnly Date);

public class Exo5
{
  private static List<Client> clients = new()
  {
    new Client(1, "Alice", "Paris"),
    new Client(2, "Bob", "Lyon"),
    new Client(3, "Chloé", "Paris"),
    new Client(4, "David", "Lille"),
    new Client(5, "Emma", "Lyon"),
  };

  private static List<Commande> commandes = new()
  {
    new Commande(1, 1, "Clavier", 49.90m, new DateOnly(2026, 1, 12)),
    new Commande(2, 1, "Écran", 189.00m, new DateOnly(2026, 2, 3)),
    new Commande(3, 2, "Souris", 19.90m, new DateOnly(2026, 2, 15)),
    new Commande(4, 3, "Chaise", 120.00m, new DateOnly(2026, 3, 1)),
    new Commande(5, 3, "Bureau", 250.00m, new DateOnly(2026, 3, 20)),
    new Commande(6, 3, "Lampe", 35.50m, new DateOnly(2026, 4, 2)),
    new Commande(7, 2, "Écran", 189.00m, new DateOnly(2026, 4, 18)),
    new Commande(8, 5, "Cahier", 3.20m, new DateOnly(2026, 5, 5)),
    new Commande(9, 1, "Stylo", 1.50m, new DateOnly(2026, 5, 9)),
    new Commande(10, 5, "Clavier", 49.90m, new DateOnly(2026, 6, 1)),
  };

  /*
Afficher le total dépensé par client (nom et total), trié du plus dépensier au moins dépensier.
Afficher le total dépensé par ville.
Afficher les clients qui n’ont passé aucune commande. Ici, pas besoin de Join : pense à Any à l’intérieur d’un Where, en comparant les identifiants.
Afficher tous les mots (avec SelectMany et Split(' ')), puis les mots sans doublon, puis le mot le plus fréquent (GroupBy, puis MaxBy sur Count()).
(défis)Afficher tous les clients, y compris David, avec leur nombre de commandes, trié par nombre décroissant. David doit apparaître avec 0.
   */

  public static void Run()
  {
    //Afficher le montant total de toutes les commandes (Sum).
    var sumAll = commandes.Sum(c => c.Montant);
    Console.WriteLine($"Total montant: {sumAll}");

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher les commandes de plus de 100 €, triées par montant décroissant (produit et montant).
    var moreThan100 = commandes.Where(c => c.Montant > 100)
      .OrderByDescending(c => c.Montant);

    foreach (var item in moreThan100)
    {
      Console.WriteLine($"{item}");
    }

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher, pour chaque produit, le nombre de commandes
    var nbOfCommand = commandes.GroupBy(c => c.Produit);

    foreach (var item in nbOfCommand)
    {
      Console.WriteLine($"{item.Key} : {item.Count()} commande(s)");
    }

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher, pour chaque commande, le nom du client et le produit (Join).
    var commandesAvecClient = commandes.Join(
      clients,
      commande => commande.ClientId,
      client => client.Id,
      (commande, client) => new { client.Nom, commande.Produit });

    foreach (var ligne in commandesAvecClient)
    {
      Console.WriteLine($"{ligne.Nom} : {ligne.Produit}");
    }

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher le total dépensé par client (nom et total), trié du plus dépensier au moins dépensier.

    var totalDispensePerCustomer = clients
      .Join(
        commandes,
        client => client.Id,
        commande => commande.ClientId,
        (client, commande) => new { client.Nom, commande.Montant })
      .GroupBy(x => x.Nom)
      .Select(g => new { Nom = g.Key, Total = g.Sum(x => x.Montant) })
      .OrderByDescending(g => g.Total);

    foreach (var item in totalDispensePerCustomer)
    {
      Console.WriteLine($"{item.Nom} : {item.Total}");
    }

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher le total dépensé par ville.

    var totalDispensePerCity = clients
      .Join(
        commandes,
        client => client.Id,
        commande => commande.ClientId,
        (client, commande) => new { client.Ville, commande.Montant })
      .GroupBy(x => x.Ville)
      .Select(g => new { Ville = g.Key, Total = g.Sum(x => x.Montant) })
      .OrderByDescending(g => g.Total);

    foreach (var item in totalDispensePerCity)
    {
      Console.WriteLine($"{item.Ville} : {item.Total}");
    }

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher les clients qui n’ont passé aucune commande. Ici, pas besoin de Join : pense à Any à l’intérieur d’un Where, en comparant les identifiants.

    var poorClient = clients
      .Where(c => !commandes.Any(commande => commande.ClientId == c.Id)!);

    foreach (var client in poorClient)
    {
      Console.WriteLine(client.Nom);
    }

    Console.WriteLine("-----------------------------------------------------------------");

    //Afficher tous les mots (avec SelectMany et Split(' ')), puis les mots sans doublon, puis le mot le plus fréquent (GroupBy, puis MaxBy sur Count()).

    var phrases = new List<string> { "le chat dort", "le chien court", "le chat mange" };


    var mots = phrases.SelectMany(p => p.Split(' '));
    Console.WriteLine(string.Join(", ", mots));


    var motsUniques = mots.Distinct();
    Console.WriteLine(string.Join(", ", motsUniques));


    var plusFrequent = mots
      .GroupBy(m => m)
      .MaxBy(g => g.Count());
    Console.WriteLine($"{plusFrequent.Key} : {plusFrequent.Count()} fois");
    
    Console.WriteLine("-----------------------------------------------------------------");
    
    //(défis) Afficher tous les clients, y compris David, avec leur nombre de commandes, trié par nombre décroissant. David doit apparaître avec 0.

    var allClients = clients
      .Select(c => new
      {
        c.Nom,
        NbCommandes = commandes.Count(cm => cm.ClientId == c.Id)
      })
      .OrderByDescending(x => x.NbCommandes);

    foreach (var item in allClients)
    {
      Console.WriteLine($"{item.Nom} : {item.NbCommandes}");
    }
    
    Console.WriteLine("-----------------------------------------------------------------");
    
    
  }
}