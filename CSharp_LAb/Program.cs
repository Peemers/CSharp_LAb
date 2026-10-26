using CSharp_LAb;

while (true)
{
  Console.WriteLine("Quel exo voulez vous lancer ?");
  Console.WriteLine("Lambda_LinQ : tapez : 1, 2, 3, 4, 5, 6, 7 (recap)");
  Console.WriteLine("q pour quitter");
  var choix = Console.ReadLine();
  Console.Clear();

  switch (choix)
  {
    case "1": Exo1.run(); break;
    case "2": Exo2.run(); break;
    case "3": Exo3.Run(); break;
    case "4": Exo4.Run(); break;
    case "5": Exo5.Run(); break;
    case "6": Exo6.Run(); break;
    case "7": Recap.Run(); break;
    case "8": Recap2.Run(); break;
    case "q" : return ;
  }

  Console.WriteLine("-------------------------------------------------");
}