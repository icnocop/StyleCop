﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StyleCopStage.cs" company="http://stylecop.codeplex.com">
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
//   Daemon stage for StyleCop. This class is automatically loaded by ReSharper daemon
//   because it's marked with the  attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StyleCop.ReSharper.Core
{
    using System;
    using System.Linq;

    using JetBrains.Application.Settings;
    using JetBrains.ReSharper.Feature.Services.CSharp.Daemon;
    using JetBrains.ReSharper.Feature.Services.Daemon;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;

    using StyleCop.Diagnostics;
    using StyleCop.ReSharper.Options;
    using StyleCop.ReSharper.ShellComponents;

    /// <summary>
    /// Daemon stage for StyleCop. This class is automatically loaded by ReSharper daemon 
    /// because it's marked with the <see cref="DaemonStageAttribute"/> attribute.
    /// </summary>
    [DaemonStage]
    public class StyleCopStage : CSharpDaemonStageBase
    {
        private readonly StyleCopRunnerInt runner;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleCopStage"/> class.
        /// </summary>
        /// <param name="bootstrapper">
        /// A reference to the main API entry points
        /// </param>
        public StyleCopStage(StyleCopBootstrapper bootstrapper)
        {
            this.runner = bootstrapper.Runner;
        }

        /// <summary>
        /// We want to add markers to the right-side stripe as well as contribute to document errors.
        /// </summary>
        /// <param name="sourceFile">
        /// File that the Stripe needs to be applied to.
        /// </param>
        /// <param name="settingsStore">
        /// The store to use.
        /// </param>
        /// <returns>
        /// A <see cref="ErrorStripeRequest"/> for the specified file.
        /// </returns>
        public ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }

        /// <summary>
        /// This method provides a <see cref="IDaemonStageProcess"/> instance which is assigned to highlighting a single document.
        /// </summary>
        /// <param name="process">
        /// Current Daemon Process.
        /// </param>
        /// <param name="settingsStore">
        /// The settingsStore store to use.
        /// </param>
        /// <param name="processKind">
        /// The process kind.
        /// </param>
        /// <param name="file">
        /// The file to analyze.
        /// </param>
        /// /// 
        /// <returns>
        /// The current <see cref="IDaemonStageProcess"/>.
        /// </returns>
        protected override IDaemonStageProcess CreateProcess(
            IDaemonProcess process, IContextBoundSettingsStore settingsStore, DaemonProcessKind processKind, ICSharpFile file)
        {
            StyleCopTrace.In(process, settingsStore, processKind, file);

            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            try
            {
                if (processKind == DaemonProcessKind.OTHER)
                {
                    StyleCopTrace.Info("ProcessKind Other.");
                    StyleCopTrace.Out();
                    return null;
                }

                if (!settingsStore.GetValue<StyleCopOptionsSettingsKey, bool>(key => key.AnalysisEnabled))
                {
                    StyleCopTrace.Info("Analysis disabled.");
                    StyleCopTrace.Out();
                    return null;
                }

                if (!this.IsSupported(process.SourceFile))
                {
                    StyleCopTrace.Info("File type not supported.");
                    StyleCopTrace.Out();
                    return null;
                }

                if (!this.FileIsValid(file))
                {
                    StyleCopTrace.Info("Source file not valid.");
                    StyleCopTrace.Out();
                    return null;
                }

                if (!settingsStore.GetValue<StyleCopOptionsSettingsKey, bool>(key => key.AnalyzeReadOnlyFiles))
                {
                    if (process.SourceFile.Properties.IsNonUserFile)
                    {
                        StyleCopTrace.Info("Not analysing non user files.");
                        StyleCopTrace.Out();
                        return null;
                    }
                }

                return StyleCopTrace.Out(new StyleCopStageProcess(this.runner, process, settingsStore, file));
            }
            catch (JetBrains.Application.Progress.ProcessCancelledException)
            {
                return null;
            }
        }

        /// <summary>
        /// Checks the given file is valid to check.
        /// </summary>
        /// <param name="sourceFile">
        /// THe file to check.
        /// </param>
        /// <returns>
        /// True if its valid.
        /// </returns>
        private bool FileIsValid(ICSharpFile sourceFile)
        {
            if (sourceFile == null)
            {
                return false;
            }

            bool hasErrorElements = new RecursiveElementCollector<IErrorElement>(null).ProcessElement(sourceFile).GetResults().Any();

            return !hasErrorElements;
        }
    }
}