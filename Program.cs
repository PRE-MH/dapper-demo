using DapperDemo.DAL;
using DapperDemo.Models;

var ur = new UserRepository();
ur.Select();
var pr = new PostRepository();
int x=0;
do
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Donner votre choix");
    Console.WriteLine("1: Ajouter User");
    Console.WriteLine("2: Supprimer User");
    Console.WriteLine("3: Mettre à jour User");
    Console.WriteLine("4: Afficher les postes d'un utilisateur");
    Console.WriteLine("5: Afficher toutes les postes");
    Console.WriteLine("6: Ajouter Post");
    Console.WriteLine("7: Supprimer Post");
    Console.WriteLine("8: Mise à jour Post");
    Console.WriteLine("9: Quitter");
    do
    {

        x = Convert.ToInt32(Console.ReadLine());
    }
    while (x < 1 || x > 9);
    User user = new User();
    switch (x)
    {
        case 1:
            Console.WriteLine("Donner un nom");
            string name = Console.ReadLine();
            user.Name = name;
            ur.Add(user);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Votre nouvelle Base de données :");
            ur.Select();
            break;
        case 2:
            Console.WriteLine("Donner l'id");
            int y = Convert.ToInt32(Console.ReadLine());
            ur.Delete(y);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Votre nouvelle Base de données :");
            ur.Select();
            break;
        case 3:
            Console.WriteLine("Donner l'id");
            int y1 = Convert.ToInt32(Console.ReadLine());
            ur.Update(y1);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Votre nouvelle Base de données :");
            ur.Select();
            break;
        case 4:
            Console.WriteLine("Donner l'id");
            int y2 = Convert.ToInt32(Console.ReadLine());
            pr.Postes(y2);
            break;
        case 5:
            pr.tous_postes();
            break;
        case 6:
            Console.WriteLine("Donner le titre de la poste :");
            string title=Console.ReadLine();
            Console.WriteLine("Donner le contenu de la poste :");
            string content=Console.ReadLine();
            Console.WriteLine("Donner l'id de l'utilisateur :");
            int ownerid =Convert.ToInt32(Console.ReadLine());
            pr.Add(title,content,ownerid);
            break;
        case 7:
            Console.WriteLine("Donner l'id de la poste :");
            int id =Convert.ToInt32(Console.ReadLine());
            pr.Delete(id);
            break;
        case 8:
            Console.WriteLine("Donner l'id de la poste :");
            int id1 =Convert.ToInt32(Console.ReadLine());
            if(pr.Verify(id1)==1){
            Console.WriteLine("Donner le titre de la poste :");
            string title1=Console.ReadLine();
            Console.WriteLine("Donner le contenu de la poste :");
            string content1=Console.ReadLine();
            Console.WriteLine("Donner l'id de l'utilisateur :");
            int ownerid1 =Convert.ToInt32(Console.ReadLine());
            pr.Update(id1,title1,content1,ownerid1);
            }else{
                Console.WriteLine("Cette poste n'existe pas");
            }
            break;
        case 9:
            Console.WriteLine("BYE BYE");
            break;
    }
} while (x != 6);