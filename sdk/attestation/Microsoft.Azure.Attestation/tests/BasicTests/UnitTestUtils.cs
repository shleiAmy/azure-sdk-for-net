// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Attestation.Tests.BasicTests
{
    public class UnitTestUtils
    {
        public static class SgxPolicy
        {
            public const string Minimal = @"{""$version"":1, ""$claims"":[""tee""], ""tee"":""$tee""}";

            public const string Default =
                "{\"$version\": 1," +
                "\"$allow-debuggable\": true," +
                "\"$claims\": [\"is-debuggable\",\"sgx-mrsigner\",\"sgx-mrenclave\",\"product-id\",\"svn\",\"tee\"]," +
                "\"is-debuggable\": \"$is-debuggable\"," +
                "\"sgx-mrsigner\": \"$sgx-mrsigner\"," +
                "\"sgx-mrenclave\": \"$sgx-mrenclave\"," +
                "\"product-id\": \"$product-id\"," +
                "\"svn\": \"$svn\"," +
                "\"tee\": \"$tee\"}";
        }

        internal static string GenerateUnsealedJsonWebToken(string jwtBody)
        {
            string returnValue = "eyJhbGciOiJub25lIn0.";
            string encodedDocument = Base64Url.Encode(Encoding.UTF8.GetBytes(jwtBody));
            returnValue += encodedDocument;
            returnValue += ".";
            return returnValue;
        }
    }
}
