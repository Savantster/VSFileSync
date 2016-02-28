// Guids.cs
// MUST match guids.h
using System;

namespace Raydude.VSFileSync
{
    static class GuidList
    {
        public const string guidVSFileSyncPkgString = "3a71bc8f-e670-4a96-a823-22118833725d";
        public const string guidVSFileSyncCmdSetString = "b3466b9e-4f2e-434c-adf7-dc6ecf2ea24b";
        public const string guidToolWindowPersistanceString = "7fa4f00f-3361-49fb-88eb-8a702cc3ad69";

        public static readonly Guid guidVSFileSyncCmdSet = new Guid(guidVSFileSyncCmdSetString);
    };
}