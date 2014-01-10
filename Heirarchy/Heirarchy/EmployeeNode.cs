using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    class EmployeeNode
    {
        public string NodeID { get; set; }
        public Designation defaultDesignation { get; set; }
        public Permissions CurrentPermissions { get; set; }
        public string UserID { get; set; }
        public string CompanyID { get; set; }

        public List<string> ParentNodeIDs { get; set; }
        public List<string> ChildrenNodeIDs { get; set; }
        public List<string> SiblingNodeIDs { get; set; }

        


        /*Deepika/Vishal will need to recreate this function after commenting out the content of Tittu's populator*/
        public static List<EmployeeNode> PopulateEmployeeList(string orgID)
        {
            List<Designation> defaultDes = Designation.PopulateDefaultDesignation(orgID);
            List<EmployeeNode> y = null;
            if (defaultDes != null)
            {
                if (orgID == "EF")
                {
                    y = new List<EmployeeNode>()
                    {   
                        //1
                        new EmployeeNode { 
                            NodeID = "1312", UserID = "A", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="ca"),
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.edit,
                                    ore_permission.read,
                                    ore_permission.edit,
                                    ore_permission.edit,
                                    ore_permission.edit
                                },
                                PermissionViewGraph = new List<bool>
                                {
                                    true,
                                    true,
                                    true,
                                    true
                                }
                            },
                            ParentNodeIDs= new List<string>(){"1212"}, 
                            ChildrenNodeIDs = new List<string>(){"6436"}, 
                            SiblingNodeIDs=new List<string>(){}
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.edit, ore_permission.edit,ore_permission.edit,ore_permission.edit,ore_permission.edit},
                            //PermissionViewGraph = new List<bool>(){true, true,true,true}
                        },
                        //2
                        new EmployeeNode { 
                            NodeID = "1212", UserID = "B", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="p"),
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.edit,
                                    ore_permission.edit,
                                    ore_permission.edit,
                                    ore_permission.edit,
                                    ore_permission.edit
                                },
                                PermissionViewGraph = new List<bool>
                                {
                                    true,
                                    true,
                                    true,
                                    true
                                }
                            },
                            ParentNodeIDs= new List<string>(){}, 
                            ChildrenNodeIDs = new List<string>(){"1312","4223","2432","3224"}, 
                            SiblingNodeIDs=new List<string>(){},
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.edit, ore_permission.edit,ore_permission.edit,ore_permission.edit,ore_permission.edit},
                            //PermissionViewGraph = new List<bool>(){true, true,true,true}
                        },
                        //3
                        new EmployeeNode { 
                            NodeID = "3224", UserID = "A", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="rec"),
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.none,
                                    ore_permission.read,
                                    ore_permission.read,
                                    ore_permission.read,
                                    ore_permission.edit
                                },
                                PermissionViewGraph = new List<bool>
                                {
                                    false,
                                    false,
                                    false,
                                    true
                                }
                            },
                            ParentNodeIDs= new List<string>(){"1212"}, 
                            ChildrenNodeIDs = new List<string>(){}, 
                            SiblingNodeIDs=new List<string>(){},
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.none, ore_permission.none,ore_permission.none,ore_permission.read,ore_permission.edit},
                            //PermissionViewGraph = new List<bool>(){false,false,false, true}
                        },
                        //4
                        new EmployeeNode { 
                            NodeID = "4223", UserID = "C", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="html"),
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.none,
                                    ore_permission.edit,
                                    ore_permission.read,
                                    ore_permission.read,
                                    ore_permission.edit
                                },
                                PermissionViewGraph = new List<bool>
                                {
                                    false,
                                    false,
                                    false,
                                    true
                                }
                            },
                            ParentNodeIDs= new List<string>(){"1212"}, 
                            ChildrenNodeIDs = new List<string>(){}, 
                            SiblingNodeIDs=new List<string>(){"2432"},
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.none, ore_permission.edit,ore_permission.read,ore_permission.read,ore_permission.edit},
                            //PermissionViewGraph = new List<bool>(){false,false,false, true}
                        },
                        //5
                        new EmployeeNode { 
                            NodeID = "2432", UserID = "D", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="html"), 
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.none,
                                    ore_permission.none,
                                    ore_permission.read,
                                    ore_permission.read,
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
                            ParentNodeIDs= new List<string>(){"1212"}, 
                            ChildrenNodeIDs = new List<string>(){}, 
                            SiblingNodeIDs=new List<string>(){"4223"},
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.none, ore_permission.none,ore_permission.read,ore_permission.read,ore_permission.none},
                            //PermissionViewGraph = new List<bool>(){false, false,false,true}
                        },
                        //6
                        new EmployeeNode { 
                            NodeID = "6436", UserID = "C", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="cs"),
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.edit,
                                    ore_permission.read,
                                    ore_permission.read,
                                    ore_permission.read,
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
                            ParentNodeIDs= new List<string>(){"1312"}, 
                            ChildrenNodeIDs = new List<string>(){"1224"}, 
                            SiblingNodeIDs=new List<string>(){},
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.edit, ore_permission.edit,ore_permission.read,ore_permission.read,ore_permission.none},
                            //PermissionViewGraph = new List<bool>(){true, false, false, true}
                        },
                        //7
                        new EmployeeNode { 
                            NodeID = "1224", UserID = "D", CompanyID = "EF", defaultDesignation = defaultDes.Find(x=>x.DesignationID=="cs"),
                            CurrentPermissions = new Permissions()
                            {
                                
                                PermissionReadEditType = new List<ore_permission>
                                {   
                                    ore_permission.none,
                                    ore_permission.none,
                                    ore_permission.read,
                                    ore_permission.read,
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
                            ParentNodeIDs= new List<string>(){"6436"}, 
                            ChildrenNodeIDs = new List<string>(){}, 
                            SiblingNodeIDs=new List<string>(){},
                            //PermissionReadEditType = new List<ore_permission>(){ore_permission.none, ore_permission.edit,ore_permission.read,ore_permission.read,ore_permission.none},
                            //PermissionViewGraph = new List<bool>(){true, false,false,true}
                        }
                    };
                }
            }
            return y;
        }
    };


}
