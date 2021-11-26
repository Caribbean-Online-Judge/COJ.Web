namespace COJ.Web.Domain.Values
{
    /// <summary>
    /// Specifies the results for Hash verification.
    /// </summary>
    public enum HashVerificationResult
    {
        /// <summary>
        /// Indicates hash verification failed.
        /// </summary>
        Failed = 0,

        /// <summary>
        /// Indicates hash verification was successful.
        /// </summary>
        Success = 1,

        /// <summary>
        /// Indicates hash verification was successful however the password was encoded using a deprecated algorithm
        /// and should be rehashed and updated.
        /// </summary>
        SuccessRehashNeeded = 2
    }
}
