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
    ///  Class for testing RolesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class RolesApiTests : IDisposable
    {
        private RolesApi instance;

        public RolesApiTests()
        {
            instance = new RolesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of RolesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' RolesApi
            //Assert.IsType<RolesApi>(instance);
        }

        /// <summary>
        /// Test CreateRole
        /// </summary>
        [Fact]
        public void CreateRoleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateRoleRequest? createRoleRequest = null;
            //var response = instance.CreateRole(createRoleRequest);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test DeleteRole
        /// </summary>
        [Fact]
        public void DeleteRoleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //var response = instance.DeleteRole(roleId);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test GetRolePermission
        /// </summary>
        [Fact]
        public void GetRolePermissionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //string? sort = null;
            //int? pageSize = null;
            //string? nextToken = null;
            //var response = instance.GetRolePermission(roleId, sort, pageSize, nextToken);
            //Assert.IsType<List<RolesPermissionResponseInner>>(response);
        }

        /// <summary>
        /// Test GetRoles
        /// </summary>
        [Fact]
        public void GetRolesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string? sort = null;
            //int? pageSize = null;
            //string? nextToken = null;
            //var response = instance.GetRoles(sort, pageSize, nextToken);
            //Assert.IsType<GetRolesResponse>(response);
        }

        /// <summary>
        /// Test RemoveRolePermission
        /// </summary>
        [Fact]
        public void RemoveRolePermissionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //string permissionId = null;
            //var response = instance.RemoveRolePermission(roleId, permissionId);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test UpdateRolePermissions
        /// </summary>
        [Fact]
        public void UpdateRolePermissionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //UpdateRolePermissionsRequest updateRolePermissionsRequest = null;
            //var response = instance.UpdateRolePermissions(roleId, updateRolePermissionsRequest);
            //Assert.IsType<UpdateRolePermissionsResponse>(response);
        }

        /// <summary>
        /// Test UpdateRoles
        /// </summary>
        [Fact]
        public void UpdateRolesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //UpdateRolesRequest? updateRolesRequest = null;
            //var response = instance.UpdateRoles(roleId, updateRolesRequest);
            //Assert.IsType<SuccessResponse>(response);
        }
    }
}
