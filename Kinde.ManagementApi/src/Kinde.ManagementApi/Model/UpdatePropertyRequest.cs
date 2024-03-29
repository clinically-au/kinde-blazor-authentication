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
    /// UpdatePropertyRequest
    /// </summary>
    [DataContract(Name = "UpdateProperty_request")]
    public partial class UpdatePropertyRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePropertyRequest" /> class.
        /// </summary>
        /// <param name="name">The name of the property..</param>
        /// <param name="description">Description of the property purpose..</param>
        /// <param name="isPrivate">Whether the property can be included in id and access tokens..</param>
        public UpdatePropertyRequest(string name = default(string), string description = default(string), bool isPrivate = default(bool))
        {
            this.Name = name;
            this.Description = description;
            this.IsPrivate = isPrivate;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the property purpose.
        /// </summary>
        /// <value>Description of the property purpose.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Whether the property can be included in id and access tokens.
        /// </summary>
        /// <value>Whether the property can be included in id and access tokens.</value>
        [DataMember(Name = "is_private", EmitDefaultValue = true)]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdatePropertyRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  IsPrivate: ").Append(IsPrivate).Append("\n");
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
