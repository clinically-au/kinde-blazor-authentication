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
    /// CreateFeatureFlagRequest
    /// </summary>
    [DataContract(Name = "CreateFeatureFlag_request")]
    public partial class CreateFeatureFlagRequest : IValidatableObject
    {
        /// <summary>
        /// The variable type.
        /// </summary>
        /// <value>The variable type.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum Str for value: str
            /// </summary>
            [EnumMember(Value = "str")]
            Str = 1,

            /// <summary>
            /// Enum Int for value: int
            /// </summary>
            [EnumMember(Value = "int")]
            Int = 2,

            /// <summary>
            /// Enum Bool for value: bool
            /// </summary>
            [EnumMember(Value = "bool")]
            Bool = 3
        }


        /// <summary>
        /// The variable type.
        /// </summary>
        /// <value>The variable type.</value>
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeEnum Type { get; set; }
        /// <summary>
        /// Allow the flag to be overridden at a different level.
        /// </summary>
        /// <value>Allow the flag to be overridden at a different level.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AllowOverrideLevelEnum
        {
            /// <summary>
            /// Enum Env for value: env
            /// </summary>
            [EnumMember(Value = "env")]
            Env = 1,

            /// <summary>
            /// Enum Org for value: org
            /// </summary>
            [EnumMember(Value = "org")]
            Org = 2,

            /// <summary>
            /// Enum Usr for value: usr
            /// </summary>
            [EnumMember(Value = "usr")]
            Usr = 3
        }


        /// <summary>
        /// Allow the flag to be overridden at a different level.
        /// </summary>
        /// <value>Allow the flag to be overridden at a different level.</value>
        [DataMember(Name = "allow_override_level", EmitDefaultValue = false)]
        public AllowOverrideLevelEnum? AllowOverrideLevel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFeatureFlagRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreateFeatureFlagRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFeatureFlagRequest" /> class.
        /// </summary>
        /// <param name="name">The name of the flag. (required).</param>
        /// <param name="description">Description of the flag purpose..</param>
        /// <param name="key">The flag identifier to use in code. (required).</param>
        /// <param name="type">The variable type. (required).</param>
        /// <param name="allowOverrideLevel">Allow the flag to be overridden at a different level..</param>
        /// <param name="defaultValue">Default value for the flag used by environments and organizations. (required).</param>
        public CreateFeatureFlagRequest(string name = default(string), string description = default(string), string key = default(string), TypeEnum type = default(TypeEnum), AllowOverrideLevelEnum? allowOverrideLevel = default(AllowOverrideLevelEnum?), string defaultValue = default(string))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for CreateFeatureFlagRequest and cannot be null");
            }
            this.Name = name;
            // to ensure "key" is required (not null)
            if (key == null)
            {
                throw new ArgumentNullException("key is a required property for CreateFeatureFlagRequest and cannot be null");
            }
            this.Key = key;
            this.Type = type;
            // to ensure "defaultValue" is required (not null)
            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue is a required property for CreateFeatureFlagRequest and cannot be null");
            }
            this.DefaultValue = defaultValue;
            this.Description = description;
            this.AllowOverrideLevel = allowOverrideLevel;
        }

        /// <summary>
        /// The name of the flag.
        /// </summary>
        /// <value>The name of the flag.</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the flag purpose.
        /// </summary>
        /// <value>Description of the flag purpose.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The flag identifier to use in code.
        /// </summary>
        /// <value>The flag identifier to use in code.</value>
        [DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
        public string Key { get; set; }

        /// <summary>
        /// Default value for the flag used by environments and organizations.
        /// </summary>
        /// <value>Default value for the flag used by environments and organizations.</value>
        [DataMember(Name = "default_value", IsRequired = true, EmitDefaultValue = true)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateFeatureFlagRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  AllowOverrideLevel: ").Append(AllowOverrideLevel).Append("\n");
            sb.Append("  DefaultValue: ").Append(DefaultValue).Append("\n");
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
