/*
 * Kinde Management API
 *
 * Provides endpoints to manage your Kinde Businesses
 *
 * The version of the OpenAPI document: 1
 * Contact: support@kinde.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xunit;

using Kinde.ManagementApi.Client;
using Kinde.ManagementApi.Api;
// uncomment below to import models
//using Kinde.ManagementApi.Model;

namespace Kinde.ManagementApi.Test.Api
{
    /// <summary>
    ///  Class for testing IndustriesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class IndustriesApiTests : IDisposable
    {
        private IndustriesApi instance;

        public IndustriesApiTests()
        {
            instance = new IndustriesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of IndustriesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' IndustriesApi
            //Assert.IsType<IndustriesApi>(instance);
        }

        /// <summary>
        /// Test GetIndustries
        /// </summary>
        [Fact]
        public void GetIndustriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string? industryKey = null;
            //string? name = null;
            //var response = instance.GetIndustries(industryKey, name);
            //Assert.IsType<SuccessResponse>(response);
        }
    }
}
