using System.Net.WebSockets;
using System.Reflection;

namespace CSharp_LAb;

public record Etudiant(int Id, string Nom, string Filiere, int Age);

public record Note(int EtudiantId, string Matiere, decimal Valeur);

public class Recap
{
  private static List<Etudiant> etudiants = new()
  {
    new Etudiant(1, "Alice", "Info", 20),
    new Etudiant(2, "Bob", "Info", 22),
    new Etudiant(3, "Chloé", "Maths", 21),
    new Etudiant(4, "David", "Maths", 19),
    new Etudiant(5, "Emma", "Physique", 23),
    new Etudiant(6, "Farid", "Physique", 20),
    new Etudiant(7, "Gaëlle", "Info", 21),
  };

  private static List<Note> notes = new()
  {
    new Note(1, "Maths", 15m), new Note(1, "Info", 18m), new Note(1, "Physique", 12m),
    new Note(2, "Maths", 9m), new Note(2, "Info", 14m),
    new Note(3, "Maths", 17m), new Note(3, "Info", 11m), new Note(3, "Physique", 16m),
    new Note(4, "Maths", 8m), new Note(4, "Physique", 10m),
    new Note(5, "Maths", 13m), new Note(5, "Physique", 19m),
    new Note(6, "Info", 7m), new Note(6, "Physique", 12m),
  };

  private static List<string> commentaires = new()
  {
    "bon travail continue",
    "bon élève",
    "peut mieux faire",
  };

