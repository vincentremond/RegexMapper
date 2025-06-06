using System;
using System.Globalization;

namespace RegexMapper;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SafeMapper;

public class RegexMap<T> where T : class
{
    private readonly Regex _regex;
    private readonly string[] _groupNames;
    private readonly IFormatProvider _formatProvider;

    public RegexMap(string pattern, RegexOptions regexOptions = RegexOptions.None,
        IFormatProvider formatProvider = null)
    {
        (_regex, _groupNames) = GetRegex(pattern, regexOptions);
        _formatProvider = formatProvider ?? CultureInfo.InvariantCulture;
    }

    private (Regex regex, string[] groups) GetRegex(string pattern, RegexOptions regexOptions)
    {
        var regex = new Regex(pattern, regexOptions | RegexOptions.ExplicitCapture);
        var groups = regex.GetGroupNames().Skip(1).ToArray();
        return (regex, groups);
    }

    public T Match(string input)
    {
        return InnerMatches(input).FirstOrDefault();
    }

    public IList<T> Matches(string input)
    {
        return InnerMatches(input).ToList();
    }

    private IEnumerable<T> InnerMatches(string input)
    {
        foreach (Match match in _regex.Matches(input))
        {
            var valuesDictionary = new Dictionary<string, string[]>();
            foreach (var groupName in _groupNames)
            {
                var matchGroup = match.Groups[groupName];

                if (!matchGroup.Success)
                {
                    valuesDictionary[groupName] = null;
                }
                else if (matchGroup.Captures.Count > 1)
                {
                    valuesDictionary[groupName] = Enumerate(matchGroup.Captures).ToArray();
                }
                else
                {
                    valuesDictionary[groupName] = new []{ matchGroup.Value};
                }

            }
            yield return SafeMap.Convert<Dictionary<string, string[]>, T>(valuesDictionary, _formatProvider);
        }
    }

    private IEnumerable<string> Enumerate(CaptureCollection matchGroupCaptures)
    {
        foreach (Capture matchGroupCapture in matchGroupCaptures)
        {
            yield return matchGroupCapture.Value;
        }
    }
}