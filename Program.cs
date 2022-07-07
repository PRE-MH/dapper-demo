using DapperDemo.DAL;
using DapperDemo.Models;

var ur = new UserRepository();
ur.SelectAllUsers();
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
    switch (x)
    {
        case 1:
            Console.WriteLine("Donner un nom");
            string name = Console.ReadLine();
            ur.Add(name);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Table User :");
            ur.SelectAllUsers();
            break;
        case 2:
            Console.WriteLine("Donner l'id");
            int userId = Convert.ToInt32(Console.ReadLine());
            if (ur.VerifyUserById(userId) == 1) {
                if (pr.GetPostsCountByUserId(userId) == 0){
                    Console.WriteLine("Cet utilisateur n'a aucune publication");
                }
                else
                {
                    Console.WriteLine("Cet utilisateur a "+pr.GetPostsCountByUserId(userId)+" publications :");
                    Console.WriteLine(pr.GetPostsByUserId(userId);
                    Console.WriteLine("Voulez-vous vraiement le supprimer");
                    string answer=Console.ReadLine();
                    if(answer.ToUpper() =="OUI")
                    {
                        ur.Delete(userId);
                        Console.WriteLine("Suppression terminée avec succès");
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Table User :");
                        ur.SelectAllUsers();
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Table Post :");
                        pr.SelectAllPosts();
                    }else if(answer.ToUpper() == "NON")
                    {
                        Console.WriteLine("Suppression annulée");
                    }
                }    
            }
            else
            {
                throw new Exception("Utilisateur inexistant");
            }
            break;
        case 3:
            Console.WriteLine("Donner l'id");
            int userId1 = Convert.ToInt32(Console.ReadLine());
            if(ur.VerifyUserById(userId1) == 1) {
                Console.WriteLine("Donner le nouveau nom :");
                string name1=Console.ReadLine();
                ur.Update(userId1,name1);
                Console.WriteLine("--------------------------");
                Console.WriteLine("Table User :");
                ur.SelectAllUsers();
            }
            else
            {
                throw new Exception("Utilisateur inexistant");
            }
            break;
        case 4:
            Console.WriteLine("Donner l'id");
            int userId2 = Convert.ToInt32(Console.ReadLine());
            if(ur.VerifyUserById(userId2) == 1) {
                if(pr.GetPostsCountByUserId(userId2)==0) {
                    Console.WriteLine("Cet utilisateur n'a aucune publication");
                }
                else
                {
                    Console.WriteLine(pr.GetPostsByUserId(userId2);
                }
            }
            else
            {
                throw new Exception("Utilisateur inexistant");
            }
            break;
        case 5:
            pr.SelectAllPosts();
            break;
        case 6:
            Console.WriteLine("Donner le titre de la poste :");
            string title=Console.ReadLine();
            Console.WriteLine("Donner le contenu de la poste :");
            string content=Console.ReadLine();
            Console.WriteLine("Donner l'id de l'utilisateur :");
            int ownerid =Convert.ToInt32(Console.ReadLine());
            pr.Add(title,content,ownerid);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Table Post :");
            pr.SelectAllPosts();
            break;
        case 7:
            Console.WriteLine("Donner l'id de la poste :");
            int PostId =Convert.ToInt32(Console.ReadLine());
            if(pr.VerifyPostById(PostId)==1){
                pr.Delete(PostId);
                Console.WriteLine("Suppression terminée avec succès");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Table Post :");
                pr.SelectAllPosts();
            }
            else
            {
                throw new Exception("Publication inexistante");
            }
            break;
        case 8:
            Console.WriteLine("Donner l'id de la poste :");
            int PostId1 =Convert.ToInt32(Console.ReadLine());
            if(pr.VerifyPostById(PostId1)==1){
                Console.WriteLine("Donner le titre de la poste :");
                string title1=Console.ReadLine();
                Console.WriteLine("Donner le contenu de la poste :");
                string content1=Console.ReadLine();
                Console.WriteLine("Donner l'id de l'utilisateur :");
                int ownerid1 =Convert.ToInt32(Console.ReadLine());
                pr.Update(PostId1,title1,content1,ownerid1);
                Console.WriteLine("--------------------------");
                Console.WriteLine("Table Post :");
                pr.SelectAllPosts();
            }else{
                throw new Exception("Publication inexistante");
            }
            break;
        case 9:
            Console.WriteLine("BYE BYE");
            break;
    }
} while (x != 6);