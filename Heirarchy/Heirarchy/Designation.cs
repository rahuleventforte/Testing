using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    class Designation
    {
        public string DesignationID { get; set; }
        public string DesignationTitle { get; set; }
        public Permissions DefaultPermissions { get; set; }
        public Permissions SoftUpperLimitsOfPermissions { get; set; }

        /*Deepika/Vishal will need to recreate this function after commenting out the content of Tittu's populator*/
        public static List<Designation> PopulateDefaultDesignation(string orgID)
        {
            List<Designation> x = null;

            if (orgID == "EF")
            {
                x = new List<Designation>(){
                    //Des1
                    new Designation 
                    {
                        DesignationID = "ca",
                        DesignationTitle = "Chief Architect",
                        DefaultPermissions = new Permissions
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.read,
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
                        SoftUpperLimitsOfPermissions = new Permissions
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
                        
                        }
                    }, // end of initialization of Des1
                    //Des2
                    new Designation 
                    {
                        DesignationID = "p",
                        DesignationTitle = "President",
                        DefaultPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.read,
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
                        SoftUpperLimitsOfPermissions = new Permissions()
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
                        }
                    }, // end of initialization of Des2
                    //Des3
                    new Designation 
                    {
                        DesignationID = "rec",
                        DesignationTitle = "Recruitment",
                        DefaultPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.none,
                                ore_permission.none,
                                ore_permission.none,
                                ore_permission.read,
                                ore_permission.none
                            },
                            PermissionViewGraph = new List<bool>
                            {
                                false,
                                false,
                                false,
                                false
                            }
                        },
                        SoftUpperLimitsOfPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.none,
                                ore_permission.read,
                                ore_permission.none,
                                ore_permission.edit,
                                ore_permission.read
                            },
                            PermissionViewGraph = new List<bool>
                            {
                                true,
                                false,
                                true,
                                false
                            }
                        }
                    }, // end of initialization of Des3
                    //Des4
                    new Designation 
                    {
                        DesignationID = "html",
                        DesignationTitle = "HTML5 Experts",
                        DefaultPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.none,
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.none,
                                ore_permission.none
                            },
                            PermissionViewGraph = new List<bool>
                            {
                                true,
                                false,
                                false,
                                false
                            }
                        },
                        SoftUpperLimitsOfPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.none,
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.none,
                                ore_permission.none
                            },
                            PermissionViewGraph = new List<bool>
                            {
                                true,
                                false,
                                false,
                                false
                            }
                        }
                    }, // end of initialization of Des4
                    //Des5
                    new Designation 
                    {
                        DesignationID = "cs",
                        DesignationTitle = "C# Experts",
                        DefaultPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.none,
                                ore_permission.none
                            },
                            PermissionViewGraph = new List<bool>
                            {
                                true,
                                false,
                                false,
                                false
                            }
                        },
                        SoftUpperLimitsOfPermissions = new Permissions()
                        {
                            PermissionReadEditType = new List<ore_permission>
                            {   
                                ore_permission.read,
                                ore_permission.edit,
                                ore_permission.edit,
                                ore_permission.read,
                                ore_permission.none
                            },
                            PermissionViewGraph = new List<bool>
                            {
                                true,
                                false,
                                false,
                                false
                            }
                        }
                    } // end of initialization of Des5
                }; // end of initalization of Designation List
            } // end of if statement to check to see what organization
            return x;
        } //end of populateDesignation function scope
    } // end of designation class scope
}//end of name space scope
