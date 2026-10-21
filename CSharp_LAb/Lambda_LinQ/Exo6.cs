namespace CSharp_LAb;

public record Auteur(int Id, string Nom, string Pays);

public record Livre(int Id, int AuteurId, string Titre, decimal Prix, int Annee);

public class Exo6
{
  private static List<Auteur> auteurs = new()
  {
    new Auteur(1, "Hugo", "France"),
    new Auteur(2, "Orwell", "Royaume-Uni"),
    new Auteur(3, "Camus", "France"),
    new Auteur(4, "Austen", "Royaume-Uni"),
    new Auteur(5, "Tolstoï", "Russie"),
  };

  private static List<Livre> livres = new()
  {
    new Livre(1, 1, "Les Misérables", 14.90m, 1862),
    new Livre(2, 1, "Notre-Dame de Paris", 9.50m, 1831),
    new Livre(3, 2, "1984", 8.90m, 1949),
    new Livre(4, 2, "La Ferme des animaux", 6.50m, 1945),
    new Livre(5, 3, "L'Étranger", 7.20m, 1942),
    new Livre(6, 3, "La Peste", 8.40m, 1947),
    new Livre(7, 4, "Orgueil et Préjugés", 7.90m, 1813),
  };

  public static void Run()
  {
    //1. Afficher, pour chaque livre, son titre et le nom de son auteur (Join).
    Console.WriteLine("1. Afficher, pour chaque livre, son titre et le nom de son auteur (Join).");

    var bookAuthor = auteurs.Join(
      livres,
      auteur => auteur.Id,
      livre => livre.AuteurId,
      (auteur, livre) => new { auteur.Nom, livre.Titre });

    foreach (var author in bookAuthor)
    {
      Console.WriteLine($"{author.Nom} - {author.Titre}");
    }

    Console.WriteLine("-----------------------------------------------------------------------------------");

    //2. Afficher les livres à plus de 8 €, avec le nom de l’auteur.
    Console.WriteLine("2. Afficher les livres à plus de 8 €, avec le nom de l’auteur.");

    var priceBookAuthor = auteurs.Join(
      livres.Where(l => l.Prix > 8),
      auteur => auteur.Id,
      livre => livre.AuteurId,
      (auteur, livre) => new { auteur.Nom, livre.Titre, livre.Prix });

    foreach (var authorBook in priceBookAuthor)
    {
      Console.WriteLine($"{authorBook.Nom} - {authorBook.Titre} - {authorBook.Prix}");
    }

    Console.WriteLine("-----------------------------------------------------------------------------------");

    //3. Afficher le titre, l’auteur et le pays de chaque livre, triés par année de publication (du plus ancien au plus récent).
    Console.WriteLine("3. Afficher le titre, l’auteur et le pays de chaque livre, triés par année de publication (du plus ancien au plus récent).");

    var publicationDateAndCountry = livres.Join(
        auteurs,
        livre => livre.AuteurId,
        auteur => auteur.Id,
        (livre, auteur) => new { livre.Titre, auteur.Nom, auteur.Pays, livre.Annee })
      .OrderBy(l => l.Annee);

    foreach (var publicationDate in publicationDateAndCountry)
    {
      Console.WriteLine($"{publicationDate.Titre} - {publicationDate.Nom} - {publicationDate.Pays} - {publicationDate.Annee}");
    }

    Console.WriteLine("-----------------------------------------------------------------------------------");

    //4. Afficher les titres des livres écrits par des auteurs français.
    Console.WriteLine("4. Afficher les titres des livres écrits par des auteurs français.");

    var frenchAuthor = auteurs.Where(a => a.Pays == "France").Join(
      livres,
      auteur => auteur.Id,
      livre => livre.AuteurId,
      (auteur, livre) => new { livre.Titre });

    foreach (var result in frenchAuthor)
    {
      Console.WriteLine($"{result.Titre}");
    }
    
    Console.WriteLine("-----------------------------------------------------------------------------------");
    
    //5. la question 1 refaite avec livres.Join(auteurs, ...). Compare l’ordre d’affichage avec ta version actuelle.
    Console.WriteLine("5. la question 1 refaite avec livres.Join(auteurs, ...). Compare l’ordre d’affichage avec ta version actuelle.");

    var authorBook2 = livres.Join(
      auteurs,
      livre => livre.AuteurId,
      auteur => auteur.Id,
      (livre, auteur) => new { livre.Titre, auteur.Nom });

    foreach (var result in authorBook2)
    {
      Console.WriteLine($"{result.Titre} - {result.Nom}");
    }
  }
}