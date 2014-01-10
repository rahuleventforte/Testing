using Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heirarchy
{
    class Program
    {
        static void Main(string[] args)
        {

            List<EmployeeNode> E = EmployeeNode.PopulateEmployeeList("EF");

            Hierarchy.Hierarchy EF = new Hierarchy.Hierarchy()
            {
                HeadIndex = 1,
                HierarchyTree = E,
                PermissionID = new List<string>() { "p1", "p2", "p3", "p4", "p5", "g1", "g2", "g3", "g4" },
                NamePermissions = new List<string>()
                {   
                    "View/Edit Page 1",
                    "View/Edit Page 2",
                    "View/Edit Page 3",
                    "View/Edit Page 4",
                    "View/Edit Page 5",
                    "View Graph 1",
                    "View Graph 2",
                    "View Graph 3",
                    "View Graph 4"
                }
            };

            List<HierarchyErrorStructure> ErrorsInStructure = new List<HierarchyErrorStructure>();
            ErrorsInStructure = EF.CheckIfPermConsistent();
            //EF.CheckIfPermConsistent(1,ref ErrorsInStructure);
            if (ErrorsInStructure.Count() > 0)
            {
                for (int i = 0; i < ErrorsInStructure.Count(); i++)
                {
                    Console.WriteLine("{0} \n", ErrorsInStructure[i].message);
                }
            }
            else
            {
                Console.WriteLine("No known errors detected");
            }

            Console.WriteLine(EF.ToString());

            Hierarchy.Hierarchy.AddNewNode(ref EF, "EF");
            Console.WriteLine(EF.ToString());

            Hierarchy.Hierarchy.RemoveNode(ref EF, 7);
            Console.WriteLine(EF.ToString());

            List<HierarchyUser> H_Users = HierarchyUser.CreateHierarchyUserList(EF);
            Console.WriteLine(HierarchyUser.PrintUserSummary(H_Users, EF));

            HierarchyUser H_User = HierarchyUser.CreateHierarchyUser(EF, "C");

            Permissions P = EF.FindUpperLimitPermissions("C", ref ErrorsInStructure);
            Console.WriteLine(P.ToShortString());

            Console.Read();
        }


    }
}
