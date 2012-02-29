using System;
using System.ComponentModel.Composition;
using System.Drawing;
using Microsoft.VisualStudio.Web.Mvc.Extensibility;
using Microsoft.VisualStudio.Web.Mvc.Extensibility.Recipes;

namespace HomeworkSubmission.MVC {
    [Export(typeof(IRecipe))]
    public class MyRecipe : IFolderRecipe {
        public bool Execute(ProjectFolder folder) {
            throw new System.NotImplementedException();
        }

        public bool IsValidTarget(ProjectFolder folder) {
            throw new System.NotImplementedException();
        }

        public string Description {
            get { throw new NotImplementedException(); }
        }

        public Icon Icon {
            get { throw new NotImplementedException(); }
        }

        public string Name {
            get { throw new NotImplementedException(); }
        }
    }
}

