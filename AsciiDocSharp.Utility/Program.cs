/**
 * This file contains content from the documentation of the https://gitlab.eclipse.org/eclipse/asciidoc-lang/ project.
 * Those contents are licensed under a Creative Commons Attribution 4.0 International License, available at https://creativecommons.org/licenses/by/4.0/.
 * AsciiDoc® and AsciiDoc Language™ are trademarks of the Eclipse Foundation, Inc. Eclipse and the Eclipse Logo are
 * registered trademarks of the Eclipse Foundation, Inc. Modified with changes to the line formatting.
 */

using AsciiDocSharp;
using AsciiDocSharp.Elements;

class Program
{
    static void Main(string[] args)
    {
        var a = Parser.ParseIntoInlines("bold *constrained* & **un**constrained" +
            "italic _constrained_ & __un__constrained" +
            "bold italic *_constrained_* & **__un__**constrained" +
            "monospace `constrained` & ``un``constrained" +
            "monospace bold `*constrained*` & ``**un**``constrained" +
            "monospace italic `_constrained_` & ``__un__``constrained" +
            "monospace bold italic `*_constrained_*` & ``**__un__**``constrained" +
            "// end::b-bold-italic-mono[]" +
            "// tag::constrained-bold-italic-mono[]" +
            "It has *strong* significance to me." +
            "I _cannot_ stress this enough." +
            "Type `OK` to accept." +
            "That *_really_* has to go." +
            "Can't pick one? Let's use them `*_all_*`." +
            "// end::constrained-bold-italic-mono[]" +
            "// tag::unconstrained-bold-italic-mono[]" +
            "**C**reate, **R**ead, **U**pdate, and **D**elete (CRUD)" +
            "That's fan__freakin__tastic!" +
            "Don't pass generic ``Object``s to methods that accept ``String``s!" +
            "It was Beatle**__mania__**!" +
            "// end::unconstrained-bold-italic-mono[]");

        List<InlineSpan> b = new();
        foreach (var inline in a)
        {
            Output(inline);
        }
        System.Console.WriteLine();
    }

    static void Output(BaseInline i)
    {
        if (i is InlineSpan span)
        {
            Console.WriteLine(span.Variant + ": ");
            Console.WriteLine("Constrained: " + span.IsConstrained);
            foreach (var e in span.Inlines)
            {
                Output(e);
            }
        }
        else if (i is InlineLiteral lit)
        {
            Console.WriteLine(lit.Value);
        }
    }
}