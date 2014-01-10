using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    enum ore_permission
    {
        none,
        read,
        edit
    }
    class Permissions
    {
        public List<ore_permission> PermissionReadEditType { get; set; }
        public List<bool> PermissionViewGraph { get; set; }
        public Permissions(int n1, int n2)
        {

            PermissionReadEditType = new List<ore_permission>();
            for (int i = 0; i < n1; i++)
            {
                PermissionReadEditType.Add(ore_permission.none);
            }

            PermissionViewGraph = new List<bool>();
            for (int i = 0; i < n2; i++)
            {
                PermissionViewGraph.Add(false);
            }

        }
        public Permissions()
        { }

        internal string ToShortString()
        {
            string output = string.Empty;
            output = "NRE: ";
            foreach (var i in PermissionReadEditType)
                output = output+ (int)i + ", ";
            output = output + "ViewGr: ";
            foreach (var i in PermissionViewGraph)
            {
                if (i == true)
                    output = output + "1, ";
                else if (i == false)
                    output = output + "0, ";
            }

            return output;
        }
    }
}
