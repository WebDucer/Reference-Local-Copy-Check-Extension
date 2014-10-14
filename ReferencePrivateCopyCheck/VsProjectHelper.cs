using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck
{
    public static class VsProjectHelper {
       private const int _MAX_RECURSION_COUNT = 10;

       #region Publics
       public static IEnumerable<Project> GetCsharpProjects(this Projects @this, int level = 0) {
          if(@this == null) { yield break; }

          foreach (Project project in @this) {
             if (project.IsOfType(Constants.ProjectType.VISUAL_C_SHARP)) {
                yield return project;
             } else if (project.ProjectItems != null && level < _MAX_RECURSION_COUNT) {
                foreach(Project subProject in project.ProjectItems.GetCsharpProjects(level)) {
                   yield return subProject;
                }
             }
          }
       }

       public static string GetConfigurationFilePath(this string solutionFileName, string configFileExtension) {
          var configFileName = string.Empty;

          if(solutionFileName != null) {
             var solutionPath = Path.GetDirectoryName(solutionFileName);
             if(solutionPath != null) {
                configFileName = Path.Combine(solutionPath,
                   Path.GetFileNameWithoutExtension(solutionFileName) + configFileExtension);
             }
          }

          return configFileName;
       }
       #endregion

       #region Privates
       private static IEnumerable<Project> GetCsharpProjects(this ProjectItems @this, int level = 0) {
          if(@this == null) { yield break; }

          foreach(ProjectItem projectItem in @this) {
             var project = projectItem.SubProject;

             if(project == null) { yield break; }

             if(project.IsOfType(Constants.ProjectType.VISUAL_C_SHARP)) {
                yield return project;
             } else if(project.IsOfType(Constants.ProjectType.SOLUTION_FOLDER) && level < _MAX_RECURSION_COUNT) {
                foreach(Project subProject in project.ProjectItems.GetCsharpProjects(level++)) {
                   yield return subProject;
                }
             }
          }
       }

       private static bool IsOfType(this Project @this, string projectType) {
          return string.Equals(@this.Kind, projectType, StringComparison.OrdinalIgnoreCase);
       }
       #endregion
    }
}
