using System;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck {
   public static class Constants {
      public const string CONFIGURATION_FILE_NAME_EXTENSION = ".vsrefconfig";

      public static class ProjectType {
         public const string SOLUTION_FOLDER = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
         public static readonly Guid SolutionFolderGuid = new Guid(SOLUTION_FOLDER);

         public const string VISUAL_BASIC = "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}";
         public static readonly Guid VisualBasicGuid = new Guid(VISUAL_BASIC);

         public const string VISUAL_C_SHARP = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
         public static readonly Guid VisuslCSharpGuid = new Guid(VISUAL_C_SHARP);

         public const string VISUAL_C_PLUS_PLUS = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";
         public static readonly Guid VisualCPlusPlusGuid = new Guid(VISUAL_C_PLUS_PLUS);

         public const string WEB_PROJECT = "{E24C65DC-7377-472b-9ABA-BC803B73C61A}";
         public static readonly Guid WebProjectGuid = new Guid(WEB_PROJECT);
      }
   }
}