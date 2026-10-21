namespace CSharp_LAb;

public record Etudiant(int Id, string Nom, string Filiere, int Age);
public record Note(int EtudiantId, string Matiere, decimal Valeur);

public class Recap
{
  private static List<Etudiant> etudiants = new()
  {
    new Etudiant(1, "Alice",  "Info",     20),
    new Etudiant(2, "Bob",    "Info",     22),
    new Etudiant(3, "Chloé",  "Maths",    21),
    new Etudiant(4, "David",  "Maths",    19),
    new Etudiant(5, "Emma",   "Physique", 23),
    new Etudiant(6, "Farid",  "Physique", 20),
    new Etudiant(7, "Gaëlle", "Info",     21),
  };

  private static List<Note> notes = new()
  {
    new Note(1, "Maths", 15m), new Note(1, "Info", 18m), new Note(1, "Physique", 12m),
    new Note(2, "Maths", 9m),  new Note(2, "Info", 14m),
    new Note(3, "Maths", 17m), new Note(3, "Info", 11m), new Note(3, "Physique", 16m),
    new Note(4, "Maths", 8m),  new Note(4, "Physique", 10m),
    new Note(5, "Maths", 13m), new Note(5, "Physique", 19m),
    new Note(6, "Info", 7m),   new Note(6, "Physique", 12m),
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
    
    //Afficher les noms des étudiants de plus de 20 ans, triés par âge décroissant.
    //Afficher les filières, sans doublon.
    //Afficher les trois étudiants les plus âgés.
    //Y a-t-il un étudiant de moins de 20 ans ? Tous les étudiants ont-ils plus de 18 ans ?
    //Afficher le nombre d’étudiants par filière.
    //Afficher l’âge moyen par filière.
    
    //Partie 2 : les notes seules
    
    //Afficher la moyenne générale de toutes les notes (arrondie à 2 décimales).
    //Afficher les notes supérieures ou égales à 15, avec leur matière, de la plus haute à la plus basse.
    //Afficher les matières, sans doublon.
    //Afficher la moyenne par matière.
    //Afficher la matière qui a la meilleure moyenne.
    //Y a-t-il une note de 20 ? Toutes les notes sont-elles supérieures ou égales à 7 ?
    
    //Partie 3 : relier avec Join
    
    //Afficher, pour chaque note, le nom de l’étudiant, la matière et la valeur.
    //Afficher la moyenne par étudiant, de la plus haute à la plus basse.
    //Afficher la moyenne des notes par filière, de la plus haute à la plus basse.
    //Afficher, pour chaque matière, le nom de l’étudiant qui a la meilleure note et cette note.
    //Afficher les trois meilleures notes, avec le nom de l’étudiant et la matière.
    
    //Partie 4 : lambdas imbriquées
    
    //Afficher les étudiants qui n’ont aucune note.
    //Afficher tous les étudiants avec leur nombre de notes (Gaëlle doit apparaître avec 0), du plus grand nombre au plus petit.
    //Afficher les étudiants qui ont au moins une note inférieure à 10.
    
    //Partie 5 : SelectMany et doublons (sur la liste commentaires)
    
    //Afficher tous les mots des commentaires, puis les mots sans doublon.
    //Afficher les mots qui apparaissent plusieurs fois (c’est la toute première fonction de notre conversation).
    //Afficher le mot le plus fréquent avec son nombre d’apparitions.
    
    //Partie 6 : stocker une lambda dans une variable
    
    //Écris une Func<decimal, string> nommée mention qui renvoie :
    //"Insuffisant" si la note est inférieure à 10,
    //"Passable" si elle est inférieure à 14,
    //"Bien" si elle est inférieure à 16,
    //"Très bien" sinon.
    //Utilise-la pour afficher, pour chaque étudiant qui a des notes, sa moyenne et sa mention.
    
    //Défis
    
    //Afficher les noms de tous les étudiants de la filière qui a la meilleure moyenne de notes (celle de la question 15), triés par nom, y compris ceux sans note. Ça se fait en deux temps : d’abord la filière, ensuite les étudiants.
    //Afficher, pour chaque filière, le nom de l’étudiant qui a la meilleure moyenne. Tu devras calculer des moyennes par étudiant, puis regrouper par filière.
    
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