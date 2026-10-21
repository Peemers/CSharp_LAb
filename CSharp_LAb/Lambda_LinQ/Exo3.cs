namespace CSharp_LAb;

/*Afficher les noms des produits en rupture de stock (Stock == 0).
Afficher les produits à moins de 50 €, du moins cher au plus cher.
Afficher le produit le plus cher.
Afficher le prix moyen de tous les produits (indice : Average).
Afficher, pour chaque catégorie, le nombre de produits.
Afficher, pour chaque catégorie, la valeur totale du stock (prix fois quantité, additionné sur la catégorie).
Défi : afficher les noms des produits disponibles (stock > 0) de la catégorie qui a la plus grande valeur de stock, triés par nom.*/

public record Produit(string Nom, string Categorie, decimal Prix, int Stock);

public class Exo3
{
  private static List<Produit> produits = new()
  {
    new Produit("Clavier", "Informatique", 49.90m, 12),
    new Produit("Souris", "Informatique", 19.90m, 0),
    new Produit("Écran", "Informatique", 189.00m, 5),
    new Produit("Chaise", "Mobilier", 120.00m, 3),
    new Produit("Bureau", "Mobilier", 250.00m, 0),
    new Produit("Lampe", "Mobilier", 35.50m, 20),
    new Produit("Cahier", "Papeterie", 3.20m, 100),
    new Produit("Stylo", "Papeterie", 1.50m, 0),
  };

  public static void Run()
  {
    var rupture = produits.Where(p => p.Stock == 0)
      .OrderBy(p => p.Nom);

    foreach (var product in rupture)
    {
      Console.WriteLine($"{product.Nom}: {product.Prix}");
    }

    Console.WriteLine("---------------------------------------------------------------------------");

    var underFifthy = produits.Where(p => p.Prix < 50)
      .OrderBy(p => p.Prix);

    foreach (var product in underFifthy)
    {
      Console.WriteLine($"{product.Nom}: {product.Prix}");
    }

    Console.WriteLine("---------------------------------------------------------------------------");

    var expensive = produits.OrderByDescending(p => p.Prix)
      .First();

    Console.WriteLine($"{expensive.Nom}: {expensive.Prix}");

    Console.WriteLine("---------------------------------------------------------------------------");

    var averagePrice = produits.Average(p => p.Prix);

    Console.WriteLine(averagePrice);

    Console.WriteLine("---------------------------------------------------------------------------");

    var catStock = produits.GroupBy(p => p.Categorie);

    foreach (var group in catStock)
    {
      Console.WriteLine($"{group.Key} : {group.Sum(p => p.Stock)}");
    }

    Console.WriteLine("---------------------------------------------------------------------------");

    var valueCatStock = produits.GroupBy(p => p.Categorie);

    foreach (var group in valueCatStock)
    {
      Console.WriteLine($"{group.Key} : {group.Sum(p => p.Prix * p.Stock)}");
    }

    Console.WriteLine("---------------------------------------------------------------------------");

    //Défi : afficher les noms des produits disponibles (stock > 0) de la catégorie qui a la plus grande valeur de stock, triés par nom.

    var bestCategory = produits.GroupBy(p => p.Categorie)
      .MaxBy(g => g.Sum(p => p.Prix * p.Stock));
    Console.WriteLine(bestCategory.Key);

    var available = produits.Where(p => p.Categorie == bestCategory.Key)
      .Where(p => p.Stock > 0)
      .OrderBy(p => p.Nom)
      .Select(p => p.Nom);

    foreach (var nom in available)
    {
      Console.WriteLine(nom);
    }
  }
}