namespace Ignore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class ReplacerTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { ReplacerStash.TrailingSpaces, @"aaa\ ", "aaa " },
                new object[] { ReplacerStash.EscapedSpaces, @"a\ b", "a b" },
                new object[] { ReplacerStash.Metacharacters, @"a $ . | * + ( ) { ^ b", @"a \$ \. \| \* \+ \( \) \{ \^ b" },
                new object[] { ReplacerStash.QuestionMark, @"a?", @"a[^/]" },
                new object[] { ReplacerStash.LeadingSlash, @"/a/b", @"^a/b" },
                new object[] { ReplacerStash.MetacharacterSlashAfterLeadingSlash, @"a/b", @"a\/b" },
                new object[] { ReplacerStash.LeadingDoubleStar, @"**/a/b", @"a/b" },
                new object[] { ReplacerStash.MiddleSlash, @"a/b", @"^a/b" },
                new object[] { ReplacerStash.MiddleSlash, @"a/b/c/d", @"^a/b/c/d" },
                new object[] { ReplacerStash.MiddleSlash, @"a/", @"a/" },
                new object[] { ReplacerStash.MiddleSlash, @"/a/", @"/a/" },
                new object[] { ReplacerStash.TrailingSlash, @"/a/", @"/a/$" },
                new object[] { ReplacerStash.NoTrailingSlash, @"/a/", @"/a/" },
                new object[] { ReplacerStash.NoTrailingSlash, @"/a", @"/a/?$" },
                new object[] { ReplacerStash.NoTrailingSlash, @"a", @"a/?$" },
                new object[] { ReplacerStash.NoTrailingSlash, @"a/b/c", @"a/b/c/?$" },
                new object[] { ReplacerStash.SingleStar, @"a/*/c", @"a/[^/]*/c" },
                new object[] { ReplacerStash.SingleStar, @"a/*.c", @"a/[^/]*.c" },
                new object[] { ReplacerStash.SingleStar, @"a/**/c", @"a/**/c" },
                new object[] { ReplacerStash.SingleStar, @"**/c", @"**/c" },
                new object[] { ReplacerStash.SingleStar, @"*.c", @"[^/]*.c" },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void ReplacerBehavior(
            Replacer replacer,
            string pattern,
            string expectedOutput) => Assert.Equal(expectedOutput, replacer.Invoke(pattern));
    }
}
