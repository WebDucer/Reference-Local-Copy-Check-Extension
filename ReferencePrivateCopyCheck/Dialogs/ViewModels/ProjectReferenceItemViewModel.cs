using System;
using System.Collections.Generic;
using System.Linq;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using GalaSoft.MvvmLight;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ProjectReferenceItemViewModel : ViewModelBase {
      private readonly Reference _originReference;
      private readonly ReferenceTemplateModel _template;

      #region Constructor

      public ProjectReferenceItemViewModel(Reference originReference, ICollection<ProjectTemplateModel> configurations) {
         _originReference = originReference;
         _template = GetTemplateModel(originReference, configurations);
      }

      #endregion

      #region Properties

      #region Is Private Copy
      public bool IsPrivateCopy {
         get {
            return _originReference.CopyLocal;
         }
         set {
            if (!Equals(_originReference.CopyLocal, value)) {
               _originReference.CopyLocal = value;
               RaisePropertyChanged(() => IsPrivateCopy);
            }
         }
      }
      #endregion

      #region Template Is Private Copy

      public bool? IsPrivateCopyInTemplate {
         get {
            return _template.HasLocalCopy;
         }
      }
      #endregion

      #region Project Name

      public string ProjectName {
         get {
            return _originReference.ContainingProject.Name;
         }
      }
      #endregion

      #region Reference Name

      public string ReferenceName {
         get {
            return _originReference.Name;
         }
      }
      #endregion
      #endregion

      #region Helper

      private static ReferenceTemplateModel GetTemplateModel(Reference originReference, ICollection<ProjectTemplateModel> configurations) {
         if (configurations == null) {
            throw new ArgumentNullException("configurations");
         }

         var projectTemplate = configurations.FirstOrDefault(f => Equals(f.ProjectIdentity, originReference.ContainingProject.UniqueName));
         if (projectTemplate == null) {
            projectTemplate = new ProjectTemplateModel(originReference.ContainingProject.UniqueName);
            configurations.Add(projectTemplate);
         }

         var referenceTemplate = projectTemplate.AssignedReferences.FirstOrDefault(f => Equals(f.ReferenceIdentity, originReference.Identity));
         if (referenceTemplate == null) {
            referenceTemplate = new ReferenceTemplateModel(originReference.Identity);
            projectTemplate.AssignedReferences.Add(referenceTemplate);
         }

         return referenceTemplate;
      }

      #endregion
   }
}