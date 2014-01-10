using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    class HierarchyUser
    {
        public string UserID { get; set; }
        public List<int> NodeIndex { get; set; }
        public Permissions TotalPermission{ get; set; }
    
        
        public static List<HierarchyUser> CreateHierarchyUserList(Hierarchy currHierarchy)
        {
            List<HierarchyUser> UsersInHierarchy = new List<HierarchyUser>();
            IEnumerable<string> userIDs = currHierarchy.HierarchyTree.Select(x => x.UserID).Distinct();
            foreach (string uniqUserID in userIDs)
            {
                HierarchyUser user = CreateHierarchyUser(currHierarchy, uniqUserID);
                UsersInHierarchy.Add(user);
            }//end of looking at each unique username
            return UsersInHierarchy;
        }//end of CreateHierarchyUser function  


        public static HierarchyUser CreateHierarchyUser(Hierarchy currHierarchy, string uniqUserID)
        {
            List<int> listOfNodeIndices = new List<int>();
            List<EmployeeNode> allUsersWithThisUserID = currHierarchy.HierarchyTree.FindAll(x => x.UserID == uniqUserID);
            Permissions sumPerm = new Permissions(currHierarchy.HierarchyTree[0].CurrentPermissions.PermissionReadEditType.Count(),
                    currHierarchy.HierarchyTree[0].CurrentPermissions.PermissionViewGraph.Count());

            foreach (var item in allUsersWithThisUserID)
            {
                int indexOfInterest = currHierarchy.HierarchyTree.IndexOf(item);
                listOfNodeIndices.Add(indexOfInterest);

                for (int i = 0; i < currHierarchy.HierarchyTree[indexOfInterest].CurrentPermissions.PermissionReadEditType.Count(); i++)
                {
                    if (sumPerm.PermissionReadEditType[i] < currHierarchy.HierarchyTree[indexOfInterest].CurrentPermissions.PermissionReadEditType[i])
                        sumPerm.PermissionReadEditType[i] = currHierarchy.HierarchyTree[indexOfInterest].CurrentPermissions.PermissionReadEditType[i];
                }
                for (int i = 0; i < currHierarchy.HierarchyTree[indexOfInterest].CurrentPermissions.PermissionViewGraph.Count(); i++)
                {
                    if (sumPerm.PermissionViewGraph[i] | currHierarchy.HierarchyTree[indexOfInterest].CurrentPermissions.PermissionViewGraph[i] == true)
                        sumPerm.PermissionViewGraph[i] = true;
                }
            }//end of looking at all users with that userID

            HierarchyUser user = new HierarchyUser()
            {
                UserID = uniqUserID,
                NodeIndex = listOfNodeIndices,
                TotalPermission = sumPerm
            };

            return user;
        }

        public string ToString(Hierarchy OrganizationHierarchy)
        {
            string output =string.Empty;
            
            output = output + "UserID: " + UserID + ", Nodes: ";
            foreach (var x in NodeIndex)
            {
                output = output + OrganizationHierarchy.HierarchyTree[x].NodeID + ", ";
            }

            output = output + TotalPermission.ToShortString();
            return output;
        }

        public static string PrintUserSummary(List<HierarchyUser> UsersInHierarchy, Hierarchy EF)
        {
            string output = string.Empty;
            foreach (var element in UsersInHierarchy)
            {
                output = output + element.ToString(EF) + "\n";

            }
            return output;
        }
               
    }
}
