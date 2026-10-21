namespace CSharp_LAb;

public record Employe(string Nom, string Service, int Age, decimal Salaire, int Anciennete);

public class Exo4
{
  private static List<Employe> employes = new()
  {
    new Employe("Alice", "IT", 29, 3200m, 3),
    new Employe("Bob", "IT", 41, 4500m, 12),
    new Employe("Chloé", "IT", 35, 3900m, 7),
    new Employe("David", "RH", 52, 3800m, 20),
    new Employe("Emma", "RH", 26, 2600m, 1),
    new Employe("Farid", "Finance", 38, 4200m, 9),
    new Employe("Gaëlle", "Finance", 45, 5100m, 15),
    new Employe("Hugo", "Finance", 31, 3300m, 4),
    new Employe("Inès", "Marketing", 27, 2900m, 2),
    new Employe("Jules", "Marketing", 33, 3500m, 6),
  };

  public static void Run()
  {
    //1. Afficher les noms des employés de plus de 35 ans, triés par âge décroissant.
    Console.WriteLine("1. Afficher les noms des employés de plus de 35 ans, triés par âge décroissant.");

    var moreThanThirtyFive = employes.Where(e => e.Age > 35)
      .OrderByDescending(e => e.Age)
      .Select(e => e.Age);

    foreach (var employee in moreThanThirtyFive)
    {
      Console.WriteLine(employee);
    }

    Console.WriteLine("----------------------------------------------------------------------------------");

    //2. Afficher le salaire moyen de l’entreprise (Average).
    Console.WriteLine("2. Afficher le salaire moyen de l’entreprise (Average).");

    var salaryAverage = employes.Select(e => e.Salaire).Average();
    Console.WriteLine(salaryAverage);

    Console.WriteLine("----------------------------------------------------------------------------------");

    //3. Afficher la liste des services, sans doublon (Select puis Distinct).
    Console.WriteLine("3. Afficher la liste des services, sans doublon (Select puis Distinct)");

    var services = employes.Select(e => e.Service).Distinct();

    Console.WriteLine(string.Join(", ", services));

    Console.WriteLine("----------------------------------------------------------------------------------");

    //4. Afficher, pour chaque service, le salaire moyen.
    Console.WriteLine("4. Afficher, pour chaque service, le salaire moyen.");

    var salaryAverageByService = employes.GroupBy(e => e.Service);

    foreach (var service in salaryAverageByService)
    {
      Console.WriteLine($"{service.Key} : {service.Average(e => e.Salaire)}");
    }

    Console.WriteLine("----------------------------------------------------------------------------------");

    //5. Afficher les trois employés les mieux payés (noms et salaires), avec Take(3).
    Console.WriteLine("5. Afficher les trois employés les mieux payés (noms et salaires), avec Take(3).");

    var highestPaid = employes.OrderByDescending(e => e.Salaire).ToList().Take(3);

    foreach (var employee in highestPaid)
    {
      Console.WriteLine($"{employee.Nom} : {employee.Service} : {employee.Salaire}");
    }

    Console.WriteLine("----------------------------------------------------------------------------------");

    //6. Afficher, pour chaque service, le nom de l’employé le mieux payé (MaxBy dans chaque groupe).
    Console.WriteLine("6. Afficher, pour chaque service, le nom de l’employé le mieux payé (MaxBy dans chaque groupe).");

    var topSalaryByService = employes.GroupBy(e => e.Service);

    foreach (var groupe in topSalaryByService)
    {
      var meilleur = groupe.MaxBy(e => e.Salaire);
      Console.WriteLine($"{groupe.Key} : {meilleur.Nom} : {meilleur.Salaire}");
    }

    Console.WriteLine("----------------------------------------------------------------------------------");

    //7. Y a-t-il au moins un employé de plus de 50 ans ? (Any)
    Console.WriteLine("7. Y a-t-il au moins un employé de plus de 50 ans ? (Any)");

    bool find = employes.Any(e => e.Age > 50);

    if (find)
    {
      Console.WriteLine($"{find} : oui ");
    }
    else
    {
      Console.WriteLine($"Pas trouvé");
    }

    Console.WriteLine("----------------------------------------------------------------------------------");

    //8. Tous les employés gagnent-ils plus de 2500 € ? (All)
    Console.WriteLine("8. Tous les employés gagnent-ils plus de 2500 € ? (All)");

    bool salary = employes.All(e => e.Salaire > 2500);
    if (salary)
    {
      Console.WriteLine($"{salary} : oui");
    }
    else
    {
      Console.WriteLine($"{salary} : non");
    }

    Console.WriteLine("----------------------------------------------------------------------------------");

    //9. Afficher les services dont le salaire moyen dépasse 3500 €, avec leur effectif, triés par salaire moyen décroissant.
    //Format attendu : Finance : 3 employés, moyenne 4200.
    Console.WriteLine("9. Afficher les services dont le salaire moyen dépasse 3500 €, avec leur effectif, triés par salaire moyen décroissant.\n Format attendu : Finance : 3 employés, moyenne 4200.");

    var topService = employes.GroupBy(e => e.Service)
      .Where(g => g.Average(e => e.Salaire) > 3500).OrderByDescending(g => g.Average(e => e.Salaire));

    foreach (var groupe in topService)
    {
      Console.WriteLine($"{groupe.Key} : {groupe.Count()} employés, moyenne {groupe.Average(e => e.Salaire):F2}");
    }
  }
}