using DapperDemo.DAL;

var ur = new UserRepository();
foreach(var o in ur.SelectAllUsers()){
    Console.WriteLine(o);
}
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
    Console.WriteLine("5: Afficher toutes les publications");
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
            foreach(var o in ur.SelectAllUsers()){
                ur.SelectAllUsers();
            }
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
                    foreach(var o7 in pr.GetPostsByUserId(userId)){
                        Console.WriteLine(o7);
                    };
                    Console.WriteLine("Voulez-vous vraiement le supprimer ?");
                    string answer=Console.ReadLine();
                    if(answer.ToUpper() =="OUI")
                    {
                        ur.Delete(userId);
                        Console.WriteLine("Suppression terminée avec succès");
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Table User :");
                        foreach(var o1 in ur.SelectAllUsers()){
                            Console.WriteLine(o1);                        
                        }
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Table Post :");
                        foreach(var o2 in pr.SelectAllPosts()){
                            Console.WriteLine(o2);
                        }
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
                foreach(var o3 in ur.SelectAllUsers()){
                    Console.WriteLine(o3);                        
                }
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
                    foreach(var result in pr.GetPostsByUserId(userId2)){
                        Console.WriteLine(result);
                    }
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
            foreach (var o4 in pr.SelectAllPosts())
            {
                Console.WriteLine(o4);
            }
            break;
        case 7:
            Console.WriteLine("Donner l'id de la poste :");
            int PostId =Convert.ToInt32(Console.ReadLine());
            if(pr.VerifyPostById(PostId)==1){
                pr.Delete(PostId);
                Console.WriteLine("Suppression terminée avec succès");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Table Post :");
                foreach(var o5 in pr.SelectAllPosts()){
                    Console.WriteLine(o5);
                }
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
                foreach (var o6 in pr.SelectAllPosts())
                {
                    Console.WriteLine(o6);
                }
            }else{
                throw new Exception("Publication inexistante");
            }
            break;
        case 9:
            Console.WriteLine("BYE BYE");
            break;
    }
} while (x != 6);