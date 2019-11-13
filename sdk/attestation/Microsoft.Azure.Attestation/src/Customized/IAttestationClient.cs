// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace Microsoft.Azure.Attestation
{
    public partial interface IAttestationClient : System.IDisposable
    {
        /// <summary>
        /// Unique name for the calling application. This is only used for telemetry and debugging.
        /// </summary>
        string NameHeader { get; set; }
    }
}
