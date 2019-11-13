// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Attestation;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.Azure.Management.ResourceManager;
using System.Net;
using Microsoft.Rest.TransientFaultHandling;
using System.Diagnostics;


namespace Microsoft.Azure.Attestation.Tests
{
    public class AttestationTestFixture : IDisposable
    {

        private bool fromConfig;
        private string rgName = "";
        public string tenantName;
        public ClientCredential clientCredential;

        private const string AADAuthority = "https://login.microsoftonline.com/common";
        private const string AADAudience = "https://attest.azure.net";
        private const string ClientID = "1950a258-227b-4e31-a9cf-717495945fc2";
        private const string BearerPrefix = "Bearer ";
        private const string AuthorizationHeader = "Authorization";

        public AttestationTestFixture()
        {
            Initialize(this.GetType());
        }

        public void Initialize(Type type)
        {
            HttpMockServer.FileSystemUtilsObject = new FileSystemUtils();
            HttpMockServer.Initialize(type, "InitialCreation", HttpRecorderMode.Record);
            HttpMockServer.CreateInstance();

            if (HttpMockServer.Mode == HttpRecorderMode.Record)
            {
                fromConfig = FromConfiguration();
            }
        }


        private bool FromConfiguration()
        {
            return true;

        }

        public AttestationClient CreateAttestationClient()
        {
            string accessToken = GetAccessTokenAsync().Result;
            AttestationCredentials credentials = new AttestationCredentials(accessToken);
            var myclient = new AttestationClient(credentials, GetHandlers());
            return myclient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var context = new AuthenticationContext(
                authority: AADAuthority,
                validateAuthority: true);
            AuthenticationResult result = await context.AcquireTokenAsync(AADAudience, ClientID, new UserCredential());
            return result.AccessToken;
        }

        public DelegatingHandler[] GetHandlers()
        {
            HttpMockServer server = HttpMockServer.CreateInstance();
            var testHttpHandler = new CustomDelegatingHandler();
            return new DelegatingHandler[] { server, testHttpHandler };
        }

        public void Dispose()
        {
            if (HttpMockServer.Mode == HttpRecorderMode.Record && !fromConfig)
            {
                var testEnv = TestEnvironmentFactory.GetTestEnvironment();
                var context = new MockContext();

                var resourcesClient = context.GetServiceClient<ResourceManagementClient>();
                resourcesClient.ResourceGroups.Delete(rgName);
            }
        }
    }
}
