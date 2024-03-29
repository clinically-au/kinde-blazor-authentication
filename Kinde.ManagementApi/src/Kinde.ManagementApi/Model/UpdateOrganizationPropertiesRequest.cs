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
    /// UpdateOrganizationPropertiesRequest
    /// </summary>
    [DataContract(Name = "UpdateOrganizationProperties_request")]
    public partial class UpdateOrganizationPropertiesRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationPropertiesRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UpdateOrganizationPropertiesRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationPropertiesRequest" /> class.
        /// </summary>
        /// <param name="properties">Property keys and values (required).</param>
        public UpdateOrganizationPropertiesRequest(Object properties = default(Object))
        {
            // to ensure "properties" is required (not null)
            if (properties == null)
            {
                throw new ArgumentNullException("properties is a required property for UpdateOrganizationPropertiesRequest and cannot be null");
            }
            this.Properties = properties;
        }

        /// <summary>
        /// Property keys and values
        /// </summary>
        /// <value>Property keys and values</value>
        [DataMember(Name = "properties", IsRequired = true, EmitDefaultValue = true)]
        public Object Properties { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateOrganizationPropertiesRequest {\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
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
