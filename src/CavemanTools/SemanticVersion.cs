using System;

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CavemanTools
{
    class SemanticBuildValue:IComparable<SemanticBuildValue>
    {
        private readonly string _value;
        private const string Validator = @"^[0-9A-Za-z]+$";
        public SemanticBuildValue(string value)
        {
            value.MustNotBeNull();
            _value = value;
            var reg = new Regex(Validator,RegexOptions.IgnoreCase|RegexOptions.Singleline|RegexOptions.CultureInvariant);
            foreach(var tk in value.Split('.'))
            {
                if (!reg.IsMatch(tk))
                {
                    throw new FormatException("Build doesn't respect semantic versioning");
                }
                _tokens.Add(new SemanticToken(tk));
            }
        }

        private List<SemanticToken> _tokens= new List<SemanticToken>();

        public int CompareTo(SemanticBuildValue other)
        {
            if (other == null) return 1;
            return Comparison(other);

        }

        protected int Comparison(SemanticBuildValue other)
        {
            var all = Math.Min(_tokens.Count, other._tokens.Count);
            for (int i = 0; i < all; i++)
            {
                var r = _tokens[i].CompareTo(other._tokens[i]);
                if (r != 0)
                {
                    return r;
                }
            }

            return _tokens.Count.CompareTo(other._tokens.Count);
        }

        public override string ToString()
        {
            return _value;
        }
    }

    class SemanticToken:IComparable<SemanticToken>,IComparable<uint>,IComparable<string>
    {
        private uint _intValue;
        private string _stringValue;
        
        public SemanticToken(string value)
        {
            if (!uint.TryParse(value,out _intValue))
            {
                _stringValue = value;
            }
        }

        public int CompareTo(SemanticToken other)
        {
            if (_stringValue!=null)
            {
                return other.CompareTo(_stringValue)*(-1);
            }
            return other.CompareTo(_intValue)*(-1);
        }

        public int CompareTo(uint other)
        {
            if (_stringValue!=null)
            {
                return 1;
            }
            return _intValue.CompareTo(other);
        }

        public int CompareTo(string other)
        {
            if (_stringValue == null) return -1;
            return _stringValue.CompareTo(other);
        }
    }

    class SemanticPreReleaseValue:SemanticBuildValue,IComparable<SemanticPreReleaseValue>
    {
        public SemanticPreReleaseValue(string value) : base(value)
        {
        }

        public int CompareTo(SemanticPreReleaseValue other)
        {
            if (other == null) return -1;
            return Comparison(other);
        }
    }

    /// <summary>
    /// Semantic version http://semver.org/
    /// </summary>
    public class SemanticVersion : IComparable<SemanticVersion>
    {
        private string _toString;
        public uint Major { get; private set; }
        public uint Minor { get; private set; }
        public uint Patch { get; private set; }
        private SemanticPreReleaseValue _pre;
        private SemanticBuildValue _build;
        public string Build
        {
            get { return _build == null ? "" : _build.ToString(); }
        }
        public string PreRelease
        {
            get { return _pre == null ? "" : _pre.ToString(); }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version">major.minor.patch[-preRelease][+build]</param>
        public SemanticVersion(string version)
        {
            var rest = version;
            string build = null;
            string pre = null;
            string major = "0";
            string minor = "0";
            string patch = "0";
            var i = version.IndexOf('+');
            if (i>0)
            {
                build = rest.Substring(i+1);
                rest = rest.Substring(0, i);
            }

            i = rest.IndexOf('-');
            if (i>0)
            {
                pre = rest.Substring(i+1);
                rest = rest.Substring(0, i);
            }

            var remain = rest.Split('.');
            if (remain.Length==3)
            {
                patch = remain[2];
            }
            if (remain.Length>=2)
            {
                minor = remain[1];
            }

            major = remain[0];

            Setup(uint.Parse(major),uint.Parse(minor),uint.Parse(patch),pre,build);
        }

        public SemanticVersion(uint major, uint minor = 0, uint patch = 0, string preRelease = null,string build=null)
        {
            Setup(major, minor, patch, preRelease, build);
        }

        /// <summary>
        /// Creates semantic version from Version
        /// </summary>
        /// <param name="version">The Revision number is ignored</param>
        public SemanticVersion(Version version,string preRelease=null,string build=null)
        {
            Setup((uint)version.Major,(uint)version.Minor,(uint)version.Build,preRelease,build);
        }


        private void Setup(uint major, uint minor, uint patch, string preRelease, string build)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            if (preRelease != null) _pre = new SemanticPreReleaseValue(preRelease);

            if (build != null) _build = new SemanticBuildValue(build);

            _toString = string.Format("{0}.{1}.{2}", Major, Minor, Patch);
            if (!string.IsNullOrEmpty(PreRelease))
            {
                _toString = _toString + "-" + PreRelease;
            }

            if (!Build.IsNullOrEmpty())
            {
                _toString = _toString + "+" + Build;
            }
        }

        public override string ToString()
        {
            return _toString;
        }

        public int CompareTo(SemanticVersion other)
        {
            if (other == null) return 1;
            if (Major > other.Major) return 1;
            if (Major < other.Major) return -1;

            if (Minor > other.Minor) return 1;
            if (Minor < other.Minor) return -1;
            
            if (Patch > other.Patch) return 1;
            if (Patch < other.Patch) return -1;
            int r = 0;
            if (_pre!=null)
            {
                r = _pre.CompareTo(other._pre);
                if (r != 0)
                {
                    return r;
                }
            }
            else
            {
                if (other._pre!=null)
                {
                    return 1;
                }
            }

            r = 0;
            if (_build!=null)
            {
                r= _build.CompareTo(other._build);
            }
            else
            {
                if (other._build!=null)
                {
                    return -1;
                }
            }
            return r;
        }

        public static implicit operator SemanticVersion(string version)
        {
            return new SemanticVersion(version);
        }
    }
}