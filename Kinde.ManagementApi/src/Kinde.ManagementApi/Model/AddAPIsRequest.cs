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
    /// AddAPIsRequest
    /// </summary>
    [DataContract(Name = "addAPIs_request")]
    public partial class AddAPIsRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAPIsRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected AddAPIsRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAPIsRequest" /> class.
        /// </summary>
        /// <param name="name">name (required).</param>
        /// <param name="audience">audience (required).</param>
        public AddAPIsRequest(string name = default(string), string audience = default(string))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for AddAPIsRequest and cannot be null");
            }
            this.Name = name;
            // to ensure "audience" is required (not null)
            if (audience == null)
            {
                throw new ArgumentNullException("audience is a required property for AddAPIsRequest and cannot be null");
            }
            this.Audience = audience;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Audience
        /// </summary>
        [DataMember(Name = "audience", IsRequired = true, EmitDefaultValue = true)]
        public string Audience { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class AddAPIsRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Audience: ").Append(Audience).Append("\n");
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
