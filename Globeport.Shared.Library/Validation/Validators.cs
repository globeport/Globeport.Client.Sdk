using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Validation
{
    public static class Validators
    {
        public const int MinCredentialsLength = 382;
        const int MaxCredentialsLength = 383;

        public const int MinEvidenceLength = 31;
        public const int MaxEvidenceLength = 33;

        public const int SessionIdLength = 43;

        public const int MaxUriLength = 2083;

        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 15;

        public const int MinPasswordLength = 6;
        public const int MinStrongPasswordLength = 12;
        public const int MaxPasswordLength = 64;

        public const int MinIdLength = 11;
        public const int MaxIdLength = 13;

        public const int MinShortNameLength = 1;
        public const int MaxShortNameLength = 32;

        public const int MinLongNameLength = 1;
        public const int MaxLongNameLength = 64;

        public const int MinShortDescriptionLength = 0;
        public const int MaxShortDescriptionLength = 256;

        public const int MinLongDescriptionLength = 0;
        public const int MaxLongDescriptionLength = 1024;

        public const int SignatureLength = 64;
        public const int TimestampedSignatureLength = 84;

        public const int MaxPacketDataLength = 131072;

        public const int RandomIdLength = 22;

        public const int MaxResourceDataLength = 131072;
        public const int MaxResourceDictionaryLength = 10;

        public static HashSet<char> HexChars = new HashSet<char>("0123456789ABCDEF".ToCharArray());
        public static HashSet<char> IdChars = new HashSet<char>("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
        public static HashSet<char> Base64Chars = new HashSet<char>("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_".ToCharArray()); 

        public static bool IsValidVersion(string version)
        {
            Version result;
            if (!Version.TryParse(version, out result)) return false;
            if (result > new Version(Globals.CurrentVersion)) return false;
            return true;
        }

        public static bool IsValidUsername(string username)
        {
            if (username.Length < MinUsernameLength) return false;
            if (username.Length > MaxUsernameLength) return false;
            if (username.StartsWith("-") || username.StartsWith("_")) return false;
            if (username.EndsWith("-") || username.EndsWith("_")) return false;
            if (username.ToCharArray().Any(i => !Base64Chars.Contains(i))) return false;
            if (Profanities.Instance.Contains(username.ToLowerInvariant())) return false;
            return true;
        }

        public static bool IsValidPassword(string password)
        {
            if (password.Length < MinPasswordLength) return false;
            if (password.Length > MaxPasswordLength) return false;

            var passwordChars = password.ToCharArray();

            return true;
        }

        public static bool IsStrongPassword(string password)
        {
            if (password.Length < MinStrongPasswordLength) return false;
            if (password.Length > MaxPasswordLength) return false;

            var passwordChars = password.ToCharArray();

            if (!passwordChars.Any(i => char.IsLower(i))) return false;
            if (!passwordChars.Any(i => char.IsUpper(i))) return false;
            if (!passwordChars.Any(i => char.IsDigit(i))) return false;
            if (!passwordChars.Any(i => char.IsPunctuation(i) || char.IsSeparator(i) || char.IsSymbol(i))) return false;

            return true;
        }

        public static bool IsValidCulture(string culture)
        {
            try
            {
                new CultureInfo(culture);
                return true;
            }
            catch(CultureNotFoundException e)
            {
                return false;
            }
        }

        public static bool IsValidHardwareId(string hardwardId)
        {
            return hardwardId.Length > 0 && hardwardId.Length < 256;
        }

        public static bool IsValidSalt(byte[] salt)
        {
            return salt.Length == 16;
        }

        public static bool IsValidVerifier(byte[] verifier)
        {
            return verifier.Length == 383 || verifier.Length == 382;
        }

        public static bool IsValidCredentials(byte[] credentials)
        {
            return credentials.Length >= MinCredentialsLength && credentials.Length <= MaxCredentialsLength;
        }

        public static bool IsValidEvidence(byte[] evidence)
        {
            return evidence.Length >= MinEvidenceLength && evidence.Length <= MaxEvidenceLength;
        }

        public static bool IsValidAbsoluteUri(string uri)
        {
            if (uri.Length > MaxUriLength) return false;
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute);
        }

        public static bool IsValidId(string id)
        {
            if (id == null) return false;
            if (id.Length < MinIdLength) return false;
            if (id.Length > MaxIdLength) return false;
            if (id.ToCharArray().Any(i => !IdChars.Contains(i))) return false;
            return true;
        }

        public static bool IsGuid(string id)
        {
            if (id == null) return false;
            Guid result;
            return Guid.TryParseExact(id, "N", out result);
        }

        public static bool IsValidShortName(string name)
        {
            if (name == null) return false;
            if (name.Length < MinShortNameLength) return false;
            if (name.Length > MaxShortNameLength) return false;
            if (name.ToCharArray().Any(i => char.IsControl(i))) return false;
            return true;
        }

        public static bool IsValidLongName(string name)
        {
            if (name == null) return false;
            if (name.Length < MinLongNameLength) return false;
            if (name.Length > MaxLongNameLength) return false;
            if (name.ToCharArray().Any(i => char.IsControl(i))) return false;
            return true;
        }

        public static bool IsValidShortDescription(string description)
        {
            if (description == null) return false;
            if (description.Length < MinShortDescriptionLength) return false;
            if (description.Length > MaxShortDescriptionLength) return false;
            if (description.ToCharArray().Any(i => char.IsControl(i))) return false;
            return true;
        }

        public static bool IsValidLongDescription(string description)
        {
            if (description == null) return false;
            if (description.Length < MinLongDescriptionLength) return false;
            if (description.Length > MaxLongDescriptionLength) return false;
            return true;
        }

        public static bool IsValidColor(string color)
        {
            if (color == null) return false;
            var colorChars = color.ToUpper().ToCharArray();
            if (colorChars.Length != 9 && colorChars.Length != 7) return false;
            if (colorChars[0] != '#') return false;
            if (colorChars.Skip(1).Any(i => !HexChars.Contains(i))) return false;
            return true;
        }

        public static bool IsValidSignature(byte[] signature)
        {
            return signature.Length == SignatureLength;
        }

        public static bool IsValidTimestampedSignature(byte[] signature)
        {
            return signature.Length == TimestampedSignatureLength;
        }

        public static bool IsValidEntityData(DataObject data)
        {
            return true;
        }

        public static bool IsValidInteractionData(byte[] data)
        {
            return true;
        }

        public static bool IsValidPacketData(byte[] data)
        {
            return data.Length < MaxPacketDataLength;
        }

        public static bool IsValidResourceData(byte[] data)
        {
            return data.Length <= MaxResourceDataLength;
        }

        public static bool IsValidAvatarType(string type)
        {
            return new[] { "Professional", "Personal", "Other" }.Contains(type);
        }
    }
}
