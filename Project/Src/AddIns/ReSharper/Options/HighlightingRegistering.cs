// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighlightingRegistering.cs" company="http://stylecop.codeplex.com">
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
//   Registers StyleCop Highlighters to allow their severity to be set.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StyleCop.ReSharper.Options
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using JetBrains.Application;
    using JetBrains.ReSharper.Feature.Services.Daemon;
    using JetBrains.ReSharper.Psi;

    using StyleCop.ReSharper.Core;
    using StyleCop.ReSharper.ShellComponents;

    /// <summary>
    /// Registers StyleCop Highlighters to allow their severity to be set.
    /// </summary>
    [ShellComponent]
    public class HighlightingRegistering : IDisposable
    {
        /// <summary>
        /// The template to be used for the group title.
        /// </summary>
        private const string GroupTitleTemplate = "StyleCop - {0}";

        /// <summary>
        /// The template to be used for the highlight ID's.
        /// </summary>
        private const string HighlightIdTemplate = "StyleCop.{0}";

        /// <summary>
        /// Initializes a new instance of the HighlightingRegistering class.
        /// </summary>
        /// <param name="bootstrapper">
        /// The entry point to the StyleCop API
        /// </param>
        public HighlightingRegistering(StyleCopBootstrapper bootstrapper)
        {
            this.Init(bootstrapper.Core);
        }

        /// <summary>
        /// Gets the highlight ID for this rule.
        /// </summary>
        /// <param name="ruleID">
        /// The rule ID.
        /// </param>
        /// <returns>
        /// The highlight ID.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// RuleID is null.
        /// </exception>
        public static string GetHighlightID(string ruleID)
        {
            if (string.IsNullOrEmpty(ruleID))
            {
                throw new ArgumentNullException("ruleID");
            }

            return string.Format(HighlightIdTemplate, ruleID);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        private static void RegisterConfigurableGroup(HighlightingSettingsManager highlightManager, string groupId, string groupName)
        {
            HighlightingSettingsManager.ConfigurableGroupDescriptor item = new HighlightingSettingsManager.ConfigurableGroupDescriptor(groupId, groupName);

            FieldInfo field = highlightManager.GetType().GetField("myConfigurableGroups", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {
                Dictionary<string, HighlightingSettingsManager.ConfigurableGroupDescriptor> items =
                    field.GetValue(highlightManager) as Dictionary<string, HighlightingSettingsManager.ConfigurableGroupDescriptor>;

                if (items != null)
                {
                    if (!items.ContainsKey(groupId))
                    {
                        items.Add(groupId, item);
                    }
                }
            }
        }

        private static void RegisterConfigurableSeverity(
            HighlightingSettingsManager highlightManager, string highlightId, string groupName, string ruleName, string description, Severity defaultSeverity)
        {
            FieldInfo allConfigurableSeverityItems = highlightManager.GetType().GetField("myConfigurableSeverityItem", BindingFlags.Instance | BindingFlags.NonPublic);

            if (allConfigurableSeverityItems != null)
            {
                Dictionary<string, ConfigurableSeverityItem> configurableSeverityItems =
                    allConfigurableSeverityItems.GetValue(highlightManager) as Dictionary<string, ConfigurableSeverityItem>;

                if (configurableSeverityItems != null)
                {
                    if (!configurableSeverityItems.ContainsKey(highlightId))
                    {
                        ConfigurableSeverityItem item = new ConfigurableSeverityItem(highlightId, null, groupName, ruleName, description, defaultSeverity, false, false, null);
                        configurableSeverityItems.Add(highlightId, item);
                    }
                }
            }

            FieldInfo configurableSeverityImplementation = highlightManager.GetType()
                                                                           .GetField(
                                                                               "myConfigurableSeverityImplementation", BindingFlags.Instance | BindingFlags.NonPublic);

            if (configurableSeverityImplementation != null)
            {
                JetBrains.Util.OneToListMap<string, PsiLanguageType> mapToLanguage =
                    configurableSeverityImplementation.GetValue(highlightManager) as JetBrains.Util.OneToListMap<string, PsiLanguageType>;

                if (mapToLanguage != null)
                {
                    if (!mapToLanguage.ContainsKey(highlightId))
                    {
                        PsiLanguageType languageType = Languages.Instance.GetLanguageByName("CSHARP");
                        mapToLanguage.Add(highlightId, languageType);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the highlight setting already exists in the HighlightingSettingsManager.
        /// </summary>
        /// <param name="highlightManager">
        /// The highlight manager.
        /// </param>
        /// <param name="highlightID">
        /// The highlight ID.
        /// </param>
        /// <returns>
        /// Boolean to say if this setting already exists in the HighlightingSettingsManager.
        /// </returns>
        private static bool SettingExists(HighlightingSettingsManager highlightManager, string highlightID)
        {
            ConfigurableSeverityItem item = highlightManager.GetSeverityItem(highlightID);
            return item != null;
        }

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">
        /// The text to split.
        /// </param>
        /// <returns>
        /// The split text.
        /// </returns>
        private static string SplitCamelCase(string input)
        {
            string output = Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();

            return output;
        }

        /// <summary>
        /// Registers the rules. Do not put the contents of this method in the constructor.
        /// If you do *sometimes* the StyleCop object won't be loaded be the time you construct it.
        /// </summary>
        /// <param name="core">
        /// The core API
        /// </param>
        private void Init(StyleCopCore core)
        {
            Dictionary<SourceAnalyzer, List<StyleCopRule>> analyzerRulesDictionary = StyleCopRule.GetRules(core);

            HighlightingSettingsManager highlightManager = HighlightingSettingsManager.Instance;

            // TODO Not sure how to get a configurable severity id with the settings store so default to warning for now
            //// var defaultSeverity = highlightManager.GetConfigurableSeverity(DefaultSeverityId, null);
            this.RegisterRuleConfigurations(highlightManager, analyzerRulesDictionary, Severity.WARNING);
        }

        /// <summary>
        /// Registers the rule configurations.
        /// </summary>
        /// <param name="highlightManager">
        /// The highlight manager.
        /// </param>
        /// <param name="analyzerRulesDictionary">
        /// The analyzer rules dictionary.
        /// </param>
        /// <param name="defaultSeverity">
        /// The default severity.
        /// </param>
        private void RegisterRuleConfigurations(
            HighlightingSettingsManager highlightManager, Dictionary<SourceAnalyzer, List<StyleCopRule>> analyzerRulesDictionary, Severity defaultSeverity)
        {
            foreach (KeyValuePair<SourceAnalyzer, List<StyleCopRule>> analyzerRule in analyzerRulesDictionary)
            {
                string analyzerName = SplitCamelCase(analyzerRule.Key.Name);
                string groupName = string.Format(GroupTitleTemplate, analyzerName);
                List<StyleCopRule> analyzerRules = analyzerRule.Value;

                RegisterConfigurableGroup(highlightManager, groupName, groupName);

                foreach (StyleCopRule rule in analyzerRules)
                {
                    string ruleName = rule.RuleID + ":" + " " + SplitCamelCase(rule.Name);
                    string highlightID = GetHighlightID(rule.RuleID);

                    if (!SettingExists(highlightManager, highlightID))
                    {
                        RegisterConfigurableSeverity(highlightManager, highlightID, groupName, ruleName, rule.Description, defaultSeverity);
                    }
                }
            }
        }
    }
}