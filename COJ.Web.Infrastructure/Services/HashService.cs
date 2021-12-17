using COJ.Web.Domain.Abstract;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using COJ.Web.Domain.Values;

namespace COJ.Web.Infrastructure.Services;

/// <summary>
/// Implements the standard Identity password hashing.
/// Based on: <see href="https://github.com/dotnet/AspNetCore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs"/>
/// </summary>
public sealed class HashService : IHashService
{
    /* =======================
     * HASHED PASSWORD FORMATS
     * =======================
     *
     * Version 2:
     * NOT IMPLEMENTED
     *
     * Version 3:
     * PBKDF2 with HMAC-SHA256, 128-bit salt, 256-bit subkey, 10000 iterations.
     * Format: { 0x01, prf (UInt32), iter count (UInt32), salt length (UInt32), salt, subkey }
     * (All UInt32s are stored big-endian.)
     */
    private readonly int _iterCount = 10000;
    private readonly int _saltSize = 128 / 8;
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
    private readonly KeyDerivationPrf _prf;

    public HashService()
    {
        _prf = KeyDerivationPrf.HMACSHA256;
        _rng = RandomNumberGenerator.Create();
    }

    /// <summary>
    /// Returns a <see cref="HashVerificationResult"/> indicating the result of a hash comparison.
    /// </summary>
    /// <param name="hashed">The hash value.</param>
    /// <param name="input">The string supplied for comparison.</param>
    /// <returns>A <see cref="HashVerificationResult"/> indicating the result of a hash comparison.</returns>
    /// <remarks>Implementations of this method should be time consistent.</remarks>
    public HashVerificationResult VerifyHash(string hashed, string input)
    {
        byte[] decodedHashedPassword = Convert.FromBase64String(hashed);

        // read the format marker from the hashed password
        if (decodedHashedPassword.Length == 0)
        {
            return HashVerificationResult.Failed;
        }

        switch (decodedHashedPassword[0])
        {
            case 0x00:
                throw new NotImplementedException();

            case 0x01:
                int embeddedIterCount;
                if (InnerVerify(decodedHashedPassword, input, out embeddedIterCount))
                {
                    // If this hasher was configured with a higher iteration count, change the entry now.
                    return (embeddedIterCount < _iterCount)
                        ? HashVerificationResult.SuccessRehashNeeded
                        : HashVerificationResult.Success;
                }
                else
                {
                    return HashVerificationResult.Failed;
                }

            default:
                return HashVerificationResult.Failed; // unknown format marker
        }
    }

    /// <summary>
    /// Returns a hashed representation of the supplied <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>A hashed representation of the supplied <paramref name="input"/>.</returns>
    public string ComputeHash(string input)
    {
        return Convert.ToBase64String(InnerHash(input, _rng));
    }

    private static bool InnerVerify(byte[] hashedPassword, string password, out int iterCount)
    {
        iterCount = default;

        try
        {
            // Read header information
            KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
            {
                return false;
            }
            byte[] salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            int subkeyLength = hashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8)
            {
                return false;
            }
            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
#if NETSTANDARD2_0 || NETFRAMEWORK
            return ByteArraysEqual(actualSubkey, expectedSubkey);
#elif NETCOREAPP
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
#else
#error Update target frameworks
#endif
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }
    private byte[] InnerHash(string password, RandomNumberGenerator rng)
    {
        byte[] salt = new byte[_saltSize];
        rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: _iterCount,
            numBytesRequested: 256 / 8);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)_prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)_iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)_saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + _saltSize, subkey.Length);

        return outputBytes;
    }
    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }
    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)(buffer[offset + 0]) << 24)
            | ((uint)(buffer[offset + 1]) << 16)
            | ((uint)(buffer[offset + 2]) << 8)
            | ((uint)(buffer[offset + 3]));
    }
}

