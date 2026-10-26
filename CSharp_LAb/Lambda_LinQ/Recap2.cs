using System.Runtime.InteropServices.JavaScript;
using System.Security.Principal;

namespace CSharp_LAb;

public record Realisateur(int Id, string Nom, string Pays);

public record Film(int Id, int RealisateurId, string Titre, string Genre, int Annee, int Duree);

public record Avis(int FilmId, string Auteur, decimal Note);

public class Recap2
{
  private static List<Realisateur> realisateurs = new()
  {
    new Realisateur(1, "Nolan", "Royaume-Uni"),
    new Realisateur(2, "Villeneuve", "Canada"),
    new Realisateur(3, "Miyazaki", "Japon"),
    new Realisateur(4, "Besson", "France"),
    new Realisateur(5, "Varda", "France"),
  };

  private static List<Film> films = new()
  {
    new Film(1, 1, "Inception", "SF", 2010, 148),
    new Film(2, 1, "Interstellar", "SF", 2014, 169),
    new Film(3, 1, "Dunkerque", "Guerre", 2017, 106),
    new Film(4, 2, "Dune", "SF", 2021, 155),
    new Film(5, 2, "Premier Contact", "SF", 2016, 116),
    new Film(6, 3, "Le Voyage de Chihiro", "Animation", 2001, 125),
    new Film(7, 3, "Princesse Mononoké", "Animation", 1997, 134),
    new Film(8, 4, "Léon", "Thriller", 1994, 110),
    new Film(9, 4, "Nikita", "Thriller", 1990, 118),
    new Film(10, 2, "Sicario", "Thriller", 2015, 121),
  };

  private static List<Avis> avis = new()
  {
    new Avis(1, "Léa", 9m), new Avis(1, "Marc", 8m), new Avis(1, "Sofia", 9m),
    new Avis(2, "Léa", 10m), new Avis(2, "Marc", 9m),
    new Avis(3, "Sofia", 7m), new Avis(3, "Marc", 6m),
    new Avis(4, "Léa", 8m), new Avis(4, "Sofia", 9m), new Avis(4, "Karim", 8m),
    new Avis(5, "Marc", 9m), new Avis(5, "Karim", 8m),
    new Avis(6, "Léa", 10m), new Avis(6, "Sofia", 10m), new Avis(6, "Karim", 9m),
    new Avis(7, "Sofia", 9m), new Avis(7, "Marc", 8m),
    new Avis(8, "Karim", 9m), new Avis(8, "Léa", 8m),
    new Avis(10, "Marc", 7m), new Avis(10, "Léa", 7m),
  };

  private static List<string> critiques = new()
  {
    "superbe film superbe musique",
    "musique incroyable",
    "film un peu long",
    "superbe photographie",
  };

