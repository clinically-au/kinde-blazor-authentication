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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using FileParameter = Kinde.ManagementApi.Client.FileParameter;
using OpenAPIDateConverter = Kinde.ManagementApi.Client.OpenAPIDateConverter;

namespace Kinde.ManagementApi.Model
{
    /// <summary>
    /// UpdateOrganizationUsersRequest
    /// </summary>
    [DataContract(Name = "UpdateOrganizationUsers_request")]
    public partial class UpdateOrganizationUsersRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationUsersRequest" /> class.
        /// </summary>
        /// <param name="users">Users to add, update or remove from the organization..</param>
        public UpdateOrganizationUsersRequest(List<UpdateOrganizationUsersRequestUsersInner> users = default(List<UpdateOrganizationUsersRequestUsersInner>))
        {
            this.Users = users;
        }

        /// <summary>
        /// Users to add, update or remove from the organization.
        /// </summary>
        /// <value>Users to add, update or remove from the organization.</value>
        [DataMember(Name = "users", EmitDefaultValue = false)]
        public List<UpdateOrganizationUsersRequestUsersInner> Users { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateOrganizationUsersRequest {\n");
            sb.Append("  Users: ").Append(Users).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