  public static void Run()
  {
    //Partie 1 : les étudiants seuls

    //1.1 Afficher les noms des étudiants de plus de 20 ans, triés par âge décroissant.
    Console.WriteLine("1.1 Afficher les noms des étudiants de plus de 20 ans, triés par âge décroissant.");

    var studentAbove20 = etudiants
      .Where(e => e.Age > 20)
      .OrderByDescending(e => e.Age)
      .Select(e => e.Nom);

    foreach (var student in studentAbove20)
    {
      Console.WriteLine($"{student}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //1.2 Afficher les filières, sans doublon.
    Console.WriteLine("1.2 Afficher les filières, sans doublon.");

    var Sectors = etudiants
      .Select(e => e.Filiere)
      .Distinct();

    foreach (var sector in Sectors)
    {
      Console.WriteLine($"{sector}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //1.3 Afficher les trois étudiants les plus âgés.
    Console.WriteLine("1.3 Afficher les trois étudiants les plus âgés.");

    var oldestStudent = etudiants
      .OrderByDescending(e => e.Age)
      .Take(3)
      .Select(e => new { e.Nom, e.Age });

    foreach (var student in oldestStudent)
    {
      Console.WriteLine($"{student.Age} {student.Nom}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Y a-t-il un étudiant de moins de 20 ans ? Tous les étudiants ont-ils plus de 18 ans ?
    Console.WriteLine("Y a-t-il un étudiant de moins de 20 ans ? Tous les étudiants ont-ils plus de 18 ans ?");

    var isAdult = etudiants.All(e => e.Age > 18);
    var isTwenty = etudiants.Any(e => e.Age < 20);

    Console.WriteLine(isTwenty ? "oui" : "non");
    Console.WriteLine(isAdult ? "oui" : "non");

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Afficher le nombre d’étudiants par filière.
    Console.WriteLine("Afficher le nombre d’étudiants par filière.");

    var studentPerSector = etudiants.GroupBy(e => e.Filiere);

    foreach (var student in studentPerSector)
    {
      Console.WriteLine($"{student.Key} : {student.Count()}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Afficher l’âge moyen par filière.
    Console.WriteLine("Afficher l’âge moyen par filière.");

    var averagePerSector = etudiants.GroupBy(e => e.Filiere);

    foreach (var student in averagePerSector)
    {
      Console.WriteLine($"{student.Key} : {student.Average(s => s.Age)}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Partie 2 : les notes seules

    //2.1 Afficher la moyenne générale de toutes les notes (arrondie à 2 décimales).
    Console.WriteLine("Afficher la moyenne générale de toutes les notes (arrondie à 2 décimales).");

    var averageScore = notes.Average(n => n.Valeur);

    Console.WriteLine($"{averageScore:F2}");

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //2.2 Afficher les notes supérieures ou égales à 15, avec leur matière, de la plus haute à la plus basse.
    Console.WriteLine("2.2 Afficher les notes supérieures ou égales à 15, avec leur matière, de la plus haute à la plus basse.");

    var upper15 = notes.Where(n => n.Valeur >= 15).OrderByDescending(n => n.Valeur);

    foreach (var note in upper15)
    {
      Console.WriteLine($"{note.Valeur} - {note.Matiere}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //2.3 Afficher les matières, sans doublon.
    Console.WriteLine("2.3 Afficher les matières, sans doublon.");

    var noDouble = notes.Select(n => n.Matiere).Distinct();

    foreach (var note in noDouble)
    {
      Console.WriteLine($"{note}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //2.4 Afficher la moyenne par matière.
    Console.WriteLine("2.4 Afficher la moyenne par matière.");

    var averagePerSubject = notes.GroupBy(n => n.Matiere);

    foreach (var note in averagePerSubject)
    {
      Console.WriteLine($"{note.Key} - {note.Average(n => n.Valeur)}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //2.1 Afficher la matière qui a la meilleure moyenne.
    Console.WriteLine("2.5 Afficher la matière qui a la meilleure moyenne.");

    var topSectorAverage = notes.GroupBy(n => n.Matiere)
      .MaxBy(g => g.Average(n => n.Valeur));

    Console.WriteLine($"{topSectorAverage.Key} - {topSectorAverage.Average(n => n.Valeur)}");

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //2.2 Y a-t-il une note de 20 ? Toutes les notes sont-elles supérieures ou égales à 7 ?
    Console.WriteLine("Y a-t-il une note de 20 ? Toutes les notes sont-elles supérieures ou égales à 7 ?");

    var isAllAbove7 = notes.All(n => n.Valeur >= 7);
    var isMaxScore = notes.Any(n => n.Valeur == 20);

    Console.WriteLine($"Note à 20 : {(isMaxScore ? "oui" : "non")}");
    Console.WriteLine($"Tout le monde au dessus de 7 : {(isAllAbove7 ? "oui" : "non")}");

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Partie 3 : relier avec Join

    //3.1 Afficher, pour chaque note, le nom de l’étudiant, la matière et la valeur.
    Console.WriteLine("3.1 Afficher, pour chaque note, le nom de l’étudiant, la matière et la valeur.");

    var gradePerStudent = notes.Join(
      etudiants,
      note => note.EtudiantId,
      etudiant => etudiant.Id,
      ((note, etudiant) => new { note.Valeur, etudiant.Nom, note.Matiere }));

    foreach (var grade in gradePerStudent)
    {
      Console.WriteLine($"{grade.Valeur} - {grade.Nom} - {grade.Matiere}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //3.2 Afficher la moyenne par étudiant, de la plus haute à la plus basse.
    Console.WriteLine("3.2 Afficher la moyenne par étudiant, de la plus haute à la plus basse.");

    var averageNote = notes.Join(
        etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Nom, note.Valeur, note.Matiere })
      .GroupBy(nn => nn.Nom)
      .Select(g => new { Nom = g.Key, Valeur = g.Average(n => n.Valeur) })
      .OrderByDescending(n => n.Valeur);

    foreach (var note in averageNote)
    {
      Console.WriteLine($"{note.Valeur:F2} - {note.Nom}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //3.3 Afficher la moyenne des notes par filière, de la plus haute à la plus basse.
    Console.WriteLine("3.3 Afficher la moyenne des notes par filière, de la plus haute à la plus basse.");

    var averagePerSector2 = notes.Join(
        etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Filiere, note.Valeur })
      .GroupBy(fv => fv.Filiere)
      .Select(g => new { Filiere = g.Key, Valeur = g.Average(n => n.Valeur) })
      .OrderByDescending(n => n.Valeur);

    foreach (var note in averagePerSector2)
    {
      Console.WriteLine($"{note.Valeur} {note.Filiere}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //3.4 Afficher, pour chaque matière, le nom de l’étudiant qui a la meilleure note et cette note.
    Console.WriteLine("3.4 Afficher, pour chaque matière, le nom de l’étudiant qui a la meilleure note et cette note.");

    var topSectorStudent = notes.Join(
        etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Nom, note.Matiere, note.Valeur })
      .GroupBy(nm => nm.Matiere)
      .Select(g => new { Matiere = g.Key, Meilleure = g.MaxBy(n => n.Valeur) });

    foreach (var note in topSectorStudent)
    {
      Console.WriteLine($"{note.Meilleure.Nom} {note.Matiere} {note.Meilleure.Valeur}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //3.5 Afficher les trois meilleures notes, avec le nom de l’étudiant et la matière.
    Console.WriteLine("3.5 Afficher les trois meilleures notes, avec le nom de l’étudiant et la matière.");

    var top3 = notes.Join(
        etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Nom, note.Matiere, note.Valeur })
      .OrderByDescending(n => n.Valeur)
      .Take(3);

    foreach (var note in top3)
    {
      Console.WriteLine($"{note.Nom} {note.Matiere} {note.Valeur}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Partie 4 : lambdas imbriquées

    var gradeWithStudent = notes.Join(
      etudiants,
      note => note.EtudiantId,
      etudiant => etudiant.Id,
      (note, etudiant) => new { etudiant.Nom, etudiant.Filiere, note.Matiere, note.Valeur });

    //4.1 Afficher les étudiants qui n’ont aucune note.
    Console.WriteLine("4.1 Afficher les étudiants qui n’ont aucune note.");

    var noGradedStudent = etudiants
      .Where(e => !notes.Any(note => note.EtudiantId == e.Id)!);

    foreach (var student in noGradedStudent)
    {
      Console.WriteLine($"{student.Nom}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //4.2 Afficher tous les étudiants avec leur nombre de notes (Gaëlle doit apparaître avec 0), du plus grand nombre au plus petit.
    Console.WriteLine("4.2 Afficher tous les étudiants avec leur nombre de notes (Gaëlle doit apparaître avec 0), du plus grand nombre au plus petit.");

    var nbNotesParEtudiant = etudiants
      .Select(e => new
      {
        e.Nom,
        NbNotes = notes.Count(n => n.EtudiantId == e.Id)
      })
      .OrderByDescending(x => x.NbNotes);

    foreach (var item in nbNotesParEtudiant)
    {
      Console.WriteLine($"{item.Nom} : {item.NbNotes}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //4.3 Afficher les étudiants qui ont au moins une note inférieure à 10.
    Console.WriteLine("4.3 Afficher les étudiants qui ont au moins une note inférieure à 10.");

    var etudiantsEnDifficulte = etudiants
      .Where(e => notes.Any(n => n.EtudiantId == e.Id && n.Valeur < 10));

    foreach (var etudiant in etudiantsEnDifficulte)
    {
      Console.WriteLine(etudiant.Nom);
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Partie 5 : SelectMany et doublons (sur la liste commentaires)

    //5.1 Afficher tous les mots des commentaires, puis les mots sans doublon.
    Console.WriteLine("5.1 Afficher tous les mots des commentaires, puis les mots sans doublon.");

    var anySentence = commentaires.SelectMany(c => c.Split(' '));
    Console.WriteLine(string.Join(", ", anySentence));

    var onlyWord = anySentence.Distinct();
    Console.WriteLine(string.Join(", ", onlyWord));

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //5.2 Afficher les mots qui apparaissent plusieurs fois (c’est la toute première fonction de notre conversation).
    Console.WriteLine("5.2 Afficher les mots qui apparaissent plusieurs fois (c’est la toute première fonction de notre conversation).");

    var wordOnly = commentaires.SelectMany(c => c.Split(' '))
      .GroupBy(w => w)
      .Where(w => w.Count() > 1);

    foreach (var word in wordOnly)
    {
      Console.WriteLine(word.Key);
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //5.3 Afficher le mot le plus fréquent avec son nombre d’apparitions.
    Console.WriteLine("5.3 Afficher le mot le plus fréquent avec son nombre d’apparitions.");

    var mostCommon = commentaires.SelectMany(c => c.Split(' '))
      .GroupBy(w => w)
      .MaxBy(g => g.Count());

    Console.WriteLine($"{mostCommon.Key} - {mostCommon.Count()}");

    Console.WriteLine("--------------------------------------------------------------------------------------------");


    //Partie 6 : stocker une lambda dans une variable

    //6.1 Écris une Func<decimal, string> nommée mention qui renvoie :
    //"Insuffisant" si la note est inférieure à 10,
    //"Passable" si elle est inférieure à 14,
    //"Bien" si elle est inférieure à 16,
    //"Très bien" sinon.
    //Utilise-la pour afficher, pour chaque étudiant qui a des notes, sa moyenne et sa mention.

    Func<decimal, string> mention = note =>
    {
      if (note < 10) return "insuffisant";
      if (note < 14) return "passable";
      if (note < 18) return "Bien";
      return "très bien";
    };

    var average = notes
      .Join(etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Nom, note.Valeur })
      .GroupBy(x => x.Nom)
      .Select(g => new { Name = g.Key, Average = g.Average(x => x.Valeur) });

    foreach (var item in average)
    {
      Console.WriteLine($"{item.Name} : {item.Average:F2} - {mention(item.Average)}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------");

    //Défis

    //7.1 Afficher les noms de tous les étudiants de la filière qui a la meilleure moyenne de notes (celle de la question 15), triés par nom, y compris ceux sans note. Ça se fait en deux temps : d’abord la filière, ensuite les étudiants.
    Console.WriteLine(
      "7.1 Afficher les noms de tous les étudiants de la filière qui a la meilleure moyenne de notes (celle de la question 15), triés par nom, y compris ceux sans note. Ça se fait en deux temps : d’abord la filière, ensuite les étudiants.");

    var bestSectorAverage = notes
      .Join(etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Filiere, note.Valeur })
      .GroupBy(fv => fv.Filiere)
      .MaxBy(g => g.Average(x => x.Valeur));

    var student2 = etudiants
      .Where(e => e.Filiere == bestSectorAverage.Key)
      .OrderBy(e => e.Nom)
      .Select(e => e.Nom);
    
    foreach (var student in student2)
    {
      Console.WriteLine(student);
    };

    //7.2 Afficher, pour chaque filière, le nom de l’étudiant qui a la meilleure moyenne. Tu devras calculer des moyennes par étudiant, puis regrouper par filière.
    Console.WriteLine("7.2 Afficher, pour chaque filière, le nom de l’étudiant qui a la meilleure moyenne. Tu devras calculer des moyennes par étudiant, puis regrouper par filière.");
    var individualBestSectorAverage = notes
      .Join(etudiants,
        note => note.EtudiantId,
        etudiant => etudiant.Id,
        (note, etudiant) => new { etudiant.Filiere, etudiant.Nom, note.Valeur })
      .GroupBy(x => x.Nom)
      .Select(e => new {Nom = e.Key, Filiere = e.First().Filiere, Moyenne = e.Average(e => e.Valeur) })
      .GroupBy(e => e.Filiere)
      .Select(g => new { Filiere = g.Key, Meilleur = g.MaxBy(e => e.Moyenne) });

    foreach (var item in individualBestSectorAverage)
    {
      Console.WriteLine($"{item.Filiere} : {item.Meilleur.Nom} ({item.Meilleur.Moyenne:F2})");
    }

    //De quoi vérifier
    //1 : Emma, Bob, Chloé, Gaëlle (dans cet ordre).
    //5 : Info 3, Maths 2, Physique 2. 6 : Info 21, Maths 20, Physique 21,5.
    //7 : 12,93. 10 : Maths 12,4, Info 12,5, Physique 13,8. 11 : Physique.
    //14 : Emma 16,00, Alice 15,00, Chloé 14,67, Bob 11,50, Farid 9,50, David 9,00.
    //15 : Info 13,60, Physique 12,75, Maths 12,40.
    //16 : Maths Chloé 17, Info Alice 18, Physique Emma 19.
    //18 : Gaëlle. 19 : Alice 3, Chloé 3, Bob 2, David 2, Emma 2, Farid 2, Gaëlle 0.
    //20 : Bob, David, Farid.
    //21 : 8 mots, dont 7 distincts. 22 : bon. 23 : bon, 2 fois.
    //24 : Emma Très bien, Alice Bien, Chloé Bien, Bob Passable, Farid Insuffisant, David Insuffisant.
    //25 : Alice, Bob, Gaëlle.
    //26 : Info Alice (15,00), Maths Chloé (14,67), Physique Emma (16,00).
  }
}