  public static void Run()
  {
    //Partie 1 : les films seuls

//1.1 Afficher les titres des films sortis après 2010, du plus récent au plus ancien.
    Console.WriteLine("1.1 Afficher les titres des films sortis après 2010, du plus récent au plus ancien.");

    var movieAfter2010 = films
      .Where(f => f.Annee > 2010)
      .OrderBy(f => f.Annee);

    foreach (var item in movieAfter2010)
    {
      Console.WriteLine($"{item.Titre}, {item.Annee}");
    }

    Console.WriteLine("----------------------------------------------------------------------------------");


//1.2 Afficher les genres, sans doublon.
    Console.WriteLine("1.2 Afficher les genres, sans doublon.");

    var category = films.Select(f => f.Genre).Distinct();

    foreach (var item in category)
    {
      Console.WriteLine($"{item}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//1.3 Afficher les trois films les plus longs (titre et durée).
    Console.WriteLine("1.3 Afficher les trois films les plus longs (titre et durée).");

    var longestMovie = films
      .OrderByDescending(f => f.Duree)
      .Take(3);

    foreach (var item in longestMovie)
    {
      Console.WriteLine($"{item.Titre}, {item.Duree} minutes");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//1.4 Y a-t-il un film de plus de 180 minutes ? Tous les films durent-ils plus de 100 minutes ?
    Console.WriteLine("1.4 Y a-t-il un film de plus de 180 minutes ? Tous les films durent-ils plus de 100 minutes ?");

    var moreThan180 = films.Any(f => f.Duree > 180);
    var allMoviesMoreThan100 = films.All(f => f.Duree > 100);

    Console.WriteLine(moreThan180 ? "oui" : "non");
    Console.WriteLine(allMoviesMoreThan100 ? "oui" : "non");
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//1.5 Afficher le nombre de films par genre.
    Console.WriteLine("1.5 Afficher le nombre de films par genre.");

    var howManyMovies = films.GroupBy(f => f.Genre);

    foreach (var item in howManyMovies)
    {
      Console.WriteLine($"{item.Count()}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//1.6 Afficher la durée moyenne par genre.
    Console.WriteLine("1.6 Afficher la durée moyenne par genre.");
    
    var movieDurationPerCategory = films
      .GroupBy(f => f.Genre)
      .Select(x => new {Genre = x.Key, Duration = x.Average(y => y.Duree)} );

    foreach (var item in movieDurationPerCategory)
    {
      Console.WriteLine($"{item.Genre}, {item.Duration:F2}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//Partie 2 : les avis seuls

//2.1 Afficher la moyenne générale de tous les avis (2 décimales).
    Console.WriteLine("2.1 Afficher la moyenne générale de tous les avis (2 décimales).");

    var gradeAverage = avis.Average(a => a.Note);

    Console.WriteLine($"{gradeAverage:F2}");
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//2.2 Afficher les avis supérieurs ou égaux à 9, avec l’auteur, de la note la plus haute à la plus basse.
    Console.WriteLine("2.2 Afficher les avis supérieurs ou égaux à 9, avec l’auteur, de la note la plus haute à la plus basse.");

    var moreThan9 = avis
      .Where(a => a.Note >= 9)
      .OrderByDescending(a => a.Note)
      .Select(a => new {auteur = a.Auteur, note = a.Note});

    foreach (var item in moreThan9)
    {
      Console.WriteLine($"{item.auteur}, {item.note}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");

//2.3 Afficher les auteurs d’avis, sans doublon.
    Console.WriteLine("2.3 Afficher les auteurs d’avis, sans doublon.");
    
    var adviceAuthor = avis.Select(a => a.Auteur).Distinct();

    foreach (var item in adviceAuthor)
    {
      Console.WriteLine($"{item}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//2.4 Afficher la moyenne par auteur.
    Console.WriteLine("2.4 Afficher la moyenne par auteur.");

    var authorAdviceAverage = avis
      .GroupBy(a => a.Auteur);

    foreach (var item in authorAdviceAverage)
    {
      Console.WriteLine($"{item.Key} - {item.Average(a => a.Note):F2}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
//2.5 Afficher l’auteur le plus sévère (celui dont la moyenne est la plus basse). Cherche l’équivalent de MaxBy pour le minimum.

    Console.WriteLine("2.5 Afficher l'auteur le plus sévère");

    var lowerAdviceAverage = avis
      .GroupBy(a => a.Auteur)
      .MinBy(a => a.Average(n => n.Note));

    Console.WriteLine($"{lowerAdviceAverage.Key} - {lowerAdviceAverage.Average(a => a.Note):F2}");
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//2.6 Y a-t-il un avis à 1 ? Tous les avis sont-ils supérieurs ou égaux à 6 ?
    Console.WriteLine("2.6 Y a-t-il un avis à 1 ? Tous les avis sont-ils supérieurs ou égaux à 6 ?");
    
    var worstAdvice = avis
      .Any(a => a.Note == 1);
    
    var moreThan6 = avis
      .All(a => a.Note >= 6);

    Console.WriteLine(worstAdvice ? "oui" : "non");
    Console.WriteLine(moreThan6 ? "oui" : "non");
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//Partie 3 : relier avec Join

//3.1 Afficher, pour chaque film, son titre et le nom de son réalisateur.
    Console.WriteLine("3.1 Afficher, pour chaque film, son titre et le nom de son réalisateur.");

    var movieDirector = films.Join(
      realisateurs,
      realisateur => realisateur.RealisateurId,
      film => film.Id,
      (film, realisateur) => new { realisateur.Nom, film.Titre });

    foreach (var item in movieDirector)
    {
      Console.WriteLine($"{item.Nom}, {item.Titre}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//3.2 Afficher, pour chaque avis, le titre du film, l’auteur et la note.
    Console.WriteLine("3.2 Afficher, pour chaque avis, le titre du film, l’auteur et la note.");
    
    var movieAdvice = avis.Join(
      films,
      avis => avis.FilmId,
      film => film.Id,
      (avis1, film) => new  { film.Titre, avis1.Auteur, avis1.Note });

    foreach (var item in movieAdvice)
    {
      Console.WriteLine($"{item.Titre}, {item.Auteur}, {item.Note}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//3.3 Afficher la moyenne par film (titre), du mieux noté au moins bien noté.
    Console.WriteLine("3.3 Afficher la moyenne par film (titre), du mieux noté au moins bien noté.");

    var movieAverageAdvice = avis.Join(
        films,
        avis => avis.FilmId,
        film => film.Id,
        (avis1, film) => new { film.Titre, avis1.Note })
      .GroupBy(tn => tn.Titre)
      .Select(x => new { Titre = x.Key, Note = x.Average(n => n.Note)})
      .OrderByDescending(x => x.Note);

    foreach (var item in movieAverageAdvice)
    {
      Console.WriteLine($"{item.Titre}, {item.Note:F2}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
    
//3.4 Afficher la moyenne par genre, du mieux noté au moins bien noté.
    Console.WriteLine("3.4 Afficher la moyenne par genre, du mieux noté au moins bien noté.");
    
    var averagePerCategory = avis.Join(
      films,
      avis => avis.FilmId,
      film => film.Id,
      (avis1, film) => new { film.Genre, avis1.Note })
      .GroupBy(tn => tn.Genre)
      .Select(x => new { Genre = x.Key, Note = x.Average(n => n.Note)})
      .OrderByDescending(x => x.Note);

    foreach (var item in averagePerCategory)
    {
      Console.WriteLine($"{item.Genre}, {item.Note}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//3.5 Afficher les trois films les mieux notés en moyenne.
    Console.WriteLine("3.5 Afficher les trois films les mieux notés en moyenne.");

    var top3Advice = avis.Join(
        films,
        avis => avis.FilmId,
        film => film.Id,
        (avis1, film) => new { film.Titre, avis1.Note })
      .GroupBy(tn => tn.Titre)
      .Select(x => new { Titre = x.Key, Note = x.Average(n => n.Note)})
      .OrderByDescending(x => x.Note)
      .Take(3);

    foreach (var item in top3Advice)
    {
      Console.WriteLine($"{item.Titre}, {item.Note:F2}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//Partie 4 : lambdas imbriquées

//4.1 Afficher les films qui n’ont aucun avis.
    Console.WriteLine("4.1 Afficher les films qui n’ont aucun avis.");
    
    var withoutAdvice = films.Where(f => !avis.Any(avis => avis.FilmId == f.Id));

    foreach (var item in withoutAdvice)
    {
      Console.WriteLine($"{item.Titre}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");

//4.2 Afficher tous les réalisateurs avec leur nombre de films (Varda doit apparaître avec 0), du plus grand nombre au plus petit.
    Console.WriteLine("4.2 Afficher tous les réalisateurs avec leur nombre de films (Varda doit apparaître avec 0), du plus grand nombre au plus petit.");

    var directorMovieCount = realisateurs
      .Select(r => new
      {
        r.Nom, NbMovie = films.Count(f => f.RealisateurId == r.Id)
      })
      .OrderByDescending(r => r.NbMovie);

    foreach (var item in directorMovieCount)
    {
      Console.WriteLine($"{item.Nom}, {item.NbMovie}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//4.3 Afficher les films qui ont reçu au moins un avis inférieur ou égal à 7.
    Console.WriteLine("4.3 Afficher les films qui ont reçu au moins un avis inférieur ou égal à 7.");

    var poorScore = films
      .Where(f => avis.Any(a => a.FilmId == f.Id && a.Note <= 7));
    
    foreach (var item in poorScore)
    {
      Console.WriteLine($"{item.Titre}");
    }
    
//4.4 Afficher les réalisateurs dont tous les films durent plus de 110 minutes. Avant de lancer le code, prédis le résultat pour Varda : un piège t’attend.
    Console.WriteLine("4.4 Afficher les réalisateurs dont tous les films durent plus de 110 minutes. Avant de lancer le code, prédis le résultat pour Varda : un piège t’attend.");

    var longestMovie2 = realisateurs
      .Where(r => films.Where(f => f.RealisateurId == r.Id).All(f => f.Duree > 110));

    foreach (var item in longestMovie2)
    {
      Console.WriteLine($"{item.Nom}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");

//Partie 5 : SelectMany (sur la liste critiques)

//5.1 Afficher tous les mots, puis les mots sans doublon.
    Console.WriteLine("5.1 Afficher tous les mots, puis les mots sans doublon.");

    var anyWord = critiques.SelectMany(c => c.Split(' '));
    Console.WriteLine(string.Join(" ", anyWord));
    var word = critiques.SelectMany(c => c.Split(' '));
    Console.WriteLine(string.Join(" ", word));
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//5.2 Afficher les mots qui apparaissent plusieurs fois.
    Console.WriteLine("5.2 Afficher les mots qui apparaissent plusieurs fois.");
    
    var anyWord2 = critiques
      .SelectMany(c => c.Split(' '))
      .GroupBy(c => c)
      .Where(g => g.Count() > 1);

    foreach (var item in anyWord2)
    {
      Console.WriteLine($"{item.Key}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//5.3 Afficher le mot le plus fréquent, avec son nombre d’apparitions.
    Console.WriteLine("5.3 Afficher le mot le plus fréquent, avec son nombre d’apparitions.");
    
    var anyWord3 = critiques.SelectMany(c => c.Split(' '))
      .GroupBy(c => c)
      .MaxBy(g => g.Count());

    Console.WriteLine($"{anyWord3.Key} - {anyWord3.Count()}");
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//Partie 6 : une Func

//6.1 Écris une Func<decimal, string> nommée appreciation qui renvoie :
//"Mauvais" si la note est inférieure à 5,
//"Moyen" si elle est inférieure à 7,
//"Bon" si elle est inférieure à 9,
//"Excellent" sinon. Utilise-la pour afficher, pour chaque film qui a des avis, sa moyenne et son appréciation.

    Func<decimal, string> appreciation = note =>
    {
      if (note < 5) return "Mauvais";
      if (note < 7) return "Moyen";
      if (note < 9) return "bon";

      return "excellent";
    };

    var average = avis.Join(
        films,
        avis => avis.FilmId,
        film => film.Id,
        (avis1, film) => new { avis1.Note, film.Titre })
      .GroupBy(nt => nt.Titre)
      .Select(g => new { Titre = g.Key, Average = g.Average(x => x.Note) });

    foreach (var item in average)
    {
      Console.WriteLine($"{item.Titre} - {item.Average:F2} - {appreciation(item.Average)}");
    }
    
    Console.WriteLine("----------------------------------------------------------------------------------");
    
//Défis

//D.1 Afficher, pour chaque genre, le film le mieux noté en moyenne, avec sa moyenne (deux niveaux de regroupement, comme le 7.2 de la série précédente).
    Console.WriteLine("D.1 Afficher, pour chaque genre, le film le mieux noté en moyenne, avec sa moyenne (deux niveaux de regroupement, comme le 7.2 de la série précédente).");

    var bestCategoryAverage = avis.Join(
        films,
        unAvis => unAvis.FilmId,
        film => film.Id,
        (unAvis, film) => new { film.Titre, film.Genre, unAvis.Note })
      .GroupBy(x => x.Titre)
      .Select(g => new
      {
        Titre = g.Key,
        Genre = g.First().Genre,
        Average = g.Average(x => x.Note)
      })
      .GroupBy(f => f.Genre)
      .Select(g => new { Genre = g.Key, Meilleur = g.MaxBy(f => f.Average) });

    foreach (var item in bestCategoryAverage)
    {
      Console.WriteLine($"{item.Genre} : {item.Meilleur.Titre} ({item.Meilleur.Genre:F2})");
    }
    
//D.2 Afficher la moyenne des avis par réalisateur, du mieux noté au moins bien noté. Il te faudra deux Join à la suite : le résultat du premier devient la première liste du second.
    Console.WriteLine("D.2 Afficher la moyenne des avis par réalisateur, du mieux noté au moins bien noté. Il te faudra deux Join à la suite : le résultat du premier devient la première liste du second.");
    
    var directorAverage = realisateurs
      .Join(
        films,
        realisateur => realisateur.Id,
        film => film.RealisateurId,
        (realisateur, film) => new { realisateur.Nom, FilmId = film.Id })
      .Join(
        avis,
        ligne => ligne.FilmId,
        unAvis => unAvis.FilmId,
        (ligne, unAvis) => new { ligne.Nom, unAvis.Note })
      .GroupBy(x => x.Nom)
      .Select(g => new { Realisateur = g.Key, Moyenne = g.Average(x => x.Note) })
      .OrderByDescending(x => x.Moyenne);
    
    foreach (var item in directorAverage)
    {
      Console.WriteLine($"{item.Realisateur} : {item.Moyenne:F2}");
    }
  }
}