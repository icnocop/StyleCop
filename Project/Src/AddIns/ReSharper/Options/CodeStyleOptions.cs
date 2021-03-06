// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeStyleOptions.cs" company="http://stylecop.codeplex.com">
//   MS-PL
// </copyright>
// <license>
//   This source code is subject to terms and conditions of the Microsoft 
//   Public License. A copy of the license can be found in the License.html 
//   file at the root of this distribution. If you cannot locate the  
//   Microsoft Public License, please send an email to dlr@microsoft.com. 
//   By using this source code in any fashion, you are agreeing to be bound 
//   by the terms of the Microsoft Public License. You must not remove this 
//   notice, or any other, from this software.
// </license>
// <summary>
//   Verify and reset the options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StyleCop.ReSharper.Options
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Application.Settings;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Feature.Services.CodeCleanup;
    using JetBrains.ReSharper.Psi.CodeStyle;
    using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
    using JetBrains.ReSharper.Psi.CSharp.CodeStyle.FormatSettings;
    using JetBrains.ReSharper.Psi.CSharp.Naming2;
    using JetBrains.ReSharper.Psi.Naming.Settings;
    using JetBrains.ReSharper.Resources.Shell;

    /// <summary>
    /// Options for code style
    /// </summary>
    public static class CodeStyleOptions
    {
        /// <summary>
        /// The order of modifiers for StyleCop.
        /// </summary>
        private const string ModifiersOrder = "public protected internal private static new abstract virtual override sealed readonly extern unsafe volatile async";

        /// <summary>
        /// Resets the CodeStyleOptions to be StyleCop compatible.
        /// </summary>
        /// <param name="settingsStore">
        /// The settings store to use. 
        /// </param>
        /// <param name="solution">
        /// The current solution, if open. May be null.
        /// </param>
        public static void CodeStyleOptionsReset(IContextBoundSettingsStore settingsStore, ISolution solution)
        {
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_FIRST_ARG_BY_PAREN, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_LINQ_QUERY, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_ARGUMENT, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_ARRAY_AND_OBJECT_INITIALIZER, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_EXPRESSION, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_EXTENDS_LIST, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_FOR_STMT, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_PARAMETER, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTIPLE_DECLARATION, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTLINE_TYPE_PARAMETER_CONSTRAINS, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTLINE_TYPE_PARAMETER_LIST, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ALLOW_COMMENT_AFTER_LBRACE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ANONYMOUS_METHOD_DECLARATION_BRACES, BraceFormatStyle.NEXT_LINE_SHIFTED_2);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.ARRANGE_MODIFIER_IN_EXISTING_CODE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AFTER_START_COMMENT, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AFTER_USING_LIST, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_FIELD, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_INVOCABLE, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_NAMESPACE, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_REGION, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_SINGLE_LINE_FIELD, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_SINGLE_LINE_INVOCABLE, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_TYPE, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_BETWEEN_USING_GROUPS, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_INSIDE_REGION, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.CASE_BLOCK_BRACES, BraceFormatStyle.NEXT_LINE_SHIFTED_2);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.CONTINUOUS_INDENT_MULTIPLIER, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.EMPTY_BLOCK_STYLE, EmptyBlockStyle.MULTILINE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.EXPLICIT_INTERNAL_MODIFIER, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.EXPLICIT_PRIVATE_MODIFIER, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_ATTRIBUTE_STYLE, ForceAttributeStyle.SEPARATE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_CHOP_COMPOUND_DO_EXPRESSION, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_CHOP_COMPOUND_IF_EXPRESSION, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_CHOP_COMPOUND_WHILE_EXPRESSION, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_FIXED_BRACES_STYLE, ForceBraceStyle.ALWAYS_ADD);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_FOR_BRACES_STYLE, ForceBraceStyle.ALWAYS_ADD);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_FOREACH_BRACES_STYLE, ForceBraceStyle.ALWAYS_ADD);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_IFELSE_BRACES_STYLE, ForceBraceStyle.ALWAYS_ADD);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_USING_BRACES_STYLE, ForceBraceStyle.DO_NOT_CHANGE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.FORCE_WHILE_BRACES_STYLE, ForceBraceStyle.ALWAYS_ADD);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.INDENT_ANONYMOUS_METHOD_BLOCK, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.INDENT_CASE_FROM_SWITCH, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.INDENT_NESTED_FIXED_STMT, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.INDENT_NESTED_USINGS_STMT, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.INITIALIZER_BRACES, BraceFormatStyle.NEXT_LINE_SHIFTED_2);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.INVOCABLE_DECLARATION_BRACES, BraceFormatStyle.NEXT_LINE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.KEEP_BLANK_LINES_IN_CODE, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.KEEP_BLANK_LINES_IN_DECLARATIONS, 1);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.KEEP_USER_LINEBREAKS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.LINE_FEED_AT_FILE_END, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.MODIFIERS_ORDER, ModifiersOrder);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.OTHER_BRACES, BraceFormatStyle.NEXT_LINE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_ABSTRACT_ACCESSORHOLDER_ON_SINGLE_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_ACCESSORHOLDER_ATTRIBUTE_ON_SAME_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_CATCH_ON_NEW_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_CONSTRUCTOR_INITIALIZER_ON_SAME_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_ELSE_ON_NEW_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_FIELD_ATTRIBUTE_ON_SAME_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_FINALLY_ON_NEW_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_METHOD_ATTRIBUTE_ON_SAME_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_ACCESSOR_ON_SINGLE_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_ACCESSORHOLDER_ON_SINGLE_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_ANONYMOUSMETHOD_ON_SINGLE_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_INITIALIZER_ON_SINGLE_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_LINQ_ON_SINGLE_LINE, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_METHOD_ON_SINGLE_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_TYPE_ATTRIBUTE_ON_SAME_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_TYPE_CONSTRAINTS_ON_SAME_LINE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.PLACE_WHILE_ON_NEW_LINE, true);

            // TODO: Set the appropriate Code Style setting
            // settingsStore.SetValue((CSharpFormatSettingsKey key) => key.REDUNDANT_THIS_QUALIFIER_STYLE, ThisQualifierStyle.This);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SIMPLE_EMBEDDED_STATEMENT_STYLE, SimpleEmbeddedStatementStyle.ON_SINGLE_LINE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_AMPERSAND_OP, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_ASTERIK_OP, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_ATTRIBUTE_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_COMMA, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_EXTENDS_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_FOR_SEMICOLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TERNARY_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TERNARY_QUEST, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TYPE_PARAMETER_CONSTRAINT_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TYPECAST_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ADDITIVE_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ALIAS_EQ, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ARROW_OP, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ASSIGNMENT_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_BITWISE_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_DOT, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_EQUALITY_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_LAMBDA_ARROW, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_LOGICAL_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_MULTIPLICATIVE_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_NULLCOALESCING_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_RELATIONAL_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_SHIFT_OP, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_ARRAY_ACCESS_BRACKETS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_ARRAY_RANK_BRACKETS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_ATTRIBUTE_COLON, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_CATCH_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_COLON_IN_CASE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_COMMA, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_EMPTY_METHOD_CALL_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_EMPTY_METHOD_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_EXTENDS_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FIXED_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FOR_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FOR_SEMICOLON, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FOREACH_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_IF_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_LOCK_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_METHOD_CALL_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_METHOD_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_NULLABLE_MARK, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_POINTER_ASTERIK_DECLARATION, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SEMICOLON, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SINGLELINE_ACCESSORHOLDER, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SIZEOF_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SWITCH_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TERNARY_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TERNARY_QUEST, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TRAILING_COMMENT, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPE_ARGUMENT_ANGLE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPE_PARAMETER_ANGLE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPE_PARAMETER_CONSTRAINT_COLON, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPEOF_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_USING_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_WHILE_PARENTHESES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_BETWEEN_ACCESSORS_IN_SINGLELINE_PROPERTY, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_IN_SINGLELINE_ACCESSORHOLDER, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_IN_SINGLELINE_ANONYMOUS_METHOD, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_IN_SINGLELINE_METHOD, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ARRAY_ACCESS_BRACKETS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ARRAY_RANK_BRACKETS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ARRAY_RANK_EMPTY_BRACKETS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ATTRIBUTE_BRACKETS, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_CATCH_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_EMPTY_METHOD_CALL_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_EMPTY_METHOD_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_FIXED_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_FOR_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_FOREACH_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_IF_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_LOCK_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_METHOD_CALL_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_METHOD_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_SINGLE_LINE_ARRAY_INITIALIZER_BRACES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_SIZEOF_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_SWITCH_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPE_ARGUMENT_ANGLES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPE_PARAMETER_ANGLES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPECAST_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPEOF_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_USING_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_WHILE_PARENTHESES, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.SPECIAL_ELSE_IF_TREATMENT, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.STICK_COMMENT, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.TYPE_DECLARATION_BRACES, BraceFormatStyle.NEXT_LINE);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_AFTER_DECLARATION_LPAR, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_AFTER_INVOCATION_LPAR, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_ARGUMENTS_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_BINARY_OPSIGN, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_DECLARATION_LPAR, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_EXTENDS_COLON, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_FIRST_TYPE_PARAMETER_CONSTRAINT, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_INVOCATION_LPAR, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_TYPE_PARAMETER_LANGLE, false);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_EXTENDS_LIST_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_FOR_STMT_HEADER_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_LIMIT, settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_LIMIT));

            // We don't need to set this. It's here for completeness.
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_LINES, true);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_MULTIPLE_DECLARATION_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_MULTIPLE_TYPE_PARAMEER_CONSTRAINTS_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_OBJECT_AND_COLLECTION_INITIALIZER_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_PARAMETERS_STYLE, WrapStyle.CHOP_IF_LONG);
            settingsStore.SetValue((CSharpFormatSettingsKey key) => key.WRAP_TERNARY_EXPR_STYLE, WrapStyle.CHOP_IF_LONG);

            settingsStore.SetValue((CSharpNamingSettings key) => key.EventHandlerPatternLong, "$object$_On$event$");
            settingsStore.SetValue((CSharpNamingSettings key) => key.EventHandlerPatternShort, "$event$Handler");

            foreach (NamedElementKinds kindOfElement in Enum.GetValues(typeof(NamedElementKinds)))
            {
                NamingPolicy policy = settingsStore.GetIndexedValue<CSharpNamingSettings, NamedElementKinds, NamingPolicy>(
                    key => key.PredefinedNamingRules, kindOfElement) ?? ClrPolicyProviderBase.GetDefaultPolicy(kindOfElement);

                NamingRule rule = policy.NamingRule;

                rule.Suffix = string.Empty;

                switch (kindOfElement)
                {
                    case NamedElementKinds.Locals:
                    case NamedElementKinds.Parameters:
                    case NamedElementKinds.PrivateInstanceFields:
                    case NamedElementKinds.PrivateStaticFields:
                        rule.Prefix = string.Empty;
                        rule.NamingStyleKind = NamingStyleKinds.aaBb;
                        break;
                    case NamedElementKinds.Interfaces:
                        rule.Prefix = "I";
                        rule.NamingStyleKind = NamingStyleKinds.AaBb;
                        break;
                    case NamedElementKinds.TypeParameters:
                        rule.Prefix = "T";
                        rule.NamingStyleKind = NamingStyleKinds.AaBb;
                        break;
                    default:
                        rule.Prefix = string.Empty;
                        rule.NamingStyleKind = NamingStyleKinds.AaBb;
                        break;
                }

                settingsStore.SetIndexedValue<CSharpNamingSettings, NamedElementKinds, NamingPolicy>(key => key.PredefinedNamingRules, kindOfElement, policy);
            }

            settingsStore.SetValue(CSharpUsingSettingsAccessor.AddImportsToDeepestScope, true);
            settingsStore.SetValue(CSharpUsingSettingsAccessor.QualifiedUsingAtNestedScope, true);
            settingsStore.SetValue(CSharpUsingSettingsAccessor.AllowAlias, true);
            settingsStore.SetValue(CSharpUsingSettingsAccessor.CanUseGlobalAlias, true);
            settingsStore.SetValue(CSharpUsingSettingsAccessor.KeepNontrivialAlias, true);
            settingsStore.SetValue(CSharpUsingSettingsAccessor.PreferQualifiedReference, false);
            settingsStore.SetValue(CSharpUsingSettingsAccessor.SortUsings, true);

            // TODO: Override file layout
            // string reorderingPatterns;
            // using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StyleCop.ReSharper800.Resources.ReorderingPatterns.xml"))
            // {
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        reorderingPatterns = reader.ReadToEnd();
            //    }
            // }

            // settingsStore.SetValue((CSharpMemberOrderPatternSettings key) => key.CustomPattern, reorderingPatterns);

            // TODO: Check the solution requirement
            if (solution != null)
            {
                CodeCleanupProfile styleCopProfile = null;

                List<CodeCleanupProfile> profiles = new List<CodeCleanupProfile>();

                CodeCleanupSettingsComponent codeCleanupSettings = Shell.Instance.GetComponent<CodeCleanupSettingsComponent>();
                ICollection<CodeCleanupProfile> currentProfiles = codeCleanupSettings.GetProfiles(settingsStore);

                // Find the StyleCop profile
                foreach (CodeCleanupProfile profile in currentProfiles)
                {
                    if (!profile.IsDefault)
                    {
                        CodeCleanupProfile clone = profile.Clone();

                        profiles.Add(clone);

                        if (clone.Name == "StyleCop")
                        {
                            styleCopProfile = clone;
                        }
                    }
                }

                if (styleCopProfile == null)
                {
                    styleCopProfile = codeCleanupSettings.CreateEmptyProfile("StyleCop");
                    profiles.Add(styleCopProfile);
                }

                SetCodeCleanupProfileSetting(styleCopProfile, "CSArrangeThisQualifier", null, true);

                SetCodeCleanupProfileSetting(styleCopProfile, "CSUpdateFileHeader", null, false);

                SetCodeCleanupProfileSetting(styleCopProfile, "CSOptimizeUsings", "OptimizeUsings", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "CSOptimizeUsings", "EmbraceInRegion", false);
                SetCodeCleanupProfileSetting(styleCopProfile, "CSOptimizeUsings", "RegionName", string.Empty);

                SetCodeCleanupProfileSetting(styleCopProfile, "CSReformatCode", null, true);

                SetCodeCleanupProfileSetting(styleCopProfile, "CSharpFormatDocComments", null, false);

                SetCodeCleanupProfileSetting(styleCopProfile, "CSReorderTypeMembers", null, true);

                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1600ElementsMustBeDocumented", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1604ElementDocumentationMustHaveSummary", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1609PropertyDocumentationMustHaveValue", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1611ElementParametersMustBeDocumented", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1615ElementReturnValueMustBeDocumented", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1617VoidReturnValueMustNotBeDocumented", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1618GenericTypeParametersMustBeDocumented", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1626SingleLineCommentsMustNotUseDocumentationStyleSlashes", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1628DocumentationTextMustBeginWithACapitalLetter", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1629DocumentationTextMustEndWithAPeriod", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1633SA1641UpdateFileHeader", 2); // Replace Copyright
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1639FileHeaderMustHaveSummary", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1642ConstructorSummaryDocumentationMustBeginWithStandardText", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1643DestructorSummaryDocumentationMustBeginWithStandardText", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Documentation", "SA1644DocumentationHeadersMustNotContainBlankLines", true);

                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1500CurlyBracketsForMultiLineStatementsMustNotShareLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1509OpeningCurlyBracketsMustNotBePrecededByBlankLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1510ChainedStatementBlocksMustNotBePrecededByBlankLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1511WhileDoFooterMustNotBePrecededByBlankLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1512SingleLineCommentsMustNotBeFollowedByBlankLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1513ClosingCurlyBracketMustBeFollowedByBlankLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1514ElementDocumentationHeaderMustBePrecededByBlankLine", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Layout", "SA1515SingleLineCommentMustBeProceededByBlankLine", true);

                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Maintainability", "SA1119StatementMustNotUseUnnecessaryParenthesis", true);

                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Ordering", "AlphabeticalUsingDirectives", 1); // Alphabetical
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Ordering", "ExpandUsingDirectives", 1); // FullyQualify
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Ordering", "SA1212PropertyAccessorsMustFollowOrder", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Ordering", "SA1213EventAccessorsMustFollowOrder", true);

                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1100DoNotPrefixCallsWithBaseUnlessLocalImplementationExists", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1106CodeMustNotContainEmptyStatements", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1108BlockStatementsMustNotContainEmbeddedComments", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1109BlockStatementsMustNotContainEmbeddedRegions", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1120CommentsMustContainText", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1121UseBuiltInTypeAlias", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1122UseStringEmptyForEmptyStrings", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1123DoNotPlaceRegionsWithinElements", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Readability", "SA1124CodeMustNotContainEmptyRegions", true);

                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Spacing", "SA1001CommasMustBeSpacedCorrectly", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Spacing", "SA1005SingleLineCommentsMustBeginWithSingleSpace", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Spacing", "SA1006PreprocessorKeywordsMustNotBePrecededBySpace", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Spacing", "SA1021NegativeSignsMustBeSpacedCorrectly", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Spacing", "SA1022PositiveSignsMustBeSpacedCorrectly", true);
                SetCodeCleanupProfileSetting(styleCopProfile, "StyleCop.Spacing", "SA1025CodeMustNotContainMultipleWhitespaceInARow", true);

                codeCleanupSettings.SetProfiles(profiles, settingsStore);
                codeCleanupSettings.SetSilentCleanupProfileName(settingsStore, styleCopProfile.Name);
            }
        }

        /// <summary>
        /// Confirms that the ReSharper code style options are all valid to ensure no StyleCop issues on cleanup.
        /// </summary>
        /// <param name="settingsStore">
        /// The settings store to use. 
        /// </param>
        /// <param name="solution">
        /// The currently open solution, if any. May be null.
        /// </param>
        /// <returns>
        /// True if options are all valid, otherwise false. 
        /// </returns>
        public static bool CodeStyleOptionsValid(IContextBoundSettingsStore settingsStore, ISolution solution)
        {
            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_FIRST_ARG_BY_PAREN))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_LINQ_QUERY))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_ARGUMENT))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_ARRAY_AND_OBJECT_INITIALIZER))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_EXPRESSION))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_EXTENDS_LIST))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_FOR_STMT))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTILINE_PARAMETER))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTIPLE_DECLARATION))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTLINE_TYPE_PARAMETER_CONSTRAINS))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALIGN_MULTLINE_TYPE_PARAMETER_LIST))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ALLOW_COMMENT_AFTER_LBRACE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ANONYMOUS_METHOD_DECLARATION_BRACES) != BraceFormatStyle.NEXT_LINE_SHIFTED_2)
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.ARRANGE_MODIFIER_IN_EXISTING_CODE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AFTER_START_COMMENT) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AFTER_USING_LIST) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_FIELD) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_INVOCABLE) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_NAMESPACE) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_REGION) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_SINGLE_LINE_FIELD) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_SINGLE_LINE_INVOCABLE) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_AROUND_TYPE) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_BETWEEN_USING_GROUPS) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.BLANK_LINES_INSIDE_REGION) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.CASE_BLOCK_BRACES) != BraceFormatStyle.NEXT_LINE_SHIFTED_2)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.CONTINUOUS_INDENT_MULTIPLIER) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.EMPTY_BLOCK_STYLE) != EmptyBlockStyle.MULTILINE)
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.EXPLICIT_INTERNAL_MODIFIER))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.EXPLICIT_PRIVATE_MODIFIER))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_ATTRIBUTE_STYLE) != ForceAttributeStyle.SEPARATE)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_CHOP_COMPOUND_DO_EXPRESSION))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_CHOP_COMPOUND_IF_EXPRESSION))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_CHOP_COMPOUND_WHILE_EXPRESSION))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_FIXED_BRACES_STYLE) != ForceBraceStyle.ALWAYS_ADD)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_FOR_BRACES_STYLE) != ForceBraceStyle.ALWAYS_ADD)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_FOREACH_BRACES_STYLE) != ForceBraceStyle.ALWAYS_ADD)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_IFELSE_BRACES_STYLE) != ForceBraceStyle.ALWAYS_ADD)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_USING_BRACES_STYLE) != ForceBraceStyle.DO_NOT_CHANGE)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.FORCE_WHILE_BRACES_STYLE) != ForceBraceStyle.ALWAYS_ADD)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.INDENT_ANONYMOUS_METHOD_BLOCK))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.INDENT_CASE_FROM_SWITCH))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.INDENT_NESTED_FIXED_STMT))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.INDENT_NESTED_USINGS_STMT))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.INITIALIZER_BRACES) != BraceFormatStyle.NEXT_LINE_SHIFTED_2)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.INVOCABLE_DECLARATION_BRACES) != BraceFormatStyle.NEXT_LINE)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.KEEP_BLANK_LINES_IN_CODE) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.KEEP_BLANK_LINES_IN_DECLARATIONS) != 1)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.KEEP_USER_LINEBREAKS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.LINE_FEED_AT_FILE_END))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.MODIFIERS_ORDER).SequenceEqual(ModifiersOrder))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.OTHER_BRACES) != BraceFormatStyle.NEXT_LINE)
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_ABSTRACT_ACCESSORHOLDER_ON_SINGLE_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_ACCESSORHOLDER_ATTRIBUTE_ON_SAME_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_CATCH_ON_NEW_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_CONSTRUCTOR_INITIALIZER_ON_SAME_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_ELSE_ON_NEW_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_FIELD_ATTRIBUTE_ON_SAME_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_FINALLY_ON_NEW_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_METHOD_ATTRIBUTE_ON_SAME_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_ACCESSOR_ON_SINGLE_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_ACCESSORHOLDER_ON_SINGLE_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_ANONYMOUSMETHOD_ON_SINGLE_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_INITIALIZER_ON_SINGLE_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_LINQ_ON_SINGLE_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_SIMPLE_METHOD_ON_SINGLE_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_TYPE_ATTRIBUTE_ON_SAME_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_TYPE_CONSTRAINTS_ON_SAME_LINE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.PLACE_WHILE_ON_NEW_LINE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SIMPLE_EMBEDDED_STATEMENT_STYLE) != SimpleEmbeddedStatementStyle.ON_SINGLE_LINE)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_AMPERSAND_OP))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_ASTERIK_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_ATTRIBUTE_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_COMMA))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_EXTENDS_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_FOR_SEMICOLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TERNARY_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TERNARY_QUEST))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TYPE_PARAMETER_CONSTRAINT_COLON))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AFTER_TYPECAST_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ADDITIVE_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ALIAS_EQ))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ARROW_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_ASSIGNMENT_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_BITWISE_OP))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_DOT))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_EQUALITY_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_LAMBDA_ARROW))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_LOGICAL_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_MULTIPLICATIVE_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_NULLCOALESCING_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_RELATIONAL_OP))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_AROUND_SHIFT_OP))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_ARRAY_ACCESS_BRACKETS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_ARRAY_RANK_BRACKETS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_ATTRIBUTE_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_CATCH_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_COLON_IN_CASE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_COMMA))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_EMPTY_METHOD_CALL_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_EMPTY_METHOD_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_EXTENDS_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FIXED_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FOR_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FOR_SEMICOLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_FOREACH_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_IF_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_LOCK_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_METHOD_CALL_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_METHOD_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_NULLABLE_MARK))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_POINTER_ASTERIK_DECLARATION))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SEMICOLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SINGLELINE_ACCESSORHOLDER))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SIZEOF_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_SWITCH_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TERNARY_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TERNARY_QUEST))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TRAILING_COMMENT))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPE_ARGUMENT_ANGLE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPE_PARAMETER_ANGLE))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPE_PARAMETER_CONSTRAINT_COLON))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_TYPEOF_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_USING_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BEFORE_WHILE_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_BETWEEN_ACCESSORS_IN_SINGLELINE_PROPERTY))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_IN_SINGLELINE_ACCESSORHOLDER))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_IN_SINGLELINE_ANONYMOUS_METHOD))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_IN_SINGLELINE_METHOD))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ARRAY_ACCESS_BRACKETS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ARRAY_RANK_BRACKETS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ARRAY_RANK_EMPTY_BRACKETS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_ATTRIBUTE_BRACKETS))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_CATCH_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_EMPTY_METHOD_CALL_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_EMPTY_METHOD_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_FIXED_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_FOR_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_FOREACH_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_IF_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_LOCK_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_METHOD_CALL_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_METHOD_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_SINGLE_LINE_ARRAY_INITIALIZER_BRACES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_SIZEOF_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_SWITCH_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPE_ARGUMENT_ANGLES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPE_PARAMETER_ANGLES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPECAST_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_TYPEOF_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_USING_PARENTHESES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPACE_WITHIN_WHILE_PARENTHESES))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.SPECIAL_ELSE_IF_TREATMENT))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.STICK_COMMENT))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.TYPE_DECLARATION_BRACES) != BraceFormatStyle.NEXT_LINE)
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_AFTER_DECLARATION_LPAR))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_AFTER_INVOCATION_LPAR))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_ARGUMENTS_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_BINARY_OPSIGN))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_DECLARATION_LPAR))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_EXTENDS_COLON))
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_FIRST_TYPE_PARAMETER_CONSTRAINT))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_INVOCATION_LPAR))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_BEFORE_TYPE_PARAMETER_LANGLE))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_EXTENDS_LIST_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_FOR_STMT_HEADER_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (!settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_LINES))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_MULTIPLE_DECLARATION_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_MULTIPLE_TYPE_PARAMEER_CONSTRAINTS_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_OBJECT_AND_COLLECTION_INITIALIZER_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_PARAMETERS_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpFormatSettingsKey key) => key.WRAP_TERNARY_EXPR_STYLE) != WrapStyle.CHOP_IF_LONG)
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpNamingSettings key) => key.EventHandlerPatternLong) != "$object$_On$event$")
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpNamingSettings key) => key.EventHandlerPatternShort) != "$event$Handler")
            {
                return false;
            }

            foreach (NamedElementKinds kindOfElement in Enum.GetValues(typeof(NamedElementKinds)))
            {
                NamingPolicy policy = settingsStore.GetIndexedValue<CSharpNamingSettings, NamedElementKinds, NamingPolicy>(
                    key => key.PredefinedNamingRules, kindOfElement);

                if (policy == null)
                {
                    policy = ClrPolicyProviderBase.GetDefaultPolicy(kindOfElement);
                }

                NamingRule rule = policy.NamingRule;
                if (rule.Suffix != string.Empty)
                {
                    return false;
                }

                switch (kindOfElement)
                {
                    case NamedElementKinds.Locals:
                    case NamedElementKinds.Parameters:
                    case NamedElementKinds.PrivateInstanceFields:
                    case NamedElementKinds.PrivateStaticFields:
                        if (rule.Prefix != string.Empty || rule.NamingStyleKind != NamingStyleKinds.aaBb)
                        {
                            return false;
                        }

                        break;

                    case NamedElementKinds.Interfaces:
                        if (rule.Prefix != "I" || rule.NamingStyleKind != NamingStyleKinds.AaBb)
                        {
                            return false;
                        }

                        break;

                    case NamedElementKinds.TypeParameters:
                        if (rule.Prefix != "T" || rule.NamingStyleKind != NamingStyleKinds.AaBb)
                        {
                            return false;
                        }

                        break;

                    default:
                        if (rule.Prefix != string.Empty || rule.NamingStyleKind != NamingStyleKinds.AaBb)
                        {
                            return false;
                        }

                        break;
                }
            }

            if (!settingsStore.GetValue(CSharpUsingSettingsAccessor.AddImportsToDeepestScope))
            {
                return false;
            }

            if (!settingsStore.GetValue(CSharpUsingSettingsAccessor.QualifiedUsingAtNestedScope))
            {
                return false;
            }

            if (!settingsStore.GetValue(CSharpUsingSettingsAccessor.AllowAlias))
            {
                return false;
            }

            if (!settingsStore.GetValue(CSharpUsingSettingsAccessor.CanUseGlobalAlias))
            {
                return false;
            }

            if (!settingsStore.GetValue(CSharpUsingSettingsAccessor.KeepNontrivialAlias))
            {
                return false;
            }

            if (settingsStore.GetValue(CSharpUsingSettingsAccessor.PreferQualifiedReference))
            {
                return false;
            }

            if (!settingsStore.GetValue(CSharpUsingSettingsAccessor.SortUsings))
            {
                return false;
            }

            if (settingsStore.GetValue((CSharpMemberOrderPatternSettings key) => key.CustomPattern) == null)
            {
                return false;
            }

            // TODO: Verify file layout
            // string reorderingPatterns;
            // using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StyleCop.ReSharper800.Resources.ReorderingPatterns.xml"))
            // {
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        reorderingPatterns = reader.ReadToEnd();
            //    }
            // }

            // if (!settingsStore.GetValue((CSharpMemberOrderPatternSettings key) => key.CustomPattern).Equals(reorderingPatterns, StringComparison.InvariantCulture))
            // {
            //    return false;
            // }

            // TODO: Not sure if this is true
            // We can only check the StyleCop profile settings if a solution is loaded.
            if (solution != null)
            {
                CodeCleanupProfile styleCopProfile = null;

                CodeCleanupSettingsComponent codeCleanupSettings = Shell.Instance.GetComponent<CodeCleanupSettingsComponent>();
                ICollection<CodeCleanupProfile> currentProfiles = codeCleanupSettings.GetProfiles(settingsStore);

                // Find the StyleCop profile
                foreach (CodeCleanupProfile profile in currentProfiles)
                {
                    if (!profile.IsDefault)
                    {
                        if (profile.Name == "StyleCop")
                        {
                            styleCopProfile = profile;
                        }
                    }
                }

                if (styleCopProfile == null)
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSArrangeThisQualifier", null))
                {
                    return false;
                }

                if (GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSUpdateFileHeader", null))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSOptimizeUsings", "OptimizeUsings"))
                {
                    return false;
                }

                if (GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSOptimizeUsings", "EmbraceInRegion"))
                {
                    return false;
                }

                if (GetCodeCleanupProfileSetting<string>(styleCopProfile, "CSOptimizeUsings", "RegionName") != string.Empty)
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSReformatCode", null))
                {
                    return false;
                }

                if (GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSharpFormatDocComments", null))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "CSReorderTypeMembers", null))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1600ElementsMustBeDocumented"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1604ElementDocumentationMustHaveSummary"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1609PropertyDocumentationMustHaveValue"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1611ElementParametersMustBeDocumented"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1615ElementReturnValueMustBeDocumented"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1617VoidReturnValueMustNotBeDocumented"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1618GenericTypeParametersMustBeDocumented"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1626SingleLineCommentsMustNotUseDocumentationStyleSlashes"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1628DocumentationTextMustBeginWithACapitalLetter"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1629DocumentationTextMustEndWithAPeriod"))
                {
                    return false;
                }

                if (GetCodeCleanupProfileSetting<int>(styleCopProfile, "StyleCop.Documentation", "SA1633SA1641UpdateFileHeader") != 2)
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1639FileHeaderMustHaveSummary"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1642ConstructorSummaryDocumentationMustBeginWithStandardText"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1643DestructorSummaryDocumentationMustBeginWithStandardText"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Documentation", "SA1644DocumentationHeadersMustNotContainBlankLines"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1500CurlyBracketsForMultiLineStatementsMustNotShareLine"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1509OpeningCurlyBracketsMustNotBePrecededByBlankLine"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1510ChainedStatementBlocksMustNotBePrecededByBlankLine"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1511WhileDoFooterMustNotBePrecededByBlankLine"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1512SingleLineCommentsMustNotBeFollowedByBlankLine"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1513ClosingCurlyBracketMustBeFollowedByBlankLine"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1514ElementDocumentationHeaderMustBePrecededByBlankLine"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Layout", "SA1515SingleLineCommentMustBeProceededByBlankLine"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Maintainability", "SA1119StatementMustNotUseUnnecessaryParenthesis"))
                {
                    return false;
                }

                // Alphabetical
                if (GetCodeCleanupProfileSetting<int>(styleCopProfile, "StyleCop.Ordering", "AlphabeticalUsingDirectives") != 1)
                {
                    return false;
                }

                // FullyQualify
                if (GetCodeCleanupProfileSetting<int>(styleCopProfile, "StyleCop.Ordering", "ExpandUsingDirectives") != 1)
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Ordering", "SA1212PropertyAccessorsMustFollowOrder"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Ordering", "SA1213EventAccessorsMustFollowOrder"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1100DoNotPrefixCallsWithBaseUnlessLocalImplementationExists"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1106CodeMustNotContainEmptyStatements"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1108BlockStatementsMustNotContainEmbeddedComments"))
                {
                    return false;
                }

                if (
                    !GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1109BlockStatementsMustNotContainEmbeddedRegions"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1120CommentsMustContainText"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1121UseBuiltInTypeAlias"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1122UseStringEmptyForEmptyStrings"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1123DoNotPlaceRegionsWithinElements"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Readability", "SA1124CodeMustNotContainEmptyRegions"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Spacing", "SA1001CommasMustBeSpacedCorrectly"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Spacing", "SA1005SingleLineCommentsMustBeginWithSingleSpace"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Spacing", "SA1006PreprocessorKeywordsMustNotBePrecededBySpace"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Spacing", "SA1021NegativeSignsMustBeSpacedCorrectly"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Spacing", "SA1022PositiveSignsMustBeSpacedCorrectly"))
                {
                    return false;
                }

                if (!GetCodeCleanupProfileSetting<bool>(styleCopProfile, "StyleCop.Spacing", "SA1025CodeMustNotContainMultipleWhitespaceInARow"))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns a setting for the profile, descriptor and property name supplied.
        /// </summary>
        /// <typeparam name="T">
        /// The return type. 
        /// </typeparam>
        /// <param name="profile">
        /// The Cleanup profile to set. 
        /// </param>
        /// <param name="descriptorName">
        /// The name to match. 
        /// </param>
        /// <param name="propertyName">
        /// The property name to match. 
        /// </param>
        /// <returns>
        /// The property value. 
        /// </returns>
        private static T GetCodeCleanupProfileSetting<T>(CodeCleanupProfile profile, string descriptorName, string propertyName)
        {
            CodeCleanupOptionDescriptor cleanupOptionDescriptor = GetDescriptor(descriptorName);

            if (cleanupOptionDescriptor == null)
            {
                return default(T);
            }

            if (cleanupOptionDescriptor.Type == typeof(bool) || (cleanupOptionDescriptor.Type == typeof(string) || cleanupOptionDescriptor.Type.IsEnum))
            {
                return (T)profile.GetSetting(cleanupOptionDescriptor);
            }

            PropertyInfo propertyInfo = GetPropertyInfo(cleanupOptionDescriptor, propertyName);

            return propertyInfo != null ? (T)propertyInfo.GetValue(profile.GetSetting(cleanupOptionDescriptor), null) : default(T);
        }

        /// <summary>
        /// Gets a CleanupOptionsDescriptor matching the descriptor name passed in.
        /// </summary>
        /// <param name="descriptorName">
        /// The name to match. 
        /// </param>
        /// <returns>
        /// The CodeCleanupOptionDescriptor for the descriptor. 
        /// </returns>
        private static CodeCleanupOptionDescriptor GetDescriptor(string descriptorName)
        {
            CodeCleanupSettingsComponent codeCleanupSettings = Shell.Instance.GetComponent<CodeCleanupSettingsComponent>();
            IEnumerable<ICodeCleanupModule> currentModules = codeCleanupSettings.Modules;

            foreach (ICodeCleanupModule module in currentModules)
            {
                foreach (CodeCleanupOptionDescriptor descriptor in module.Descriptors)
                {
                    if (descriptor.Name == descriptorName && module.LanguageType.Name == "CSHARP")
                    {
                        return descriptor;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a PropertyInfo object matching the descriptor and the property name supplied.
        /// </summary>
        /// <param name="descriptor">
        /// The name to match. 
        /// </param>
        /// <param name="propertyName">
        /// The property name to match. 
        /// </param>
        /// <returns>
        /// A PropertyInfo matching. 
        /// </returns>
        private static PropertyInfo GetPropertyInfo(CodeCleanupOptionDescriptor descriptor, string propertyName)
        {
            return (from info in descriptor.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    let browsableAttributes = (BrowsableAttribute[])info.GetCustomAttributes(typeof(BrowsableAttribute), false)
                    where (browsableAttributes.Length != 1) || browsableAttributes[0].Browsable
                    select info).FirstOrDefault(info => info.Name == propertyName);
        }

        /// <summary>
        /// Sets a CodeCleanupProfile setting for the profile, descriptor and property name passed in.
        /// </summary>
        /// <param name="profile">
        /// The Cleanup profile to set. 
        /// </param>
        /// <param name="descriptorName">
        /// The descriptor name to match. 
        /// </param>
        /// <param name="propertyName">
        /// The property name to match. 
        /// </param>
        /// <param name="value">
        /// The new value. 
        /// </param>
        private static void SetCodeCleanupProfileSetting(CodeCleanupProfile profile, string descriptorName, string propertyName, object value)
        {
            CodeCleanupOptionDescriptor cleanupOptionDescriptor = GetDescriptor(descriptorName);

            if (cleanupOptionDescriptor == null)
            {
                return;
            }

            if (cleanupOptionDescriptor.Type == typeof(bool) || (cleanupOptionDescriptor.Type == typeof(string) || cleanupOptionDescriptor.Type.IsEnum))
            {
                profile.SetSetting(cleanupOptionDescriptor, value);
                return;
            }

            PropertyInfo propertyInfo = GetPropertyInfo(cleanupOptionDescriptor, propertyName);

            if (propertyInfo == null)
            {
                return;
            }

            object descriptorOptionsContainer = profile.GetSetting(cleanupOptionDescriptor);
            propertyInfo.SetValue(descriptorOptionsContainer, value, null);
            profile.SetSetting(cleanupOptionDescriptor, descriptorOptionsContainer);
        }
    }
}