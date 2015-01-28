using System.Collections.Generic;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Design {
   internal class DesignTimeListByReferenceViewModel : ListByReferenceViewModel {
      public DesignTimeListByReferenceViewModel()
         : base(LoadDesignTimeData(), LoadDesignTimeTemplates()) {}

      private static IEnumerable<VSProject> LoadDesignTimeData() {
         var proj1Refs = new DesignReferenceList();
         var proj2Refs = new DesignReferenceList();
         var proj1 = new DesignVSProject {
            References = proj1Refs,
            Project = new DesignProject {
               Name = "Project 1",
               UniqueName = "Project 1",
               IsDirty = false,
               Saved = true,
            }
         };
         var proj2 = new DesignVSProject {
            References = proj2Refs,
            Project = new DesignProject {
               Name = "Project 2",
               UniqueName = "Project 2",
               IsDirty = true,
               Saved = false
            }
         };

         var projectList = new List<VSProject> {
            proj1,
            proj2
         };

         var projRef = proj1Refs.Add("Ref11", proj1);
         projRef.CopyLocal = false;
         projRef = proj1Refs.Add("Ref12", proj1);
         projRef.CopyLocal = false;
         projRef = proj1Refs.Add("Ref13", proj1);
         projRef.CopyLocal = true;
         projRef = proj1Refs.Add("Ref14", proj1);
         projRef.CopyLocal = false;

         projRef = proj2Refs.Add("Ref21", proj2);
         projRef.CopyLocal = false;
         projRef = proj2Refs.Add("Ref22", proj2);
         projRef.CopyLocal = false;
         projRef = proj2Refs.Add("Ref23", proj2);
         projRef.CopyLocal = true;
         projRef = proj2Refs.Add("Ref24", proj2);
         projRef.CopyLocal = false;

         return projectList;
      }

      private static ICollection<ProjectTemplateModel> LoadDesignTimeTemplates() {
         return new List<ProjectTemplateModel> {
            new ProjectTemplateModel("Project 1") {
               AssignedReferences = new List<ReferenceTemplateModel> {
                  new ReferenceTemplateModel("Ref11", true),
                  new ReferenceTemplateModel("Ref12", false)
               }
            },
            new ProjectTemplateModel("Project 2") {
               AssignedReferences = new List<ReferenceTemplateModel> {
                  new ReferenceTemplateModel("Ref21", false)
               }
            }
         };
      }
   }
}