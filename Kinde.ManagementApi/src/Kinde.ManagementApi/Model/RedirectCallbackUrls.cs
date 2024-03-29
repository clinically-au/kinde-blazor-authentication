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
    /// RedirectCallbackUrls
    /// </summary>
    [DataContract(Name = "redirect_callback_urls")]
    public partial class RedirectCallbackUrls : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectCallbackUrls" /> class.
        /// </summary>
        /// <param name="redirectUrls">An application&#39;s redirect URLs..</param>
        public RedirectCallbackUrls(List<string> redirectUrls = default(List<string>))
        {
            this.RedirectUrls = redirectUrls;
        }

        /// <summary>
        /// An application&#39;s redirect URLs.
        /// </summary>
        /// <value>An application&#39;s redirect URLs.</value>
        [DataMember(Name = "redirect_urls", EmitDefaultValue = false)]
        public List<string> RedirectUrls { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class RedirectCallbackUrls {\n");
            sb.Append("  RedirectUrls: ").Append(RedirectUrls).Append("\n");
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
