// Guids.cs
// MUST match guids.h

using System;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck
{
    static class GuidList
    {
        public const string guidReferencePrivateCopyCheckPkgString = "870b8cc6-9403-452b-8565-45d632f9152d";
        public const string guidReferencePrivateCopyCheckCmdSetString = "bf2de637-370b-4dc6-a054-f6c208dee742";

        public static readonly Guid guidReferencePrivateCopyCheckCmdSet = new Guid(guidReferencePrivateCopyCheckCmdSetString);
    };
}