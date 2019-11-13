﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace Microsoft.Azure.Attestation
{
    /// <summary>
    /// The Attestation credential class that implements <see cref="CustomDelegatingHandler"/>
    /// </summary>
    public class CustomDelegatingHandler : DelegatingHandler
    {
        internal AttestationClient Client { get; set; }

        internal const string InternalNameHeader = "csharpsdk";

        /// <summary>
        /// SendAsync.
        /// </summary>
        /// <param name="request"> request. </param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var appName = InternalNameHeader;
            if (!string.IsNullOrWhiteSpace(Client.NameHeader))
            {
                appName += $",{Client.NameHeader}";
            }

            request.Headers.Add("x-ms-app", appName);

            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            return response;
        }
    }
}