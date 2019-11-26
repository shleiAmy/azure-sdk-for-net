// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.StorageCache.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An error response.
    /// </summary>
    public partial class CloudErrorBody
    {
        /// <summary>
        /// Initializes a new instance of the CloudErrorBody class.
        /// </summary>
        public CloudErrorBody()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CloudErrorBody class.
        /// </summary>
        /// <param name="code">An identifier for the error. Codes are invariant
        /// and are intended to be consumed programmatically.</param>
        /// <param name="details">A list of additional details about the
        /// error.</param>
        /// <param name="message">A message describing the error, intended to
        /// be suitable for display in a user interface.</param>
        /// <param name="target">The target of the particular error. For
        /// example, the name of the property in error.</param>
        public CloudErrorBody(string code = default(string), IList<CloudErrorBody> details = default(IList<CloudErrorBody>), string message = default(string), string target = default(string))
        {
            Code = code;
            Details = details;
            Message = message;
            Target = target;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets an identifier for the error. Codes are invariant and
        /// are intended to be consumed programmatically.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a list of additional details about the error.
        /// </summary>
        [JsonProperty(PropertyName = "details")]
        public IList<CloudErrorBody> Details { get; set; }

        /// <summary>
        /// Gets or sets a message describing the error, intended to be
        /// suitable for display in a user interface.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the target of the particular error. For example, the
        /// name of the property in error.
        /// </summary>
        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }

    }
}
