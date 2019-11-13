// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.Azure.Attestation.Models;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Microsoft.Azure.Attestation.Tests.BasicTests
{
    //public enum TeeKind
    //{
    //    SgxEnclave = 0,
    //    OpenEnclave = 1,
    //    CyResComponent = 2,
    //    AzureGuest = 3
    //}
    public class AttestationOperationsTests : IClassFixture<AttestationTestFixture> 
    {
        private AttestationTestFixture fixture;

        private void Initialize()
        {
            // TOdo
        }


        public AttestationOperationsTests(AttestationTestFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public void UpdatePolicy()
        {
            using (MockContext ctx = MockContext.Start(this.GetType()))
            {
                var attestationClient = GetAttestationClient();
                string sgxPolicyAllowDebugging = @"{
                        ""$version"": 1,
                        ""$allow-debuggable"" : true,
                        ""$claims"":[
                            ""is-debuggable"" ,
                            ""sgx-mrsigner"",
                            ""sgx-mrenclave"",
                            ""product-id"",
                            ""svn"",
                            ""tee"",
                            ""NotDebuggable""
                        ],
                        ""NotDebuggable"": {""yes"":{""$is-debuggable"":true, ""$mandatory"":true, ""$visible"":false}},
                        ""is-debuggable"" : ""$is-debuggable"",
                        ""sgx-mrsigner"" : ""$sgx-mrsigner"",
                        ""sgx-mrenclave"" : ""$sgx-mrenclave"",
                        ""product-id"" : ""$product-id"",
                        ""svn"" : ""$svn"",
                        ""tee"" : ""$tee""
                    }";

                // set policy
                var getResult = attestationClient.Policy.PrepareToSetWithHttpMessagesAsync("SgxEnclave", UnitTestUtils.GenerateUnsealedJsonWebToken(sgxPolicyAllowDebugging)).Result;
                Assert.Equal(HttpStatusCode.OK, getResult.Response.StatusCode);
                Assert.IsType<string>(getResult.Body);

                // delete policy
                var deleteResult = attestationClient.Policy.DeleteWithHttpMessagesAsync("SgxEnclave", UnitTestUtils.GenerateUnsealedJsonWebToken(string.Empty)).Result;
                Assert.Equal(HttpStatusCode.OK, deleteResult.Response.StatusCode);

                // verify we're now seeing the default SGX policy.
                var sgxPolicy = attestationClient.Policy.GetWithHttpMessagesAsync("SgxEnclave").Result.Body;
                Assert.True(sgxPolicy is AttestationPolicy);
                Assert.Equal(UnitTestUtils.GenerateUnsealedJsonWebToken(UnitTestUtils.SgxPolicy.Default), ((AttestationPolicy)sgxPolicy).Policy);

            }
        } 

        public AttestationClient GetAttestationClient()
        {
            Initialize();
            return fixture.CreateAttestationClient();
        }

    }


}
