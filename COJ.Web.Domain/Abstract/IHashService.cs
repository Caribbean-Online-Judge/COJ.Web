using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Abstract;

public interface IHashService
{
    /// <summary>
    /// Returns a hashed representation of the supplied <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>A hashed representation of the supplied <paramref name="input"/>.</returns>
    string ComputeHash(string input);
    /// <summary>
    /// Returns a <see cref="HashVerificationResult"/> indicating the result of a hash comparison.
    /// </summary>
    /// <param name="hashed">The hash value.</param>
    /// <param name="input">The string supplied for comparison.</param>
    /// <returns>A <see cref="HashVerificationResult"/> indicating the result of a hash comparison.</returns>
    /// <remarks>Implementations of this method should be time consistent.</remarks>
    HashVerificationResult VerifyHash(string hash, string input);
}

