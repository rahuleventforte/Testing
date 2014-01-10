using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{

    class Hierarchy
    {
        public List<EmployeeNode> HierarchyTree { get; set; }
        public int HeadIndex { get; set; }
        public List<string> PermissionID{get; set;}
        public List<string> NamePermissions { get; set; }

        public override string ToString()
        {
            string output =string.Empty;
            foreach(var element in HierarchyTree)
            {
                output = output + "NodeID: " + element.NodeID +
                    ", UserID: " + element.UserID +
                    ", Position: " + element.defaultDesignation.DesignationID + ", "+ element.CurrentPermissions.ToShortString();
                    
                output = output + " ChildIDs: ";
                for(int i=0;i<element.ChildrenNodeIDs.Count();i++)
                {
                    output = output + element.ChildrenNodeIDs[i];
                    if(i!=element.ChildrenNodeIDs.Count())
                    {
                        output = output + ", ";
                    }
                }
                output = output + "\n";
            }
            return output;

        }

        /* This function takes no input and checks if entire tree is consistent*/
        public List<HierarchyErrorStructure> CheckIfPermConsistent()
        {
            int parentIndex = HeadIndex;
            List<HierarchyErrorStructure> ListOfErrors = new List<HierarchyErrorStructure>();

            List<int> Traversed = new List<int>();
            //This is where recursion starts
            CheckIfPermConsistent(parentIndex, ref ListOfErrors, ref Traversed);
            return ListOfErrors;

        }

        /*This function checks to see if the tree is consistent starting from the input node and no previous traversal history*/
        public void CheckIfPermConsistent(int parentIndex, ref List<HierarchyErrorStructure> ListOfErrors)
        {
            List<int> Traversed = new List<int>();
            //This is where recursion starts
            CheckIfPermConsistent(parentIndex, ref ListOfErrors, ref Traversed);
        }

        /*This function checks to see if the tree is consistent starting from the input node and a traversal history*/
        public void CheckIfPermConsistent(int parentIndex, 
            ref List<HierarchyErrorStructure> ListOfErrors,
            ref List<int> Traversed)
        {
            var parent = HierarchyTree[parentIndex];

            //Checks if there are any cyclic references
            bool containsItem = Traversed.Any(x => x == parentIndex);
            if (containsItem == false)
                Traversed.Add(parentIndex);
            else
            {
                HierarchyErrorStructure newError =
                            new HierarchyErrorStructure(
                                errortype.cyclicReference,
                                parentIndex,
                                parentIndex,
                                "Cyclic Error detected at Node " + parent.NodeID
                            );
                ListOfErrors.Add(newError);
                return;
            }

            //checking each child
            foreach (var item in parent.ChildrenNodeIDs)
            {
                var child = HierarchyTree.Find(x => x.NodeID == item);
                if (child == null)
                {
                    HierarchyErrorStructure newError =
                            new HierarchyErrorStructure(
                                errortype.childNotValid,
                                parentIndex,
                                -1,
                                "User " + parent.UserID + " is listed to have an invalid subordinate " + item + " at node " + parent.NodeID
                            );
                    ListOfErrors.Add(newError);
                    return;
                }

                for (int i = 0; i < child.CurrentPermissions.PermissionReadEditType.Count(); i++)
                {
                    if (parent.CurrentPermissions.PermissionReadEditType[i] == ore_permission.none &
                    (child.CurrentPermissions.PermissionReadEditType[i] == ore_permission.read |
                    child.CurrentPermissions.PermissionReadEditType[i] == ore_permission.edit))
                    {
                        HierarchyErrorStructure newError =
                            new HierarchyErrorStructure(
                                errortype.childMorePermission,
                                parentIndex,
                                HierarchyTree.IndexOf(child),
                                "User " + parent.UserID + " has less permissions than subordinate " + child.UserID + " at node " + parent.NodeID
                            );
                        ListOfErrors.Add(newError);
                    }

                    if (parent.CurrentPermissions.PermissionReadEditType[i] == ore_permission.read &
                    child.CurrentPermissions.PermissionReadEditType[i] == ore_permission.edit)
                    {
                        HierarchyErrorStructure newError =
                            new HierarchyErrorStructure(
                                errortype.childMorePermission,
                                parentIndex,
                                HierarchyTree.IndexOf(child),
                                "User " + parent.UserID + " has less permissions than subordinate " + child.UserID + " at node " + parent.NodeID
                            );
                        ListOfErrors.Add(newError);
                    }
                }


                for (int i = 0; i < child.CurrentPermissions.PermissionViewGraph.Count(); i++)
                {
                    if (parent.CurrentPermissions.PermissionViewGraph[i] == false &
                    child.CurrentPermissions.PermissionViewGraph[i] == true)
                    {
                        HierarchyErrorStructure newError =
                            new HierarchyErrorStructure(
                                errortype.childMorePermission,
                                parentIndex,
                                HierarchyTree.IndexOf(child),
                                "User " + parent.UserID + " has less permissions than subordinate " + child.UserID + " at node " + parent.NodeID
                            );
                        ListOfErrors.Add(newError);
                    }
                }
                //This is where recursion starts
                CheckIfPermConsistent(HierarchyTree.IndexOf(child), ref ListOfErrors, ref Traversed);
            }
        }

        /*Deepika/Vishal will need to recreate this function after commenting out the content of Tittu add function*/
        public static void AddNewNode(ref Hierarchy hierarchyNow, string orgID)
        {
            List<Designation> defaultDes = Designation.PopulateDefaultDesignation(orgID);
            EmployeeNode newEmployee = new EmployeeNode()
            {
                NodeID = "1372",
                UserID = "E",
                CompanyID = "EF",
                defaultDesignation = defaultDes.Find(x => x.DesignationID == "html"),
                CurrentPermissions = new Permissions
                {
                    PermissionReadEditType = new List<ore_permission>
                    {   
                        ore_permission.none,
                        ore_permission.none,
                        ore_permission.read,
                        ore_permission.none,
                        ore_permission.none
                    },
                    PermissionViewGraph = new List<bool>
                    {
                        false,
                        false,
                        false,
                        true
                    }
                },
                ParentNodeIDs = new List<string>() { "4223" },
                ChildrenNodeIDs = new List<string>() { },
                SiblingNodeIDs = new List<string>() { },
            };

            List<HierarchyErrorStructure> E = Hierarchy.CheckHierarchyCompatibilityOnAddition(hierarchyNow, newEmployee);

            if (E.Count() == 0)
            {
                hierarchyNow.HierarchyTree.Add(newEmployee);
                foreach (var parentnodeID in newEmployee.ParentNodeIDs)
                {
                    EmployeeNode parent = hierarchyNow.HierarchyTree.Find(x => x.NodeID == parentnodeID);
                    parent.ChildrenNodeIDs.Add(newEmployee.NodeID);
                }
                foreach (var siblingnodeID in newEmployee.SiblingNodeIDs)
                {
                    EmployeeNode sibling = hierarchyNow.HierarchyTree.Find(x => x.NodeID == siblingnodeID);
                    sibling.SiblingNodeIDs.Add(newEmployee.NodeID);
                }
            }
            else
            {
                if (E.Count() > 0)
                {
                    for (int i = 0; i < E.Count(); i++)
                    {
                        Console.WriteLine("{0} \n", E[i].message);
                    }
                }
                Console.WriteLine("Add not successful");
            }
        }

        //Note: remove only possible if it has no children
        public static List<HierarchyErrorStructure> RemoveNode(ref Hierarchy hierarchyNow, int indexToRemove)
        {
            List<HierarchyErrorStructure> E = null;
            EmployeeNode toRemove = hierarchyNow.HierarchyTree[indexToRemove];
            if (toRemove.ChildrenNodeIDs.Count() != 0)
            {
                HierarchyErrorStructure errorInstance = new HierarchyErrorStructure(
                    errortype.hasKidsCannotDelete,
                    indexToRemove,
                    -1,
                    "Delete children of node with index {0} before attempting to delete it");
                E.Add(errorInstance);
            }
            else
            {
                foreach (var parentNodeID in toRemove.ParentNodeIDs)
                {
                    var parent = hierarchyNow.HierarchyTree.Find(x => x.NodeID == parentNodeID);
                    parent.ChildrenNodeIDs.Remove(toRemove.NodeID);
                }

                foreach (var siblingNodeID in toRemove.SiblingNodeIDs)
                {
                    var sibling = hierarchyNow.HierarchyTree.Find(x => x.NodeID == siblingNodeID);
                    sibling.SiblingNodeIDs.Remove(toRemove.NodeID);
                }

                hierarchyNow.HierarchyTree.Remove(toRemove);

            }
            return E;

        }

        public static List<HierarchyErrorStructure> CheckHierarchyCompatibilityOnAddition(Hierarchy hierarchyNow, EmployeeNode newEmployee)
        {
            hierarchyNow.HierarchyTree.Add(newEmployee);
            List<HierarchyErrorStructure> E = hierarchyNow.CheckIfPermConsistent();          
            hierarchyNow.HierarchyTree.RemoveAt(hierarchyNow.HierarchyTree.Count() - 1);
            return E;
        }

        public List<HierarchyErrorStructure> ChangePermissionUser(string _UserID, Permissions newPermission) 
        { 
            /* Check if permission structure is compatible with permission of this hierarchy
             * Check if hierarchy is non empty
             * Check if userID exists
             * For each entity of Permission List, 
             *      if new permission is higher than existing user permission:
             *          for each node  with the user
             *               max_p(node)=calculate min{parent permission and soft_threshold} [this is the maximum allowable permission for this node]
             *          if sums of all nodes{ max_p(node)} is greater than or equal to newpermission, 
             *               change new permission of the user          
             *               change permissions of each node to min{newpermission, max_p(node)}
             *      elseif new permission is lower than existing user permission:
             *          for each node  with the user
             *              change permission to min(currentpermission,newpermission)
             */
            List<HierarchyErrorStructure> Errors = new List<HierarchyErrorStructure>();
            HierarchyUser TheUser = HierarchyUser.CreateHierarchyUser(this, _UserID);
            Permissions currPermission = TheUser.TotalPermission;

            /*Checking non-empty hierarchy*/
            if(HierarchyTree.Count()==0)
            {
                HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.emptyTreeCannotModifyPermission, -1, -1,
                    "Aborted attempt to change the permission in an empty Hierarchy Tree");
                Errors.Add(newError);
                return Errors;
            }
            /* Checking if the UserID exists */
            List<EmployeeNode> NodesWithUserID = HierarchyTree.FindAll(x => x.UserID == _UserID);
            if(NodesWithUserID.Count()==0)
            {
                HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.userNotFoundCannotModifyPermission, -1, -1,
                    "Aborted attempt to change the permission of a User that does not exist");
                Errors.Add(newError);
                return Errors;
            }
            
            /*Check to see if the newpermission is compatible or not*/
            //note that this is assignment and return, not .Add, change if necessary in future
            //Errors=utility.permissionStructureConsistencyErrors(newPermission, HierarchyTree[0].CurrentPermissions);
            if (Errors.Count() != 0)
            {
                HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.inconsistentPermissionNumbersHenceModificationImpossible, -1, -1,
                    "Aborted attempt to change the permission of a User since new permission is not compatible with Hierarchy");
                Errors.Add(newError);
                return Errors;
            }

            Permissions UpperLimits = FindUpperLimitPermissions(_UserID, ref Errors);
            /*Changing permissions for read/edit type*/
            for(int i=0; i<newPermission.PermissionReadEditType.Count(); i++)
            {
                ore_permission newPerm = newPermission.PermissionReadEditType[i];
                ore_permission upperPerm = UpperLimits.PermissionReadEditType[i];

                if (newPerm <= upperPerm)
                {
                    foreach(int j in TheUser.NodeIndex)
                    {
                        ore_permission parentPerm = ore_permission.edit;

                        if (HierarchyTree[j].ParentNodeIDs.Count() != 0)
                        {
                            EmployeeNode parent = HierarchyTree.Find(x => x.NodeID == HierarchyTree[j].ParentNodeIDs[0]);
                            parentPerm = parent.CurrentPermissions.PermissionReadEditType[i];
                        }
                        ore_permission softPerm = HierarchyTree[j].defaultDesignation.SoftUpperLimitsOfPermissions.PermissionReadEditType[i];
                        ore_permission NodeUpper = parentPerm < softPerm ? parentPerm : softPerm;

                        HierarchyTree[j].CurrentPermissions.PermissionReadEditType[i] = NodeUpper < newPerm ? NodeUpper : newPerm;

                    }

                }
                else
                {
                    HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.rulesRestrictPermissionModification, -1, -1,
                    "Aborted attempt to change the Read/Write permission of a User due to Permission Rules. Try to relax Default designation soft rules.");
                    Errors.Add(newError);
                    return Errors;
                }
            }
            

            /*Changing permissions for view graph type */
            for (int i = 0; i < newPermission.PermissionViewGraph.Count(); i++)
            {
                bool newPerm = newPermission.PermissionViewGraph[i];
                bool upperPerm = UpperLimits.PermissionViewGraph[i];
                if (newPerm == false & upperPerm == true)
                {
                    foreach (int j in TheUser.NodeIndex)
                    {
                        bool parentPerm = true;

                        if (HierarchyTree[j].ParentNodeIDs.Count() != 0)
                        {
                            EmployeeNode parent = HierarchyTree.Find(x => x.NodeID == HierarchyTree[j].ParentNodeIDs[0]);
                            parentPerm = parent.CurrentPermissions.PermissionViewGraph[i];
                        }
                        bool softPerm = HierarchyTree[j].defaultDesignation.SoftUpperLimitsOfPermissions.PermissionViewGraph[i];
                        bool NodeUpper = parentPerm & softPerm ;

                        HierarchyTree[j].CurrentPermissions.PermissionViewGraph[i] = NodeUpper & newPerm ;

                    }
                }
                else
                {
                    HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.rulesRestrictPermissionModification, -1, -1,
                    "Aborted attempt to change the View Graph permission of a User due to Permission Rules. Try to relax Default designation soft rules.");
                    Errors.Add(newError);
                    return Errors;
                }
            }
            return Errors;
        }
           
        public Permissions FindUpperLimitPermissions(string _UserID, ref List<HierarchyErrorStructure> Errors)
        {/* Check if permission structure is compatible with permission of this hierarchy
             * Check if hierarchy is non empty
             * Check if userID exists
             * For each entity of Permission List, 
             *      if new permission is higher than existing user permission:
             *          for each node  with the user
             *               max_p(node)=calculate min{parent permission and soft_threshold} [this is the maximum allowable permission for this node]
             *          if sums of all nodes{ max_p(node)} is greater than or equal to newpermission, 
             *               change new permission of the user          
             *               change permissions of each node to min{newpermission, max_p(node)}
             *      elseif new permission is lower than existing user permission:
             *          for each node  with the user
             *              change permission to min(currentpermission,newpermission)
             */

            HierarchyUser TheUser = HierarchyUser.CreateHierarchyUser(this, _UserID);
            Permissions currPermission = TheUser.TotalPermission;
                

            /*Checking non-empty hierarchy*/
            if (HierarchyTree.Count() == 0)
            {
                HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.emptyTreeCannotModifyPermission, -1, -1,
                    "Aborted attempt to change the permission in an empty Hierarchy Tree");
                Errors.Add(newError);
                return null;
            }

            Permissions UpperLimits = new Permissions(this.HierarchyTree[0].CurrentPermissions.PermissionReadEditType.Count(),
                this.HierarchyTree[0].CurrentPermissions.PermissionViewGraph.Count());
            

            /* Checking if the UserID exists */
            List<EmployeeNode> NodesWithUserID = HierarchyTree.FindAll(x => x.UserID == _UserID);
            if (NodesWithUserID.Count() == 0)
            {
                HierarchyErrorStructure newError = new HierarchyErrorStructure(errortype.userNotFoundCannotModifyPermission, -1, -1,
                    "Aborted attempt to change the permission of a User that does not exist");
                Errors.Add(newError);
                return UpperLimits;
            }

            /*Finding Upper Limit permissions for read/edit type*/
            for (int i = 0; i < this.HierarchyTree[0].CurrentPermissions.PermissionReadEditType.Count(); i++)
            {
                List<ore_permission> tempPermsReadEditType = new List<ore_permission>();

                foreach (var item in NodesWithUserID)
                {
                    ore_permission parentPerm = ore_permission.edit;

                    if(item.ParentNodeIDs.Count() != 0)
                    {
                        EmployeeNode parent = HierarchyTree.Find(x => x.NodeID == item.ParentNodeIDs[0]);
                        parentPerm = parent.CurrentPermissions.PermissionReadEditType[i];
                    }
                    ore_permission softPerm = item.defaultDesignation.SoftUpperLimitsOfPermissions.PermissionReadEditType[i];
                    ore_permission temp_ore = parentPerm < softPerm ? parentPerm : softPerm;
                    tempPermsReadEditType.Add(temp_ore);
                }

                //UpperLimits.PermissionReadEditType[i] = utility.FindMax_OREPerm_List(tempPermsReadEditType);
            }


            /*Finding Upper Limit permissions for view graph type*/
            for (int i = 0; i < this.HierarchyTree[0].CurrentPermissions.PermissionViewGraph.Count(); i++)
            {
                List<bool> tempPermsViewGraph = new List<bool>();
                foreach (var item in NodesWithUserID)
                {

                    bool parentPerm = true;
                    if (item.ParentNodeIDs.Count() != 0)
                    {
                        EmployeeNode parent = HierarchyTree.Find(x => x.NodeID == item.ParentNodeIDs[0]);
                        parentPerm = parent.CurrentPermissions.PermissionViewGraph[i];
                    }
                    bool softPerm = item.defaultDesignation.SoftUpperLimitsOfPermissions.PermissionViewGraph[i];
                    bool temp_bool = parentPerm & softPerm;

                    tempPermsViewGraph.Add(temp_bool);
                }
                //UpperLimits.PermissionViewGraph[i] = utility.BoolList_OR(tempPermsViewGraph);
            }
            return UpperLimits;
        }
    }
}
