using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    enum errortype
    {
        childNotValid,
        childMorePermission,
        cyclicReference,
        inconsistentReadEditPermissionCount,
        inconsistentViewGraphPermissionCount,
        inconsistentPermissionIDNames,
        inconsistentPermissionIDcount,
        inconsistentPermissionNumbersHenceModificationImpossible,
        hasKidsCannotDelete,
        emptyTreeCannotModifyPermission,
        userNotFoundCannotModifyPermission,
        rulesRestrictPermissionModification
    }

    class HierarchyErrorStructure
    {

        public errortype type { get; set; }
        public int nodeIndex { get; set; }
        public int associatedIndex { get; set; }
        public string message { get; set; }

        public HierarchyErrorStructure(errortype _type, int _nodeIndex, int _associatedIndex, string _message)
        {
            type = _type;
            _nodeIndex = nodeIndex;
            associatedIndex = _associatedIndex;
            message = _message;
        }
    }

    

}
