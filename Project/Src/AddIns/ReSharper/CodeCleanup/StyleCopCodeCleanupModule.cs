﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StyleCopCodeCleanupModule.cs" company="http://stylecop.codeplex.com">
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
//   Custom StyleCop CodeCleanUp module to fix StyleCop violations.
//   We ensure that most of the ReSharper modules are run before we are.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StyleCop.ReSharper.CodeCleanup
{
    using System.Collections.Generic;

    using JetBrains.DocumentModel;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Daemon.CSharp.CodeCleanup;
    using JetBrains.ReSharper.Feature.Services.CodeCleanup;
    using JetBrains.ReSharper.Feature.Services.CSharp.CodeCleanup;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Files;

    using StyleCop.Diagnostics;
    using StyleCop.ReSharper.CodeCleanup.Descriptors;
    using StyleCop.ReSharper.CodeCleanup.Options;
    using StyleCop.ReSharper.CodeCleanup.Rules;

    /// <summary>
    ///   Custom StyleCop CodeCleanUp module to fix StyleCop violations.
    ///   We ensure that most of the ReSharper modules are run before we are.
    /// </summary>
    [CodeCleanupModule(ModulesBefore = new[] { typeof(UpdateFileHeader), typeof(HighlightingCleanupModule), typeof(ReplaceByVar), typeof(ReformatCode) })]
    public class StyleCopCodeCleanupModule : ICodeCleanupModule
    {
        /// <summary>
        ///   Documentation descriptor.
        /// </summary>
        private static readonly DocumentationDescriptor DocumentationDescriptor = new DocumentationDescriptor();

        /// <summary>
        ///   Layout descriptor.
        /// </summary>
        private static readonly LayoutDescriptor LayoutDescriptor = new LayoutDescriptor();

        /// <summary>
        ///   Maintainability descriptor.
        /// </summary>
        private static readonly MaintainabilityDescriptor MaintainabilityDescriptor = new MaintainabilityDescriptor();

        /// <summary>
        ///   Ordering descriptor.
        /// </summary>
        private static readonly OrderingDescriptor OrderingDescriptor = new OrderingDescriptor();

        /// <summary>
        ///   Readability descriptor.
        /// </summary>
        private static readonly ReadabilityDescriptor ReadabilityDescriptor = new ReadabilityDescriptor();

        /// <summary>
        ///   Spacing descriptor.
        /// </summary>
        private static readonly SpacingDescriptor SpacingDescriptor = new SpacingDescriptor();

        /// <summary>
        /// Gets the collection of option descriptors.
        /// </summary>
        /// <value>
        /// The descriptors.
        /// </value>
        public ICollection<CodeCleanupOptionDescriptor> Descriptors
        {
            get
            {
                return new CodeCleanupOptionDescriptor[]
                           {
                              DocumentationDescriptor, LayoutDescriptor, MaintainabilityDescriptor, OrderingDescriptor, ReadabilityDescriptor, SpacingDescriptor 
                           };
            }
        }

        /// <summary>
        /// Gets a value indicating whether the module is available on selection, or on the whole file.
        /// </summary>
        /// <value>
        /// The is available on selection.
        /// </value>
        public bool IsAvailableOnSelection
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the language this module can operate.
        /// </summary>
        /// <value>
        /// The language type.
        /// </value>
        public PsiLanguageType LanguageType
        {
            get
            {
                return CSharpLanguage.Instance;
            }
        }

        /// <summary>
        /// Check if this module can handle given project file.
        /// </summary>
        /// <param name="projectFile">
        /// The project file to check.
        /// </param>
        /// <returns>
        /// <c>True.</c>if the project file is available; otherwise 
        /// <c>False.</c>.
        /// </returns>
        public bool IsAvailable(IPsiSourceFile projectFile)
        {
            return projectFile.GetDominantPsiFile<CSharpLanguage>() != null;
        }

        /// <summary>
        /// Process clean-up on file.
        /// </summary>
        /// <param name="projectFile">
        /// The project file to process.
        /// </param>
        /// <param name="rangeMarker">
        /// The range marker to process.
        /// </param>
        /// <param name="profile">
        /// The code cleanup settings to use.
        /// </param>
        /// <param name="progressIndicator">
        /// The progress indicator.
        /// </param>
        public void Process(
            IPsiSourceFile projectFile, IRangeMarker rangeMarker, CodeCleanupProfile profile, JetBrains.Application.Progress.IProgressIndicator progressIndicator)
        {
            if (projectFile == null)
            {
                return;
            }

            if (!this.IsAvailable(projectFile))
            {
                return;
            }

            ISolution solution = projectFile.GetSolution();

            ICSharpFile file = projectFile.GetDominantPsiFile<CSharpLanguage>() as ICSharpFile;

            if (file == null)
            {
                return;
            }

            var services = solution.GetPsiServices(); 
            services.Transactions.Execute("Code cleanup", () => this.InternalProcess(profile, file));

            StyleCopTrace.Out();
        }

        /// <summary>
        /// Get default setting for given profile type.
        /// </summary>
        /// <param name="profile">
        /// The code cleanup profile to use.
        /// </param>
        /// <param name="profileType">
        /// Determine if it is a full or reformat <see cref="CodeCleanup.DefaultProfileType"/>.
        /// </param>
        public void SetDefaultSetting(CodeCleanupProfile profile, CodeCleanup.DefaultProfileType profileType)
        {
            // Default option are set in the constructors.
            OrderingOptions orderingOptions = new OrderingOptions();
            profile.SetSetting(OrderingDescriptor, orderingOptions);

            LayoutOptions layoutOptions = new LayoutOptions();
            profile.SetSetting(LayoutDescriptor, layoutOptions);

            DocumentationOptions documentationOptions = new DocumentationOptions();
            profile.SetSetting(DocumentationDescriptor, documentationOptions);

            SpacingOptions spacingOptions = new SpacingOptions();
            profile.SetSetting(SpacingDescriptor, spacingOptions);

            ReadabilityOptions readabilityOptions = new ReadabilityOptions();
            profile.SetSetting(ReadabilityDescriptor, readabilityOptions);

            MaintainabilityOptions maintainabilityOptions = new MaintainabilityOptions();
            profile.SetSetting(MaintainabilityDescriptor, maintainabilityOptions);
        }

        /// <summary>
        /// Processes all the cleanup.
        /// </summary>
        /// <param name="profile">
        /// The current profile to use.
        /// </param>
        /// <param name="file">
        /// The file to clean.
        /// </param>
        private void InternalProcess(CodeCleanupProfile profile, ICSharpFile file)
        {
            DocumentationOptions documentationOptions = profile.GetSetting(DocumentationDescriptor);
            LayoutOptions layoutOptions = profile.GetSetting(LayoutDescriptor);
            MaintainabilityOptions maintainabilityOptions = profile.GetSetting(MaintainabilityDescriptor);
            OrderingOptions orderingOptions = profile.GetSetting(OrderingDescriptor);
            ReadabilityOptions readabilityOptions = profile.GetSetting(ReadabilityDescriptor);
            SpacingOptions spacingOptions = profile.GetSetting(SpacingDescriptor);

            // Process the file for all the different Code Cleanups we have here
            // we do them in a very specific order. Do not change it.
            new ReadabilityRules().Execute(readabilityOptions, file);
            new MaintainabilityRules().Execute(maintainabilityOptions, file);
            new DocumentationRules().Execute(documentationOptions, file);
            new LayoutRules().Execute(layoutOptions, file);
            new SpacingRules().Execute(spacingOptions, file);
            new OrderingRules().Execute(orderingOptions, file);
        }
    }
}